//. Дан массив строк. Используя лямбда-выражение с Array.FindAll, найдите все строки, длина которых больше 5 символов.
//Используя Array.ConvertAll, преобразуйте их к верхнему регистру.
//Выведите результаты.

//2. Создайте Predicate<string> для проверки, является ли строка палиндромом (читается одинаково слева направо и справа налево).
//Используйте его с Array.FindLast для поиска последней строки-палиндрома.

class Program {
    static bool isPalindrome(string x)
    {
        for (int i = 0; i < x.Length/2; i++)
        {
            if (x[i] != x[x.Length - i - 1])
            {
                return false;
            }
        }
        return true;
    }
    static void Main()
    {
        string[] StrArr = { "sdaadasd", "asd", "hello", "goodbye", "nice", "The long dark", "Cauldron"};

        string[] task1 = Array.ConvertAll(Array.FindAll(StrArr, (elem) => elem.Length > 5), (x) => x.ToUpper());
        foreach (string str in task1)
        {
            Console.WriteLine(str);
        }


        string[] StrArr2 = { "lolol", "asd", "424", "242", "h", "The long dark", "Cauldrone" };
        Predicate<string> predicate = x => isPalindrome(x);

        Console.WriteLine();

        string[] task2 = Array.FindAll(StrArr2, x => predicate(x));
        foreach (string str in task2)
        {
            Console.WriteLine(str);
        }
        Console.WriteLine();
        string res2 = Array.FindLast(StrArr2, x=> predicate(x));
        Console.WriteLine(res2);
        //Console.WriteLine(isPalindrome("lolol"));
    }
}