﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuSpace
{
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

            MenuItem = new List<Tuple<string, GetMethod>>
            {
                new Tuple<string, GetMethod>("Del", Del),
                new Tuple<string, GetMethod>("Ask", Ask)
            };
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
                        SetColorText(PosXmenu, PosYmenu + i, "->" + MenuItem[i].Item1, ConsoleColor.Yellow);
                    }
                    else
                    {
                        SetColorText(PosXmenu, PosYmenu + i, "  " + MenuItem[i].Item1, ConsoleColor.Green);
                    }
                }

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

        }

        public void Add(Tuple<string, GetMethod> item)
        {
            MenuItem.Add(item);
        }

        private void Ask()
        {
            Console.WriteLine("Method Ask");

        }
        private void Del()
        {
            Console.WriteLine("Method Del");

        }

    }
}