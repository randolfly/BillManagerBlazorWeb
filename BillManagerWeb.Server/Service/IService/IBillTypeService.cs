using BillManagerWeb.Server.Models;

namespace BillManagerWeb.Server.Service.IService;

public interface IBillTypeService
{
    Task<List<string>> GetBillTypeNames();
}