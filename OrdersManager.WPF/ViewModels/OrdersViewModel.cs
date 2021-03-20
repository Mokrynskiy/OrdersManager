using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using OrdersManager.WPF.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class OrdersViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IOrderDialog _orderDialog;
        private ObservableCollection<OrderModel> _orders;
        private OrderModel selectedOrder;
        public  ObservableCollection<OrderModel> Orders { get => _orders; set=>Set(ref _orders, value); }
        public  OrderModel SelectedOrder { get => selectedOrder; set => Set(ref selectedOrder, value); }
        
        #region LoadOrdersCommand (загрузка данных о заказах)
        private ICommand _loadOrdersCommand;
        public ICommand LoadOrdersCommand => _loadOrdersCommand
            ??= new LambdaCommand(LoadOrdersCommanExecuted, LoadOrdersCommandExecute);
        private bool LoadOrdersCommandExecute() => true;
        private void LoadOrdersCommanExecuted()
        {
            Orders = new ObservableCollection<OrderModel>();
            Orders.Clear();
            var order = _ordersRepository.Items;
            foreach (var item in order)
            {
                string authorShortName = "";
                if (item.Author != null)
                {
                    authorShortName = $"{item.Author.Surname} {item.Author.Name.FirstOrDefault()}. {item.Author.Patronymic.FirstOrDefault()}.";
                }
                
                Orders.Add(new OrderModel
                {
                    Id = item.Id,
                    Contractor = item.Contractor,
                    AuthorId = item.Author.Id,
                    Date = item.Date,
                    AuthorShortName = authorShortName
                });
            }               
        }
        #endregion

        #region DeleteOrderCommand (Удаление данных о заказе)        
        private ICommand _deleteOrderCommand;
        public ICommand DeleteOrderCommand => _deleteOrderCommand
            ??= new LambdaCommand(DeleteOrderCommanExecuted, DeleteOrderCommandExecute);
        private bool DeleteOrderCommandExecute() => true;
        private void DeleteOrderCommanExecuted()
        {
            if (selectedOrder is null)
            {
                MessageBox.Show("Для удаления заказа необходимо выделить запись в таблице Заказы!!!", "Ошибка!!!");
            }
            else
            {
                if (MessageBox.Show($"Вы действительно хотите удалить заказ\n " +
                    $"{selectedOrder.Date.ToShortDateString()} - {SelectedOrder.Contractor}",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _ordersRepository.Remove(selectedOrder.Id);
                        Orders.Remove(SelectedOrder);
                        selectedOrder = Orders.FirstOrDefault();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region EditOrderCommand (Редактирование данных о заказе)        
        private ICommand _editOrderCommand;
        public ICommand EditOrderCommand => _editOrderCommand
            ??= new LambdaCommand(EditOrderCommanExecuted, EditOrderCommandExecute);
        private bool EditOrderCommandExecute() => true;
        private void EditOrderCommanExecuted()
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Необходимо выделить запись для редактирования!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string title = "Редактирование данных о заказе";
            var ord = SelectedOrder;
            var empl = _employeeRepository.Items.ToArray();
            if (_orderDialog.Edit(ord, empl, title, true))
            {
                var order = _ordersRepository.GetById(ord.Id);
                order.Contractor = ord.Contractor;
                order.Date = ord.Date;
                order.Author = _employeeRepository.GetById(ord.AuthorId);
                _ordersRepository.Update(order);
                this.LoadOrdersCommand.Execute(null);
                SelectedOrder = ord;
                return;
            }
        }
        #endregion

        #region AddOrderCommand (Добавление заказа)        
        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand => _addOrderCommand
            ??= new LambdaCommand(AddOrderCommanExecuted, AddOrderCommandExecute);
        private bool AddOrderCommandExecute() => true;
        private void AddOrderCommanExecuted()
        {
            string title = "Создание нового заказа";
            var ord = new OrderModel();            
            ord.Date = DateTime.Now;
            var empl = _employeeRepository.Items.ToArray();
            if (_orderDialog.Edit(ord, empl, title, true))
            {
                if (ord.Contractor != null && ord.AuthorId != null)
                {
                    var order = new Order();
                    order.Contractor = ord.Contractor;
                    order.Date = ord.Date;
                    order.Author = _employeeRepository.GetById(ord.AuthorId);
                    ord.Id = _ordersRepository.Add(order).Id;
                    ord.AuthorShortName = $"{order.Author.Surname} {order.Author.Name.FirstOrDefault()}. {order.Author.Patronymic.FirstOrDefault()}.";
                    Orders.Add(ord);
                    SelectedOrder = ord;
                    return;
                }
                MessageBox.Show("Для добавления заказа необходимо заполнить все поля!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        #endregion
        public OrdersViewModel(IRepository<Order> ordersRepository, IRepository<Employee> employeesRepository, IOrderDialog orderDialog)
        {            
            _ordersRepository = ordersRepository;
            _employeeRepository = employeesRepository;
            _orderDialog = orderDialog;
        }
        

    }
}
