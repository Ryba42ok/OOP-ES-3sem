//Создайте файл с числами от 1 до 20.
//Прочитайте файл и создайте два новых файла: с четными числами и с нечетными числами.
//2. Напишите программу, которая подсчитывает количество строк в текстовом файле.


using System.IO;

class Programm
{
    static void Main(string[] args)
    {
        string fileName = "indiv.txt";
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не найден (Individ.txt)");
        }
        string[] str = File.ReadAllLines(fileName);
        
        string[] corStr = str[0].Split(' ');
        var evenNumb = from elem in corStr
                       let el = int.Parse(elem) 
                       where el % 2 == 0
                       select elem;

        var oddNumb = from elem in corStr
                      let el = int.Parse(elem)
                       where el % 2 == 1
                       select elem;

        File.Delete("first.txt");
        File.Delete("second.txt");

        File.AppendAllLines("first.txt", evenNumb);
        File.AppendAllLines("second.txt", oddNumb);

        Console.WriteLine("число строк файла: " + str.Length);



    }
}