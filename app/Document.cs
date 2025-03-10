using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 

namespace app
{
    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details {  get; set; }    

        public DateTime CreatedDate { get; set; }



    }
    public class DocumentData : DbContext
    {
        public DocumentData() : base("MyDocumentData") { }

        public DbSet<Document> Documents { get; set; } 
    }

}
