namespace MyApi.DTOs
{
    public class PaginatedResult<T>
    {
        public int TotalItens { get; set; }
        public int TotalPages
        {
            get => (int)Math.Ceiling((double)TotalItens / PageSize);
            set { PageSize = value; }
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public List<T> Rows { get; set; } = new List<T>();
    }
}
