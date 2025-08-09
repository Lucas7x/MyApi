using MyApi.Models;

namespace MyApi.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> List(string? name, string? email, bool? isActive);
        Person GetById(int id);
        Person Create(Person person);
        Person Update(Person person);
        Person Delete(Person person);
        void SaveChanges();
    }
}
