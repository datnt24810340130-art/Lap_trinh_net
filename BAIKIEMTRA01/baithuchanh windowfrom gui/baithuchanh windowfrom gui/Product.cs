using System;

namespace baithuchanh_windowfrom_gui
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public byte[] Avatar { get; set; }
    }
}
