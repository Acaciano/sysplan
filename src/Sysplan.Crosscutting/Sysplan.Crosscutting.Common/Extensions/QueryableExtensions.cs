using Sysplan.Crosscutting.Common.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class QueryableExtensions
    {
        #region Private Methods

        private static LambdaExpression CreateLambdaExpression<TEntity>(string property, out Type propertyType)
            where TEntity : class
        {
            var parameter = Expression.Parameter(typeof(TEntity), "Entity");

            PropertyInfo propertyInfo;
            Expression accessProperty;

            if (property.Contains('.'))
            {
                var childProperties = property.Split('.');
                propertyInfo = typeof(TEntity).GetProperty(childProperties[0]);
                accessProperty = Expression.MakeMemberAccess(parameter, propertyInfo);
                for (var i = 1; i < childProperties.Length; i++)
                {
                    propertyInfo = propertyInfo.PropertyType.GetProperty(childProperties[i]);
                    accessProperty = Expression.MakeMemberAccess(accessProperty, propertyInfo);
                }
            }
            else
            {
                propertyInfo = typeof(TEntity).GetProperty(property);
                accessProperty = Expression.MakeMemberAccess(parameter, propertyInfo);
            }

            propertyType = propertyInfo.PropertyType;
            return Expression.Lambda(accessProperty, parameter);
        }

        private static MethodCallExpression CreateMethodCallExpression<TEntity>(IQueryable<TEntity> entities,
            string method, string property, LambdaExpression lambdaExpression = null) where TEntity : class
        {
            var entityType = typeof(TEntity);
            MethodCallExpression methodCallExpression;

            if (lambdaExpression == null)
            {
                Type propertyType;
                lambdaExpression = CreateLambdaExpression<TEntity>(property, out propertyType);
                methodCallExpression = Expression.Call(typeof(Queryable), method, new[] { entityType, propertyType },
                    entities.Expression, Expression.Quote(lambdaExpression));
            }
            else
            {
                methodCallExpression = Expression.Call(typeof(Queryable), "Where", new[] { entities.ElementType },
                    entities.Expression, lambdaExpression);
            }

            return methodCallExpression;
        }

        #endregion

        public static IQueryable<TEntity> Order<TEntity>(this IQueryable<TEntity> entities, Order order) where TEntity : class
        {
            if (entities == null || !entities.Any() || order == null || string.IsNullOrWhiteSpace(order.Property))
                return entities;

            var method = order.Crescent ? "OrderBy" : "OrderByDescending";
            var methodCallExpression = CreateMethodCallExpression(entities, method, order.Property);
            return entities.Provider.CreateQuery<TEntity>(methodCallExpression);
        }

        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> entities, Restriction restriction)
            where TEntity : class
        {
            if (entities == null || !entities.Any() || restriction == null ||
                string.IsNullOrWhiteSpace(restriction.Property) || string.IsNullOrWhiteSpace(restriction.Value))
                return entities;

            var parameter = Expression.Parameter(typeof(TEntity), "Entity");

            var property = restriction.Property.Split('.')
                .Aggregate<string, MemberExpression>(null,
                    (current, prop) => Expression.Property(current ?? (parameter as Expression), prop));

            try
            {
                var filterExpression = Expression.Constant(Convert.ChangeType(restriction.Value, property.Type));

                if (restriction.Condition == Condition.Default && filterExpression.Type != typeof(String))
                {
                    restriction.Condition = Condition.Equal;
                }

                Expression expression = null;

                switch (restriction.Condition)
                {
                    case Condition.Equal:
                        expression = Expression.Equal(property, filterExpression);
                        break;
                    case Condition.Different:
                        expression = Expression.NotEqual(property, filterExpression);
                        break;
                    case Condition.GreaterThan:
                        expression = Expression.GreaterThan(property, filterExpression);
                        break;
                    case Condition.GreaterThanEqual:
                        expression = Expression.GreaterThanOrEqual(property, filterExpression);
                        break;
                    case Condition.LessThan:
                        expression = Expression.LessThan(property, filterExpression);
                        break;
                    case Condition.LessThanEqual:
                        expression = Expression.LessThanOrEqual(property, filterExpression);
                        break;
                    case Condition.Default:
                        expression = Expression.Call(property, typeof(string).GetMethod("Contains"),
                            Expression.Constant(restriction.Value));
                        break;
                }

                var lambdaExpression = Expression.Lambda(expression, parameter);

                var methodCallExpression = CreateMethodCallExpression(entities, "Where", restriction.Property,
                    lambdaExpression);
                return entities.Provider.CreateQuery<TEntity>(methodCallExpression);
            }
            catch
            {
                return null;
            }
        }

        public static PagedList<TEntity> Paged<TEntity>(this IQueryable<TEntity> entities,
            Page page, Order order, Restriction restriction) where TEntity : class
        {
            var pagedResult = new PagedList<TEntity>(entities, entities.ToList(),
                new PaginationObject
                {
                    Order = order,
                    Page = page,
                    Restriction = restriction,
                });

            pagedResult.Results = pagedResult.Results.Skip((page.Index - 1) * page.Quantity).Take(page.Quantity).ToList();

            return pagedResult;
        }

        public static IQueryable<TEntity> IncludeProperties<TEntity>(this IQueryable<TEntity> entities,
            params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            if (properties != null)
                entities = properties.Aggregate(entities, (current, include) => current.Include(include));

            return entities;
        }
    }
}