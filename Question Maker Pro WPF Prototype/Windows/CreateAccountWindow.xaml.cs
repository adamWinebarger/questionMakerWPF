using Firebase.Auth;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Question_Maker_Pro_WPF_Prototype.Windows
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {

        FirebaseAuthClient _firebaseAuthClient = ((App)Application.Current).firebaseAuthProvider();

        public CreateAccountWindow()
        {
            InitializeComponent();
        }

       private async void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == submitButton.Content) {
                if (validateForm())
                {
                    //Case for all required fields being input

                    //So ChatGPT thinks that the play here is to create our own "loading window" and have it appear while the stuff is going on
                    // sounds like messagebox won't be able to take care of this on its own.

                    if (await createUser() == true)
                    {
                        DialogResult = true;
                        ((App)Application.Current).isLoggedIn = true;
                        Close();
                       
                    }
                } else
                {
                    MessageBox.Show("Account Creation Failed");
                }

            } else if (tag == cancelButton.Content) {
                //Cancel button pressed. This should set the dialog to false and then return us to our main window
                DialogResult = false;
                Close();
            } else
            {
                MessageBox.Show("This should theoretically be unreachable.");
            }
        }

        private bool validateForm()
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            //string passwordPattern = @"^(?=.*[a-zA-Z0-9])(?=.*\d)(?=.*[!@#$%^&*]).{6,}$";
            

            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || Regex.IsMatch(emailTextBox.Text, emailPattern) == false)
            {
                MessageBox.Show("Invalid Email Detected");
                return false;
            }

            if (string.IsNullOrEmpty(passwordEntry.Password) || !passwordIsValid(passwordEntry.Password))
            {

                MessageBox.Show("Invalid Password Detected");
                return false;
            }

            if (passwordEntry.Password.Equals(confirmPasswordEntry.Password) == false)
            {
                MessageBox.Show("It looks like your passwords do not match. Please ensure that you input the identical text into both password fields");
                return false;
            }

            if (firstNameTextbox.Text.Length < 2 || lastNameTextbox.Text.Length < 2)
            {
                MessageBox.Show("First Name or Last name is too short");
                return false;
            }

            return true;
        }

        private bool passwordIsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) { return false; }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSpecial = new Regex(@"[!@#$%^&*_\-]+");
            var hasMinimumChars = new Regex(@".{6,}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) && hasSpecial.IsMatch(password)
                && hasMinimumChars.IsMatch(password);
        }

        private async Task<bool> createUser()
        {
            string firstName = firstNameTextbox.Text, lastName = lastNameTextbox.Text, email = emailTextBox.Text, password = passwordEntry.Password;

            /* Okay, so let's rethink this a bit... so what we first want to do is probably check and see if the user already exists in users.
             If it does... then I think there's not really anything we need to do there. Actually, let's add the adminID in there so that the mobile
            app can check and see if someone's parent/teacher account has an administrator code and prompt them as to whether they're looking to use
            the app as a therapist rather than a parent/teacher. That way we don't have to change too much on the login screen visual-wise and 
            the functionality will be basically undetectable to anyone who doesn't know it's there.
            
             So with that in mind, I think we need to pass everything into the user doc *except* for the org name because there's no real reason
            for that to be in there. Then we can tie the admin collection to something like subscriptions or something later on down the line. Idk,
            but it seems like having some degree of separation between the two things might be helpful as this thing starts to get bigger.
            
             Then again, we are kind of overthinking this for an extremely unlikely edge-case. But still...*/

            try {


                QuerySnapshot userQuery = await K.firestoreDB!.Collection("users").WhereEqualTo("email", email).GetSnapshotAsync(),
                    adminQuery = await K.firestoreDB!.Collection("administrators").WhereEqualTo("email", email).GetSnapshotAsync();
                string uuid;

                if (adminQuery.Documents.Count > 0)
                {
                    MessageBox.Show("Error: An administrative account with this email already exists");
                    return false;
                }

                if (userQuery.Documents.Count > 0)
                {
                    //This will be the case if an account exists within users, meaning they already have an account with Firebase auth
                    await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);

                    uuid = _firebaseAuthClient.User.Uid;

                    if (userQuery.Documents[0].ContainsField("adminID"))
                    {
                        MessageBox.Show("Administrator account already exists for this email.", "Problem");
                        return false;
                    }
                    else
                    {
                        Google.Cloud.Firestore.DocumentReference docRef = K.firestoreDB!.Collection("Users")
                            .Document(String.Format("{0}, {1} ({2})", lastName, firstName, uuid));

                        await docRef.UpdateAsync("adminID", uuid);
                    }
                } else
                {
                    //This is what happens whnen no account, regular or otherwise, has been created using this email
                    await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password);
                    uuid = _firebaseAuthClient.User.Uid;
                }

                //should we log in here?

                //string uuid = _firebaseAuthClient.User.Uid;

                /*So with that, down here now should be where we're handling the logging of our administrator account which will have the org name,
                    and possibly be handling account and subscription shit later on down the line
                */
                Google.Cloud.Firestore.DocumentReference adminReference = K.firestoreDB!.Collection("administrators")
                    .Document(String.Format("{0}, {1} ({2})", lastName, firstName, uuid)); //Might come back and change this... idk.

                Dictionary<string, object> adminData = new()
                {
                    { "adminID", uuid },
                    { "lastName", lastName },
                    { "firstName", firstName },
                    { "email", email }
                };

                //Catchment for if the person already had a user account so that we can bump them up to an adminAccount

                //Remember we changed users to just the UUID because that was how we built most things on the flutter side.
                Google.Cloud.Firestore.DocumentReference userRef = K.firestoreDB!.Collection("users")
                    .Document(uuid);

                await userRef.SetAsync(adminData);

                if (String.IsNullOrWhiteSpace(organizationTextBox.Text) == false)
                {
                    adminData.Add("organizaiton", organizationTextBox.Text);
                }

                await adminReference.SetAsync(adminData);

                await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);

                return true;

            } catch (Exception ex) {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
