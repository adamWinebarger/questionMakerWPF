using Firebase.Auth;
using Google.Cloud.Firestore;
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

namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for PatientDataPage.xaml
    /// </summary>
    public partial class PatientDataPage : Page
    {
        CollectionReference collection = K.firestoreDB!.Collection("Patients");
        string s = "";

        FirebaseAuthClient firebaseAuthClient = ((App)Application.Current).firebaseAuthProvider();
        List<Patient> patientList = new();

        public PatientDataPage()
        {
            InitializeComponent();

            //Task.Run(() => this.testDatabaseTalk()).Wait();
            //MessageBox.Show(s);

            if (firebaseAuthClient != null && ((App)Application.Current).isLoggedIn)
            {
                populatePatientDataGrid();
            }
        }

        /* So what all will we need here? 
         * 
         * Since this page is simply going to be a table that lists the patients, we could probably just get away with loading some stuff into 
         * our Patient class - or maybe sticking to a dictionary
         */


        async Task testDatabaseTalk()
        {
            //MessageBox.Show("Fire 1");
            Query query = collection;
            //MessageBox.Show("Fire 2");
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            //MessageBox.Show("Fire 3");
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                //MessageBox.Show(documentSnapshot.Id);
                s += documentSnapshot.Id.ToString();
                s += "\n";
            }
            //MessageBox.Show(s); 
        }

        async void populatePatientDataGrid()
        {
            Query query = K.firestoreDB!.Collection("Patients").WhereEqualTo("AdministratorCode", firebaseAuthClient.User.Uid);
            QuerySnapshot dataSnapshot = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in dataSnapshot.Documents)
            {
                Patient patient = new(docsnap);
                patientList.Add(patient);
            }

            patientDataGrid.ItemsSource = patientList;
        }

        private void patientDataGrid_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void onRowSelection(object sender, RoutedEventArgs e)
        {

        }

        private void onRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //What do we want the main thing to look like when someone selects one of the patients? Some kind of sub-menu?
            //Probably should have a think about this a bit. 
            Patient patient = patientDataGrid.SelectedItem as Patient;
            NavigationService.Navigate(new PatientViewPage(patient!), UriKind.Relative);

        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == viewPatientButton.Content) {
                //We need to have... whatever is going to happen with the patient viewer happen when we push this button (also be good to have this 
                // happen when they double-click the row for the patient)

            } else if (tag == copyParentCodeButton.Content) {
                copyCode2Clipboard(ParentOrTeacher.Parent);

            } else if (tag == copyTeacherCodeButton.Content) {
                copyCode2Clipboard(ParentOrTeacher.Teacher);

            } else if (tag == EditTeseterQuestionnaireButton.Content) {
                //So here, we need to pass a Patient Document to QuestionMakerPage2, preload the existing questions, and then allow 
                //the user to make some changes to... you know, allow the people to edit the questionnaires. 

                if (patientDataGrid.SelectedIndex >= 0)
                {
                    Patient patient = patientDataGrid.SelectedItem as Patient;
                    //MessageBox.Show(patient.firstname);
                    NavigationService.Navigate(new QuestionMakerPage2(patient!), UriKind.Relative);
                }
            } else if (tag == viewPatientButton.Content) {

                if (patientDataGrid.SelectedIndex >= 0)
                {
                    Patient patient = patientDataGrid.SelectedItem as Patient;
                    NavigationService.Navigate(new PatientViewPage(patient!), UriKind.Relative);
                }
            } else if (tag == newTesterButton.Content)
            {
                NavigationService.Navigate(new NewTesterInfoPage(), UriKind.Relative);
            } else if (tag == preloadQuestionnaireButton.Content) {
                
                NavigationService.Navigate(new QuestionMakerPage2(), UriKind.Relative);
            }
        }

        void copyCode2Clipboard(ParentOrTeacher parentOrTeacher)
        {
            if (patientDataGrid.SelectedItem == null)
            {
                MessageBox.Show("No patient Selected");
                return;
            }
            int selectedIndex = patientDataGrid.SelectedIndex;

            var item = patientDataGrid.SelectedItem;
            String code2Copy = "";

            if (parentOrTeacher == ParentOrTeacher.Parent)
            {
                //var data = ((Patient)item).parentCode;
                //MessageBox.Show(data);
                code2Copy = ((Patient)item).parentCode;
                MessageBox.Show("Parent Code copied to clipboard");
            } else if (parentOrTeacher == ParentOrTeacher.Teacher)
            {
                code2Copy = ((Patient)item).teacherCode;
                MessageBox.Show("Teacher Code copied to clipboard");
            } else
            {
                MessageBox.Show("This should theoretically be unreachable. Either you did something very wrong or some changes were made in the software" +
                    "and the developer forgot to make necessary changes");
            }

            Clipboard.SetText(code2Copy);
        }

        internal enum ParentOrTeacher
        {
            Parent,
            Teacher
        }
    }
}
