//Создайте класс Student с полями Name, Group, Grades (List<int>).
//Используйте Dictionary<string, List<Student>> для группировки студентов по группам. Реализуйте методы:
//•	Добавление студента
//•	Удаление студента
//•	Поиск студентов по группе
//•	Вычисление среднего балла для каждого студента
//•	Сортировка студентов в группе по среднему баллу
//•	Вывод статистики по каждой группе (количество студентов, средний балл группы)

class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public List<int> Grades { get; set; }

    public Student(string  name, string group, List<int> grades)
    {
        Name = name;
        Group = group;
        Grades = grades;
    }

    public float calculateAverege()
    {
        float res = 0f;
        foreach (int gr in Grades)
        {
            res += gr;
        }
        return res / Grades.Count;
    }
}

class StudentList
{
    public Dictionary<string, List<Student>> Dict { get; set; }
    public StudentList()
    {
        Dict = new Dictionary<string, List<Student>>();
    }

    public void Add(Student student)
    {
        if (!Dict.ContainsKey(student.Group)) 
            Dict[student.Group] = new List<Student>();

        Dict[student.Group].Add(student);
    }
    public void Remove(Student student)
    {
        if (Dict.ContainsKey(student.Group))
        {
            Dict[student.Group].Remove(student);
        }
    }
    public void searchByGroup(string group)
    {
        if (Dict.ContainsKey(group))
        {

            foreach (Student student in Dict[group])
            {
                Console.WriteLine(student.Name);
                
            }
            return;
        }
        Console.WriteLine("Группа не найдена");
    }
    public float calculateAverege(Student student)
    {
        float res = 0f;
        foreach(int gr in student.Grades)
        {
            res += gr;
        }
        return res / student.Grades.Count;
    }
    public void sortByAveregeInGroup(string group)
    {
        if (Dict.ContainsKey(group))
        {
            Dict[group].Sort((a, b) => b.calculateAverege().CompareTo(a.calculateAverege()));
            return;
        }
        Console.WriteLine("Группа не найдена");
    }

    
}

class TextAnalyser
{
    public List<string> text { get; set; }
    public Dictionary<string, int> WordFreq { get; set; }
    public HashSet<string> Uniq { get; set; }
    public SortedDictionary<string, int> SortDict { get; set; }
    public Queue<string> Queue { get; set; }

    public TextAnalyser(List<string> text)
    {
        this.text = text;
        WordFreq = new Dictionary<string, int>();
        Uniq = new HashSet<string>();
        Queue = new Queue<string>();
        SortDict = new SortedDictionary<string, int>();
    }

    public void Analyse()
    {
        foreach (string s in text)
        {
            
            //Частота
            if (!WordFreq.ContainsKey(s.ToLower()))
                WordFreq[s.ToLower()] = 1;
            else
                WordFreq[s.ToLower()]++;
            //уник
            Uniq.Add(s.ToLower());


            //сортировка
            if (!SortDict.ContainsKey(s.ToLower()))
                SortDict[s.ToLower()] = 1;
            else
                SortDict[s.ToLower()]++;

            
            
        }
        var newSort = SortDict.OrderByDescending(a => a.Value).ToDictionary(a => a.Key, a=> a.Value); 
        foreach (string s in newSort.Keys)
        {
            Queue.Enqueue(s);
        }
        
    }
}

//Задача 4. Телефонная книга с поиском
//Создайте телефонную книгу, используя Dictionary<string, List<string>>, где ключ - фамилия, значение - список телефонов (у одного человека может быть несколько телефонов). Реализуйте:
//•	Добавление контакта(фамилия и телефон)
//•	Добавление дополнительного телефона к существующему контакту
//•	Удаление контакта
//•	Удаление конкретного телефона у контакта
//•	Поиск по фамилии (возвращает все телефоны)
//•	Поиск по телефону (возвращает фамилию владельца) - для этого создайте обратный словарь Dictionary<string, string>

class Telephone
{
    public Dictionary<string, List<string>> Dict;
    public Telephone()
    {
        Dict = new Dictionary<string, List<string>>();
    }
    public void add(string s, string tel)
    {
        if (Dict.ContainsKey(s))
        {
            Dict[s].Add(tel);
        }
        else
        {
            Dict[s] = new List<string>();
        }
    }

    public void deleteContact(string abon)
    {
        if (Dict.ContainsKey(abon))
        {
            Dict.Remove(abon);
        }
    }

    public void deleteTeleph(string abon, string phone)
    {
        if (Dict.ContainsKey(abon))
        {
            Dict[abon].Remove(phone);
        }
    }
    public void searchByKey(string key)
    {
        if (Dict.ContainsKey(key))
        {
            Console.Write(key + ": ");
            foreach (string phone in Dict[key])
            {
                Console.Write(phone + ", ");
            }
            Console.WriteLine();
        }
    }

    public string searchByPhone(string phone)
    {
        foreach (string key in Dict.Keys)
        {
            foreach(var list in Dict.Values)
            {
                foreach(string ph in list)
                {
                    if (ph == phone)
                    {
                        return key;
                    }
                }
            }
        }
        return null;
    }
}
class Programm
{
    static void Main(string[] args)
    {
        List<int> st1Gr = new List<int> { 1, 2, 3, 4, 5 };
        List<int> st2Gr = new List<int> { 1, 2, 3, 4, 10 };
        List<int> st3Gr = new List<int> { 1, 2, 3, 4, 9 };
        List<int> st4Gr = new List<int> { 1, 2, 3, 4, 8 };

        Student st1 = new Student("Vova", "FIT", st1Gr);
        Student st2 = new Student("Vova2", "FIT", st2Gr);
        Student st3 = new Student("Igor", "FIT", st4Gr);
        Student st4 = new Student("Ivan", "TOV", st3Gr);

        StudentList stList = new StudentList();
        stList.Add(st1);
        stList.Add(st2);
        stList.Add(st3);
        stList.Add(st4);

        stList.sortByAveregeInGroup("FIT");

        foreach (List<Student> group in stList.Dict.Values)
        {
            
            foreach (Student student in group)
            {
                Console.WriteLine(student.Name + " " + student.Group);
            }
        }
        stList.searchByGroup("FIT");




        Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
        List<string> sentence = [
    "Код", "должен", "быть", "чистым", "чистым",
    "и", "эффективным", "потому", "что", "этот",
    "код", "будут", "читать", "люди", "люди", "и", "машины"
];


        TextAnalyser analyse = new TextAnalyser(sentence);
        analyse.Analyse();

        foreach(string s in analyse.WordFreq.Keys)
        {
            Console.WriteLine("Слово " +  s + " Частота --> " + analyse.WordFreq[s]);
        }
        Console.WriteLine();
        Console.WriteLine("Уникальные слова: ");
        foreach(string un in analyse.Uniq)
        {
            Console.WriteLine(un);
        }

        Console.WriteLine("Топ 10 встречающихся слов");
        int k = 0;
        while(k != 10 && analyse.Queue.Count != 0)
        {
            Console.WriteLine(analyse.Queue.Dequeue());
            k++;
        }


        var book = new Telephone();
        book.add("Иванов", "+79161234567");
        book.add("Иванов", "+79167654321");   
        book.add("Петров", "+79031112233");
        book.add("Сидоров", "+79995556677");
    










    }
}
