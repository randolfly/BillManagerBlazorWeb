namespace BillManagerWeb.Server.Models;

public class Asset {
    public int Id { get; set; }

    // 相对根目录的文件相对地址
    public string FilePath { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}