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
    /// Interaction logic for NewTesterInfoPage.xaml
    /// </summary>
    public partial class NewTesterInfoPage : Page
    {
        public NewTesterInfoPage()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!checkAllFieldsValid())
                return;

            Patient newPatient = new(lastNameTextBox.Text, firstNameTextBox.Text, getGender(), DateOnly.FromDateTime(dobPicker.SelectedDate!.Value.Date));
            //MessageBox.Show("Success");
            NavigationService.Navigate(new QuestionMakerPage(newPatient), UriKind.Relative);
            
        }

        private Gender getGender()
        {
            if (maleRadioButton.IsChecked == true)
                return Gender.male;
            if (femaleRadioButton.IsChecked != true) return Gender.female;
            return Gender.other;
        }

        private bool checkAllFieldsValid()
        {
            if (lastNameTextBox.Text == "" || firstNameTextBox.Text == "")
            {
                MessageBox.Show("It appears that you are missing either a last name or a first name");
                return false;
            }

            if (maleRadioButton.IsChecked == false && femaleRadioButton.IsChecked == false && otherRadioButton.IsChecked == false)
            {
                MessageBox.Show("Please Select a gender from the options listed");
                return false;
            }

            if (!dobPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a valid Date of Birth");
                return false;
            }

            return true;
        }

    }
}
