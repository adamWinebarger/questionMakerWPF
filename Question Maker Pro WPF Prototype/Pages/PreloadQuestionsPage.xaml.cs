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
using System.IO;

namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for PreloadQuestionsPage.xaml
    /// </summary>
    public partial class PreloadQuestionsPage : Page
    {
        private List<string> questionList;
        private int currentIndex = 0;

        public PreloadQuestionsPage()
        {
            InitializeComponent();
            questionList = new();
            populatequestionComboBox();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == addQuestionButton.Content)
            {
                addQuestion();
            } else if (tag == replaceButton.Content)
            {
                editQuestion();
            } else if (tag == insertButton.Content)
            {
                insertQuestion();
            } else if (tag == removeButton.Content)
            {
                removeQuestion();
            } else if (tag == submitButton.Content)
            {
                createTestQuestionsTextFile();
            }
        }

        private void questionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = currentQuestionComboBox.SelectedIndex;
            questionTextBlock.Text = (currentIndex < questionList.Count && currentIndex >= 0) ?
                questionList[currentIndex] : "";
            toggleButtonVisibility();

        }

        private void toggleButtonVisibility()
        {
            removeButton.Visibility = (currentIndex == questionList.Count) ? 
                Visibility.Hidden : Visibility.Visible;
            replaceButton.Visibility = (currentIndex == questionList.Count) ?
                Visibility.Hidden : Visibility.Visible;
            insertButton.Visibility = (currentIndex == questionList.Count) ?
                Visibility.Hidden : Visibility.Visible;
            submitButton.Visibility = (questionList.Count > 0) ?
                Visibility.Visible : Visibility.Hidden;

        }

        private void createTestQuestionsTextFile()
        {

        }

        private void populatequestionComboBox()
        {
            currentQuestionComboBox.Items.Clear();

            for (int i = 0; i <= questionList.Count; i++)
                currentQuestionComboBox.Items.Add((i+1).ToString());

            toggleButtonVisibility();

        }

        bool questionIsValid()
        {
            if (questionTextBlock.Text.Length > 0)
                return true;
            MessageBox.Show("Error! Invalid value detected in the question block");
            return false;
        }

        private void addQuestion()
        {
            if (questionIsValid())
            {
                questionList.Add(questionTextBlock.Text);
                populatequestionComboBox();
                currentIndex = questionList.Count;
                currentQuestionComboBox.SelectedIndex = currentIndex;
                questionTextBlock.Text = "";
            }
        }

        private void editQuestion()
        {
            if (questionIsValid())
            {
                questionList[currentIndex] = questionTextBlock.Text;
                //populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = ++currentIndex;
            }
        }

        private void insertQuestion()
        {
            if (questionIsValid())
            {
                questionList.Insert(currentIndex, questionTextBlock.Text);
                int temp = ++currentIndex;
                populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = temp;
            }
        }

        private void removeQuestion()
        {
            if (questionIsValid())
            {
                int temp = currentIndex;
                questionList.RemoveAt(currentIndex);
                populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = temp;
            }
        }
    }
}
