using BillManagerWeb.Server.DataContext;
using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Service.IService;
using BillManagerWeb.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace BillManagerWeb.Server.Service;

public class BillService : IBillService {
    private readonly AppDataContext dataContext;

    public BillService(AppDataContext dataContext) {
        this.dataContext = dataContext;
    }

    public async Task<List<Bill>> GetBills() {
        return await dataContext.Bills
            .Include(b => b.BillPersons)
            .ThenInclude(bp => bp.Person)
            .Include(b => b.Assets)
            .Include(b => b.BillTypes)
            .ToListAsync();
    }

    public async Task<List<Bill>> GetBillsByPerson(int personId) {
        return await dataContext.BillPersons
            .Where(x => x.PersonId == personId)
            .Select(x => x.Bill)
            .ToListAsync();
    }

    public async Task<Bill?> GetBillById(int billId) {
        return await dataContext.Bills
            .Include(b => b.BillPersons)
            .ThenInclude(bp => bp.Person)
            .Where(b => b.Id == billId)
            .FirstOrDefaultAsync();
    }

    public async Task RemoveBill(int billId) {
        var bill = await dataContext.Bills
            .Where(b => b.Id == billId)
            .FirstOrDefaultAsync();
        if (bill != null)
        {
            dataContext.Bills.Remove(bill);
            await dataContext.SaveChangesAsync();
        }
    }

    public async Task UpdateBill(Bill bill) {
        // check relation changed
        dataContext.Bills.Update(bill);
        await dataContext.SaveChangesAsync();
    }
}
