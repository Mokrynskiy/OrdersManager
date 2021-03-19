using System;

namespace OrdersManager.WPF.Models
{
    public class OrderModel
    {
        public int Id { get; set; }        
        public string Contractor { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public string AuthorShortName { get; set; }
    }
}
