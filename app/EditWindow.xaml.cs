using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Xml.Linq;

namespace app
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private int id;

        DocumentData db = new DocumentData();
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Document selected) : this()
        {
            
            tbxTitle.Text = selected.Title;
            tbxDetails.Text = selected.Details;

            id = selected.Id;





        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //read the data from the screen
            string title = tbxTitle.Text;

            string detials = tbxDetails.Text;




            using (db)
            {
                Document d = new Document() { Id = id, Title = title, Details = detials, CreatedDate = DateTime.Now };

                db.Documents.AddOrUpdate(d);


                db.SaveChanges();

                //update


                MainWindow main = this.Owner as MainWindow;

                main.lbxDocuments.ItemsSource = null;


                var query = from docs in db.Documents
                            select docs;

                var results = query.ToList();

                main.lbxDocuments.ItemsSource = results;
            }




            //close the window 
            this.Close();
        }

        private void btnDelete_Clickk(object sender, RoutedEventArgs e)
        {
           

            //check nut null
            if (id != 0)//ensure something is selected
            {

                

                Document docToDelete = db.Documents.Find(id);

                //delete from db
                db.Documents.Remove(docToDelete);
                db.SaveChanges();

                MainWindow main = this.Owner as MainWindow;

                main.lbxDocuments.ItemsSource = null;


                var query = from docs in db.Documents
                            select docs;

                var results = query.ToList();

                main.lbxDocuments.ItemsSource = results;

                this.Close();

            }
        }
    }
}
