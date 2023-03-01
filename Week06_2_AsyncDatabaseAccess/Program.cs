using Week06_2_AsyncDatabaseAccess.Data;
using Week06_2_AsyncDatabaseAccess.Models;

namespace Week06_2_AsyncDatabaseAccess
{
    internal class Program
    {
        private static AppDbContext db = new AppDbContext();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Sync. DB Method");
            DbSave();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.WriteLine("Starting Async. DB Method");

            var task = DbSaveAsync();

            Console.WriteLine("Some other work!!!");

            await task;

        }

        private static void DbSave()
        {
            for (int i = 0; i < 100000; i++)
            {
                Person newPerson = new Person()
                {
                    FirstName = "Sync",
                    LastName = "Doe",
                    CreatedTime = DateTime.Now
                };

                db.Persons.Add(newPerson); //Add the newly object to the queue to be inserted later
            }

            db.SaveChanges(); //New records are not saved in the database unless SaveChanges(), or SaveChangesAsync() is called.

        }

        private static async Task DbSaveAsync()
        {
            for (int i = 0; i < 100000; i++)
            {
                Person newPerson = new Person()
                {
                    FirstName = "Async",
                    LastName = "Doe",
                    CreatedTime = DateTime.Now
                };

                db.Persons.Add(newPerson);
            }

            Console.WriteLine("Before Async");
            await db.SaveChangesAsync();
            Console.WriteLine("Async Complete!");
        }

    }
}