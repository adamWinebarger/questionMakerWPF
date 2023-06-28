using Google.Cloud.Firestore;
using Question_Maker_Pro_WPF_Prototype.Pages;
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

namespace Question_Maker_Pro_WPF_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            Uri uri = new("Pages/LoginPage.xaml", UriKind.Relative);
            navframe.Navigate(uri);
            K.database = connect2FirestoreDB();
        }

        private void onWindowLoad(object sender, RoutedEventArgs e)
        {
           //database = connect2FirestoreDB();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            //MessageBox.Show(tag.ToString());

            if (tag == newTesterButton.Content)
                navframe.Navigate(new NewTesterInfoPage(), UriKind.Relative);
            else if (tag == homeButton.Content)
            {
                //takes us to the home page
            } else if (tag == viewTestingDataButton.Content)
            {
                //takes us to our testing view data page.
                /*
                 * So here we probably will want to have a page load with a table-view that will populate
                 * with all the entries from the firebase database that correspond with the user of the
                 * program. We also might want to have some text boxes and buttons and things for search 
                 * parameters.
                 */
            } else if (tag == preloadQuestionsButton.Content)
            {
                //here's where we'll want to do load our page for preloading questions; and from there
                // we'll have to incorporate that IO bit into the questionmaker page in order to load 
                // the questions from the pre-saved textfile that will be generated here
                navframe.Navigate(new QuestionMakerPage(true), UriKind.Relative);
            }

        }

        FirestoreDb connect2FirestoreDB()
        {
            
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            FirestoreDb db = FirestoreDb.Create("schoolquestiontester");
            //MessageBox.Show("Succss");
            return db;
        }
    }
}
