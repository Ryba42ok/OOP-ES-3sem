//Создайте атрибут DisplayNameAttribute для отображения "красивых" имен свойств.
//Напишите метод, который выводит все свойства класса с их отображаемыми именами. 
//2. Реализуйте атрибут RunOnStartupAttribute, который помечает методы,
//вызываемые автоматически при запуске программы (имитация через рефлексию).

using System.ComponentModel.DataAnnotations;
using System.Reflection;


[AttributeUsage(AttributeTargets.Property,
                AllowMultiple = true,
                Inherited = false)]
public class DisplayNameAttribute : Attribute
{
    public string Name { get; }

    public DisplayNameAttribute(string name)
    {
        Name = name;
    }


    public bool isValid(object value)
    {
        return value != null && (value?.ToString()[0] >= 'A' &&  (value?.ToString()[0] <= 'Z'));
    }
}


[AttributeUsage(AttributeTargets.Method)]
class RunOnStartupAttribute : Attribute
{
    public string Message { get; }

    public RunOnStartupAttribute(string message)
    {
        Message = message;
    }
}

class User
{
    [DisplayName("Имя пользователя")]
    public string Name { get; set; }

    [DisplayName("Возраст пользователя")]
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}




class Program
{
    [RunOnStartup("Запускается на начале")]
    static void Main(string[] args)
    {
        User a = new User("Vova", 19);
        User b = new User("Roma", 29);

        printUser(a);



        Assembly asm = Assembly.GetExecutingAssembly();
        Type[] types = asm.GetTypes();
        foreach (Type type in types)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (MethodInfo method in methods)
            {
                var attr = method.GetCustomAttribute<RunOnStartupAttribute>();
                if (attr != null)
                {
                    Console.WriteLine(method.Name + " - " + attr.Message);
                }
            }
        }

    }

    [RunOnStartup("Запускается на начале")]
    public static void printUser(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] prop = type.GetProperties();

        foreach (PropertyInfo propInfo in prop)
        {
            var attr = propInfo.GetCustomAttribute<DisplayNameAttribute>();

            string str = attr.Name;
            object val = propInfo.GetValue(obj);


            Console.Write(str + " - ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(val);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    } 
}


