using dop1;
using dop2;
using dop3;
using System;

namespace dop1
{
    //1. Создайте обобщенный класс GenericComparator<T> с методом Compare(T a, T b, string criterion),
    //который сравнивает два объекта по заданному критерию
    //(например, для чисел — по значению, для строк — по длине, для дат — по году, для пользовательских классов — по конкретному свойству).
    //Критерий передается в виде строки.
    //Используйте рефлексию для доступа к свойствам объектов.
    //Добавьте обработку исключений, если свойство не найдено или типы несовместимы.

    class NotFoundExc : Exception
    {
        public NotFoundExc(string message) : base(message) { }
    }

    public class GenericComparator<T>
    {
        public int Compare(T x, T y, string criter)
        {
            Type type = typeof(T);
            if ((type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(short) || type == typeof(long)) && criter == "Value")
            {
                if (x is IComparable comp)
                {
                    return comp.CompareTo(y);
                }
            }
            else if (type == typeof(string) && criter == "Length")
            {
                string s1 = x as string;
                string s2 = y as string;
                if (s1.Length < s2.Length)
                    return -1;
                else if (s1.Length > s2.Length)
                    { return 1; }
                else {  return 0; }
            }
            else if (type == typeof(string) && criter == "Year")
            {
                string s1 = x as string;
                string s2 = y as string;

                if (int.Parse(s1.Split(".")[2]) < int.Parse(s2.Split(".")[2]))
                {
                    return -1;
                }
                else if (int.Parse(s1.Split(".")[2]) > int.Parse(s2.Split(".")[2]))
                {
                    return 1;
                }
                else { return 0; }
            }
            throw new NotFoundExc("Nothing found");
        }
    }
}
namespace dop2
{
    //2. Разработайте обобщенный класс ChainBuilder<T> где T : class, который позволяет создавать цепочку связанных объектов.
    //Реализуйте методы AddNext(T item), GetNext(T item), IsCircular() для проверки, образует ли цепочка цикл.
    //Внутри используйте словарь для хранения связей "элемент -> следующий элемент".
    //Добавьте обработку исключений при попытке создать цикл (если метод AllowCircular установлен в false).
    class Exc : Exception
    {
        public Exc(string name) : base(name) { }
    }

    public class ChainBuilder<T> where T : class
    {
        public Dictionary<T, T> dict;
        public bool AllowCircular;
        private T last;
        public ChainBuilder(bool allow) {
            this.dict = new Dictionary<T, T>();
            AllowCircular = allow;
            this.last = null;
        }

       

        public void AddNext(T item)
        {
            if (item == null)
            {
                return;
            }

            if (this.last == null)
            {
                this.last = item;
                return;
            }

            if (this.dict.ContainsKey(item))
            {
                throw new Exc("Цикл");
            }

            dict[this.last] = item;
            this.last = item;
        }
        public T GetNext(T item)
        {
            if (item == null)
                return null;

            return dict[item];
        }
    }
}
namespace dop3
{
    public enum SortAlgorithm
    {
        Bubble,
        Selection,
        Insertion,
        Quick
    }
    public class SortStatistics
    {
        public long Comparisons { get; set; }
        public long Swaps { get; set; }
        public SortAlgorithm Algorithm { get; set; }
        public int ElementCount { get; set; }
        public TimeSpan Elapsed { get; set; }

        public override string ToString()
        {
            return $"Алгоритм: {Algorithm} | Элементов: {ElementCount} | " +
                   $"Сравнений: {Comparisons} | Обменов: {Swaps} | Время: {Elapsed.TotalMilliseconds:F3} мс";
        }
    }
    public class Sorter<T> where T : IComparable<T>
    {
   
        public SortStatistics LastStatistics { get; private set; }

        private long _comparisons;
        private long _swaps;

        public void Sort(T[] array, SortAlgorithm algorithm)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length == 0)
            {
                LastStatistics = new SortStatistics
                {
                    Algorithm = algorithm,
                    ElementCount = 0,
                    Comparisons = 0,
                    Swaps = 0,
                    Elapsed = TimeSpan.Zero
                };
                Console.WriteLine(LastStatistics);
                return;
            }

            // Сбрасываем счётчики
            _comparisons = 0;
            _swaps = 0;

            var startTime = DateTime.UtcNow;

            switch (algorithm)
            {
                case SortAlgorithm.Bubble: BubbleSort(array); break;
                case SortAlgorithm.Selection: SelectionSort(array); break;
                case SortAlgorithm.Insertion: InsertionSort(array); break;
                case SortAlgorithm.Quick: QuickSort(array); break;
                default:
                    throw new ArgumentException($"Неизвестный алгоритм: {algorithm}", nameof(algorithm));
            }

            var elapsed = DateTime.UtcNow - startTime;

            LastStatistics = new SortStatistics
            {
                Algorithm = algorithm,
                ElementCount = array.Length,
                Comparisons = _comparisons,
                Swaps = _swaps,
                Elapsed = elapsed
            };

            Console.WriteLine(LastStatistics);
        }

      
        public void BubbleSort(T[] array)
        {
            int n = array.Length;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    _comparisons++;
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap(array, j, j + 1);
                        swapped = true;
                    }
                }
                if (!swapped) break; // уже отсортировано
            }
        }

        // ---------------- Selection Sort ----------------
    
        public void SelectionSort(T[] array)
        {
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    _comparisons++;
                    if (array[j].CompareTo(array[minIdx]) < 0)
                        minIdx = j;
                }
                if (minIdx != i)
                    Swap(array, i, minIdx);
            }
        }

        // ---------------- Insertion Sort ----------------
       
        public void InsertionSort(T[] array)
        {
            int n = array.Length;

            for (int i = 1; i < n; i++)
            {
                T key = array[i];
                int j = i - 1;

                while (j >= 0)
                {
                    _comparisons++;
                    if (array[j].CompareTo(key) > 0)
                    {
                        array[j + 1] = array[j];
                        _swaps++; // считаем перемещение как обмен
                        j--;
                    }
                    else break;
                }
                array[j + 1] = key;
            }
        }

        // ---------------- Quick Sort ----------------
       
        public void QuickSort(T[] array)
        {
            QuickSortRecursive(array, 0, array.Length - 1);
        }

        private void QuickSortRecursive(T[] array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSortRecursive(array, low, pivotIndex - 1);
                QuickSortRecursive(array, pivotIndex + 1, high);
            }
        }

        private int Partition(T[] array, int low, int high)
        {
            T pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                _comparisons++;
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    if (i != j)
                        Swap(array, i, j);
                }
            }

            if (i + 1 != high)
                Swap(array, i + 1, high);

            return i + 1;
        }

        // ---------------- Вспомогательные ----------------
        private void Swap(T[] array, int i, int j)
        {
            if (i == j) return;
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            _swaps++;
        }
    }
}

class Node
{
    public int id;
    public string name;
    public Node(int id, string name) {
        this.id = id;
        this.name = name;
    }
}

class Program
{
    static void Main(string[] args)
    {
        GenericComparator<int> a = new GenericComparator<int>();
        try
        {
            Console.WriteLine(a.Compare(0, 1, "Value"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        GenericComparator<string> b = new GenericComparator<string>();
        try
        {
            Console.WriteLine(b.Compare("01.10.2025", "02.10.2023", "Year"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        GenericComparator<string> c = new GenericComparator<string>();
        try
        {
            Console.WriteLine(c.Compare("asd", "asdgds", "Length"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        try
        {
            Node firts = new Node(1, "ads");
            Node sec = new Node(2, "asasddasd");
            Node th = new Node(3, "asasd");

            ChainBuilder<Node> builder = new ChainBuilder<Node>(true);
            builder.AddNext(firts);
            builder.AddNext(sec);
            builder.AddNext(th);
            //builder.AddNext(firts);

            Console.WriteLine(builder.GetNext(th).name);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }



        var sorter = new Sorter<int>();

        int[] original = { 5, 2, 9, 1, 5, 6, 3, 8, 4, 7 };
        var copy = (int[])original.Clone();
        var copy2 = (int[])original.Clone();


        sorter.Sort(copy, SortAlgorithm.Selection);
        sorter.Sort(copy2, SortAlgorithm.Quick);

        foreach (int i in copy2)
        {
            Console.Write($"{i}, ");
        }
        Console.WriteLine();

        // Строки
        var strSorter = new Sorter<string>();
        string[] words = { "banana", "apple", "cherry", "date" };
        strSorter.Sort(words, SortAlgorithm.Quick);
        
       

    }
}