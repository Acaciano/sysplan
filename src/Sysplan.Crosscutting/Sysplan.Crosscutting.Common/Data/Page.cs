namespace Sysplan.Crosscutting.Common.Data
{
    public class Page
    {
        public int Index { get; set; }
        public int Quantity { get; set; }

        public Page(int index, int quantity)
        {
            Index = index < 1 ? 1 : index;
            Quantity = quantity < 1 ? 1 : quantity;
        }
    }
}