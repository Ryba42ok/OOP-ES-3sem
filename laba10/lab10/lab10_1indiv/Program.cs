//1.Создайте класс Employee с полями: Name, Department, Salary, HireDate.
//Найти трех самых высокооплачиваемых сотрудников из отдела "IT". 
//2. Используя ту же коллекцию, определите среднюю зарплату по каждому отделу и выведите отделы, где средняя зарплата превышает 50000.

using System.Linq;
using System.Linq.Expressions;

class Employee
{
    public string Name;
    public string Department;
    public decimal Salary;
    public string HireDate;

    public Employee(string name, string department, decimal salary, string hireDate)
    {
        Name = name;
        Department = department;
        Salary = salary;
        HireDate = hireDate;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = [
        new Employee ("Vova", "IT",  50000, "21.10.2025"),
        new Employee("Vova2", "IT", 1000000, "21.10.2025"),
        new Employee("Vova3", "IT", 70000, "21.10.2025"),
        new Employee("Vova4", "IT", 80000, "21.10.2025"),
        new Employee("Vova5", "IT", 90000, "21.10.2025"),
         new Employee("Vova6", "TOV", 100000, "21.10.2025")
        ];
        
        
        var highlyPaidEmpl = from emp in (employees).OrderByDescending(emp => emp.Salary)
                             where emp.Department == "IT"
                             select emp;

        int i = 0;
        foreach (var elem in highlyPaidEmpl)
        {
            if (i == 3)
                break;
            Console.WriteLine(elem.Name + "  " + elem.Salary);
            i++;
        }

        var aver = from emp in (employees).OrderByDescending(emp => emp.Salary)
                   group emp by emp.Department into g
                   select new
                   {
                       department = g.Key,
                       AveregeSal = g.Average(n => n.Salary),
                   };


        Console.WriteLine("Средняя зарплата по отделам");
        foreach (var elem in  aver)
        {
            Console.WriteLine(elem.department + "\n" + elem.AveregeSal);
        }
    }


}
