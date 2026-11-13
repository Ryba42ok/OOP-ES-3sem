//Создайте класс MathOperations со статическими методами для арифметических операций (сложение, вычитание, умножение, деление).
//Используйте делегат, который позволяет пользователю выбрать, какая операция будет выполнена.
//Реализуйте возможность последовательного выполнения нескольких операций над числом (как в калькуляторе с памятью).
//Добавьте возможность отмены последней операции.

using dop1;
using dop2;
using dop3;
using System;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;
namespace dop1
{
    public delegate int OperDeleg(int x, int y);

    class MathOperations
    {

        public static int Add(int a, int b)
        {
            return a + b;
        }
        public static int Sub(int a, int b)
        {
            return a - b;
        }
        public static int Mul(int a, int b)
        {
            return a * b;
        }
        public static int Div(int a, int b)
        {
            return a / b;
        }
    }

    class Calculator
    {
        private int curVal;
        private Stack<int> hyst = new Stack<int>();

        public Calculator(int startValue)
        {

            curVal = startValue;
        }
        public int ExecOper(OperDeleg operation, int value)
        {
            curVal = operation(curVal, value);
            hyst.Push(curVal);
            return curVal;
        }

        public void undo()
        {
            if (hyst.Count > 0)
            {
                hyst.Pop();
                curVal = hyst.Peek();
            }
            else
            {
                Console.WriteLine("История пуста");
                return;
            }
        }

        public void showHyst()
        {
            Stack<int> temp = new Stack<int>(new Stack<int>(hyst));


            for (int i = hyst.Count; i > 0; i--)
            {
                Console.Write(i + " -> ");
                Console.WriteLine(temp.Pop());
            }

        }
    }
}
namespace dop2
{
    //Создайте игру "Угадай число". Игрок загадывает число, компьютер пытается угадать.
    //Используйте события для уведомления о ходе игры: GameStarted, NumberGuessed, GameOver.
    //При угадывании числа генерируйте событие с информацией о количестве попыток.
    //Добавьте подписчиков, которые показывают статистику, записывают ход игры в лог и выдают подсказки.

    delegate void Game_st(object sender, EventArgs e);

    class GameStarted
    {
        public void start(object sender, EventArgs e)
        {
            Console.WriteLine("Игра началась! Загадайте число от 0 до 9.");
        }
    }
    class NumberGuessed
    {
        public void OnGuessed(object sender, EventArgs e)
        {
            Console.WriteLine("Число угадано");
        }
    }
    class GameOver
    {
        public void OnOver(object sender, EventArgs e)
        {
            Console.WriteLine("Закончились попытки");

        }
    }

    class Game
    {
        public event Game_st start;
        public event Game_st guess;
        public event Game_st guessed;

        public void game()
        {
            Random random = new Random();
            int attemps = 0;
            bool g = false;

            start?.Invoke(this, EventArgs.Empty);

            while (!g && attemps != 3)
            {
                Console.WriteLine("Я думаю что это число: " + random.Next(0, 10));
                string s = Console.ReadLine();
                if (s.ToLower() == "да")
                {
                    guess?.Invoke(this, EventArgs.Empty);
                    break;
                }
                else if (s.ToLower() == "нет")
                {

                    attemps++;
                    continue;
                }
                else
                {
                    Console.WriteLine("Неверный формат");
                    continue;
                }
            }
            if (attemps == 3)
            {
                guessed?.Invoke(this, EventArgs.Empty);
            }

            


        }

    }
}
namespace dop3
{
    //    Создайте класс Keyboard, который генерирует событие KeyPressed при нажатии клавиши.Реализуйте несколько обработчиков этого события:
    //•	Обработчик для цифр(выводит "Нажата цифра X")
    //•	Обработчик для букв(выводит "Нажата буква X")
    //•	Обработчик для специальных клавиш(Enter, Escape, Пробел)
    //•	Обработчик, который считает количество нажатий каждой клавиши
    //Продемонстрируйте подписку на событие, обработку различных клавиш и возможность отписки.

    class isDig
    {
        public void onDig(KeyPressed e)
        {
            Console.WriteLine("Нажата цифра: " + e.Str);
        }
    }
    class isLet
    {
        public void onLet(KeyPressed e)
        {
            Console.WriteLine("Нажата буква: " + e.Str);
        }
    }
    class isSpec
    {
        public void onSpec(KeyPressed e)
        {
            Console.WriteLine("Нажат спец символ: " + e.Str);
        }
    }



    delegate void Key(KeyPressed e);
    class Keyboard
    {
        public event Key isDig;
        public event Key isLet;
        public event Key isSpec;

        public void onPressed(KeyPressed e)
        {
            
            if (char.IsLetter(e.Str))
            {
                isLet?.Invoke(e);
            }
            else if (char.IsDigit(e.Str))
            {
                isDig?.Invoke(e);
            }
            else
            {
                isSpec?.Invoke(e);
            }
            
        }
    }
    class KeyPressed : EventArgs
    {
        public char Str { get; set; }
        public KeyPressed(char str)
        {
            this.Str = str;
        }

    }
}

    class Program
    {
        static void Main(string[] args)
        {
        {
            Calculator calc = new Calculator(0);
            bool stop = true;
            string k;
            OperDeleg deleg;

            while (stop)
            {
                try
                {
                    Console.WriteLine("---Calculator---");
                    Console.WriteLine("Choose option");
                    Console.WriteLine("1 - operation\n2 - undo\n3 - hystory\n4 - Exit\n");

                    k = (Console.ReadLine());
                    int num = int.Parse(k);

                    switch (num)
                    {
                        case 1:
                            {
                                Console.WriteLine("1 - Add\n2 - Sub\n3 - Multi\n4 - Div\n5 - Exit\n");
                                string sw2;
                                sw2 = Console.ReadLine();
                                int.TryParse(sw2, out int n);
                                switch (n)
                                {
                                    case 1:
                                        {
                                            string k_str = Console.ReadLine();
                                            int k_ = int.Parse(k_str);
                                            deleg = MathOperations.Add;
                                            int New_ = calc.ExecOper(deleg, k_);
                                            Console.WriteLine("Результат - " + New_);

                                        }
                                        break;
                                    case 2:
                                        {
                                            string k_str = Console.ReadLine();
                                            int k_ = int.Parse(k_str);
                                            deleg = MathOperations.Sub;
                                            int New_ = calc.ExecOper(deleg, k_);
                                            Console.WriteLine("Результат - " + New_);
                                        }
                                        break;
                                    case 3:
                                        {
                                            string k_str = Console.ReadLine();
                                            int k_ = int.Parse(k_str);
                                            deleg = MathOperations.Mul;
                                            int New_ = calc.ExecOper(deleg, k_);
                                            Console.WriteLine("Результат - " + New_);
                                        }
                                        break;
                                    case 4:
                                        {
                                            string k_str = Console.ReadLine();
                                            int k_ = int.Parse(k_str);
                                            deleg = MathOperations.Div;
                                            int New_ = calc.ExecOper(deleg, k_);
                                            Console.WriteLine("Результат - " + New_);
                                        }
                                        break;
                                    case 5:
                                        {
                                            stop = false;
                                        }
                                        break;
                                    default:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Введены некорректные данные");
                                        }
                                        break;
                                }
                            }
                            break;
                        case 2:
                            {
                                calc.undo();
                            }
                            break;
                        case 3:
                            {
                                calc.showHyst();
                            }
                            break;
                        case 4:
                            {
                                stop = false;
                            }
                            break;
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Введены некорректные данные");
                                //stop = false;
                            }
                            break;
                    }
                }
                catch (DivideByZeroException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                catch (OverflowException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
            }
        }


        GameStarted gameStarted = new GameStarted();
        NumberGuessed NumbGues = new NumberGuessed();
        GameOver GameOver = new GameOver();

        Game game = new Game();

        game.start += gameStarted.start;
        game.guess += NumbGues.OnGuessed;
        game.guessed += GameOver.OnOver;
        game.game();

        Keyboard keyboard = new Keyboard();
            isLet let = new isLet();
            isDig dig = new isDig();
            isSpec spec = new isSpec();

            keyboard.isLet += let.onLet;
            keyboard.isDig += dig.onDig;
            keyboard.isSpec += spec.onSpec;
            string str = "abc23`#%";
            foreach (char c in str)
            {
                KeyPressed pressed = new KeyPressed(c);
                keyboard.onPressed(pressed);
            }
            
            
        }
    }
