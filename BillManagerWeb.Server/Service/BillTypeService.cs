using BillManagerWeb.Server.DataContext;
using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace BillManagerWeb.Server.Service;

public class BillTypeService : IBillTypeService
{
    private readonly AppDataContext dataContext;

    public BillTypeService(AppDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<List<string>> GetBillTypeNames()
    {
        return await dataContext.BillTypes
            .Select(x=>x.Name)
            .Distinct()
            .ToListAsync();
    }
}