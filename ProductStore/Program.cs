using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMenu = System.Collections.Generic.List<System.Tuple<string, GetMethod>>;
using ItemMenu = System.Tuple<string, GetMethod>;
using MenuSpace;
public delegate void GetMethod();

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
    enum ProductType { NoType, Food, Сlothes, Jewelry, Electronics, Toys, Furniture, HouseholdChemical }

    abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public ProductType Type { get; set; }
        public DateTime DateExpiration { get; set; }


        public abstract void ShowInformation();
        public Product(string name = "noname",
                       double price = 0.00,
                       int quantity = 0,
                       ProductType type = ProductType.NoType,
                       DateTime dateexp = new DateTime())
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Type = type;
            DateExpiration = dateexp;
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

    class StoreProduct
    {
        private Menu Menu1;
        private IMenu Item1;

        public StoreProduct()
        {
            Item1 = new IMenu
            {
                new ItemMenu(" Добавить товар", new GetMethod(Receipt)),
                new ItemMenu(" Реализавать товар", new GetMethod(Selling)),
                new ItemMenu(" Списать товар", new GetMethod(Cancellation)),
                new ItemMenu(" Выход", new GetMethod(Exit))
            };

            Menu1 = new Menu(5, 5, Item1);

        }
        private void Receipt()
        {
            Console.WriteLine("Добавить товар");
        }

        private void Selling()
        {
            Console.WriteLine("Реализавать товар");
        }
        private void Cancellation()
        {
            Console.WriteLine("Списать товар");
        }
        private void Exit()
        {
            Console.WriteLine("Выход");
        }

        public void Run()
        {
            Menu1.Show();
        }

    }


    class Program
    {

        static void Main(string[] args)
        {

            StoreProduct store = new StoreProduct();
            store.Run();

            Console.ReadKey();

        }

    }
}

