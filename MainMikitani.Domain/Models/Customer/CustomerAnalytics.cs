namespace MainMikitan.Domain.Models.Customer
{
    public record CustomerAnalytics
    {
        public int TotalOrderQuantity { get; set; }
        public int AvarageMonthlyOrderQuantity { get; set; }
        public int TotalOfProfit { get; set; }
        public int AvarageMonthlyProfit { get; set; }
        public int AvarageApplicationView { get; set; }
    }
}