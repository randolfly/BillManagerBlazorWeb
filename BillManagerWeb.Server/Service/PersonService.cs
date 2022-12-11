using BillManagerWeb.Server.DataContext;
using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Service.IService;
using BillManagerWeb.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace BillManagerWeb.Server.Service;

public class PersonService : IPersonService {
    private readonly AppDataContext dataContext;

    public PersonService(AppDataContext dataContext) {
        this.dataContext = dataContext;
    }

    public async Task<List<Person>> GetPersons() {
        return await dataContext.Persons.ToListAsync();
    }
    public async Task<Person?> GetPersonById(int personId) {
        return await dataContext.Persons.FirstOrDefaultAsync(a => a.Id.Equals(personId));
    }

    public async Task UpdatePerson(Person person) {
        dataContext.Persons.Update(person);
        await dataContext.SaveChangesAsync();
    }
}
