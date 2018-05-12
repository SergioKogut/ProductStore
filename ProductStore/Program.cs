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
                       ProductType type = ProductType.NoType,
                       double price = 0.00,
                       int quantity = 0,
                       DateTime dateexp = new DateTime())
        {
            Name = name;
            Type = type;
            Price = price;
            Quantity = quantity;
            DateExpiration = dateexp;
        }



        public double GetTotalValue()
        {
            return Price * Quantity;
        }

    }

    class Food : Product
    {

        public Food(string name = "noname",
                       ProductType type = ProductType.NoType,
                       double price = 0.00,
                       int quantity = 0,
                       DateTime dateexp = new DateTime()):base(name, type, price, quantity, dateexp)
        { }


        public override void ShowInformation()
        {
            {
                Console.WriteLine($"Product:  Name:{Name,15}" +
                                  $"          Type:{Type,10}" +
                                  $"          Price: {Price,12}grn " +
                                  $"          Quantity: {Quantity,5} p " +
                                  $"          Date expiration {DateExpiration,10} ");
            }
        }


    }

    class HouseholdChemicals : Product
    {

        public override void ShowInformation()
        {
            throw new NotImplementedException();
        }

    }

    class StoreProduct : List<Product>
    {
        private Menu Menu1;
        private IMenu Item1;
        private bool ExitFlag = true;
       
       
        public StoreProduct()
        {
            Item1 = new IMenu
            {
                new ItemMenu(" Вывести список товаров", new GetMethod(Print)),
                new ItemMenu(" Добавить товар", new GetMethod(Receipt)),
                new ItemMenu(" Реализавать товар", new GetMethod(Selling)),
                new ItemMenu(" Списать товар", new GetMethod(Cancellation)),
                new ItemMenu(" Выход", new GetMethod(Exit))
            };

            Menu1 = new Menu(5, 5, Item1);

        }
        private void Print()
        {
            Console.WriteLine(new string('-', 80));
            foreach (var product in this)
            {
                product.ShowInformation();
            }

            Console.WriteLine(new string('-', 80));
            Console.Read();
            Console.Clear();
        }
        
           
            
        
        private void Receipt()
        {
            Console.WriteLine("ДОБАВИТЬ ТОВАР:");
            double price;
            int quantity;
            DateTime dateexp;
            Console.Write("Введите название товара:");
            string name = Console.ReadLine();
            ProductType type = ProductType.NoType;
            do
            {
                Console.Write("Введите цену товара:");
            } while (!double.TryParse(Console.ReadLine(), out price));

            do
            {
                Console.Write("Введите количество товара:");
            } while (!Int32.TryParse(Console.ReadLine(), out quantity));

            /*
            do
            {
                Console.Write("Введите дату изготовление:");
            } while (!DateTime.TryParse(Console.ReadLine(), out dateexp));
            */

            dateexp= DateTime.Now;

            Product temp = new Food(name, type, price, quantity, dateexp);

            this.Add(temp);
            Console.Clear();
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
            ExitFlag = false;
        }

        public void Run()
        {
            do
            {
                Menu1.Show();

            } while (ExitFlag);

            
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

