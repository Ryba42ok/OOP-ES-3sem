using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Runtime.CompilerServices;

class smartArray
{
    public List<int> list = new List<int>();

    public int this[int index]
    {
        get => list[index - 1];
        set => list[index - 1] = value;
    }

    public int Count => list.Count;
    public void Add(int value) => list.Add(value);
    public bool Remove(int value) => list.Remove(value);


    public static smartArray operator +(smartArray arr)
    {
        
        smartArray res = new smartArray();
        for (int i = 1; i <= arr.Count; i++)
        {
            res.Add(0);
        }
        int last = arr[arr.Count];
        for (int i = arr.Count; i >= 2; i--)
        {
            res[i] = arr[i - 1];
        }
        res[1] = last;

        return res;
    }
    public static smartArray operator -(smartArray arr)
    {
        smartArray res = new smartArray();
        for (int i = 1; i <= arr.Count; i++)
        {
            res.Add(0);
        }
        int first = arr[1];
        for (int i = 1; i < arr.Count; i++)
        {
            res[i] = arr[i + 1];
        }
        res[arr.Count] = first;

        return res;
    }
    public static smartArray operator + (smartArray l, int value)
    {
        for(int i = 1; i < l.Count + 1; i++) {
            l[i] += value;
        }
        return l;
    }
    public static smartArray operator -(smartArray l, int value)
    {
        for (int i = 1; i < l.Count + 1; i++)
        {
            l[i] -= value;
        }
        return l;
    }
}

class cyclicArr
{
    public List<int> list = new List<int>();
    public int Count => list.Count;
    public void Add(int val) => list.Add(val);
   
    public int this[int index] {
        get {
            if (index > 0 || index % Count == 0)
                return list[(index) % Count];
            else
                return list[((index) % Count) + Count];
        }
        set {if (index > 0 || index % Count == 0)
                list[(index)%Count] = value;
            else
                list[((index) % Count) + Count] = value;
        }
    }

    public static cyclicArr operator ++(cyclicArr arr)
    {

        cyclicArr res = new cyclicArr();
        for (int i = 1; i <= arr.Count; i++)
        {
            res.Add(0);
        }
        int last = arr[arr.Count];
        for (int i = arr.Count; i >= 2; i--)
        {
            res[i] = arr[i - 1];
        }
        res[1] = last;

        return res;
    }
    public static cyclicArr operator --(cyclicArr arr)
    {
        cyclicArr res = new cyclicArr();
        for (int i = 1; i <= arr.Count; i++)
        {
            res.Add(0);
        }
        int first = arr[1];
        for (int i = 1; i < arr.Count; i++)
        {
            res[i] = arr[i + 1];
        }
        res[arr.Count] = first;

        return res;
    }
};

class AlternatingArray
{
    List<int> list = new List<int>();

    public void Add(int val) => list.Add(val);
    public int Count => list.Count;
    public int this[int index] {
        get { return list[index]; } 
        set { list[index] = value; }
    }

    public static AlternatingArray operator + (AlternatingArray l1, AlternatingArray l2)
    {
        AlternatingArray res = new AlternatingArray();
        AlternatingArray more = (l1.list.Count < l2.list.Count) ? l2: l1;
        int less = (l1.list.Count < l2.list.Count)? l1.list.Count : l2.list.Count;
        int buf = 0;
        for (int i = 0; i < less; i++)
        {
            res.list.Add(l1.list[i]);
            res.list.Add(l2.list[i]);
            buf = i;
        }
        for (int i = buf + 1; i < more.list.Count; i++)
        {
            res.list.Add(more.list[i]);
        }
        

        return res;
    }

    public static AlternatingArray operator  * (AlternatingArray arr, int value)
    {
        AlternatingArray res = new AlternatingArray();
        for(int i = 0; i < value; i++)
            for(int j = 0; j < arr.Count; j++)
            {
                res.Add(arr[j]);
            }
        return res;
    }
}
public static class expr
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
}


class Program
{
    static void Main(string[] args)
    {
        smartArray l1= new smartArray();
        l1.Add(1);
        l1.Add(2);
        l1.Add(3);
        l1.Add(4);
        l1.Add(5);
        l1.Add(6);
        l1.Add(7);
        foreach (int i in l1.list)
            Console.Write($"{i} ");
        Console.WriteLine();
        l1 = +l1;
        foreach (int i in l1.list)
            Console.Write($"{i} ");
        l1 = -l1;
        Console.WriteLine();
        foreach (int i in l1.list)
            Console.Write($"{i} ");
        Console.WriteLine();


        Console.WriteLine(l1[1]);

        Console.WriteLine((l1 + 2)[2]);
        Console.WriteLine((l1 - 2)[2]);

        l1.list.Shuffle();

        foreach (int i in l1.list)
            Console.Write($"{i} ");

        Console.WriteLine();
        Console.WriteLine(l1[1]);



        cyclicArr l2 = new cyclicArr();
        l2.Add(1);
        l2.Add(2);
        l2.Add(3);


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine(l2[5]);
        l2 = ++l2;
        Console.WriteLine(l2[5]);
        l2 = --l2;
        Console.WriteLine(l2[5]);
        Console.WriteLine(); 
        
        Console.WriteLine(l2[-6]);

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        AlternatingArray altArr1 = new AlternatingArray();
        AlternatingArray altArr2 = new AlternatingArray();

        altArr1.Add(1);
        altArr1.Add(2);
        altArr1.Add(3);
        altArr1.Add(4);

        altArr2.Add(1);
        altArr2.Add(2);
        altArr2.Add(3);
        altArr2.Add(4);

        AlternatingArray altArr3 = new AlternatingArray();
        altArr3 = altArr1 + altArr2;
        for (int i = 0; i < altArr3.Count; i++)
            Console.Write(altArr3[i] + " ");
        Console.WriteLine();
        altArr3 = altArr1 * 3;
        for (int i = 0; i < altArr3.Count; i++)
            Console.Write(altArr3[i] + " ");
        Console.WriteLine();
    }
}