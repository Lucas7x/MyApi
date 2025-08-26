using System.Reflection;

namespace MyApi.Helpers
{
    public class SearchableFieldsHelper
    {
        public static IEnumerable<string> GetFields<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name);
        }
    }
}
