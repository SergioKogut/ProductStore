using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Разработать архитектуру классов иерархии товаров
при разработке системы управления потоками товаров для
дистрибьюторской компании. Прописать члены классов.
Создать диаграммы взаимоотношений классов.
Должны быть предусмотрены разные типы товаров,
в том числе:
• бытовая химия;
• продукты питания.
Предусмотреть классы управления потоком товаров
(пришло, реализовано, списано, передано).
 */


namespace ProductStore
{


    abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public abstract void ShowInformation();
        public Product(string name = "noname", double price = 0.00, int quantity = 0)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double GetTotalValue()
        {
            return Price * Quantity;
        }

    }

    class Food : Product
    {
        public override void ShowInformation()
        {
            throw new NotImplementedException();
        }
    }

    class HouseholdChemicals : Product
    {

        public override void ShowInformation()
        {
            throw new NotImplementedException();
        }

    }

    class StoreProduct: List<Product>
    {
        public StoreProduct(IEnumerable<Product> collection) : base(collection) { }
        public StoreProduct() { }
        public void Run()
        {
            ShowProducts();
        }

        


        private void ShowProducts()
        {
            if (this.Capacity==0)
            {
                Console.WriteLine("No product");
            }
            else
            {
                foreach (var product in this)
                {
                    Console.WriteLine(product);
                }
            }
            



        }




    }




    class Program
    {
        static void Main(string[] args)
        {
            StoreProduct store = new StoreProduct();

            store.Run();
            



        }
    }
}
