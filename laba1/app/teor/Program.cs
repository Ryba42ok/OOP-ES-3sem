namespace mynamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // TODO: Add code to start application here
            Console.WriteLine("Введите ваше имя");
            string str = Console.ReadLine();
            Console.WriteLine("Привет " + str + "!!!");
            Console.WriteLine("Введите  один символ с клавитуры");
            int kod = Console.Read();
            char sim = (char)kod;
            Console.WriteLine("Код символа  " + sim + " = " + kod);

            Console.WriteLine("Код символа  {0} = {1}", sim, kod);

            int s1 = 255;
            int s2 = 32;
            Console.WriteLine(" \n{0,5}\n+{1,4}\n-----\n{2,5}", s1, s2, s1 + s2);
            Console.WriteLine(" \n{1,5}\n+{0,4}\n-----\n{2,5X}", s1, s2, s1 + s2);


        }
    }
}