//Используя стандартные делегаты Action и Func, создайте методы для обработки списка чисел: подсчет суммы, подсчет произведения, вывод на экран.
//2.Создайте класс TemperatureSensor с событием TemperatureChanged.
//Генерируйте событие при изменении температуры.6
//Создайте классы-подписчики Heater и Cooler, реагирующие на изменение.

using System.Runtime.CompilerServices;

delegate void Changed(ChangeTemp e);
class ChangeTemp : EventArgs
{
    private int temp;
    private int primTemp;

    public int Temp {  get { return temp; } set { temp = value; } }
    public int PrimTemp { get { return primTemp; } }

    public ChangeTemp(int temp)
    { 
        this.Temp = temp; 
    }
}

class Sensor
{
    public event Changed _act;
    public void onChange(int temp)
    {
        _act?.Invoke(new ChangeTemp(temp));
    }
}

class Cooler
{
    public void onCooler(ChangeTemp arg)
    {
        if (arg.Temp < arg.PrimTemp)
            Console.WriteLine("Кулер выключен");
        else
            Console.WriteLine("Кулер включён");
    }
}

class Heater
{
    public void onHeater(ChangeTemp arg)
    {
        if (arg.Temp < arg.PrimTemp)
            Console.WriteLine("Обогреватель выключен");
        else
            Console.WriteLine("Обогреватель включён");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<int> vals = new List<int>() {1, 2, 3, 4, 5, 6 };
        Action<List<int>> sum = (vals) =>
        {
            int sum = 0;
            foreach (int elem in vals)
            {
                sum += elem;
            }
            Console.WriteLine(sum);
        };

        Func<List<int>, int> mul = (vals) =>
        {
            int pr = 1;
            foreach (int elem in vals)
            {
                pr *= elem;
            }
            return pr;
        };

        sum(vals);
        Console.WriteLine(mul(vals));


        Sensor sens = new Sensor();
        Cooler cooler = new Cooler();
        Heater heater = new Heater();

        sens._act += cooler.onCooler;
        sens._act += heater.onHeater;

        sens.onChange(26);
    }



}