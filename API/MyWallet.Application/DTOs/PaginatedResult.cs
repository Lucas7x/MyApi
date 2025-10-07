using MyWallet.Application.Helpers;
using System.Linq.Dynamic.Core;

namespace MyWallet.Application.DTOs
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(IQueryable<T> query, int pageSize, int currentPage, string sortBy, bool descending)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            SortOrder = descending ? "desc" : "asc";
            TotalItens = query.Count();

            SortBy = GetValidSortByField(sortBy);

            query = ApplySortFilter(query, SortBy, descending);

            TotalItens = query.Count();
            Rows = query.Skip((CurrentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
        }

        public PaginatedResult() { }

        public int TotalItens { get; set; }
        public int TotalPages
        {
            get => (int)Math.Ceiling((double)TotalItens / PageSize);
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; private set; } = "asc";
        public List<T> Rows { get; set; } = new List<T>();

        private string GetValidSortByField(string currentSortBy)
        {
            IEnumerable<string> validFieldNames = SearchableFieldsHelper.GetFields<T>();
            var field = validFieldNames
                        .Any(f => string.Equals(f, currentSortBy, StringComparison.OrdinalIgnoreCase))
                        ? currentSortBy : "id";
            return field;
        }

        private IQueryable<T> ApplySortFilter(IQueryable<T> query, string propertyName, bool descending)
        {
            return query.OrderBy(SortBy + " " + SortOrder);
        }
    }
}
