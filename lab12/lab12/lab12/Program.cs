using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Работа с каталогами\n");

        string rootDir = "TestRoot";

        // Создаем корневой каталог
        if (!Directory.Exists(rootDir))
        {
            Directory.CreateDirectory(rootDir);
            Console.WriteLine($"Создан каталог: {rootDir}");
        }

        // Создаем подкаталоги
        string subDir1 = Path.Combine(rootDir, "SubDir1");
        string subDir2 = Path.Combine(rootDir, "SubDir2");

        Directory.CreateDirectory(subDir1);
        Directory.CreateDirectory(subDir2);
        Console.WriteLine($"Созданы подкаталоги: SubDir1, SubDir2");

        // Создаем файлы в каталогах
        File.WriteAllText(Path.Combine(rootDir, "root.txt"), "Файл в корне");
        File.WriteAllText(Path.Combine(subDir1, "file1.txt"), "Файл в SubDir1");
        File.WriteAllText(Path.Combine(subDir1, "file2.txt"), "Еще один файл в SubDir1");
        File.WriteAllText(Path.Combine(subDir2, "file3.txt"), "Файл в SubDir2");

        Console.WriteLine("Созданы тестовые файлы.");

        // Используем DirectoryInfo для получения информации о каталоге
        DirectoryInfo dirInfo = new DirectoryInfo(rootDir);
        Console.WriteLine($"\nСодержимое каталога {rootDir}:");

        // Получаем подкаталоги
        DirectoryInfo[] subDirs = dirInfo.GetDirectories();
        Console.WriteLine("Подкаталоги:");
        foreach (DirectoryInfo dir in subDirs)
        {
            Console.WriteLine($"  {dir.Name} (создан: {dir.CreationTime})");
        }

        // Получаем файлы
        FileInfo[] files = dirInfo.GetFiles();
        Console.WriteLine("Файлы:");
        foreach (FileInfo file in files)
        {
            Console.WriteLine($"  {file.Name} - {file.Length} байт");
        }

        // Рекурсивно получаем все файлы
        Console.WriteLine("\nВсе файлы рекурсивно:");
        string[] allFiles = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
        foreach (string file in allFiles)
        {
            Console.WriteLine($"  {file}");
        }

        // Удаление каталога (рекурсивное)
        Console.WriteLine($"\nУдаление каталога {rootDir}...");
        //Directory.Delete(rootDir, true); // true - рекурсивное удаление
        Console.WriteLine($"Каталог {rootDir} удален.");
    }
}
