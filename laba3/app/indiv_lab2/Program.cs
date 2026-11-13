//Создайте абстрактный класс Dish (блюдо) с полями название, калорийность, время приготовления.
//Добавьте абстрактный метод Cook() (приготовить).
//Реализуйте классы Soup, Salad и Dessert.
//В каждом классе добавьте специфичные ингредиенты и методы.
//Используйте свойства с проверками для полей.

abstract class Dish
{
    protected string Name { get; set; }
    protected int calories { get; set; }
    protected double time { get; set; }
    public Dish(string Name, int calories, double time)
    {
        this.Name = Name;
        this.calories = calories;
        this.time = time;
    }
    public abstract bool cook();
}
class Soup :  Dish
{
    public string SpecIngr {  get; set; }

    public Soup (string Name, int calories, double time, string SpecIngr) : base(Name, calories, time) 
    {  
        this.SpecIngr = SpecIngr;
    }
    public override bool cook()
    {
        return true;
    }
}
class Salad : Dish
{
    public string SpecIngr { get; set; }

    public Salad(string Name, int calories, double time, string SpecIngr) : base(Name, calories, time)
    {
        this.SpecIngr = SpecIngr;
    }
    public override bool cook()
    {
        return true;
    }
    public void mixIngregients() { }
}

class Dessert : Dish
{
    public string SpecIngr { get; set; }

    public Dessert(string Name, int calories, double time, string SpecIngr) : base(Name, calories, time)
    {
        this.SpecIngr = SpecIngr;
    }
    public override bool cook()
    {
        return true;
    }
    public void EatWithTea() { }
}

class program
{
    static void Main(string[] args)
    {
        Dish[] dishes = new Dish[]
        {
            new Soup("Фобо", 1200, 3, "Капуста"),
            new Salad("Цезарь", 500, 0.5, "Сухари"),
            new Soup("Торт", 1200, 5, "Клубника")
        };
        dishes[0].cook();

        Salad s = new Salad("Фобо1", 1200, 3, "Капуста");
        s.mixIngregients();
    }
}