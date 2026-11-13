//. Создайте атрибут TrackChangesAttribute, который помечает свойства объекта, за изменениями которых нужно следить.
//Реализуйте класс ChangeTracker, который принимает объект,
//анализирует его свойства, помеченные этим атрибутом, и сохраняет их начальные значения.
//Добавьте метод GetChanges, который возвращает список измененных свойств с указанием старого и нового значения.
//Также реализуйте метод AcceptChanges, который обновляет сохраненные значения до текущих, считая их новой точкой отсчета.
//Продемонстрируйте работу на примере класса Employee с полями имени, должности и оклада.

using System.Reflection;
using System.Text;

[AttributeUsage(AttributeTargets.Property)]
class TrackChangesAttribute : Attribute
{
    public string Message { get; }
    public TrackChangesAttribute(string mess)
    {

        Message = mess;
    }
}

class ChangeTracker
{
    public Dictionary<string, object> dict;
    public Employee Obj { get; set; }

    public ChangeTracker(Employee obj)
    {
        dict = new Dictionary<string, object>();
        Obj = obj;


        Type type = obj.GetType();
        foreach (PropertyInfo property in type.GetProperties())
        {
            if (property.CanRead)
            {
                dict.Add(property.Name, property.GetValue(obj));
            }
        }
    }

    public void GetChanges()
    {
        Type type = Obj.GetType();

        var tempObj = Convert.ChangeType(Obj, type);

        PropertyInfo[] propInf = type.GetProperties();
        foreach (var prop in propInf)
        {
            var attr = prop.GetCustomAttribute<TrackChangesAttribute>();
            if (attr != null) {
                if (!prop.GetValue(tempObj).Equals(dict[prop.Name]))
                {
                    Console.WriteLine($"{attr.Message} с {dict[prop.Name]} на {prop.GetValue(tempObj)}");
                }
            }
        }
        
    }

    public void changeProp(Employee employee, string prop, object to)
    {
        Type type = employee.GetType();
        foreach (var property in type.GetProperties())
        {
            if (prop == property.Name)
            {
                var _to = Convert.ChangeType(to, property.PropertyType);
                if (_to == property.GetValue(employee))
                {
                    Console.WriteLine("Такое значение свойства " + property.Name + " уже установлено");
                    break;
                }
                property.SetValue(employee, _to);
                break;
            }
        }
    }

    public void AcceptChanges()
    {
        Type type = this.Obj.GetType();
        foreach(var property in type.GetProperties())
        {
            dict[property.Name] = property.GetValue(Obj);
        }

    }

}


class Employee
{
    public string Name { get; set; }

    [TrackChanges("Сменена специальность")]
    public string Speciality { get; set; }

    [TrackChanges("Сменена зарплата")]
    public int Salary { get; set; } 


    public Employee(string name, string speciality, int salary)
    {
        Name = name;
        Speciality = speciality;
        Salary = salary;

    }
}



//2.Создайте атрибут MaskAttribute с параметрами, задающими шаблон маскировки
//(например, "показать первые 3 символа, остальное заменить звездочками") и символ для маскировки.
//Реализуйте класс DataMasker, который принимает объект и возвращает его копию,
//в которой все поля, помеченные атрибутом Mask, заменены на замаскированные строки согласно шаблону.
//Для сложных типов данных (например, email или телефон) реализуйте предопределенные шаблоны маскировки,
//которые можно задавать через именованные параметры.
//Продемонстрируйте работу на примере класса User с полями логина, email, телефона и номера кредитной карты.

[AttributeUsage(AttributeTargets.Property)]
class MaskAttribute : Attribute
{
    public string Message { get; set; }
    public string Mode { get; set; }
    public MaskAttribute(string message, string mode = "default") 
    { 
        this.Message = message; 
        this.Mode = mode;
    }
}
class User
{
    //[Mask("Зашифровано")]
    public string Login { get; set; }
    [Mask("Зашифровано")]
    public string Password { get; set; }
    [Mask("Зашифровано", "email")]
    public string Email { get; set; }
    [Mask("Зашифровано", "phone")]
    public string Phone { get; set; }
    [Mask("Зашифровано", "credit")]
    public string CreditCardNumber { get; set; }

    public User(string login, string password, string email, string phone, string creditCardNumber)
    {
        Login = login;
        Password = password;
        Email = email;
        Phone = phone;
        CreditCardNumber = creditCardNumber;
    }
    public User()
    {

    }
}

class DataMasker 
{
    public object Obj { get; set; }
    public DataMasker(object obj)
    {
        Type type = obj.GetType();
        Obj = Activator.CreateInstance(type);
        foreach (PropertyInfo prop in type.GetProperties())
        {
            var attr = prop.GetCustomAttribute<MaskAttribute>();
            if (attr != null)
            {
                var attrProp = prop.GetValue(obj);
                string conv = attrProp?.ToString();
                StringBuilder result = new StringBuilder(conv.Length);

                switch (attr.Mode)
                {
                    case "default": {
                            for (int i = 0; i < conv.Length; i++)
                            {
                                if (i > 3)
                                {
                                    result.Append('*');
                                }
                                else
                                {
                                    result.Append(conv[i]);
                                }
                            }
                            break;
                        }
                    case "email":
                        {
                            string[] strs = conv.Split("@");
                            for (int i = 0; i < strs[0].Length; i++)
                            {
                                if (i >= 3)
                                {
                                    result.Append('*');
                                }
                                else
                                {
                                    result.Append(conv[i]);
                                }
                            }
                            result.Append("@");
                            result.Append(strs[1]);


                            break;
                        }
                    case "phone":
                        {
                            string[] strs = conv.Split("-");
                            result.Append(strs[0]);
                            for (int i = 0; i < strs[1].Length; i++)
                            {
                                if (i >= 4)
                                {
                                    result.Append('*');
                                }
                                else
                                {
                                    result.Append(conv[strs[0].Length +i]);
                                }
                            }
                            
                            break;
                        }
                    case "credit":
                        {
                            for (int i = 0; i < conv.Length; i++)
                            {
                                if (i >= 3 && conv[i] != ' ')
                                {
                                    result.Append('*');
                                }

                                else
                                {
                                    result.Append(conv[i]);
                                }
                            }
                            break;
                        }

                }
                
                prop.SetValue(Obj, result.ToString());
            }
            else
            {
                prop.SetValue(Obj, prop.GetValue(obj));
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Employee emp1 = new Employee("Vova", "FIT", 50000);
        ChangeTracker ch = new ChangeTracker(emp1);

        ch.changeProp(emp1, "Speciality", "Halal");
        ch.changeProp(emp1, "Speciality", "Halal1");
        ch.AcceptChanges();

        ch.changeProp(emp1, "Salary", 50000);

        ch.GetChanges();

        //Console.WriteLine(emp1.Speciality);
        User user = new User("vova" , "vovapas", "vovagorevoy@gmail.com", "+375-447402210", "1234 5678 9123 4567");

        DataMasker dataMasker = new DataMasker(user);

        //Console.WriteLine(dataMasker.Obj);
        Type type = dataMasker.Obj.GetType();
        User ConvObj = (User)dataMasker.Obj;
        foreach (PropertyInfo pr in type.GetProperties())
        {
            Console.WriteLine(pr.Name + " = " + pr.GetValue(ConvObj));
        }
        
    }
}