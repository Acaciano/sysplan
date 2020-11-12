namespace Sysplan.Crosscutting.Common.Data
{
    public enum Condition
    {
        Default,
        Different,
        Equal,
        GreaterThan,
        GreaterThanEqual,
        LessThan,
        LessThanEqual,
        StartsWith,
        EndsWith
    }

    public class Restriction
    {
        public string Property { get; set; }
        public Condition Condition { get; set; }
        public string Value { get; set; }

        public Restriction(string property, Condition condition, string value)
        {
            Property = property;
            Condition = condition;
            Value = value;
        }
    }
}