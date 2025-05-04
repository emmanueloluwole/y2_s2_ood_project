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
            //exception handling
            try
            {
                // Get the selected document from the ListBox
                Document selected = lbxDocuments.SelectedItem as Document;

                // Check if something is selected
                if (selected != null)
                {
                    // Remove from the local list of documents
                    documents.Remove(selected);

                    // Find the document in the database by its ID
                    Document docToDelete = db.Documents.Find(selected.Id);

                    // Ensure the document exists in the database before attempting deletion
                    if (docToDelete != null)
                    {
                        // Remove the document from the database
                        db.Documents.Remove(docToDelete);

                        // Save changes to the database
                        db.SaveChanges();

                        // Update the ListBox's ItemsSource to reflect the changes
                        lbxDocuments.ItemsSource = null;
                        lbxDocuments.ItemsSource = documents;
                    }
                }
                else
                {
                    // Handle case where no document was selected
                    MessageBox.Show("Please select a document to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                MessageBox.Show("An error occurred while deleting the document: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        //method to seach document
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            List<Document> searchResult = new List<Document>();
            string searchText = string.Empty;

            try
            {
                // Make sure to safely get the search text, trimming leading/trailing spaces and converting to lowercase
                searchText = tbxSearch.Text.ToLower().Trim();

                // Check if searchText is not empty
                if (!string.IsNullOrEmpty(searchText))
                {
                    foreach (Document d in documents)
                    {
                        // Ensure the Title is not null before trying to use it
                        if (!string.IsNullOrEmpty(d.Title) && d.Title.ToLower().Contains(searchText))
                        {
                            searchResult.Add(d);
                        }
                    }
                }
                //seen this online about showbox and it format, handy to use than console.writeline
               
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                MessageBox.Show("An error occurred while searching: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Ensure the ListBox shows the full list of documents after the error
                searchResult.Clear();
                searchResult.AddRange(documents);  // Add the full list of documents back to the searchResult
            }
            finally
            {
                // This will always execute, even if there's an exception, to update the ListBox
                lbxDocuments.ItemsSource = null;  // Clear existing items
                //    lbxDocuments.ItemsSource = searchResult;  // Set new filtered list,  i used this before but it doesnt display the list of old documents after i search for null 
                lbxDocuments.ItemsSource = searchResult.Count > 0 ? searchResult : documents;  // If searchResult has items, show those; otherwise, show the full list of documents
                
            }






        }


    }
}
