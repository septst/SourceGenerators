namespace SourceGeneratorConsole;

partial class Program
{
    static void Main(string[] args)
    {
        HelloFrom("Generated Code");
        Console.WriteLine("Hello from Console");
        Console.ReadKey();
    }

    static partial void HelloFrom(string name);
}