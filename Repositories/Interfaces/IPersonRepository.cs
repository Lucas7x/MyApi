using MyApi.Models;

namespace MyApi.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> List(string? name, string? email, bool? isActive);
        Person Get(int id);
        Person Create(Person person);
        Person Update(int id, Person person);
        Person Delete(Person person);
    }
}
