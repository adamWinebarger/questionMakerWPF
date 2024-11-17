using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
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
using Firebase.Auth;
using Question_Maker_Pro_WPF_Prototype.Windows;
using System.Text.RegularExpressions;


namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        FirebaseAuthClient _firebaseAuthClient = ((App)Application.Current).firebaseAuthProvider(); 
        
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == loginButton.Content) {

                if (await attemptLogin(usernameTextBox.Text, passwordTextBox.Password) == true) {
                    ((App)Application.Current).isLoggedIn = true;

                    NavigationService.Navigate(new PatientDataPage(), UriKind.Relative);

                }

            } else if (tag == createAccountButton.Content) {
                var createAccout = new CreateAccountWindow();
                createAccout.ShowDialog();

                if (createAccout.DialogResult == true)
                {
                    //We gotta go to the home screen from here... maybe the patientView screen?
                    NavigationService.Navigate(new PatientDataPage(), UriKind.Relative);
                }

            } else {

            }
        }

        private async Task<bool> attemptLogin(string login, string password)
        {
            //So this will attempt to make a firebase call where it logs in with a username and password; and then, I guess

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (Regex.IsMatch(login, emailPattern) == false) {
                MessageBox.Show("Invalid email detected.");
                return false;
            }

            try
            {

                //So this may have just become redundant. Since the same stuff is essentially going into administrators *and* into users...
                //maybe we can just get rid of the administrators thing altogether and just use the presence of an adminCode to determine 
                //the privileges based on that.
                //
                //Well, let's actually test this theory.
                QuerySnapshot qs = await K.firestoreDB!.Collection("users").WhereEqualTo("email", login).GetSnapshotAsync();

                if (qs.Documents.Count != 1)
                {
                    MessageBox.Show("Something has gone wrong. Please contact... Someone"); //Come back and change this
                    return false;
                }

                DocumentSnapshot doc = qs.Documents[0];

                if (!doc.ContainsField("adminID") || doc.GetValue<string>("adminID") == null)
                {
                    //We should probably beef this up a little bit. But I think that this is fine for now.
                    MessageBox.Show("Error: Either your account does not exist or you don't have sufficient permissions to be accessing this program.", "Error");
                    return false;
                }

                var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(login, password);

                if (userCredential != null) { return true; } else {
                    MessageBox.Show("Login Failed. Ensure that your email and password are correct. If you do not have an account, select the \"Create Account\" button.");
                    return false; 
                }

            } catch (Exception ex)
            {
                MessageBox.Show(String.Format("Login Failed: {0}", ex.Message));
                return false;
            }
            
        }

        void go2HomePage()
        {
            //Likely won't use this
        }

        bool isValidLogin(string login)
        {
            //likely won't use this either

            return false;
        }
    }
}
