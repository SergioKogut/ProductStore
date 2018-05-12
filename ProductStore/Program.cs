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
   enum ProductType { БезТипа, Молочные, Одежда, Украшения, Електроника, Игрушки, Инструменты, БытоваяХимия }

    abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }



        public abstract void ShowInformation();
        public Product(string name = "без имени",
                       string type = "без типа",
                       double price = 0.00,
                       int quantity = 0)
        {
            Name = name;
            Type = type;
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
        private string PackageType;
        private string PackageSize;

        public Food(string name = "нет имени",
                       string type = "молочные",
                       double price = 0.00,
                       int quantity = 0,
                       string packagetype = "",
                       string packagesize = "") : base(name, type, price, quantity)
        {
            PackageType = packagetype;
            PackageSize = packagesize;
        }


        public override void ShowInformation()
        {
            {
                Console.WriteLine($" {Name,-20}" +
                                  $"{Type,-15}" +
                                  $"{Price,-20}" +
                                  $"{Quantity,-15}" +
                                  $"{PackageType,-20}" +
                                  $"{PackageSize,-15}");
            }
        }


    }

    class HouseholdChemicals : Product
    {
        private string PackageType;
        private string PackageSize;


        

        public HouseholdChemicals(string name = "нет имени",
                       string type = "молочные",
                       double price = 0.00,
                       int quantity = 0,
                       string packagetype = "",
                       string packagesize = "") : base(name, type, price, quantity)
        {
            PackageType = packagetype;
            PackageSize = packagesize;
        }

        public override void ShowInformation()
        {
            {
                Console.WriteLine($" {Name,-20}" +
                                  $"{Type,-15}" +
                                  $"{Price,-20}" +
                                  $"{Quantity,-15}" +
                                  $"{PackageType,-20}" +
                                  $"{PackageSize,-15}");
            }
        }



    }

    class StoreProduct : List<Product>
    {
        //private int idProduct = 0;
        private Menu Menu1;// меню
        private IMenu Item1;//список елементов меню
        private bool ExitFlag = true; // флаг выхода из програмы
        private double Summa = 0;
        private int SellingCount = 0;
        private double SellingSumma = 0;
        private int CancellationCount = 0;
        private double CancellationSumma = 0;


        public double GetTotalValue()
        {
            Summa = 0;
            if (this.Count != 0)
            {
                foreach (var product in this)
                {
                    Summa += product.Price * product.Quantity;
                }
            }
            else
            {
                Summa = 0;
            }

            return Summa;

        }
        public double GetTotalQuantity()
        {
            int TempQuantity = 0;
            foreach (var product in this)
            {
                TempQuantity += product.Quantity;
            }
            return TempQuantity;

        }



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
            int idProduct = 1;
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"ID товара  Название товара   Тип товара     Цена товара    Количество товара   Тип упаковки    Обьем упаковки ");
            Console.WriteLine(new string('-', 110));
            if (this.Count != 0)
            {
                foreach (var product in this)
                {
                    Console.Write($"{idProduct,-10}");
                    product.ShowInformation();
                    idProduct++;
                }
            }
            else
            {
                Console.WriteLine("На складе нет товаров");
            }
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"Количество всех  товаров на складе:    { GetTotalQuantity()} штук");
            Console.WriteLine($"Цена всех товаров на складе:    { GetTotalValue()} грн\n ");
            Console.WriteLine($"Количество реализованных со склада товаров:    { SellingCount} штук");
            Console.WriteLine($"Цена реализованных со склада товаров:    { SellingSumma} грн\n ");
            Console.WriteLine($"Количество списанных со склада товаров:    { CancellationCount} штук");
            Console.WriteLine($"Цена списанных со склада товаров:    {CancellationSumma} грн\n ");

            Console.Read();
            Console.Clear();
        }


        private void Receipt()
        {
            Console.WriteLine("ПРИХОД:");
            double price;
            int quantity;
            string name;
            string type;
            Console.ReadLine();

            Console.Write("Выберите тип товара :");
            HorizontalMenu producttype = new HorizontalMenu(new List<string>() {  "Молочные", "Бытовая химия" });
            Console.WriteLine();
            type = producttype.Show();

            do
            {
                Console.Write("Введите название товара:");
                name = Console.ReadLine();

            } while (name.Length == 0);

            do
            {
                Console.Write("Введите цену товара:");
            } while (!double.TryParse(Console.ReadLine(), out price));

            do
            {
                Console.Write("Введите количество товара:");
            } while (!Int32.TryParse(Console.ReadLine(), out quantity));

            Console.Write("Введите тип упаковки:");
            HorizontalMenu typepacks = new HorizontalMenu(new List<string>() { "Пластик", "Бумажная" });
            string typepack = typepacks.Show();

            Console.Write("Введите размер упаковки:");
            HorizontalMenu sizepacks = new HorizontalMenu(new List<string>() { "1л", "2л", "3л" });
            string typesize = sizepacks.Show();
            Product temp;
            switch (type)
            {
                 
                case "Молочные":
                     temp = new Food(name, type, price, quantity, typepack, typesize);
                    this.Add(temp);
                    break;
                case "Бытовая химия":
                     temp = new HouseholdChemicals(name, type, price, quantity, typepack, typesize);
                    this.Add(temp);
                    break;

                default:
                    break;
            }
            Console.Clear();
        }

        private void Selling()
        {
            Console.WriteLine("РЕАЛИЗАЦИЯ:");
            if (this.Count != 0)
            {
                int number, quantity;
                Console.ReadLine();
                do
                {
                    do
                    {
                        Console.Write("Введите номер товара:");
                    } while (!Int32.TryParse(Console.ReadLine(), out number));

                } while (number <= 0 || number > this.Count);


                do
                {
                    Console.Write("Введите количество товара для реализации:");
                } while (!Int32.TryParse(Console.ReadLine(), out quantity));

                if (quantity < this[number - 1].Quantity)
                {
                    this[number - 1].Quantity -= quantity;
                    SellingCount += quantity;
                    SellingSumma += quantity * this[number - 1].Price;
                    // Summa -= quantity * this[number - 1].Price;
                }
                else if (quantity == this[number - 1].Quantity)
                {
                    SellingCount += quantity;
                    SellingSumma += quantity * this[number - 1].Price;
                    this.Remove(this[number - 1]);

                }
                else
                {

                    Console.Write("На складе нет такого количества товара!");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Write("На складе нет товаров для реализации!");
                Console.ReadLine();
            }

            Console.Clear();
        }

        private void Cancellation()
        {
            Console.WriteLine("CПИСАНИЕ:");
            if (this.Count != 0)
            {
                int number, quantity;
                Console.ReadLine();
                do
                {
                    do
                    {
                        Console.Write("Введите номер товара для списания:");
                    } while (!Int32.TryParse(Console.ReadLine(), out number));

                } while (number <= 0 || number > this.Count);


                do
                {
                    Console.Write("Введите количество товара для списание:");
                } while (!Int32.TryParse(Console.ReadLine(), out quantity));

                if (quantity < this[number - 1].Quantity)
                {
                    this[number - 1].Quantity -= quantity;
                    CancellationCount += quantity;
                    CancellationSumma += quantity * this[number - 1].Price;
                    // Summa -= quantity * this[number - 1].Price;
                }
                else if (quantity == this[number - 1].Quantity)
                {
                    CancellationCount += quantity;
                    CancellationSumma += quantity * this[number - 1].Price;
                    this.Remove(this[number - 1]);

                }
                else
                {
                    // Console.Write("На складе нет такого количества товара!");
                    Console.Write("На складе нет такого количества товара для списания!");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Write("На складе нет товаров для списания!");
                Console.ReadLine();
            }




            Console.Clear();
        }
        private void Exit()
        {
            Console.WriteLine("Выход\n Спасибо за пользование програмой!");

            ExitFlag = false;
        }

        public void Run()
        {
            do
            {
               Console.WriteLine( "СИСТЕМА УПРАВЛЕНИЯ ПОТОКАМИ ТОВАРОВ");
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

