using RazorEngineCore;
using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Core.Demo
{
 

    public class Person: TemplateDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Tags { get; set; }
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }

    }
    public class Address
    {
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
    }
    public class Product
    {
        public string ProductName { get; set; }
        public float Price { get; set; }

    }
}
