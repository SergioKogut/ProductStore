using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuSpace
{
    class HorizontalMenu
    {
        private List<string> MenuItem;
        private int ActiveItem = 0;
        private ConsoleKeyInfo key;
        private int PosXmenu = Console.CursorLeft; 
        private int PosYmenu = Console.CursorTop;
        
        public HorizontalMenu(List<string> temp)
        {
            MenuItem = temp;
            
        }

        private void SetColorText(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        public string Show()
        {

            do
            {
                //друк меню
                
                    for (int i = 0; i < MenuItem.Count; i++)
                    {
                        if (i == ActiveItem)
                        {
                            SetColorText(PosXmenu + i*10, PosYmenu , MenuItem[i], ConsoleColor.Yellow);
                        }
                        else
                        {
                            SetColorText(PosXmenu +i*10, PosYmenu , MenuItem[i], ConsoleColor.Green);
                        }
                    }
                //перевірка нажаття клавіш
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow && ActiveItem < MenuItem.Count - 1)
                {
                    ActiveItem++;
                }
                if (key.Key == ConsoleKey.LeftArrow && ActiveItem > 0)
                {
                    ActiveItem--;
                }

            }//вихді якщо нажади Ентер
            while (key.Key != ConsoleKey.Enter);
            //Console.Clear();
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine();
            return MenuItem[ActiveItem];



        }//вихді якщо нажади Ентер




    }

    

        class Menu
    {

        private int ActiveItem = 0;
        private int PosXmenu = 10;
        private int PosYmenu = 10;

        private GetMethod Method;
        private List<Tuple<string, GetMethod>> MenuItem;
        private ConsoleKeyInfo key;
        public Menu()
        {

        }

        public Menu(int x, int y, List<Tuple<string, GetMethod>> temp)
        {
            PosXmenu = x;
            PosYmenu = y;
            MenuItem = temp;
        }

        private void SetColorText(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }

        public void Show()
        {
           
            do
            {
                
                //друк меню
                for (int i = 0; i < MenuItem.Count; i++)
                {
                    if (i == ActiveItem)
                    {
                        SetColorText(PosXmenu, PosYmenu + i, MenuItem[i].Item1, ConsoleColor.Yellow);
                    }
                    else
                    {
                        SetColorText(PosXmenu, PosYmenu + i, MenuItem[i].Item1, ConsoleColor.Green);
                    }
                }
                SetColorText(PosXmenu, PosYmenu-1, "  МЕНЮ", ConsoleColor.Green);
                //перевірка нажаття клавіш
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow && ActiveItem < MenuItem.Count - 1)
                {
                    ActiveItem++;
                }
                if (key.Key == ConsoleKey.UpArrow && ActiveItem > 0)
                {
                    ActiveItem--;
                }

            }//вихді якщо нажади Ентер
            while (key.Key != ConsoleKey.Enter);
            Console.Clear();
            Method = MenuItem[ActiveItem].Item2;
            Method.Invoke();
        }//вихді якщо нажади Ентер
        

        public void Add(Tuple<string, GetMethod> item)
        {
            MenuItem.Add(item);
        }

        

    }
}
