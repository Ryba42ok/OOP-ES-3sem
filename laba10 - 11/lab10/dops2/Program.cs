//Создайте метод, который принимает объект Type и выводит полную информацию о типе:
//имя, пространство имен, базовый класс,
//реализуемые интерфейсы, все поля (с указанием типа и модификаторов доступа),
//все свойства, все методы (с параметрами и возвращаемыми типами), все конструкторы.
//Используйте рефлексию для получения всей информации.
using System.Reflection;

public interface customInterface
{

}

class Humanity
{

}
class Human : Humanity, customInterface
{
    public string Name { get; set; }
    public int Age { get; set; }
    private string Password;

    public Human(string name, int age, string password)
    {
        Name = name;
        Age = age;
        Password = password;
    }

    public void getReflInfo(Type type)
    {
        Console.WriteLine($"Имя типа:{type.Name}\n");
        Console.WriteLine($"Пространсто имён: {(!string.IsNullOrEmpty(type.Namespace)? type.Namespace : "Отсутствует")}\n");
        Console.WriteLine($"Базовый класс: {type.BaseType}\n");
        Console.WriteLine($"Реализуемый интерфейсы:");
        var realInterf = type.GetInterfaces();
        foreach (var intf in realInterf)
        {
            Console.WriteLine($"\t{intf.Name}");
        }
        Console.WriteLine("Поля:");
        var fields = type.GetFields();
        foreach(var field in fields)
        {
            if (fields.Length == 0)
                break;
            Console.WriteLine($"тип: {field.FieldType}\t модификатор: {(field.IsPublic? "public" : "private")} \t название поля {field.Name}");
        }

        Console.WriteLine("Свойства");
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (props.Length == 0)
                break;
            Console.WriteLine($"тип: {prop.PropertyType}\t название свойства {prop.Name}");
        }

        Console.WriteLine("Методы");
        var meth = type.GetMethods();
        foreach(var m in meth)
        {
            Console.Write($"{m.Name}\t Возвращает: {m.ReturnType} Параметры: ");
            var parm = m.GetParameters();
            foreach(var p in parm)
            {
                Console.Write($"{p.Name} тип {p.ParameterType}");
            }
            Console.WriteLine();
        }

        var constructors = type.GetConstructors();
        Console.WriteLine("Конструкторы");
        foreach(var c in constructors)
        {
            Console.WriteLine(c.Name);
        }
    }

    static public bool CompareObjects(Human first, Human second)
    {
        Type f = typeof(Human);
        Type s = typeof(Human);

        var fir = f.GetProperties();
        var sec = s.GetProperties();

        for (int i = 0; i < fir.Length; i++)
        {
            if (!object.Equals(fir[i].GetValue(first), sec[i].GetValue(second))) {
                return false;
            }
        }
        return true;
    }
}



class Program
{
    static void Main(string[] args)
    {
        //Human h1 = new Human("Vova", 42, "Hello World42");
        //Human h2 = new Human("Vova", 42, "Hello World42");
        //Type th1 = typeof(Human);
        //h1.getReflInfo(th1);

        //Console.WriteLine(Human.CompareObjects(h1, h2));


        //Console.ReadLine();
        //Console.Clear();

        Assembly asm = Assembly.GetExecutingAssembly();
        Console.WriteLine("Данные о сборке");
        Console.WriteLine(asm.FullName);
        
        Console.WriteLine("Расположение файла " + asm.CodeBase);

        var staitistics = asm.GetTypes();
        Console.WriteLine("Информация о типах сборки: ");
        foreach(var t in staitistics)
        {
            Console.WriteLine(t.Name + "-> ");
        }
    }

    
}