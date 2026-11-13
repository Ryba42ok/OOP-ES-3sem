//1. Создайте класс LambdaCalculator, который хранит словарь операций.
//Ключ — символ операции (char), значение — лямбда-выражение типа Func<double, double, double>.
//Реализуйте методы RegisterOperation для добавления новой операции и Calculate для вычисления выражения вида "5 + 3".
//Добавьте возможность работы с унарными операциями (например, "sin 30", "sqrt 16").
//Придумайте, как обрабатывать приоритет операций и скобки.

using System;
using System.Data;

class LambdaCalculator
{
    Dictionary<char, Func<double, double, double>> dictionary;
    public void RegisterOperation()
    {
        
    }
    public void Calculate()
    {

    }
}

//2. Разработайте класс PredicateComposer<T>, который позволяет комбинировать предикаты с помощью методов And, Or, Not.
//Каждый метод должен принимать лямбда-выражение и возвращать новый композер.
//Реализуйте метод Build, возвращающий итоговый Predicate<T>.
//Пример использования: composer.And(x => x > 5).Or(x => x < 0).Not().Build(). 

class PredicateComposer<T>
{
    public Predicate<T> cur;

    public T value;
    public PredicateComposer(Predicate<T> predicate = null)
    {
        cur = predicate ?? (x => true);
    }

    public PredicateComposer<T> And(Predicate<T> other) {
        return new PredicateComposer<T>(x =>  cur(x) && other(x));
    }

    public PredicateComposer<T> Or(Predicate<T> other)
    {
        return new PredicateComposer<T>(x => cur(x) || other(x));
    }
    public PredicateComposer<T> Not()
    {
        return new PredicateComposer<T>(x => !cur(x));
    }

    public Predicate<T> build()
    {
        return cur;
    }

}

class TextAnalyzer
{
    public List<string> Text;
    Dictionary<string, Predicate<string>> rules;
    public TextAnalyzer()
    {
        Text = new List<string>();
        rules = new Dictionary<string, Predicate<string>>();
    }
    public void AddWord(string word) {
        Text.Add(word);
    }

    public bool AddRule(string name, Predicate<string> rule)
    {
        if (!string.IsNullOrEmpty(name))
        {
            rules[name] = rule;
            return true;
        }
        return false;
    }
    public int CountSatisfying()
    {
        int res = 0;
        foreach (string name in Text) {
            foreach (var rule in rules.Values)
            {
                if (rule(name) == true)
                    res++;
            }
        }
        return res;
    }
    public string FindFirstSatisfying(string ruleName)
    {
        if (!string.IsNullOrWhiteSpace(ruleName))
        {
            var rule = rules[ruleName];
            foreach (string name in Text)
            {
                if (rule(name) == true)
                {
                    return name;
                }
            }
        }
        return null;
    }

}

//4. Создайте класс DataValidator<T>, который использует лямбда-выражения для проверки объектов на соответствие набору правил.
//Реализуйте следующие методы:
//•	AddRule(string ruleName, Predicate < T > rule, string errorMessage) — добавляет правило валидации. Параметр rule — это лямбда-выражение, которое принимает объект и возвращает true, если правило выполняется. Например: AddRule("Возраст > 0", p => p.Age > 0, "Возраст должен быть положительным").
//•	Validate(T item) — применяет все добавленные правила к объекту и возвращает true, если объект удовлетворяет всем правилам, иначе false.
//•	GetErrors(T item) — применяет все правила к объекту и возвращает массив строк с сообщениями об ошибках только для тех правил, которые не выполнились.
//•	ValidateAll(T[] items) — проверяет массив объектов и возвращает статистику по каждому правилу: сколько объектов его нарушили и список нарушителей.
//•	GetRuleStatistics() — возвращает информацию о всех правилах: имя, количество проверок, количество нарушений.
//•	RemoveRule(string ruleName) — удаляет правило по имени.
//Все проверки должны выполняться с использованием лямбда-выражений, переданных в AddRule. Продемонстрируйте работу на классе Person с правилами: возраст от 18 до 65, имя не пустое и длиннее 2 символов, email содержит '@' и '.', зарплата в диапазоне от МРОТ до 500000.

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }

    public Person(string name, int age, string email, int salary)
    {
        Name = name;
        Age = age;
        Email = email;
        Salary = salary;
    }

    
}
class DataValdator<T> where T : class
{
    
    public Dictionary<string, KeyValuePair<Predicate<T>, string>> Rules;
    public DataValdator() {
       
        Rules = new Dictionary<string, KeyValuePair<Predicate<T>, string>>();
    }
    public void addRule(string name, Predicate<T> rule, string errorMesssage)
    {
        Rules[name] = new KeyValuePair<Predicate<T>, string>(rule, errorMesssage);
    }
    public bool Validate(T item)
    {
       
        foreach(var rule in Rules.Values)
        {
            if (rule.Key(item) == false)
            {
                return false;
            }
        }
        
        return true;
    }
    public void GetErrors(T item)
    {
        foreach(var rule in Rules.Values)
        {
            if (rule.Key(item) == false)
            {
                Console.WriteLine("Ошибка: " + item + " "+ rule.Value);
            }
        }
    }
    public void ValidateAll(List<T> values)
    {
        foreach (var val in values)
        {
            if(!Validate(val))
            {
                GetErrors(val);
            }
        }
    }

    public void GetRuleStatistics()
    {
        if (Rules.Count == 0)
        {
            Console.WriteLine("\nRule list empty");
            return;
        }
        Console.WriteLine("\nRule List");
        foreach(var rule in Rules)
        {
            Console.WriteLine("Правило: " + rule.Key + " Ошибка " + rule.Value.Value);
            
        }
    }
    public void RemoveRule(string ruleName)
    {
        
        foreach( var rule in Rules.Keys)
        {
            if (rule ==  ruleName)
            {
                Rules.Remove(ruleName);
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        PredicateComposer<int> composer = new PredicateComposer<int>();
        Predicate<int> comp = composer.And(x => x > 5).Or(x => x < 0).Not().build();
        Console.WriteLine(comp(-1));

        TextAnalyzer analiser = new TextAnalyzer();

        analiser.AddWord("adimir");
        analiser.AddWord("Ivan");
        analiser.AddWord("Cauldron");
        analiser.AddWord("sddassadsadasddas");
        analiser.AddWord("World");
        analiser.AddWord("Hello");


        analiser.AddRule("More then 10", x => x.Length > 10);
        analiser.AddRule("starts with 'a'", x => x[0] == 'a');
        Console.WriteLine(analiser.CountSatisfying());
        Console.WriteLine(analiser.FindFirstSatisfying("More then 10"));
        Console.WriteLine(analiser.FindFirstSatisfying("starts with 'a'"));

        Console.WriteLine("\n\n\n");
        List<Person> Empl = new List<Person>();
        Person person = new Person("Vova", 19, "vovagorevoy@gmail.com", 50000);

        Person person1 = new Person("", -2, "Igor@gmail.com", 10000);

        Person person2 = new Person("Ivan", 19, "Ivan@gmail.com", 50000);
        Empl.Add(person);
        Empl.Add(person1);
        Empl.Add(person2);



        DataValdator<Person> dataValdator = new DataValdator<Person>();
        dataValdator.addRule("Возраст > 0", x => x.Age > 0, "Возраст должен быть больше 0");
        dataValdator.addRule("Пустое имя", x => !string.IsNullOrWhiteSpace(x.Name), "Имя не должно быть пустым");

        Console.WriteLine(dataValdator.Validate(person) + "\n");



        //dataValdator.GetErrors(person);

        dataValdator.ValidateAll(Empl);

        dataValdator.RemoveRule("Возраст > 0");
        dataValdator.GetRuleStatistics();
    }


}