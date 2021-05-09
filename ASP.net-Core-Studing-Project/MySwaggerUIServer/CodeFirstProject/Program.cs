using System;

namespace CodeFirstProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new CodeFirstDbContext();
            Console.WriteLine("Hello World!");
            db.SaveChanges();
        }
    }
}
