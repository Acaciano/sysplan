using FluentValidation.Results;
using Sysplan.Crosscutting.Common.Extensions;
using Sysplan.Crosscutting.Domain.Events;
using Newtonsoft.Json;
using System;

namespace Sysplan.Crosscutting.Domain.Commands
{
    public abstract class Command : CommandMessage
    {
        [JsonIgnore()]
        public DateTime Timestamp { get; private set; }

        [JsonIgnore()]
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now.ToBrazilianTimezone();
        }

        public abstract bool IsValid();
    }
}