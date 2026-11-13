//Анализ продаж компании.
//Реализуйте приложение для анализа данных о продажах.
//Создайте классы Product, Customer, Sale. Данные хранятся в трех коллекциях.
//Необходимо с помощью LINQ выполнить следующие задачи: найти топ-5 самых продаваемых товаров (по количеству продаж);
//определить самого прибыльного клиента (клиента, купившего товаров на наибольшую сумму);
//вывести статистику продаж по месяцам с нарастающим итогом; найти товары, которые ни разу не продавались;
//для каждого клиента определить средний чек его покупок.

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(int id ,string name, int price)
    {
        Id = id;
        Name = name;
        Price = price;
    } 
}

class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

class Sale
{
    public int Id { get; set; }
    public int CustId { get; set; }
    public int ProdId { get; set; }
    public int Count { get; set; }
    public Sale ( int id, int cId,int pId, int count )
    {
        Id = id;
        CustId = cId;
        ProdId = pId;
        Count = count;
    }
}






public class Employee
{
    public string Department { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    public Employee(string dept, string gender, int age, double salary)
    {
        Department = dept;
        Gender = gender;
        Age = age;
        Salary = salary;
    }
}



class Program
{
    static void Main(string[] args)
    {
        List<Product> products = new List<Product>
            {
                new Product(0, "Ноутбук", 50000),
                new Product(1, "Мышка", 1500),
                new Product(2, "Монитор", 15000),
                new Product(3, "Клавиатура", 3000),
                new Product(4, "Коврик", 500),
                new Product(5, "Принтер", 12000), // ни разу не продастся
                new Product(6, "Наушники", 4000)
            };


        var prod = (from el in products
                    orderby el.Price descending, el.Name
                    select el).Take(5);

        foreach (var el in prod)
        {
            Console.WriteLine(el.Name + " " + el.Price);
        }





        List<Customer> customers = new List<Customer>
            {
                new Customer(0, "Алексей"),
                new Customer(1, "Елена"),
                new Customer(2, "Иван")
            };

        List<Sale> sales = new List<Sale>
            {
                new Sale(1, 0, 0, 1), // Алексей купил Ноутбук (50000)
                new Sale(2, 0, 1, 2), // Алексей купил 2 Мышки (3000)
                new Sale(3, 1, 2, 1), // Елена купила Монитор (15000)
                new Sale(4, 1, 3, 1), // Елена купила Клавиатуру (3000)
                new Sale(5, 2, 4, 5), // Иван купил 5 Ковриков (2500)
                new Sale(6, 2, 0, 1), // Иван купил Ноутбук (50000)
                new Sale(7, 0, 6, 0)  // Алексей купил Наушники (4000)
            };

        var top5 = from elem in sales
                   group elem by elem.ProdId into g
                   let total = g.Sum(p => p.Count)

                   orderby total descending 

                   select new
                   {
                       ProdId = g.Key,
                       Count = total
                   };

        var commonPrice = from elem in sales
                         join p in products on elem.ProdId equals p.Id
                         select new
                         {
                             elem.CustId,
                             Price = p.Price * elem.Count
                         };

        var top1 = from elem in commonPrice
                   group elem by elem.CustId into g
                   let total = g.Sum(p => p.Price)
                   orderby total descending
                   select new { prodId = g.Key, PriceSpent = total };

        Console.WriteLine("Самые продаваемые товары");
        int i = 1;
        foreach (var sale in top5)
        {
            if (i > 5)
                break;
            Console.WriteLine(products[sale.ProdId].Name + " - количество " + sale.Count);
            i++;
        }

        Console.WriteLine("Самый прибыльный покупатель:");
        var topClientInfo = top1.FirstOrDefault();
        Console.WriteLine(customers[topClientInfo.prodId].Name + " --> " + topClientInfo.PriceSpent);


        var statist1 = from elem in sales
                       join p in products on elem.ProdId equals p.Id
                       select p.Price * elem.Count;
                      

        Console.WriteLine("Общая сумма \n -> " + statist1.Sum());

        var notSaled = from elem in sales
                       where elem.Count == 0
                       select elem;

        Console.WriteLine("Не продавались");
        foreach (var sale in notSaled) {
            Console.WriteLine(products[sale.Id - 1].Name);
        }

        var averege1 = from elem in sales
                      join p in products on elem.ProdId equals p.Id
                      select new {Element = elem, Price = p.Price * elem.Count };

        var averege2 = from elem in averege1
                       group elem by elem.Element.CustId into g
                       select new {
                           CustId = g.Key, 
                           Aver = g.Average(p => p.Price) 
                       };

        Console.WriteLine("Средние затраты каждого");

        foreach (var sale in averege2)
        {
            Console.WriteLine(customers[sale.CustId].Name + " -> " + sale.Aver);
        }






        List<Employee> employees = new List<Employee>
        {
            new Employee("IT", "М", 25, 120000),
            new Employee("IT", "М", 35, 180000),
            new Employee("IT", "Ж", 28, 140000),
            new Employee("HR", "Ж", 30, 80000),
            new Employee("HR", "Ж", 40, 95000),
            new Employee("HR", "М", 32, 85000),
            new Employee("Sales", "М", 22, 60000),
            new Employee("Sales", "Ж", 24, 65000),
            new Employee("Sales", "Ж", 29, 75000)
        };

        var groupedData = from emp in employees
                          group emp by emp.Department into deptGroup
                          select new
                          {
                              DepartmentName = deptGroup.Key,
                              SubGroups = from subEmp in deptGroup
                                          group subEmp by subEmp.Gender into genderGroup
                                          select new
                                          {
                                              Gender = genderGroup.Key,
                                              AvgAge = genderGroup.Average(e => e.Age),
                                              AvgSalary = genderGroup.Average(e => e.Salary)
                                          }
                          };

        
        Console.WriteLine("Статистика по отделам и полу:");
        Console.WriteLine();

        foreach (var dept in groupedData)
        {
            Console.WriteLine($"Отдел: {dept.DepartmentName}");

            foreach (var sub in dept.SubGroups)
            {
                Console.WriteLine($"  Пол: {sub.Gender} Средний возраст: {sub.AvgAge:F1} лет  Средняя зарплата: {sub.AvgSalary:F2} руб.");
            }

            Console.WriteLine(); 
        }
    }
}
