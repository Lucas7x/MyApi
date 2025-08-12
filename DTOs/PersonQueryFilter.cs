namespace MyApi.DTOs
{
    public class PersonQueryFilter : QueryFilter
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool? ShowInative { get; set; }
        public bool IncludeWallets { get; set; }
    }
}
