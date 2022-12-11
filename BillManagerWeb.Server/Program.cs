using BillManagerWeb.Server.Data;
using BillManagerWeb.Server.DataContext;
using BillManagerWeb.Server.Service;
using BillManagerWeb.Server.Service.IService;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBootstrapBlazor();


// Add services to the container.
builder.Services.AddDbContext<AppDataContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IHelperService, HelperService>();


// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
