//1.Создайте класс Employee с полями name, position и salary.
//Добавьте метод PrintInfo(), выводящий информацию о сотруднике.
//Добавьте метод GiveRaise(double percent), увеличивающий зарплату на заданный процент.
//Создайте сотрудника, выведите информацию, повысьте зарплату и выведите снова.

//2. Добавьте статическое поле TotalEmployees, увеличивающееся при создании каждого нового сотрудника.
//Добавьте статический метод для получения общего количества сотрудников.


class Employee
{
    string name { get; set; }
    string position { get; set; }
    double salary {  get; set; }

    static int TotalEmployees = 0;

    //Конструктор
    public Employee(string name, string position, double salary)
    {
        this.name = name;
        this.position = position;
        this.salary = salary;
        TotalEmployees++;
    }
    public Employee()
    {
        name = "пустое";
        position = "нет";
        salary = 0.0d;
        TotalEmployees++;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Позиция: {position}");
        Console.WriteLine($"Зарплата: {salary}\n");
    }
    public void GiveRaise(double percent)
    {
        this.salary *= 1 + (percent / 100d);
    }

    public static void getCount()
    {
        Console.WriteLine(TotalEmployees);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Employee employee1 = new Employee();
        employee1.PrintInfo();
        Employee employee2 = new Employee("Алексей", "GameDev", 50);
        employee2.PrintInfo();
        employee2.GiveRaise(20);
        employee2.PrintInfo();
        Employee.getCount();
        Employee employee3 = new Employee("Том", "GameDev", 50);
        employee3.PrintInfo();
        Employee.getCount();
    }
}