using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class OrdersViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<Employee> _employeesRepository;
        public  ObservableCollection<OrderModel> Orders { get; set; }

        #region LoadOrdersCommand (отображение представления заказов)
        private ICommand _loadOrdersCommand;
        public ICommand LoadOrdersCommand => _loadOrdersCommand
            ??= new LambdaCommand(LoadOrdersCommanExecuted, LoadOrdersCommandExecute);
        private bool LoadOrdersCommandExecute() => true;
        private void LoadOrdersCommanExecuted()
        {
            Orders.Clear();
            var order = _ordersRepository.Items;
            foreach (var item in order)
            {
                string authorShortName = $"{item.Author.Surname} {item.Author.Name.FirstOrDefault()}. {item.Author.Patronymic.FirstOrDefault()}.";
                Orders.Add(new OrderModel
                {
                    OrderId = item.Id,
                    Contractor = item.Contractor,
                    EmployeeId = item.Author.Id,
                    OrderDate = item.Date,
                    EmployeeShortName = authorShortName
                });
            }
               
        }
        #endregion
        public OrdersViewModel(IRepository<Order> ordersRepository, IRepository<Employee> employeesRepository)
        {
            
            _ordersRepository = ordersRepository;
            _employeesRepository = employeesRepository;
            Orders = new ObservableCollection<OrderModel>();
        }
        

    }
}
