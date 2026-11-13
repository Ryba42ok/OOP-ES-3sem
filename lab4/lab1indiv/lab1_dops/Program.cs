using System.Diagnostics;
using dop1;
using dop2;
using dop3;
using System;
using System.Collections.Immutable;


namespace dop1
{
    public interface IPlugin
    {
        void Initialize();
        void Execute(string data);
        void Shutdown();
    }
    class LoggerPlugin : IPlugin
    {
        public void Initialize()
        {
            Console.WriteLine("Plugin-Logger запущен");
        }
        public void Execute(string data)
        {
            Console.WriteLine("Выполнение 'data'");
        }
        public void Shutdown()
        {
            Console.WriteLine("Logger завершил работу");
        }
    }
    class TimerPlugin : IPlugin
    {
        private Stopwatch time = new Stopwatch();
        public void Initialize()
        {

            Console.WriteLine("Plugin-Timer запущен");


        }
        public void Execute(string data)
        {
            time.Start();

            System.Threading.Thread.Sleep(1000);

            time.Stop();

            Console.WriteLine("Выполнение заняло " + time.ElapsedMilliseconds + "ms");
        }
        public void Shutdown()
        {
            Console.WriteLine("Timer завершил работу");
        }
    }
    class ValidatorPlugin : IPlugin
    {
        public void Initialize()
        {
            Console.WriteLine("Validator запущен");
        }
        public void Execute(string data)
        {
            if (data == null)
            {
                Console.WriteLine("Ошибка данных");
                return;
            }
            Console.WriteLine("Всё хорошо");

        }
        public void Shutdown()
        {
            Console.WriteLine("Validator завершил работу");
        }
    }


    class PluginManager
    {
        private List<IPlugin> allPlugs = new List<IPlugin>();

        public void add(IPlugin plugin)
        {
            allPlugs.Add(plugin);
        }

        public void InitAll()
        {
            foreach (IPlugin plugin in allPlugs)
            {
                plugin.Initialize();
            }
        }
        public void ExecAll(string data)
        {
            foreach (IPlugin plugin in allPlugs)
            {
                plugin.Execute(data);
            }
        }
        public void ShutAll()
        {
            foreach (IPlugin plugin in allPlugs)
            {
                plugin.Shutdown();
            }
        }
    }
}

namespace dop2
{
    interface IFlyable
    {
        void Fly();
    }

    interface ISwimmable
    {
        void Swim();
    }

    interface IRunnable
    {
        void Run();
    }

    class Duck : IFlyable, IRunnable, ISwimmable
    {
        public void Fly() { }
        public void Run() { }
        public void Swim() { }
    }

    class Fish : ISwimmable
    {
        public void Swim() { }
    }

    class Dog : IRunnable, ISwimmable
    {
        public void Run() { }
        public void Swim() { }
    }


    class AnList
    {
        private readonly List<object> list = new List<object>();

        public int FlCount = 0;
        public int SwCount = 0;
        public int RnCount = 0;


        public void add(object obj) { list.Add(obj); }
        public void Count()
        {
            foreach(object item in list)
            {
                if (item as IFlyable != null) 
                {
                    FlCount++;
                }
                if (item as ISwimmable != null)
                {
                    SwCount++;
                }
                if (item as IRunnable!= null)
                {
                    RnCount++;
                }
            }
        }

        public void DisplayInf()
        {
            Console.WriteLine("Плавает " + SwCount);
            Console.WriteLine("Летает " + FlCount);
            Console.WriteLine("Бегает " + RnCount);
        }
    }

}

namespace dop3
{
    class Student : IComparable<Student>
    {
        public int Age;
        public string Name;
        public int Grade;

        public Student(string name, int age, int grade)
        {
            this.Name = name;
            this.Age = age;
            this.Grade = grade;
        }

        public int CompareTo(Student other)
        {
            if (other == null) return 1;

            return other.Grade.CompareTo(this.Grade);
        }
    }
    class StudentComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            bool ignoreCase = true;
            if (x == null || y == null) return 0;

            // Сортировка по имени в алфавитном порядке
            return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

}



class Program
{

    static void Main(string[] args)
    {
        LoggerPlugin l = new LoggerPlugin();
        TimerPlugin t = new TimerPlugin();
        ValidatorPlugin v = new ValidatorPlugin();


        PluginManager manager = new PluginManager();

        manager.add(l);
        manager.add(t);
        manager.add(v);

        string data = "hal";

        manager.InitAll();
        Console.WriteLine();
        manager.ExecAll(data);
        Console.WriteLine();
        manager.ShutAll();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Dog dog = new Dog();
        Duck duck = new Duck();
        Fish fish = new Fish();

        AnList anList = new AnList();
        anList.add(dog);
        anList.add(duck);
        anList.add(fish);

        anList.Count();
        anList.DisplayInf();


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();


        Student[] students = new Student[] {
            new Student("Ваня",1, 2),
            new Student("Вова", 1, 4),
            new Student("Дима", 1, 3),
            new Student("Кирилл", 1, 5),
        };
        Console.WriteLine("Было");
        PrintStudents(students);
        Console.WriteLine("Стало");
        Array.Sort(students);
        PrintStudents(students);

        Array.Sort(students, new StudentComparer());
        Console.WriteLine("\n--- Сортировка по имени (по алфавиту): ---");
        PrintStudents(students);

    }


    static void PrintStudents(Student[] students)
    {
        int i = 0;
        foreach (var student in students)
        {
            Console.WriteLine(i++ + " " +student.Name + " " + student.Age + " " + student.Grade);
        }
    }
}

