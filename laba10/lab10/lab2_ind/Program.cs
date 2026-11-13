using System.Reflection;
//1.Создайте класс Rectangle с полями width и height и методами GetArea() и GetPerimeter(). Используя рефлексию, найдите все методы, которые возвращают числовое значение.
//2. Получите информацию о сборке, в которой находится класс Rectangle, и выведите полное имя сборки и её версию.

class Rectangle
{
    public int width;
    public int height;

    public int getArea()
    {
        return width * height;
    }
    public int getPerimetr()
    {
        return 2 * (height + width);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Type rect = typeof(Rectangle);

        MethodInfo[] methods = rect.GetMethods();
        foreach (MethodInfo method in methods)
        {
            if (method.ReturnType == typeof(int))
            {
                Console.WriteLine(method.Name);
            }
        }
        Assembly asm = Assembly.GetExecutingAssembly();
        Assembly arr = rect.Assembly;

        Console.WriteLine($"Полное имя сборки {arr.FullName}");

        Console.WriteLine($"Версия {arr.GetName().Version}");
    }
}