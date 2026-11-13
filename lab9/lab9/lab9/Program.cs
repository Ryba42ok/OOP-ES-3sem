//1.Создайте Stack<int> и заполните его числами от 1 до 10.
//Извлеките 3 числа из стека и выведите их. Добавьте числа 20, 30, 40.
//Извлеките все оставшиеся числа.
//2. Создайте List<Student> с полями Name и Grade.
//Добавьте 5 студентов.
//Отсортируйте список по оценке (по убыванию).
//Выведите топ-3 студента.

using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }

    public Student(string name, int grade)
    {
        Name = name; 
        Grade = grade;
    }
}
class Programm
{
    static void Main(string[] args)
    {
        Stack<int> st = new Stack<int>();
        for (int i = 1; i <= 10; i++) { 
            st.Push(i);
        }

        for (int i = 0; i < 3; i++)
        {
            st.Pop();
        }

        for (int i = 20; i <= 40; i+= 10)
        {
            st.Push(i);
        }

        while(st.Count != 0)
        {
            st.Pop();
        }

        Student st1 = new Student("Vladimir", 4);
        Student st2 = new Student("Ivan", 5);
        Student st3 = new Student("Dmitry", 6);
        Student st4 = new Student("Kirill", 7);
        Student st5 = new Student("Maksim", 9);

        List<Student> list = new List<Student>();
        list.Add(st1);
        list.Add(st2);
        list.Add(st3);
        list.Add(st4);
        list.Add(st5);

        list.Sort((a, b) => b.Grade.CompareTo(a.Grade));

        for (int i = 0; i < 3; i++) {
            Console.WriteLine(list[i].Name + " " + list[i].Grade);
        }
    }
}
