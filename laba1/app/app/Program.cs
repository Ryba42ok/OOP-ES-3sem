using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;

class _types
{
    public void allVars()
    {
        Console.WriteLine("\nbool");
        bool bl1 = false;
        bool bl2 = true;
        Console.WriteLine(bl1);
        Console.WriteLine(bl2);

        //целые числа от 0 до 255;
        Console.WriteLine("\nbyte");
        byte bt1 = 0;
        byte bt2 = 255;

        //byte bt3 = 256; Ошибка 
        Console.WriteLine(bt1);
        Console.WriteLine(bt2);

        //тот же байт, но со знаком на старшем бите
        Console.WriteLine("\nsbyte");
        sbyte sbt1 = -128;
        sbyte sbt2 = 127;
        //sbyte sbt3 = 128; //ошибка
        Console.WriteLine(sbt1);
        Console.WriteLine(sbt2);

        Console.WriteLine("\nchar");
        char chr1 = 'a';
        char chr2 = '\n';
        char chr3 = '4';
        Console.Write(chr1);
        Console.Write(chr2);
        Console.Write(chr3);
        Console.Write(chr2);

        //необходим для абсолютно точных вычислений Хранится в десятичном формате в отличае,  от float/double
        //но занимает больше памяти и не работает быстро,
        //тк не имеет прямого доступа к аппаратному уровню
        //инициализация происходит добавление суффикса m для литералов
        Console.WriteLine("\ndecimal");
        decimal dec1 = 1.2321234335m;
        Console.WriteLine(dec1);

        //как и в с++
        Console.WriteLine("\ndouble");
        double dbl1 = 1.23423d;
        Console.WriteLine(dbl1);
        Console.WriteLine("\nfloat");
        float flt1 = 1.2345365f;
        Console.WriteLine(flt1);

        Console.WriteLine("\nint");
        Console.WriteLine("Введите 2 числа int");

        int integer1 = System.Convert.ToInt32((Console.ReadLine()));
        int integer2 = int.Parse(Console.ReadLine());

        Console.WriteLine(integer1);
        Console.WriteLine(integer2);
        //беззнаковый int
        Console.WriteLine("\nuint");
        uint unsinteger = 4294967295;
        Console.WriteLine(unsinteger);

        //Основное отличие int от nint заключается в том, что размер int фиксирован,
        //а размер nint меняется в зависимости от разрядности операционной системы и процессора.



        Console.WriteLine("\nnint");

        int a = int.MaxValue;
        nint b = nint.MaxValue;
        Console.WriteLine(a);  // 2_147_483_647
        Console.WriteLine(b); // 9_223_372_036_854_775_807 (равно long.MaxValue)

        //unint
        Console.WriteLine("\nnuint");
        uint unint1 = uint.MaxValue;
        nuint unint2 = nuint.MaxValue;
        Console.WriteLine(unint1);
        Console.WriteLine(unint2);


        Console.WriteLine("\nlong");
        long lg1 = long.MaxValue;
        long lg2 = long.MinValue;
        Console.WriteLine(lg1);
        Console.WriteLine(lg2);

        Console.WriteLine("\nulong");
        ulong ulg1 = ulong.MaxValue;
        ulong ulg2 = ulong.MinValue;
        Console.WriteLine(ulg1);
        Console.WriteLine(ulg2);

        Console.WriteLine("\nshort");
        short shrt1 = short.MaxValue;
        short shrt2 = short.MinValue;
        Console.WriteLine(shrt1);
        Console.WriteLine(shrt2);

        Console.WriteLine("\nushort");
        ushort ushrt1 = ushort.MaxValue;
        ushort ushrt2 = ushort.MinValue;
        Console.WriteLine(ushrt1);
        Console.WriteLine(ushrt2);
    }
    public void conversion()
    {
        Console.WriteLine("Явное приведение");
        int integr = int.MaxValue;
        short shrt = short.MaxValue;
        float flt = float.MaxValue;
        uint uinteger = uint.MaxValue;
        byte bt = 255;

        Console.WriteLine($"Integer = {integr}\nshort = {shrt} \nfloat = {flt} \nuint = {uinteger}\nbyte = {bt}\n   After Conversion\n");

        long itol = (long)integr;
        int stoi = (int)shrt;
        double ftod = (double)flt;
        ulong uitoul = (ulong)uinteger;
        char btoc  = (char)bt;


        Console.Write($"int to long: {itol}\nshort to int: {stoi}\nfloat to double: {ftod}\nuint to ulong: {uitoul}\nbyte to char: '{btoc}'\n");

        Console.WriteLine("Неявное приведение");
        int intgr2 = 100;
        long lg2= integr;

        float flt2 = 1.3f;
        double dbl2 = flt2;

        byte bt2 = 0x30;
        short sh2 = bt2;

        short shrt2 = 10;
        int intg = shrt2;

        int int3 = int.MinValue;
        float flt3 = int3;

     

    }
    public void package()
    {
        int variable = 42;
        Console.WriteLine("До упаковки " + variable);
        object obj = variable;
        Console.WriteLine("После упаковки " + obj);

        Console.WriteLine("Распаковка");
        int var2 = (int)obj;
        Console.WriteLine("После распаковки " + var2);
        
    }
    public void useVar()
    {

        // i компилируется как int
        var i = 5;

        // s как string
        var s = "Hello";

        // a как int[]
        var a = new[] { 0, 1, 2 };
    }
    public void Nullable()
    {
        //значимые переменные
        int? age = null; // Переменная пуста
        if (age.HasValue)
        {
            Console.WriteLine(age.Value); 
        }
        //ссылочные переменные
        String? name = null;
        if (name.Length != 0)
        {
            Console.WriteLine("Строка существует");
        }


        
    }
    public void useVar2()
    {
        var i = 5;
        //i = 2.453d;
        //т.к в первой строке мы присваиваем i ццелочисленное 5,
        //а затем пытаемся неявно привести его к числу double,
        //то компилятор не сможет обработать такое преобразование и выдаст ошибку
    }
}

class strings {
    public void literals()
    {
        string str1 = "КОТ";
        string str2 = "Котёнок";


        Console.WriteLine(string.Compare(str1, str2)); //-1 т.к hello меньше 
        Console.WriteLine(string.Compare(str2, str1)); //1 т.к hello меньше, но передана вторым параметром 
        Console.WriteLine(string.Compare(str1, str1)); //0 т.к 
    }
    public void methods()
    {
        string s1 = "hello,";
        string s2 = "world,";
        string s3 = "time,";

        string str1 = string.Concat(s1, s2, s3);
        Console.WriteLine("\ns1 после сцепления с s2 " + str1 );
        string str2 = str1;
        Console.WriteLine("\nКопия str1 " + str2);

        Console.WriteLine("\nПодстрока в " + str1);
        string str3 = str1.Substring(0, 12);
        Console.WriteLine(str3);

        string[] str4 = str1.Split(',');
        Console.WriteLine($"\nПосле разделения строки {str1} на подстроки по символу ,");
        foreach (string s in str4)
            Console.Write(s + ' ');

        Console.WriteLine($"\n\nВставка подстроки N_Time в строку {str2} ");
        string str5 = str2.Insert(str2.Length, "N-Time");
        Console.WriteLine(str5);

        Console.WriteLine($"\nУдаление подстроки N_Time из строки {str5} "); //это и есть интерполирование строк
        string str6 = str5.Remove(str5.Length - "N-Time".Length);
        Console.WriteLine(str6);

    }
    public void nullstr()
    {
        string? str1 = null;
        string str2 = "";
        bool isStr1 = string.IsNullOrEmpty(str1);
        bool isStr2 = string.IsNullOrEmpty(str2);

        Console.WriteLine(isStr1);
        Console.WriteLine(isStr2);

        bool bl3 = string.IsNullOrWhiteSpace("                "); // проверка на пробелы
        Console.WriteLine(bl3);

        
    }

    public void StrBuild()
    {
        StringBuilder stringBuilder = new StringBuilder("ABCZ", 50);
        Console.WriteLine(stringBuilder.Length);
        Console.WriteLine(stringBuilder.Capacity);

        Console.WriteLine(stringBuilder);

        stringBuilder.Append(new char[] { 'D', 'E', 'F' });

        Console.WriteLine(stringBuilder);

        
        stringBuilder.Replace("Z", "");

        Console.WriteLine(stringBuilder);

        stringBuilder.Insert(0, "Alphabet: ");
        Console.WriteLine(stringBuilder);

        string ResultStr = stringBuilder.ToString();
    }
}

class arrays
{
    public void Matrix()
    {
        const int n = 5;
        const int m = 5;

        int[,] arr = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                arr[i, j] = i + j;
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void ArrStr()
    {
        string[] strArr = { "time", "hello", "world", "sun" };
        foreach (string str in strArr)
            Console.Write(str + " ");

        Console.WriteLine("Какую позицию массива вы хотите поменять");
        int pos = int.Parse(Console.ReadLine());
        if (pos > strArr.Length || pos < 0)
        {
            Console.WriteLine("Позиция должна быть в пределах размера");
            return;
        }
        Console.WriteLine("Что вы хотите вставить вместо этой позиции");
        string str1 = Console.ReadLine();

        strArr[pos] = str1;

        foreach (string str in strArr)
            Console.Write(str + " ");
    }

    public void ladderArray()
    {
        const int n = 3;
        const int m = 6;

        float[,] arr = new float[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 2; j < m; j++)
            {
                Console.WriteLine($"Введите значение массива на позиции [{i},{j}]");
                float value = float.Parse(Console.ReadLine());
                arr[i, j] = value;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (arr[i, j] != 0.0f)
                    Console.Write("{0, 8}", arr[i, j]);
                else Console.Write("        ");
            }
            Console.WriteLine();
        }
    }

    public void last()
    {
        var str = "Hello World";
        Console.WriteLine(str.Length);
        Console.WriteLine(str.Insert(str.Length, "N-Time"));

        var arr = new[] { 1, 2, 3, 4 };
        foreach (int i in arr)
            Console.WriteLine(i);
    }

}

class Cartej
{
    public void create()
    {
        (int, string, char, string, ulong) t1;
        t1.Item1 = 1;
        t1.Item2 = "Hello";
        t1.Item3 = ',';
        t1.Item4 = "World";
        t1.Item5 = ulong.MaxValue;
        
        Console.WriteLine($"{t1.Item1}\n{t1.Item2}\n{t1.Item3}\n{t1.Item4}\n{t1.Item5}");
        Console.WriteLine($"{t1.Item2}{t1.Item3} {t1.Item4}");

        //Распаковка
        int var1 = (int)t1.Item1;
        string var2 = (string)t1.Item2;

        //упаковка
        int v = 42;
        object ob = v;

        var(integer, str1, chr, str2, ul) = t1;
        //integer  = 1 str1 = Hello char = ',' str2 = World ...

        var (_, str3 ,_, str4, _) = t1;
        //интересует только Hello и World

        //сравнение кортежей
        (string a, int b) t2; t2.a = "gello"; t2.b = 424242;
        (string a, int b) t3; t3.a = "hello"; t3.b = 4242;
        //сравнивается по первым двум значениям, если они равны, то сравниваются следующие


        Console.WriteLine(t2.CompareTo(t3));
    }
}

class funct
{
    public void funcCr()
    {
        void ch()
        {
            int a = int.MaxValue;
            try
            {
                checked
                {
                    a += 1;
                    Console.WriteLine(a);
                }
            }
            //Когда происходит арифметическое переполнение, контекст проверки переполнения определяет, что происходит
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void unch()
        {
            int b = int.MaxValue;
            unchecked
            {
                b += 1;
                Console.WriteLine(b);
            }
        }

        ch();
        unch();
    }


}

class Program
{
    static void Main()
    {
        (int, int, int, char) GetSmth(int[] arr, string str)
        {
            int min = int.MaxValue, max = int.MinValue, sum = 0;
            foreach (int i in arr)
            {
                if (i < min)
                    min = i;
                if (i > max)
                    max = i;
                sum += i;
            }
            return (min, max, sum, str[0]);
        }
        int[] arr = { 1, 2, 3, 4, 5, 0 };
        string hello = "hello world";

        var (min, max, sum, first) = GetSmth(arr, hello);

        _types task1 = new _types();
        task1.allVars();

        strings task2 = new strings();
        task2.literals();

        arrays task3 = new arrays();
        task3.ladderArray();
        

        Cartej task4 = new Cartej();
        task4.create();


        funct task5 = new funct();
        funct f = new funct();

        f.funcCr();



    }
}