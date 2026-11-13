using System.Runtime.CompilerServices;

abstract class BankAccount
{
    protected int num;
    protected string own;
    protected decimal balance;
    protected string date;
    protected string pin;

    protected bool IsBlocked = false;
    int attemps = 0;

    public abstract void Deposit(decimal amount, string pin);
    public abstract void Withdraw(decimal amount, string pin);
    public bool verifyPin(string pincode)
    {
        if (IsBlocked)
            return false;
        if (attemps == 3)
            IsBlocked = true;
        if (pincode == pin)
        {
            attemps = 0;
            return true;
        }
        attemps++;
        return false;
    }

    protected BankAccount(int num, string own, decimal balance, string date, string pin)
    {
        this.num = num;
        this.own = own;
        this.balance = balance;
        this.date = date;
        this.pin = pin;
    }
}    

    class SavingsAccount : BankAccount
    {
        public SavingsAccount(int num, string own, decimal balance, string date, string pin) :
            base(num, own, balance, date, pin)
        { }

        public override void Deposit(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                this.balance += amount;
            }
        }
        public override void Withdraw(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                if (balance - amount > 0)
                    this.balance -= amount;
                else
                {
                    Console.WriteLine("Вы снимаете больше, чем у вас есть на балансе");
                }
            }
        }
        public void MountlySal()
        {
            this.balance += balance * 0.01m;
        }
    }
    class CheckingAccount : BankAccount
    {
        public CheckingAccount(int num, string own, decimal balance, string date, string pin) :
            base(num, own, balance, date, pin)
        { }

        public override void Deposit(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                this.balance += amount - amount * 0.01m;
            }
        }
        public override void Withdraw(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                if (balance - amount > 0)
                    this.balance -= amount + amount * 0.01m;
                else
                {
                    Console.WriteLine("Вы снимаете больше, чем у вас есть на балансе");
                }
            }
        }
    }
    class CreditAccount : BankAccount
    {
        public decimal duty = 0;
        private decimal limit = 1000;
        public CreditAccount(int num, string own, decimal balance, string date, string pin) :
            base(num, own, balance, date, pin)
        { }

        public override void Deposit(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                this.balance += amount;
            }
        }
        public void DutyDeposit(decimal amount, string pin)
        {
            if (verifyPin(pin) && duty != 0)
            {
                if (duty - amount > 0)
                    duty -= amount;
                else
                {
                    duty = 0;
                    balance += Math.Abs(duty-amount);
                }
            }
        }
        public override void Withdraw(decimal amount, string pin)
        {
            if (verifyPin(pin))
            {
                if (balance - amount < 0)
                {
                    duty += Math.Abs(balance - amount);
                }
            else if (balance - amount > 0)
            {
                balance -= amount;
            }
                else if (duty > limit)
            {
                Console.WriteLine("Вы превысили лимит, так нельзя");
            }

            }
        }


}
//2.Создайте абстрактный класс Character с полями имени, уровня, здоровья, силы, ловкости, интеллекта.
//Добавьте абстрактные методы Attack() и TakeDamage().
//Реализуйте классы Warrior (воин) с системой ярости и блокирования,
//Mage (маг) с системой маны и заклинаниями, Archer (лучник) с системой меткости и критических ударов.
//Инкапсулируйте здоровье так, чтобы оно не могло упасть ниже 0.
//Добавьте систему опыта и повышения уровня.
//При повышении уровня характеристики растут в зависимости от класса.
abstract class Character
{
    public string name;
    public int level;
    public int health;
    public decimal streight;
    public decimal lovk;
    public decimal intel;

    public abstract decimal Attack();
    public abstract void TakeDamage();

    protected Character(string name, int level, int health, decimal streight, decimal lovk, decimal intel)
    {
        this.name = name;
        this.level = level;
        this.health = health;
        this.streight = streight;
        this.lovk = lovk;
        this.intel = intel;
    }
    public abstract void levelUp();
    public void addExp(int exp)
    {
        if (exp >= 100)
        {
            while (exp >= 100)
            {
                levelUp();
                exp -= 100;
            }
        }
    }
}
class Warrior : Character
{
    public Warrior(string name, int level, int health, decimal streight, decimal lovk, decimal intel) : base (name, level, health, streight, lovk, intel)
    { }
    public override decimal Attack()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.7m && lovk <= num)
        {
            return streight * 2;
        }
        return 0;
    }
    public override void levelUp()
    {
        health += 5;
        streight += 5;
        lovk += 3;
    }
   
    public override void TakeDamage()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.5m && lovk <= num)
        {
            if (health - 20 < 0)
            { 
                Console.WriteLine("Хп закончились");
                return;
            }    
            health-= 20;
        }
        Console.WriteLine("Parried");
    }
    public void Fury()
    {
        //int dur = 30;

        streight *= 1.5m;
        lovk *= 0.5m;
    }
    public bool Parry()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.5m && lovk <= num)
        {
            return false;
        }
        return true;
    }

}
class Mage : Character
{
    public Mage(string name, int level, int health, decimal streight, decimal lovk, decimal intel, int mana) : base(name, level, health, streight, lovk, intel) { this.mana = mana; }
    public int mana;
    public override decimal Attack()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.3m && lovk <= num)
        {
            return streight * 2;
        }
        return 0;
    }
    public override void levelUp()
    {
        health += 3;
        intel += 5;
        mana += 10;
    }
    public void addExp(int exp)
    {
        while (exp - 100 > 100)
        {
            levelUp();
            exp -= 100;
        }
    }
    public override void TakeDamage()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.2m && lovk <= num)
        {
            if (health - 20 < 0)
            {
                Console.WriteLine("Хп закончились");
                return;
            }
            health -= 20;
        }
        Console.WriteLine("Parried");
        
    }

    public decimal FireBall()
    {
        if (mana - 20 > 0)
            return intel * 1.5m;
        return 0;
    }

    public decimal LightScroll()
    {
        if (mana - 50 > 0)
            return intel * 4m;
        return 0;
    }
    public decimal ColdBreath()
    {
        if (mana - 15 > 0)
            return intel;
        return 0;
    }
}
class Archer : Character
{
    public Archer(string name, int level, int health, decimal streight, decimal lovk, decimal intel, decimal extrahit) : base(name, level, health, streight, lovk, intel) { this.extrahit = extrahit; }
    public decimal extrahit;
    public override decimal Attack()
    {
        Random random = new Random();
        int num = random.Next(100);
        int num2 = random.Next(100);
        if (0 <= lovk * 0.2m && lovk <= num)
        {
            if (0 <= extrahit && extrahit <= num2)
            {
                return 20 * 1.65m;
            }
            return 20;
        }
        Console.WriteLine("Parried");
        return 0;
    }
    public override void levelUp()
    {
        health += 5;
        streight += 3;
        lovk += 5;
    }
    public void addExp(int exp)
    {
        while (exp - 100 > 100)
        {
            levelUp();
            exp -= 100;
        }
    }
    public override void TakeDamage()
    {
        Random random = new Random();
        int num = random.Next(100);
        if (0 <= lovk * 0.2m && lovk <= num)
        {
            if (health - 20 < 0)
            {
                Console.WriteLine("Хп закончились");
                return;
            }
            health -= 20;
        }
        Console.WriteLine("Parried");

    }
}

public abstract class Ticket
{
    public string EventName { get; set; }       // мероприятие
    public int Seat { get; set; }               // место
    public double BasePrice { get; set; }       // базовая цена
    public DateTime EventDate { get; set; }     // дата мероприятия
    public string TicketNumber { get; }         // уникальный номер

    // Инкапсуляция статуса
    private bool _isBooked;
    public bool IsBooked
    {
        get { return _isBooked; }
        private set { _isBooked = value; }
    }

    private static int _counter = 1000;

    public Ticket(string eventName, int seat, double basePrice, DateTime eventDate)
    {
        EventName = eventName;
        Seat = seat;
        BasePrice = basePrice;
        EventDate = eventDate;
        _isBooked = false;
        TicketNumber = "T" + (++_counter);
    }

    // Абстрактный метод расчёта цены
    public abstract double CalculatePrice();

    // Бронирование
    public bool Book()
    {
        if (_isBooked)
        {
            Console.WriteLine($"Место {Seat} уже занято!");
            return false;
        }
        _isBooked = true;
        Console.WriteLine($"Билет {TicketNumber} на место {Seat} забронирован.");
        return true;
    }

    // Возврат билета со штрафом
    public double Refund()
    {
        if (!_isBooked)
        {
            Console.WriteLine("Билет не был забронирован.");
            return 0;
        }

        double price = CalculatePrice();
        double penaltyPercent = GetPenaltyPercent();
        double refundSum = price - price * penaltyPercent / 100;

        _isBooked = false;
        Console.WriteLine($"Возврат билета {TicketNumber}. Штраф: {penaltyPercent}%");
        Console.WriteLine($"К возврату: {refundSum:F2}");
        return refundSum;
    }

    // Штраф зависит от времени до мероприятия
    private double GetPenaltyPercent()
    {
        TimeSpan timeLeft = EventDate - DateTime.Now;

        if (timeLeft.TotalDays >= 10) return 10;
        if (timeLeft.TotalDays >= 3) return 30;
        if (timeLeft.TotalDays >= 1) return 50;
        return 80;
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"--- {GetType().Name} ---");
        Console.WriteLine($"Номер: {TicketNumber}");
        Console.WriteLine($"Мероприятие: {EventName}");
        Console.WriteLine($"Место: {Seat}");
        Console.WriteLine($"Дата: {EventDate:dd.MM.yyyy HH:mm}");
        Console.WriteLine($"Базовая цена: {BasePrice:F2}");
        Console.WriteLine($"Итоговая цена: {CalculatePrice():F2}");
        Console.WriteLine($"Статус: {(IsBooked ? "занято" : "свободно")}");
    }
}






class program
{
    static void Main(string[] args)
    {
        CreditAccount cred = new CreditAccount(1, "vlad", 100, "124", "1234");
        cred.Withdraw(500, "1234");
        Console.WriteLine(cred.duty);
        cred.DutyDeposit(1000, "1234");
        Console.WriteLine(cred.duty);

        Console.WriteLine();
        Console.WriteLine("dop2");

        //dop2
        Warrior war1 = new Warrior("Варвар", 1, 30, 5, 5, 5);
        Console.WriteLine(war1.Attack());
        Console.WriteLine(war1.Attack());
        Console.WriteLine(war1.Attack());
        Console.WriteLine(war1.Attack());
        Console.WriteLine(war1.Attack());

        war1.TakeDamage();
        war1.TakeDamage();
        war1.TakeDamage();
        war1.TakeDamage();
        war1.TakeDamage();

        war1.addExp(101);
        Console.WriteLine(war1.streight);

        war1.Fury();
        Console.WriteLine(war1.streight);

        Archer arch1 = new Archer("Amazon", 5, 100, 5, 5 ,5, 20);
        Console.WriteLine(arch1.name);
        Console.WriteLine(arch1.Attack());
        Console.WriteLine(arch1.Attack());
        Console.WriteLine(arch1.Attack());
        Console.WriteLine(arch1.Attack());
        Console.WriteLine(arch1.Attack());
    }
}