//dop1
using System.Runtime.CompilerServices;

class Logger
{
    static int InstanceCount = 0;

    public Logger()
    {
        InstanceCount++;
    }
    
    public static int GetInstanceCount()
    {
        return InstanceCount;
    }


}
//dop2
class Order
{
    int orderID;
    string customerName;

    public static int amount = 0;
    
    public Order() : this(0, "Неизвестно") { }

    public Order(string Name) : this(0, Name) { }

    public Order(int OrderID, string CustomerName)
    {
        this.orderID = OrderID;
        this.customerName = CustomerName;
        amount++;
    }

    public void PrintInf()
    {
        Console.WriteLine(orderID);
        Console.WriteLine(customerName);
        Console.WriteLine();
    }
}
//dop3
class Document
{
    int id;
    string title;

    private static int nextId = 0;

    public Document(string title)
    {
        this.id= nextId++;
        this.title = title;
    }
    
    public static void ResetCounter()
    {
        nextId = 0;
    }

    public void PrintInf()
    {
        Console.WriteLine(this.id);
        Console.WriteLine(this.title);
        Console.WriteLine();
    }

}


class Program
{
    static void Main(string[] args)
    {
        Logger a1 = new Logger();
        Console.WriteLine(Logger.GetInstanceCount());
        Logger a2 = new Logger();
        Console.WriteLine(Logger.GetInstanceCount());
        Logger a3 = new Logger();
        Console.WriteLine(Logger.GetInstanceCount());
        Logger a4 = new Logger();
        Console.WriteLine(Logger.GetInstanceCount());




        Order order = new Order("Hala");
        order.PrintInf();
        Order order1 = new Order(1, "ha");
        order1.PrintInf();
        Order order2 = new Order();
        order2.PrintInf();
        Order order3 = new Order();
        order3.PrintInf();
        Console.WriteLine(Order.amount);





        Document doc1 = new Document("АИСД");
        doc1.PrintInf();
        Document doc2 = new Document("КПО");
        doc2.PrintInf();
        Document doc3 = new Document("ООПиСП");
        doc3.PrintInf();
        Document.ResetCounter();
        Document doc4 = new Document("КСиС");
        doc4.PrintInf();
    }



}