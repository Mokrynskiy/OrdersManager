using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using OrdersManager.DAL.Entityes;
using OrdersManager.Interfaces;
using OrdersManager.WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace OrdersManager.WPF.ViewModels
{
    public class OrdersViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IRepository<Order> _ordersRepository;        
        private OrderModel selectedOrder;
        public  ObservableCollection<OrderModel> Orders { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public OrderModel SelectedOrder
        {
            get
            {
                return this.selectedOrder;
            }

            set
            {
                if (value != this.selectedOrder)
                {
                    this.selectedOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region LoadOrdersCommand (загрузка данных о заказах)
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
                string authorShortName = "";
                if (item.Author != null)
                {
                    authorShortName = $"{item.Author.Surname} {item.Author.Name.FirstOrDefault()}. {item.Author.Patronymic.FirstOrDefault()}.";
                }
                
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
                    $"{selectedOrder.OrderDate.ToShortDateString()} - {SelectedOrder.Contractor}",
                    "Внимание!!!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _ordersRepository.Remove(selectedOrder.OrderId);
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
            
            Orders = new ObservableCollection<OrderModel>();
        }
        

    }
}
