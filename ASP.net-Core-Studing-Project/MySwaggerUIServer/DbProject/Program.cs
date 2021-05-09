using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var db = new DbForPracticeProjectContext();
            var users = await db.Users.Where(c => c.Age > 80).ToArrayAsync();
            var messages = await db.Messages.Where(c => c.UserId == 6).ToArrayAsync();

            foreach(var user in users)
            {
                Console.WriteLine($"{user.Name} is {user.Age}");
            }

            Console.WriteLine("=============================================================");


            foreach (var message in messages)
            {
                Console.WriteLine($"{message.Head} is {message.Date}");
            }

        }
    }
}
