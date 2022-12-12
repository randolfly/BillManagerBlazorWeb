using System.Collections.Concurrent;
using BillManagerWeb.Server.Data;
using BillManagerWeb.Server.Models;
using BillManagerWeb.Server.Service;
using BillManagerWeb.Server.Service.IService;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Console = System.Console;

namespace BillManagerWeb.Server.Pages;

public partial class BillTable : ComponentBase
{
    private readonly ConcurrentDictionary<Foo, IEnumerable<SelectedItem>> _cache = new();

    private List<Bill> BillResults { get; set; } = new List<Bill>();

    [Inject] private IBillService BillService { get; set; }
    [Inject] private IPersonService PersonService { get; set; }
    [Inject] private IBillTypeService BillTypeService { get; set; }


    private ModelUiExchange<Person> PersonItems { get; set; } = new();
    private ModelUiExchange<string> BillTypeItems { get; set; } = new();

    private static IEnumerable<int> PageItemsSource => new int[]
    {
        20, 40
    };

    protected override async Task OnInitializedAsync()
    {
        BillResults = await BillService.GetBills();
        PersonItems.ModelItems = await PersonService.GetPersons();
        PersonItems.UiItems = PersonItems.ModelItems.Select(x => new SelectedItem(x.Name, x.Name)).ToList();
        BillTypeItems.ModelItems = await BillTypeService.GetBillTypeNames();
        BillTypeItems.UiItems = BillTypeItems.ModelItems.Select(x => new SelectedItem(x, x)).ToList();
    }

    /// <summary>
    /// 选中Person列修改方法，执行BillPerson转换
    /// </summary>
    /// <param name="bill">订单</param>
    /// <param name="items">UI选中的Person列表</param>
    /// <returns></returns>
    private Task OnSelectedPersonItemsChanged(Bill bill, IEnumerable<SelectedItem> items)
    {
        var billPersons = PersonItems.ModelItems.Where(p =>
        {
            foreach (var selectedItem in items)
            {
                if (p.Name == selectedItem.Text)
                {
                    return true;
                }
            }

            return false;
        }).Select(p => new BillPerson()
        {
            BillId = bill.Id,
            Bill = bill,
            PersonId = p.Id,
            Person = p
        }).ToList();
        billPersons.ForEach(bp => Console.WriteLine($"CHANGE, {bp.Person.Name}"));
        bill.BillPersons = billPersons;
        return Task.CompletedTask;
    }

    private Task OnSelectedBillTypeItemsChanged(Bill bill, IEnumerable<SelectedItem> items)
    {
        var billTypes = items.Select(x => new BillType()
        {
            Name = x.Text
        }).ToList();
        billTypes.ForEach(b => Console.WriteLine($"CHANGE, {b.Name}"));
        bill.BillTypes = billTypes;
        return Task.CompletedTask;
    }


    /// <summary>
    /// 存储表单的操作
    /// </summary>
    /// <param name="bill">当前行订单</param>
    /// <param name="changedType">修改类型</param>
    /// <returns>是否保存成功</returns>
    private async Task<bool> OnSaveAsync(Bill bill, ItemChangedType changedType)
    {
        if (changedType != ItemChangedType.Add)
        {
            var oldItem = BillResults.FirstOrDefault(i => i.Id == bill.Id);
            if (oldItem != null)
            {
                oldItem.BillTypes.Clear();
                oldItem.BillTypes = bill.BillTypes;
                oldItem.BillPersons = bill.BillPersons;
                await BillService.UpdateBill(oldItem);
            }

            return true;
        }

        return false;
    }
}

/// <summary>
/// UI和Model之间的交互
/// </summary>
/// <typeparam name="T">模型类型</typeparam>
internal class ModelUiExchange<T>
{
    // Model数据源
    public List<T> ModelItems { get; set; } = new List<T>();

    // UI显示数据源
    public List<SelectedItem> UiItems { get; set; } = new List<SelectedItem>();

    // UI选择绑定数据源
    public IEnumerable<string> SelectedUiItems { get; set; } = Enumerable.Empty<string>();
}