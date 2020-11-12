using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sysplan.Crosscutting.Common.Data
{
    public class PagedList<TEntity> where TEntity : class
    {
        public PagedList() { }

        public PagedList(IQueryable<TEntity> entities, IList<TEntity> results, PaginationObject pagination)
        {
            Restriction = pagination.Restriction;
            Order = pagination.Order;
            Page = pagination.Page;

            entities = entities.Filter(pagination.Restriction);
            TotalRecords = entities.Count();
            CurrentPage = pagination.Page.Index;
            PageSize = pagination.Page.Quantity;
            TotalPages = (int)Math.Ceiling(TotalRecords / (double)pagination.Page.Quantity);
            Results = entities.Order(pagination.Order).ToList();
        }

        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IList<TEntity> Results { get; set; }

        public int FirstRecordOnPage
        {
            get { return TotalRecords > 0 ? (CurrentPage - 1) * PageSize + 1 : 0; }
        }

        public int LastRecordOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, TotalRecords); }
        }

        [JsonIgnore]
        public Page Page { get; set; }

        [JsonIgnore]
        public Restriction Restriction { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}