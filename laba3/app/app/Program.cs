//1.Создайте базовый класс Employee с полями Name и Salary, методом CalculateBonus() (виртуальным).
//Создайте производные классы Manager (бонус 20% от зарплаты)
//и Developer (бонус 10% от зарплаты + 500 за каждый проект).
//2. Добавьте в базовый класс поле HireDate и метод GetExperience(), возвращающий количество лет работы.
//Учитывайте стаж при расчете бонуса (дополнительный процент за каждый год).

using System;

class Emploee
{
    public string today = "23.09.2026";
    public string Name { get; set; }
    public int Salary { get; set; }
    public string HireDate { get; set; }

    public Emploee(string name, int salary, string HireDate)
    {
        this.Name = name;
        this.Salary = salary;
        this.HireDate = HireDate;
    }

    public virtual float CalculateBonus() {
        return Salary * (1 + (this.GetExperience()/100.0f));
    }
    public int GetExperience()
    {
        string[] arrTod = today.Split(".");
        string[] arrHire = HireDate.Split(".");
        
        return System.Convert.ToInt32(arrTod[2]) - System.Convert.ToInt32(arrHire[2]);
    }


}
class Manager : Emploee
{
    public Manager(string name, int salary, string HireDate) : base(name, salary, HireDate) { }

    private int bonus = 20;
    public override float CalculateBonus()
    {
        return Salary*(1 + (bonus / 100.0f + this.GetExperience()/100.0f));
    }
}

class Developer : Emploee
{
    
    public int ProjectCount = 0;
    private int bonus = 10;

    public override float CalculateBonus() {
        return Salary * (1 + (bonus / 100.0f + this.GetExperience() / 100.0f)) + 500 * ProjectCount;
    }
    public Developer(string name, int salary, string HireDate, int projectCount = 0) : base(name, salary, HireDate)
    {
        this.ProjectCount = projectCount;
    }
}

class Programm
{
    static void Main(string[] args)
    {
        Emploee[] emploee = new Emploee[]
        {
            new Emploee("Иван", 1000, "21.5.2016"),
            new Manager("Владимир", 1000, "21.5.2018"),
            new Developer("Дмитрий", 1000, "21.5.2025", 42)
        };
       foreach (Emploee emp in emploee)
       {
            Console.WriteLine(emp.Name + ": "+ emp.GetExperience() + ", "  + emp.CalculateBonus() );
       }
    }
}
