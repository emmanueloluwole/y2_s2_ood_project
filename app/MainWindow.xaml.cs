using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public List<Document> documents;

        DocumentData db = new DocumentData();
        public MainWindow()
        {
            InitializeComponent();

            var query1 = from d in db.Documents
                         select d;

            documents = query1.ToList();

            lbxDocuments.ItemsSource = documents;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //create new window
            AddWindow window1 = new AddWindow();

            //set owner of new window
            window1.Owner = this;

            //display new window
            window1.ShowDialog();
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Player selected = lbxSelectedPlayers.SelectedItem as Player;
            Document selected = lbxDocuments.SelectedItem as Document;

            //check nut null
            if (selected != null)//ensure something is selected
            {

                documents.Remove(selected);

                Document docToDelete= db.Documents.Find(selected.Id);

                //delete from db
                db.Documents.Remove(docToDelete);
                db.SaveChanges();

                lbxDocuments.ItemsSource = null;
                lbxDocuments.ItemsSource = documents;

            }


        }



        private void lbxDocuments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Document selected = lbxDocuments.SelectedItem as Document;

            if (selected != null)
            {
                //create new window
                EditWindow window2 = new EditWindow(selected);

                //set owner of new window
                window2.Owner = this;

                //display new window
                window2.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Document> searchResult = new List<Document>();
            string searchText = tbxSearch.Text.ToLower();

            if (searchText != null)
            {

               

                foreach (Document d in documents)
                {
                    if (d.Title.ToLower().Contains(searchText))
                    {
                        searchResult.Add(d);
                        
                    }

                }

            }
            lbxDocuments.ItemsSource = null;
            lbxDocuments.ItemsSource= searchResult;
           




        }


    }
}
