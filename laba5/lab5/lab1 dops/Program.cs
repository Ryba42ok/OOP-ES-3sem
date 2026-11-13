
using dop1;
using dop2;
using dop3;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace dop1
{
    //Создайте класс Calculator с методами Add, Subtract, Multiply, Divide.
    //В методе Divide выбрасывайте DivideByZeroException при делении на ноль.
    //Добавьте пользовательский интерфейс, который запрашивает операцию и числа, обрабатывает все возможные исключения
    //(неверный ввод, деление на ноль, переполнение). Программа должна продолжать работу после ошибки.
    class Calculator
    {
        public int Add(int x, int y) { return x + y; }
        public int Sub(int x, int y) { return x - y; }
        public int Multi(int x, int y) { return x * y; }
        public int Div(int x, int y) {
            if (y == 0)
            {
                DivideByZeroException e = new DivideByZeroException("Divide by zero ex");
                throw e;
            }
            return x / y; }
    }
}
namespace dop2
{
//Создайте класс Bank с несколькими счетами.Реализуйте метод Transfer, который переводит деньги между счетами.
//При переводе могут возникать исключения:
//•	AccountNotFoundException - если счет не найден
//•	InsufficientFundsException - если недостаточно средств
//•	InvalidAmountException - если сумма отрицательная или равна нулю
//Все исключения должны быть обработаны в вызывающем коде. Добавьте возможность отката транзакции при ошибке (если деньги уже списаны с одного счета, но не зачислены на другой, вернуть их обратно).
    class AccountNotFoundException : Exception
    {
        //public string Message { get; }
        public AccountNotFoundException(string message) : base(message) { }
    }
    class InsufficientFundsException : Exception
    {
        //public string Message { get; }
        public InsufficientFundsException(string message) : base(message) { }
    }
    class InvalidAmountException : Exception
    {
        //public string Message { get; }
        public InvalidAmountException(string message) : base(message)
        {
            
        }
    }

    class Bank
    {
        static int[] all = new int[32];
        public int id { get; }
        public int balance { get; set; }
        static int count = 0;

        public Bank(int id, int balance) { 
            this.id = id;
            this.balance = balance;
            all[count] = id;
            count++;
        }
        
        public void Transfer(Bank other, int sum)
        {
            foreach (int i in all) {
                if (i == other.id)
                {
                    if (this.balance < sum)
                    {
                        InsufficientFundsException ex = new InsufficientFundsException("Ошибка: Недостаточно средств");
                        throw ex;
                    }
                    if (sum <= 0)
                    {
                        InvalidAmountException ex = new InvalidAmountException("Ошибка: Неверные деньги");
                        throw ex;
                    }

                    Console.WriteLine("Деньги переведены");
                    this.balance -= sum;
                    other.balance += sum;
                    return;
                }
            }
            AccountNotFoundException exep = new AccountNotFoundException("Ошибка: Аккаунт не найден");
            throw exep;
        }
    }
}
namespace dop3
{
    //Создайте класс UserValidator для проверки данных пользователя(имя, email, возраст, телефон).
    //Валидатор должен проверять все поля и собирать все ошибки в список, а не выбрасывать исключение при первой ошибке.
    //Создайте собственное исключение ValidationException, которое содержит список всех ошибок.
    //В конце валидации, если есть ошибки, выбросить это исключение.
    //В вызывающем коде вывести все ошибки.

    class ValidationExeption  : Exception
    {
        public List<string> Errors { get; }
        public ValidationExeption(List<string> ex)  : base("Ошибка валидации пользователя: "){ Errors = ex; }           
    }
    class UserValidator
    {
        string? name;
        string email;
        int age;
        string number;

        public UserValidator(string name, string email, int age, string number)
        {
            this.name = name;
            this.email = email;
            this.age = age;
            this.number = number;
        }

        public List<string> ex = new List<string>();

        int i = 0;

        public void Validate() {
            for (int i = 0; i <email.Length; i++)
            {
                if (email[i] == '@')
                {
                    if (email.Substring(i, 6) == "@gmail")
                    {
                        i += 6;
                    }
                    else if (email.Substring(i, 5) == "@mail")
                    {
                        i += 5;
                    }
                    else
                    {
                        
                        ex.Add("Неверный адрес почты");
                        break;
                    }
                    if (email.Substring(i, 4) != ".com")
                    {
                       
                        ex.Add("Неверный адрес почты");
                        break;
                    }
                }
            }
            
           

            if (name == "" || name == null)
            {
                ex.Add("Exeption of Name: empty");
            }

            if (0 > age || age > 120)
                ex.Add("Exeption of age");
            if (string.IsNullOrWhiteSpace(this.number))
            {
                ex.Add("Exeption of number");
            }


            if (ex.Count > 0)
            {
                throw new ValidationExeption(ex);
            }


            
        }


    }
}

class Program
{
    static void Main(string[] args)
    {
        //dop1
        {
            Calculator calculator = new Calculator();


            bool stop = true;
            string k = "5";
            while (stop)
            {
                try
                {
                    Console.WriteLine("---Calculator---");
                    Console.WriteLine("Choose operation");
                    Console.WriteLine("1 - Add\n2 - Sub\n3 - Multi\n4 - Div\n5 - Exit\n");

                    k = (Console.ReadLine());
                    int num = int.Parse(k);

                    switch (num)
                    {
                        case 1:
                            {
                                string a = Console.ReadLine();
                                string b = Console.ReadLine();
                                int iA = int.Parse(a);
                                int iB = int.Parse(b);
                                Console.Clear();
                                Console.WriteLine("Результат: " + calculator.Add(iA, iB) + '\n');
                            }
                            break;
                        case 2:
                            {
                                string a = Console.ReadLine();
                                string b = Console.ReadLine();
                                int iA = int.Parse(a);
                                int iB = int.Parse(b);
                                Console.Clear();
                                Console.WriteLine("Результат: " + calculator.Sub(iA, iB) + '\n');
                            }
                            break;
                        case 3:
                            {
                                string a = Console.ReadLine();
                                string b = Console.ReadLine();
                                int iA = int.Parse(a);
                                int iB = int.Parse(b);
                                Console.Clear();
                                Console.WriteLine("Результат: " + calculator.Multi(iA, iB) + '\n');
                            }
                            break;
                        case 4:
                            {
                                string a = Console.ReadLine();
                                string b = Console.ReadLine();
                                int iA = int.Parse(a);
                                int iB = int.Parse(b);
                                Console.Clear();
                                Console.WriteLine("Результат: " + calculator.Div(iA, iB) + '\n');
                            }
                            break;
                        case 5:
                            {
                                stop = false;
                            }
                            break;
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Введены некорректные данные");
                                //stop = false;
                            }
                            break;
                    }
                }
                catch (DivideByZeroException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                catch (OverflowException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
            }
        }
        //dop2
        {
            Bank bank1 = new Bank(1, 200);
            Bank bank2 = new Bank(2, 300);
            Bank bank3 = new Bank(3, 400);
            try
            {

                bank1.Transfer(bank2, 300);
                Console.WriteLine(bank1.balance);
                Console.WriteLine(bank2.balance);
            }
            catch (AccountNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InsufficientFundsException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidAmountException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {

                bank1.Transfer(bank2, 300);
                Console.WriteLine(bank1.balance);
                Console.WriteLine(bank2.balance);
            }
            catch (AccountNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InsufficientFundsException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidAmountException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        //dop3
        try
        {
            UserValidator userValidator = new UserValidator("Vova", "vovagorevoy@gmail.com", 18, "+37544");
            userValidator.Validate();
        }
        catch (ValidationExeption ex)
        {
            foreach(var exep in ex.Errors)
            {
                Console.WriteLine(exep);
            }
        }
    }

}