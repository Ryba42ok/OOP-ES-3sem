using System;

class Emploee
{
    public string today = "23.09.2026";
    public string Name { get; set; }
    public int Salary { get; set; }
    public string HireDate { get; set; }

    public static float GetTotalSalary(Emploee[] employees)
    {
        float total = 0;
        foreach (Emploee emp in employees)
        {
            total += emp.CalculateSalary();
        }
        return total;
    }
    public static Emploee FindHighestPaid(Emploee[] employees)
    {
        Emploee highestPaid = employees[0];

        foreach (Emploee emp in employees)
        {
            highestPaid = (highestPaid.CalculateSalary() < emp.CalculateSalary()) ? emp : highestPaid;
            ;
        }
        return highestPaid;
    }
    public Emploee(string name, int salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public virtual float CalculateSalary()
    {
        return Salary;
    }


}
class Manager : Emploee
{
    public Manager(string name, int salary) : base(name, salary) { }

    private int bonus = 20;
    public override float CalculateSalary()
    {
        return Salary * (1 + (bonus / 100.0f));
    }
}
class Developer : Emploee
{

    public int ProjectCount = 0;
    private int bonus = 10;

    public override float CalculateSalary()
    {
        return Salary * (1 + (bonus / 100.0f)) + 500 * ProjectCount;
    }
    public Developer(string name, int salary, int projectCount = 0) : base(name, salary)
    {
        this.ProjectCount = projectCount;
    }
}
class SalesPerson : Emploee
{
    public int SaleVol;
   
    public override float CalculateSalary()
    {
        return Salary + SaleVol * 0.05f;
    }

    public SalesPerson(string name, int salary, int SaleVol) : base(name, salary)
    {
        this.SaleVol = SaleVol;
    }
}

class Shape
{
    public string color { get; set; }
    public virtual double GetArea()
    {
        return 0;
    }
    public virtual double GetPerimetr()
    {
        return 0;
    }
    public virtual bool CanContain(Shape other)
    {
        return true;
    }
    public Shape(string color)
    {
        this.color = color;
    }
}
class Circle : Shape
{
    public int radius { get; set; }
    public override double GetPerimetr()
    {
        return 2 * Math.PI * radius;
    }
    public override double GetArea()
    {
        return Math.PI * radius*radius;
    }
    public override bool CanContain(Shape other)
    {
        return (GetArea() > other.GetArea());
    }
    public Circle(string color , int radius) : base(color) {
        this.radius = radius;
    }
}
class Rectangle : Shape
{
    public int a { get; set; }
    public int b { get; set; }
    public override double GetPerimetr()
    {
        return (a + b) * 2;
    }
    public override double GetArea()
    {
        return a*b;
    }
    public override bool CanContain(Shape other)
    {
        return (GetArea() > other.GetArea());
    }
    public Rectangle(string color, int a, int b) : base(color)
    {
        this.a = a;
        this.b = b;
    }
}
class Triangle : Shape
{
    public int a { get; set; }
    public int b { get; set; }
    public int c { get; set; }

    public override double GetPerimetr()
    {
        return a + b + c;
    }
    public override double GetArea()
    {
        double halfPer = GetPerimetr() / 2.0d;
        return Math.Sqrt(halfPer*(halfPer - a)* (halfPer - b)* (halfPer - c));
    }
    public override bool CanContain(Shape other)
    {
        return (this.GetArea() > other.GetArea());
    }
    public Triangle(string color, int a, int b, int c) : base(color)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
}

class Vehicle
{
    public string Brand { get; set; }
    public double Speed { get; set; }
    public virtual double getTravelTime(double distance, int hour)
    {
        return 0;
    }
    public Vehicle(string brand, double speed)
    {
        Brand = brand;
        Speed = speed;
    }
}
class Car : Vehicle
{
    public Car(string brand, double speed) : base(brand, speed) { }
    public override double getTravelTime(double distance, int hour)
    {
        if ((hour >= 8 &&  hour <= 10) || (hour >= 18 && hour <= 20))
        {
            this.Speed *= 0.7d;
        }
        return distance/Speed;
    }
}

class Bicycle : Vehicle
{
    public Bicycle(string brand, double speed) : base(brand, speed) { }
    public override double getTravelTime(double distance, int hour)
    {
        double time = 0;
        double tempdist = 0;
        double speed = Speed;
        while(distance != tempdist)
        {
            time += 1 / speed / 6;
            if (tempdist % 10 == 0)
            {
                
                speed *= 0.9;
                
                tempdist++;
                continue;
            }
            tempdist++;
        }
        
        return time;
    }


}

class PublicTransport : Vehicle
{
    public PublicTransport(string brand, double speed) : base(brand, speed) { }
    public override double getTravelTime(double distance, int hour)
    {
        if (hour >= 23 && hour <= 6)
        {
            Console.WriteLine("Автобус не выезжает в это время либо введено некорректное время");
            return -1;
        }

        return distance/Speed + 1/6;
    }
}

class Programm
{
    static void Main(string[] args)
    {
        Emploee[] emploee = new Emploee[]
        {
            new Emploee("Иван", 1000),
            new Manager("Владимир", 1000),
            new Developer("Дмитрий", 1000, 42),
            new SalesPerson("Матвей", 1000, 100) //0.05     5/100   1/20   1000/20 50
        };
        foreach (Emploee emp in emploee)
        {
            Console.WriteLine(emp.Name + ": "  + emp.CalculateSalary());
        }
        Console.WriteLine();
        Console.WriteLine("Общая ЗП: " + Emploee.GetTotalSalary(emploee));
        Console.WriteLine("Самая высокая ЗП: " + Emploee.FindHighestPaid(emploee).Name);


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        //dop2
        Shape[] shapes = new Shape[] {
            new Circle("green", 10),
            new Rectangle("green", 10, 20),
            new Triangle("green", 10, 20, 30),
        };
        foreach (Shape item in shapes)
        {
            Console.WriteLine(item.color + ' ' + item.GetPerimetr() + ' ' + item.GetArea());
        }
        Circle circ2 = new Circle("green", 20);
        Console.WriteLine(circ2.GetArea() + " " + shapes[0].GetArea());
        Console.WriteLine(shapes[0].CanContain(circ2));
        Console.WriteLine(circ2.CanContain(shapes[0]));

        //dop3

        Vehicle[] cars = new Vehicle[] {
            new Car("Jaguar", 20),
            new Bicycle("BMX", 10),
            new PublicTransport("Kamaz", 10),
        };
        foreach (Vehicle car in cars)
        {
            Console.WriteLine(car.Brand + ' ' + car.Speed + ' ' + car.getTravelTime(20, 3) + "ч");
        }
    }
}
