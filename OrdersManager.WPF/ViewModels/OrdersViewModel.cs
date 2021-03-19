using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class OrdersViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;
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
                    ManagerId = item.Author.Id,
                    Date = item.Date,
                    ManagerShortName = authorShortName
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

        }
        #endregion
        #region AddOrderCommand (Добавление заказа)        
        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand => _addOrderCommand
            ??= new LambdaCommand(AddOrderCommanExecuted, AddOrderCommandExecute);
        private bool AddOrderCommandExecute() => true;
        private void AddOrderCommanExecuted()
        {

        }
        #endregion
        public OrdersViewModel(IRepository<Order> ordersRepository)
        {            
            _ordersRepository = ordersRepository;           
           
        }
        

    }
}
