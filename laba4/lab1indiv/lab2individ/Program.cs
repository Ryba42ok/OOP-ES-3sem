//1.Создайте структуру TimePeriod с полями: часы, минуты, секунды.
//Реализуйте методы: вывода времени в формате "чч:мм:сс",
//сравнения двух временных периодов, добавления минут. Продемонстрируйте работу.
//2. Создайте перечисление ChessPiece
//(Pawn, Knight, Bishop, Rook, Queen, King)
//с указанием стоимости фигуры (пешка=1, конь = 3, слон = 3, ладья = 5, ферзь = 9, король = 0).
//Напишите метод, который принимает массив фигур и возвращает общую стоимость.


struct TimePeriod
{
    public int hours;
    public int minutes;
    public int seconds;

    public TimePeriod(int hours, int minutes, int seconds)
    {
        if (hours >= 24 || minutes >= 60 || seconds >= 60)
        {
            Console.WriteLine("Некорректные данные");
            return;
        }
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public void DisplayInfo()
    {
        if (hours < 10)
            Console.Write("0" + hours + ":");
        else
            Console.Write(hours + ":");
        if (minutes < 10)
            Console.Write("0" + minutes + ":");
        else
            Console.Write(minutes + ":");
        if (seconds < 10)
            Console.WriteLine("0" + seconds);
        else
            Console.WriteLine(seconds);
    }

    public int Compare(TimePeriod other)
    {
        if (hours < other.hours)
            return -1;
        else if (hours > other.hours)
            return 1;
        if (minutes <  other.minutes)
            return -1;
        else if (minutes > other.minutes)
            return 1;
        if (seconds < other.seconds)
            return -1;
        else if (seconds > other.seconds)
            return 1;

        return 0;
    }

    public void addMinutes(int minutes)
    {
        

        if(this.minutes + minutes >= 60)   //23 50 + 30 // 00 20
        {
            int deltaH = minutes / 60;
            if (this.hours + deltaH >= 24) {
                int deltaH2 = 24 - this.hours;
                this.hours = 0;
                this.hours += deltaH - deltaH2;
            }
            int deltaMin = Math.Abs((this.minutes + minutes)%60);
            this.minutes = deltaMin;
        }
        else
        {
            this.minutes += minutes;
        }
    }
}
 

public enum ChessPiece
{
    Pawn = 1,
    Knight = 3,
    Bishop = 3,
    Rook = 5,
    Queen = 9,
    King = 0
}
class ind2
{
    public int commonPrice(ChessPiece[] array)
    {
        int res = 0;
        foreach (ChessPiece piece in array)
        {
            res += (int)piece;
        }
        return res;
    }
}


class Program
{
    static void Main(string[] args)
    {
        TimePeriod time = new TimePeriod(21, 1, 1);
        time.addMinutes(571);
        time.DisplayInfo();

        TimePeriod time2 = new TimePeriod(22, 1, 1);

        Console.WriteLine(time.Compare(time2));



        ChessPiece[] arr = { ChessPiece.Pawn, ChessPiece.King, ChessPiece.Queen };
        ind2 i = new ind2();
        Console.WriteLine(i.commonPrice(arr));
    }


}