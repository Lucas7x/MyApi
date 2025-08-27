namespace MyWallet.Application.QueryFilters
{
    public class QueryFilter
    {
        private int pageIndex = 1;
        private int pageSize = 10;
        private string sortBy = "id";
        public string SortBy
        {
            get => sortBy;
            set
            {
                if (value is not null && value != "") sortBy = value.ToLower();
            }
        }
        public bool Descending { get; set; } = false;
        public int PageIndex
        {
            get => pageIndex;
            set
            {
                if (value > 0) pageIndex = value;
            }
        }
        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value > 0) pageSize = value;
            }
        }
    }
}
