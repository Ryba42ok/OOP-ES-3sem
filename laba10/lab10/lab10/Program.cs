using System;
using System.Collections.Generic;
using System.Linq;

// Класс студента
public class Student
{
    public string Name { get; set; }          // Имя студента
    public string Faculty { get; set; }       // Название факультета
    public List<int> Grades { get; set; }     // Список оценок студента

    // Свойство для вычисления среднего балла
    // Использует LINQ метод Average() для подсчета среднего арифметического всех оценок
    public double AverageGrade => Grades.Average();
}

class Program
{
    static void Main()
    {
        // Создание источника данных (коллекция студентов)
        // Инициализируем список объектов Student с конкретными значениями
        List<Student> students = new List<Student>
        {
            new Student { Name = "Иван Петров", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5, 4 } },
            new Student { Name = "Анна Смирнова", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3, 5 } },
            new Student { Name = "Петр Сидоров", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5, 5 } },
            new Student { Name = "Мария Иванова", Faculty = "ФИТ", Grades = new List<int> { 4, 4, 4, 4 } },
            new Student { Name = "Ольга Кузнецова", Faculty = "Экономика", Grades = new List<int> { 3, 3, 4, 3 } }
        };

        // Параметры запроса - задаем условия фильтрации
        string targetFaculty = "ФИТ";          // Ищем студентов только этого факультета
        double minAverage = 4.0;                // Минимальный средний балл для отбора

        // ========== 1. Синтаксис запросов (Query Syntax) ==========
        // Этот синтаксис похож на SQL и более декларативен
        var querySyntaxResult = from student in students           // Источник данных
                                where student.Faculty == targetFaculty &&    // Фильтрация по факультету
                                      student.AverageGrade >= minAverage     // И по среднему баллу
                                orderby student.Name                // Сортировка по имени (по возрастанию)
                                select new                          // Проекция - создаем анонимный тип
                                {
                                    FullName = student.Name,        // Новое свойство - полное имя
                                    Avg = student.AverageGrade      // Новое свойство - средний балл
                                };

        // Вывод результатов первого запроса
        Console.WriteLine("Результат (синтаксис запросов):");
        foreach (var item in querySyntaxResult)    // Здесь происходит реальное выполнение запроса
        {
            // Форматированный вывод с двумя знаками после запятой
            Console.WriteLine($"  {item.FullName} — {item.Avg:F2}");
        }

        Console.WriteLine();  // Пустая строка для разделения результатов

        // ========== 2. Синтаксис методов (Method Syntax) ==========
        // Этот синтаксис использует методы расширения и лямбда-выражения
        var methodSyntaxResult = students
            .Where(s => s.Faculty == targetFaculty &&              // Фильтрация - лямбда-выражение
                   s.AverageGrade >= minAverage)
            .OrderBy(s => s.Name)                                   // Сортировка по имени
            .Select(s => new                                        // Проекция в анонимный тип
            {
                FullName = s.Name,
                Avg = s.AverageGrade
            });

        // Вывод результатов второго запроса
        Console.WriteLine("Результат (синтаксис методов):");
        foreach (var item in methodSyntaxResult)     // Здесь происходит реальное выполнение запроса
        {
            Console.WriteLine($"  {item.FullName} — {item.Avg:F2}");
        }

    }
}
