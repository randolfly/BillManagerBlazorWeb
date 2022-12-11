namespace BillManagerWeb.Server.Models;

using System.Text.Json.Serialization;

public class BillPerson {
    public int BillId { get; set; }
    public int PersonId { get; set; }

    [JsonIgnore]
    public Bill Bill { get; set; } = new();

    [JsonIgnore]
    public Person Person { get; set; } = new();

}