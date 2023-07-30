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
        CollectionReference collection = K.database.Collection("Patients");
        string s = "";
        public PatientDataPage()
        {
            InitializeComponent();

            Task.Run(() => this.testDatabaseTalk()).Wait();
            MessageBox.Show(s);
        }


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
    }
}
