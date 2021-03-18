using System;

namespace OrdersManager.WPF.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }        
        public string Contractor { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeShortName { get; set; }
    }
}
