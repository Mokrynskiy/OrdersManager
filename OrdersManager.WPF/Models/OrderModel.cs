using System;

namespace OrdersManager.WPF.Models
{
    public class OrderModel
    {
        public int Id { get; set; }        
        public string Contractor { get; set; }
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public string ManagerShortName { get; set; }
    }
}
