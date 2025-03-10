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

        DocumentData db=new DocumentData();
        public MainWindow()
        {
            InitializeComponent();

            var query1= from d in db.Documents
                        select d;

            documents = query1.ToList();

            lbxDocuments.ItemsSource = documents;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //create new window
            Window1 window1 = new Window1();

            //set owner of new window
            window1.Owner = this;

            //display new window
            window1.ShowDialog();
        }

        private void lbxDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //create new window
            Window2 window2 = new Window2();

            //set owner of new window
            window2.Owner = this;
        }
    }
}
