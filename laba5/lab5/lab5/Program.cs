//1.Напишите программу, которая читает число из строки.
//Используйте int.Parse и обработайте FormatException и OverflowException.
//Продемонстрируйте работу с разными входными данными.
//2. Создайте собственное исключение EmptyStringException.
//Напишите метод ValidateString, который проверяет строку на пустоту и выбрасывает исключение.

class EmptyStringException : Exception
{
    public EmptyStringException() : base() { }
    public EmptyStringException(string message) : base(message) { 
    
    }
}
class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите число");
            string? a = Console.ReadLine();
            
            if (a == "" || a == null)
            {
                EmptyStringException e = new EmptyStringException("Exeption of string: empty");
                throw e;
            }
            int.Parse(a);

        }
        catch (FormatException ex)
        {
            Console.Write("Ошибка код: " + ex.Message);
        }
        catch(OverflowException ex) 
        {
            Console.WriteLine("Ошибка код: " + ex.Message);
        }
        catch (EmptyStringException ex)
        {
            Console.WriteLine("Ошибка код: " + ex.Message);
        }
    }
}