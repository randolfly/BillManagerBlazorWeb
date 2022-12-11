using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Utils;


namespace BillManagerWeb.Server.Service.IService;


public interface IPersonService {
    Task<List<Person>> GetPersons();

    Task<Person?> GetPersonById(int personId);
    Task UpdatePerson(Person person);
}