namespace MainMikitan.Domain.Requests.Customer
{
    public record FillCustomerInterestRequest
    {
        public List<int> InfoIds { get; set; } = null!;
    }
}
