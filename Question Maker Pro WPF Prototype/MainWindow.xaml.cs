using Google.Cloud.Firestore;
using Firebase.Auth;
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
using Firebase.Auth.Providers;

namespace Question_Maker_Pro_WPF_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //FirebaseAuthProvider firebaseAuthProvider = ((App)Application.Current).firebaseAuthProvider();

        public MainWindow()
        {
            InitializeComponent();
            
            Uri uri = new("Pages/LoginPage.xaml", UriKind.Relative);
            navframe.Navigate(uri);
            //K.database = connect2FirestoreDB();
        }

        private void onWindowLoad(object sender, RoutedEventArgs e)
        {
            K.firestoreDB = connect2FirestoreDB();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            bool isLoggedIn = ((App)Application.Current).isLoggedIn;

            //MessageBox.Show(tag.ToString
            
            //We tacked on the isLoggedIn to each of the button conditionals just for a placeholder. But we may want to wrap our login-required buttons
            //into a single if for isLoggedIn so we can create a messagebox for those buttons telling people to please login first. We'll also probably
            //want some different conditionals in this function for a few different things like the logout button and maybe a few others. 
            //
            //Also, I still don't really know what we're going to use the home button for yet. Might just keep that as the patient viewer or something
            //... idk

            if (tag == newTesterButton.Content && isLoggedIn)
                navframe.Navigate(new NewTesterInfoPage(), UriKind.Relative);
            else if (tag == homeButton.Content)
            {
                //takes us to the home page
            } else if (tag == viewTestingDataButton.Content && isLoggedIn)
            {
                //takes us to our testing view data page.
                /*
                 * So here we probably will want to have a page load with a table-view that will populate
                 * with all the entries from the firebase database that correspond with the user of the
                 * program. We also might want to have some text boxes and buttons and things for search 
                 * parameters.
                 */
                navframe.Navigate(new PatientDataPage(), UriKind.Relative);
            } else if (tag == preloadQuestionsButton.Content && isLoggedIn)
            {
                //here's where we'll want to do load our page for preloading questions; and from there
                // we'll have to incorporate that IO bit into the questionmaker page in order to load 
                // the questions from the pre-saved textfile that will be generated here
                navframe.Navigate(new QuestionMakerPage(true), UriKind.Relative);
            } else if (tag == testButton.Content)
            {
                //MessageBox.Show("This fired");
                navframe.Navigate(new QuestionMakerPage2(), UriKind.Relative);
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
