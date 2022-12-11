using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Utils;


namespace BillManagerWeb.Server.Service.IService;

public interface IHelperService {
    // update the bill-person map 
    public Task UpdateBillPerson(BillPerson billPerson, IEnumerable<BillPerson> targetBillPersons);
}
