//Создайте класс IntSet для работы с множеством целых чисел (уникальные элементы).
//Перегрузите операторы: +(объединение множеств),
//* (пересечение), - (разность множеств), > и < (проверка на подмножество).
//Реализуйте методы расширения для List<int>: Shuffle()(перемешивание элементов),
//GetUnique()(получение уникальных элементов), Average()(среднее арифметическое).
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
class IntSet
{
    List<int> _items = new List<int>();

    public void Add(int item)
    {
        if(!_items.Contains(item))
            _items.Add(item);
    }

    public static IntSet operator +(IntSet a, IntSet b)
    {
        IntSet c = new IntSet();
        foreach (int item in a._items)
        {
            c.Add(item);
        }
        foreach (int item in b._items)
        {
            c.Add(item);
        }

        return c;
    }

    public static IntSet operator *(IntSet a, IntSet b)
    {
        IntSet c = new IntSet();
        foreach(int item in a._items)
        {
            if (b._items.Contains(item))
            {
                c.Add(item);
            }
        }

        return c;
    }

    public static IntSet operator -(IntSet a, IntSet b)
    {
        IntSet c = new IntSet();
        
        foreach(int item in a._items)
        {
            if (!b._items.Contains(item))
            {
                c.Add(item);
            }
        }

        return c;
    }

    

    public static bool operator < (IntSet a, IntSet b)
    {
        bool res = true;

        foreach(int item in a._items)
        {
            if(!b._items.Contains(item))
                res = false;
        }

        return res;
    }

    public static bool operator > (IntSet a, IntSet b)
    {
        bool res = true;

        foreach (int item in b._items)
        {
            if (!a._items.Contains(item))
                res = false;
        }

        return res;
    }

    public override string ToString()
    {
        return "{" + string.Join(", ", _items) + "}";
    }

    
}

public static class Expr
{
    public static List<int> Shuffle(this List<int> list)
    {
        Random rnd = new Random();

        int temp = rnd.Next(0, list.Count - 1);
        for (int i = 0; i < list.Count; i++)
        {
            (list[i], list[temp]) = (list[temp], list[i]);
            temp = rnd.Next(0, list.Count - 1);
        }

        return list;
    }
    public static List<int> GetUnique(this List<int> list) 
    {
        List<int>  res = new List<int>();
        foreach (int item in list)
        {
            if (!res.Contains(item))
                res.Add(item);
        }
        return res;    
    }

    public static double Average(this List<int> list)
    {
        double sum = 0;
        for (int i = 0; i < list.Count;i++)
        {
            sum+= list[i];
        }
        return sum/list.Count;
    }


}

class Program
{
    static void Main()
    {
        IntSet a = new IntSet();
        a.Add(1); a.Add(2); a.Add(3);
        IntSet b = new IntSet();
        b.Add(1); b.Add(2); b.Add(3); b.Add(4);


        IntSet c = new IntSet();
        c = a + b;
        
        Console.WriteLine(c);

        c = a * b;

        Console.WriteLine(c);

        c = a - b;

        Console.WriteLine(c);

        bool rs = a < b;
        if (rs)
            Console.WriteLine("явл подмножеством");
        else
            Console.WriteLine("не явл подмножеством");

        rs = a > b;
        if (rs)
            Console.WriteLine("явл подмножеством");
        else
            Console.WriteLine("не явл подмножеством");


        List<int> list = new List<int>();
        list.Add(1); list.Add(2); list.Add(3); list.Add(4); list.Add(5); list.Add(5);
        Console.WriteLine(list.Count);  
        list.Shuffle();
        foreach (int i in list)
            Console.Write($"{i}, ");
        Console.WriteLine();

        List<int> list2 = new List<int>();
        list2 = list.GetUnique();
        foreach (int i in list2)
            Console.Write($"{i}, ");
        Console.WriteLine();

        Console.WriteLine(list2.Average());
    }
}