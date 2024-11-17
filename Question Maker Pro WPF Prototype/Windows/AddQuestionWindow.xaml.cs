using Question_Maker_Pro_WPF_Prototype.Enums;
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
using System.Windows.Shapes;

namespace Question_Maker_Pro_WPF_Prototype.Windows
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {

        public string questionText { get; private set; }
 
        public AddQuestionWindow(TestTypeSelection testTypeSelection)
        {
            
            InitializeComponent();
            if (testTypeSelection == TestTypeSelection.Parent)
            {
                parentRadioButton.IsChecked = true;
            } else if (testTypeSelection == TestTypeSelection.Teacher)
            {
                teacherRadioButton.IsChecked = true;
            }

        }

        public AddQuestionWindow(string questionToEdit, int index)
        {
            InitializeComponent();
            titleLabel.Content = String.Format("Edit Question #{0}", index.ToString());
            addQuestionGrid.Visibility = Visibility.Hidden;
            questionTextBlock.Text = questionToEdit;
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (addQuestionGrid.Visibility == Visibility.Visible && parentRadioButton.IsChecked != true && 
                teacherRadioButton.IsChecked != true && bothRadioButton.IsChecked != true) {
                MessageBox.Show("Error! You must select whether this question is added to the parent or the teacher questionnaire", "Warning");
                return;
            }

            if (questionTextBlock.Text.Length < 2)
            {
                MessageBox.Show("Error. Inssufficient Text length. Please write your question within the block provided.", "Too little information.");
                return;
            }

            if (tag == submitButton.Content) {
                //Submit Button Logic here
                questionText = questionTextBlock.Text;
                DialogResult = true;
                Close();

            } else if (tag == backButton.Content) {
                //Back Button Logic Here
                DialogResult = false;
                Close();

            } else {

                MessageBox.Show("This should be theoretically unreachable.");
            }
        }
    }
}
