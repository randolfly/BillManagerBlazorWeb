using BillManagerWeb.Server.DataContext;
using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Service.IService;
using BillManagerWeb.Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace BillManagerWeb.Server.Service;

public class AssetService : IAssetService {
    private readonly AppDataContext dataContext;

    public AssetService(AppDataContext dataContext) {
        this.dataContext = dataContext;
    }

    public async Task<Asset?> GetAssetById(int assetId) {
        return await dataContext.Assets.FirstOrDefaultAsync(a => a.Id.Equals(assetId));
    }
    public async Task AddAsset(Asset asset) {
        await dataContext.Assets.AddAsync(asset);
    }
}
