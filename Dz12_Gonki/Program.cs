using System;
using static System.Console;
using System.Threading;


namespace Dz13_Gonki
{
    public delegate void Delegate();

    class Program
    {
        static void Main(string[] args)
        {
            FinishEvent finish = new FinishEvent();

            int flag = 0;
            while (flag == 0)
            {
                int ch = 0;
                Write($"1- Sport  2- Bus");
                ch = Convert.ToInt32(ReadLine());

                string n1, n2;
                Write($"Name 1- ");
                n1 = ReadLine();
                Write($"Name 2- ");
                n2 = ReadLine();

                if (ch == 1)
                { 
                    Car c1 = new Sport(n1);
                    Car c2 = new Sport(n2);
                    int fin = c1.Gonki_2(c1, c2);

                    if (fin == 1) finish.GeneratorEvent(c1.name); 
                    else finish.GeneratorEvent(c2.name); 
                }
                else
                {  
                    Car c1 = new Bus(n1);
                    Car c2 = new Bus(n2);
                    int fin = c1.Gonki_2(c1, c2);

                    if (fin == 1) finish.GeneratorEvent(c1.name);
                    else finish.GeneratorEvent(c2.name);
                } 
            } 
        }
    }
     
    class FinishEvent
    {
        public event Delegate ev;
        public void GeneratorEvent(string n)
        {
            WriteLine($"Car {n} - Win");
            ev?.Invoke();
        }
    }

    abstract class Car
    {  
        public string name { get; set; }
        public int speed { get; set; }
        public int pyt = 1000;
        public int finish { get; set; } 

        virtual public int Gonki()
        {
            Random r = new Random();
            if (finish < pyt)
            {
                finish += speed * r.Next(1, 3);
                WriteLine($"Car {name} - {finish}");
                return 0;
            }
            else return 1;  
        } 
        virtual public int Gonki_2(Car c1, Car c2)
        {
            for (int i = 0; i < 10; i++)
            {
                int x1, x2;
                x1 = c1.Gonki();
                if (x1 == 1)
                {
                    return 1;
                    break;
                }
                x2 = c2.Gonki();
                if (x2 == 1)
                {
                    return 2;
                    break;
                }
                Thread.Sleep(500); 
            }
            return 0;
        }

        virtual public void Finish()
        {
            WriteLine($"Car {name} - Win");
        }
    }

    class Sport: Car
    {
        public Sport(string n)
        {
            name = n;
            Random r = new Random();
            speed = r.Next(180, 200);
        }

        override public int Gonki()
        {
            Random r = new Random();
            if (finish < pyt)
            {
                finish += speed * r.Next(1, 3);
                WriteLine($"Car {name} - {finish}");
                return 0;
            }
            else return 1;
        }
    }

    class Bus : Car
    {
        public Bus(string n)
        {
            name = n;
            Random r = new Random();
            speed = r.Next(80, 100);
        }

        public void Gonki(Bus c)
        {
            if (c.speed > speed) WriteLine($"Win {c.name}");
            else WriteLine($"Win {name}");
        }
    }
} 