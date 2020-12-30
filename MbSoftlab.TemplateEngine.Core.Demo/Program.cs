using MbSoftLab.TemplateEngine.Core;
using System;
using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Core.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Person testModel = new Person()
            {
                FirstName = "Justin",
                LastName = "LastName",
                Tags = new List<string>() { "Tag1", "Tag2", "Tag3" },
                Address=new Address()
                {
                    Street="Straße",
                    PostCode="7872"
                },
                Orders=new List<Order>()
                {
                    new Order()
                    {
                        Id=1,
                        Products=new List<Product>()
                        {
                            new Product()
                            {
                               ProductName="Product1",
                               Price=150
                            },
                            new Product()
                            {
                               ProductName="Product2",
                               Price=50
                            }
                        } 
                    }
                }
                
            };


            ITemplateEngine<Person> templateEngine = new RazorTemplateEngine<Person>();
            templateEngine.LoadTemplateFromFile<Person>("TestModel.cshtml");
            Console.WriteLine(templateEngine.CreateStringFromTemplate(testModel));

            Console.ReadLine();

            
        }
    }
}
