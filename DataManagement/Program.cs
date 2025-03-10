using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using app;

namespace DataManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DocumentData db = new DocumentData();

            using (db)
            {
                Document d1 = new Document() {Id=1, Title="First Document", CreatedDate=new DateTime(2025,03,10), Details="This is a sample of my first document created" };
                Document d2 = new Document() { Id = 2, Title = "Second Document", CreatedDate = new DateTime(2025, 03, 10), Details = "This is a sample of my second document created" };
                Document d3 = new Document() { Id = 3, Title = "thrid Document", CreatedDate = new DateTime(2025, 03, 10), Details = "This is a sample of thrid document created" };
                
                db.Documents.Add(d1);
                db.Documents.Add(d2);
                db.Documents.Add(d3);

                Console.WriteLine("documents addeed to database");

                db.SaveChanges();
                Console.WriteLine("saved to databases");
            }
        }
    }
}
