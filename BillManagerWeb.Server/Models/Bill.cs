namespace BillManagerWeb.Server.Models;

using System.ComponentModel.DataAnnotations;

public class Bill {
    public int Id { get; set; }

    [Required(ErrorMessage = "请选择时间")]
    [Display(Name = "日期")]
    public DateTime DateTime { get; set; }

    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "简介")]
    public string Brief { get; set; } = String.Empty;

    [Required(ErrorMessage = "账单主体不能为空")]
    [Display(Name = "人员")]
    public List<BillPerson> BillPersons { get; set; } = new();

    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "价格")]
    public decimal Price { get; set; }

    public string Detail { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "状态")]
    public BillState BillState { get; set; }

    [Required(ErrorMessage = "需要指定是否可报销")]
    [Display(Name = "可报销否")]
    public RbsType RbsType { get; set; }

    [Display(Name = "订单类型")]
    public List<BillType> BillTypes { get; set; } = new();

    [Display(Name = "订单附件")]
    public List<Asset>? Assets { get; set; } = new();
}

// 显示订单是否被报销
public enum RbsType {
    Rbs,// 可报销文件
    Nrbs// 不可报销文件
}

public enum BillState {
    Done,// 已完成 
    Todo// 未完成
}
