using Question_Maker_Pro_WPF_Prototype.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
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
    /// Interaction logic for saveQuestionnaireWindow.xaml
    /// </summary>
    public partial class saveQuestionnaireWindow : Window
    {
        TestTypeSelection testTypeSelection;

        public saveQuestionnaireWindow(TestTypeSelection testTypeSelection)
        {
            this.testTypeSelection = testTypeSelection;
            InitializeComponent();
            if (testTypeSelection == TestTypeSelection.Parent)
                parentRadioButton.IsChecked = true;
            else
                teacherRadioButton.IsChecked = true;

        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == null)
            {
                return;
            } else if (tag == saveButton.Content)
            {
                DialogResult = true;
                Close();

            } else if (tag == backButton.Content)
            {
                DialogResult = false;
                Close();
            }
        }

        
    }
}
