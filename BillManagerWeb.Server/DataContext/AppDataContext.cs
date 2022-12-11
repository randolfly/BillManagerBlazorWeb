using Microsoft.EntityFrameworkCore;
using BillManagerWeb.Server.Models;

namespace BillManagerWeb.Server.DataContext;

public class AppDataContext : DbContext {
    public DbSet<Bill> Bills { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<BillPerson> BillPersons { get; set; }

    public DbSet<Asset> Assets { get; set; }

    public DbSet<BillType> BillTypes { get; set; }

    public static readonly ILoggerFactory ConsoleLoggerFactory =
        LoggerFactory.Create(builder => {
            builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<BillPerson>().HasKey(x => new
        {
            x.BillId,
            x.PersonId
        });


        var persons = new List<Person>
        {
            new Person { Id = 1, Name = "randolf" },
            new Person { Id = 2, Name = "pdd" },
            new Person { Id = 3, Name = "张荣侨" },
            new Person { Id = 4, Name = "YXY" }
        };
        modelBuilder.Entity<Person>().HasData(persons);

        var billTypes = new List<BillType>
        {
            new BillType { Id = 1, Name = "食品" },
            new BillType { Id = 2, Name = "设备" },
            new BillType { Id = 3, Name = "娱乐" },
            new BillType { Id = 4, Name = "知识" },
            new BillType { Id = 5, Name = "工资" },
            new BillType { Id = 6, Name = "其余" }
        };

        modelBuilder.Entity<BillType>().HasData(billTypes);

        var bills = new List<Bill>
        {
            new Bill
            {
                Id = 1,
                BillState = BillState.Done,
                RbsType = RbsType.Rbs,
                Brief = "测试1",
                Detail = "asaa",
                Price = 10m
            },
            new Bill
            {
                Id = 2,
                BillState = BillState.Todo,
                RbsType = RbsType.Nrbs,
                Brief = "测试2",
                Detail = "asaa222",
                Price = -100m
            }
        };

        modelBuilder.Entity<Bill>().HasData(bills);
    }


}
