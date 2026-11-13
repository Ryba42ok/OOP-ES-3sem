//1.Создайте интерфейс IPlayable с методами Play() и Pause().
//Реализуйте его в классах MusicPlayer и VideoPlayer. Продемонстрируйте работу.
//2. Добавьте явную реализацию интерфейса для случая,
//когда оба класса имеют метод Stop(), но с разным поведением.

interface IPlayable
{
    void Play();
    void Pause();
}

interface IStop
{
    void Stop();
}

class MusicPlayer : IPlayable, IStop
{
    public void Play()
    {
        Console.WriteLine("MusicPlayer запущен");
    }
    public void Pause()
    {
        Console.WriteLine("MusicPlayer приостановлен");
    }

    void IStop.Stop()
    {
        Console.WriteLine("MusicPlayer остановлен");
    }
}

class VideoPlayer : IPlayable, IStop
{
    public void Play()
    {
        Console.WriteLine("VideoPlayer запущен");
    }
    public void Pause()
    {
        Console.WriteLine("VideoPlayer приостановлен");
    }

    void IStop.Stop()
    {
        Console.WriteLine("VideoPlayer остановлен");
    }
}

class Program
{
    static void Main(string[] args)
    {
        MusicPlayer m1 = new MusicPlayer();
        m1.Play();
        m1.Pause();
        IStop stop = m1;
        stop.Stop();
        
        VideoPlayer v1 = new VideoPlayer();
        v1.Play();
        v1.Pause();
        stop = v1;
        stop.Stop();
    }
}