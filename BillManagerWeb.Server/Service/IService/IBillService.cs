using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Utils;


namespace BillManagerWeb.Server.Service.IService;


public interface IBillService {
    Task<List<Bill>> GetBills();
    Task<List<Bill>> GetBillsByPerson(int personId);
    Task<Bill?> GetBillById(int billId);

    Task RemoveBill(int billId);

    Task UpdateBill(Bill bill);
}