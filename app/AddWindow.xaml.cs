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

namespace app
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        DocumentData db = new DocumentData();
        public AddWindow()
        {
            InitializeComponent();


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read the data from the screen
            string title = tbxTitle.Text;

            string detials = tbxDetails.Text;


            //create the object
            
            using (db)
            {
                Document d = new Document() { Title=title, Details=detials, CreatedDate=DateTime.Now};

                //add the object to the db
                db.Documents.Add(d);
                //save
                db.SaveChanges();

                //update
                MainWindow main = this.Owner as MainWindow;
                main.documents.Add(d);
                main.lbxDocuments.ItemsSource = null;
                main.lbxDocuments.ItemsSource = main.documents;
            }




            //close the window 
            this.Close();


            


        }
    }
}
