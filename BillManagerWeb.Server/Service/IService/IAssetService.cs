using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Utils;


namespace BillManagerWeb.Server.Service.IService;

public interface IAssetService {
    Task<Asset?> GetAssetById(int assetId);
    Task AddAsset(Asset asset);
}
