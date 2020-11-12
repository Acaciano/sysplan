using FluentValidation.Results;
using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Common.Extensions;
using Sysplan.Crosscutting.Domain.Events;
using System;

namespace Sysplan.Crosscutting.Domain.Queries
{
    public abstract class Query<TResponse> : QueryMessage<TResponse>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Query()
        {
            Timestamp = DateTime.Now.ToBrazilianTimezone();
        }

        public abstract bool IsValid();
    }
}