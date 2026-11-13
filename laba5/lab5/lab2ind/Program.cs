//Создайте обобщенный интерфейс IMatrix<T> с операциями: Set(int row, int col, T value), Get(int row, int col), GetRows(), GetCols().
//2.Реализуйте обобщенный класс Matrix<T>
//где T : struct, IComparable<T>, который наследует интерфейс IMatrix<T>.
//Внутри используйте двумерный массив фиксированного размера.
//Реализуйте методы интерфейса с обработкой исключений (выход за границы матрицы) и блоком finally для логирования.
//Добавьте метод FindMax() для поиска максимального элемента. Проверьте работу с типами int, float, double.
 
using System.Collections.Generic;

class OutOfRange : Exception
{
    public OutOfRange(string message) : base(message) { }
}

public interface IMatrix<T>
{
 
    void Set(int row, int col, T value);
    T Get(int row, int col);
    int GetRows();
    int GetCols();
}

public class Realisation<T> : IMatrix<T>
{
    public T[,] matrix;
    private int Rows;
    private int Cols;
    public Realisation(int rows, int cols)
    {
        this.Rows = rows;
        this.Cols = cols;
        this.matrix = new T[rows, cols];
        
    }
    public void Set(int row, int col, T value)
    {
        if (this.Rows < row || this.Cols < col || Rows < 0 || Cols < 0)
        {
            OutOfRange ex = new OutOfRange("Выход за границу массива");
            throw ex;
        }
        
        matrix[row, col] = value;
    }
    public T Get(int row, int col) {
        if (this.Rows < row || this.Cols < col || Rows < 0 || Cols < 0)
        {
            OutOfRange ex = new OutOfRange("Выход за границу массива");
            throw ex;
        }
        return matrix[row, col];
    }
    public int GetRows() { return this.Rows; }
    public int GetCols() { return this.Cols; }
}
class Program
{
    static void Main(string[] args)
    {
        Realisation<int> a = new Realisation<int>(4, 4);
        try
        {
            
            a.Set(1, 5, 3);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Матрица:"); 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(a.Get(i, j) + " ");
                }
                Console.WriteLine();
                
            }
        }

        Console.WriteLine();
        Console.WriteLine();

        Realisation<float> b = new Realisation<float>(4, 4);
        try
        {
            b.Set(1, 3, 3.0f);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Матрица:");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(b.Get(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}