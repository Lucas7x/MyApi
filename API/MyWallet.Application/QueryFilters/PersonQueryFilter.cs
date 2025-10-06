namespace MyWallet.Application.QueryFilters
{
    public class PersonQueryFilter : QueryFilter
    {
        public string? Name { get; set; }
        public bool? ShowInative { get; set; }
        public bool IncludeWallets { get; set; }
    }
}
