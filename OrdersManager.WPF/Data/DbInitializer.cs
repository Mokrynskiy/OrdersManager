using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrdersManager.DAL.Context;
using OrdersManager.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.WPF.Data
{
    class DbInitializer
    {
        private readonly OrdersManagerDbContext _db;
        private readonly ILogger<DbInitializer> _loger;
        public DbInitializer(OrdersManagerDbContext db, ILogger<DbInitializer> loger)
        {
            _db = db;
            _loger = loger;
        }
        public async Task InitializeAsync()
        {
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await _db.Database.MigrateAsync();
            await InitializeDataAsync();
        }
        
        private async Task InitializeDataAsync()
        {
            _db.Departments.Add(new Department
            {
                Name = "Департамент 1",
                ManagerId = 0,
                Employees = new List<Employee>
                {
                new Employee { Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-5).AddMonths(-6).AddYears(-25),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"ВостокСтрой\"", Date=DateTime.Now.AddDays(-5)},
                    new Order{ Contractor = "ООО \"Транзит\"", Date=DateTime.Now.AddDays(-60)}
                    }
                },
                new Employee { Surname = "Петров", Name = "Сергей", Patronymic = "Степанович", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-6).AddMonths(-10).AddYears(-29),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"Апгрейд\"", Date=DateTime.Now.AddDays(-8)},
                    new Order{ Contractor = "ООО \"Контакт\"", Date=DateTime.Now.AddDays(-10)}
                    }
                },
                new Employee { Surname = "Сидоров", Name = "Федор", Patronymic = "Михайлович", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-20).AddMonths(-1).AddYears(-35),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"Дельта\"", Date=DateTime.Now.AddDays(-14)},
                    new Order{ Contractor = "ООО \"Ростелеком\"", Date=DateTime.Now.AddDays(-25)}
                    }
                }
                }
            });
            _db.Departments.Add(new Department
            {
                Name = "Департамент 2",
                ManagerId = 0,
                Employees = new List<Employee>
                {
                new Employee { Surname = "Ивлева", Name = "Татьяна", Patronymic = "Сергеевна", Gender = Gender.Женский, Birthday = DateTime.Now.AddDays(-15).AddMonths(-3).AddYears(-28),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"Кентавр\"", Date=DateTime.Now.AddDays(-36)},
                    new Order{ Contractor = "ООО \"Эквилибриум\"", Date=DateTime.Now.AddDays(-46)}
                    }
                },
                new Employee { Surname = "Трубачев", Name = "Константин", Patronymic = "Владимирович", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-3).AddMonths(-8).AddYears(-39),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"СтройГрад\"", Date=DateTime.Now.AddDays(-49)},
                    new Order{ Contractor = "ООО \"Брусника\"", Date=DateTime.Now.AddDays(-54)}
                    }
                },
                new Employee { Surname = "Свиридова", Name = "Елена", Patronymic = "Ивановна", Gender = Gender.Женский, Birthday = DateTime.Now.AddDays(-21).AddMonths(-8).AddYears(-37),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"МонолитСтрой\"", Date=DateTime.Now.AddDays(-18)},
                    new Order{ Contractor = "ООО \"РусИнТех\"", Date=DateTime.Now.AddDays(-19)}
                    }
                }
                }
            });
            _db.Departments.Add(new Department
            {
                Name = "Департамент 3",
                ManagerId = 0,
                Employees = new List<Employee>
                {
                new Employee { Surname = "Лаврентьев", Name = "Дмитрий", Patronymic = "Валентинович", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-25).AddMonths(-4).AddYears(-31),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"Глобус\"", Date=DateTime.Now.AddDays(-29)},
                    new Order{ Contractor = "ООО \"ГрандАвто\"", Date=DateTime.Now.AddDays(-32)}
                    }
                },
                new Employee { Surname = "Ковалев", Name = "Александр", Patronymic = "Григорьевич", Gender = Gender.Мужcкой, Birthday = DateTime.Now.AddDays(-9).AddMonths(-3).AddYears(-33),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"Системы Автоматики\"", Date=DateTime.Now.AddDays(-78)},
                    new Order{ Contractor = "ООО \"Кедр\"", Date=DateTime.Now.AddDays(-69)}
                    }
                },
                new Employee { Surname = "Конева", Name = "Екатерина", Patronymic = "Валерьевна", Gender = Gender.Женский, Birthday = DateTime.Now.AddDays(-20).AddMonths(-8).AddYears(-38),
                    Orders = new List<Order>{
                    new Order{ Contractor = "ООО \"ЭлектроСибМонтаж\"", Date=DateTime.Now.AddDays(-85)},
                    new Order{ Contractor = "ООО \"Корунд\"", Date=DateTime.Now.AddDays(-47)}
                    }
                }
                }
            });
            _db.SaveChanges();
            foreach (var item in _db.Departments.ToArray())
            {
                var emploeey = (from e in _db.Employees where e.Department == item select e).FirstOrDefault();
                item.ManagerId = emploeey.Id;
                _db.SaveChanges();
            }
        }
       
    }
}
