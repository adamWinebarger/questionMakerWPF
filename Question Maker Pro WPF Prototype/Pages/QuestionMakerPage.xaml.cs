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
using Google.Cloud.Firestore;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Question_Maker_Pro_WPF_Prototype.Enums;

namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for QuestionMakerPage.xaml
    /// </summary>
    public partial class QuestionMakerPage : Page
    {
        bool preloadQuestion;
        private Patient? patient = null;

        //bool preload; //this will be so that we can use the same Page for creating question survey and generating new questions
        List<String> teacherQuestionList = new List<String>(), 
            parentQuestionList = new List<string>();
        int currentIndex = 0;
        TestTypeSelection currentSelectedTestType = TestTypeSelection.Teacher;
        bool parentQuestionListSaved = false, teacherQuestionListSaved = false;

        public QuestionMakerPage(Patient patient)
        {
            InitializeComponent();
            this.patient = (Patient) patient;
            //MessageBox.Show(patient.lastname);
            this.preloadQuestion = false;
            teacherQuestionList = generateDefaultQuestionList("TeacherPreloadQuestions");
            parentQuestionList = generateDefaultQuestionList("ParentPreloadQuestions");
            populatequestionComboBox();
            currentQuestionComboBox.SelectedIndex = 0;
        }

        public QuestionMakerPage(bool preloadQuestion = true)
        {
            InitializeComponent();
            this.preloadQuestion = preloadQuestion;
            teacherQuestionList = generateDefaultQuestionList("TeacherPreloadQuestions");
            parentQuestionList = generateDefaultQuestionList("ParentPreloadQuestions");
            if (preloadQuestion)
            {
                titleLabel.Content = "Pre-build Question List";
            }
            populatequestionComboBox();
            currentQuestionComboBox.SelectedIndex = 0;
        }

        /* Methods pertaining to the buttons */
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;
            int index = currentQuestionComboBox.SelectedIndex;

            if (tag == submitButton.Content)
            {
                //this is where we would have all the Patient data go to the cloud firestore database
                submit();
            } else if (tag == addQuestionButton.Content)
            {
                addQuestion();
            } else if (tag == insertButton.Content)
            {
                insertQuestion();
            } else if (tag == replaceButton.Content)
            {
                editQuestion();
            } else if (tag == removeButton.Content)
            {
                removeQuestion();
            } else if (tag == saveButton.Content)
            {
                saveQuestionList2File();
            } else if (tag == nextButton.Content)
            {
                var questionListInQuestion = (teacherRadioButton.IsChecked == true) ? teacherQuestionList : parentQuestionList;
                if (currentQuestionComboBox.SelectedIndex < questionListInQuestion.Count)
                {
                    currentQuestionComboBox.SelectedIndex = ++currentIndex;
                }
                updateQuestionTextBlock();
            } else if (tag == backButton.Content)
            {
                if (currentQuestionComboBox.SelectedIndex > 0)
                {
                    currentQuestionComboBox.SelectedIndex = --currentIndex;
                }
                updateQuestionTextBlock();
            }
        }

        private void toggleButtonVisibility()
        {
            removeButton.Visibility = (currentIndex == teacherQuestionList.Count) ?
                Visibility.Hidden : Visibility.Visible;
            replaceButton.Visibility = (currentIndex == teacherQuestionList.Count) ?
                Visibility.Hidden : Visibility.Visible;
            insertButton.Visibility = (currentIndex == teacherQuestionList.Count) ?
                Visibility.Hidden : Visibility.Visible;
            submitButton.Visibility = (teacherQuestionList.Count > 0) ?
                Visibility.Visible : Visibility.Hidden;
        }

        /*Radio button methods*/

        private void radioButtonChecked(object sender, RoutedEventArgs e)
        {
            var tag = ((RadioButton)sender).Content;

            if (tag == parentRadioButton.Content)
                currentSelectedTestType = TestTypeSelection.Parent;
            else if (tag == teacherRadioButton.Content)
                currentSelectedTestType = TestTypeSelection.Teacher;
            var listInQuestion = (currentSelectedTestType == TestTypeSelection.Teacher) ? teacherQuestionList : parentQuestionList;

            //currentIndex = (currentIndex > listInQuestion.Count()) ? listInQuestion.Count() : currentIndex;
            populatequestionComboBox();
        }



        /* Stuff pertaining to the questions, buttons, etc. */
        private void submit()
        {
            /*
                
            So for this method, if we're preloading, then we should be generating a file with all of 
            the questions in it and storing it within the program. But if we're using this page to create
            a new patient, then it should adding the question list to our patient class, then making a call
            to the firebase database, creating a new entry based on the information within our patient class
            and then uploading all the necessary information (personal info, the patient code, and then
            the questions
            
             */

            if (preloadQuestion)
            {
                saveQuestionList2File();
                if (!teacherQuestionListSaved || !parentQuestionListSaved)
                {
                    if (MessageBox.Show("It looks like you have only saved one of your question lists. Would you like to save the other one?", "Notice",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        currentSelectedTestType = (parentRadioButton.IsChecked == true) ? TestTypeSelection.Teacher : TestTypeSelection.Parent;
                    }
                    saveQuestionList2File();
                }

            } else
            {
                //This is where we will be making a call to load data into our firebase database
                addPatientInfoDocument();

            }
            
        }

        private void addQuestion()
        {
            List<String> currentSelectedQuestionList = (currentSelectedTestType == TestTypeSelection.Teacher)
                ? teacherQuestionList : parentQuestionList;

            if (questionIsValid())
            {
                currentSelectedQuestionList.Add(questionTextBlock.Text);
                populatequestionComboBox();
                currentIndex = currentSelectedQuestionList.Count;
                currentQuestionComboBox.SelectedIndex = currentIndex;
                questionTextBlock.Text = "";
            }

        }

        private void editQuestion()
        {
            List<String> currentSelectedQuestionList = (currentSelectedTestType == TestTypeSelection.Teacher)
                ? teacherQuestionList : parentQuestionList;

            if (questionIsValid())
            {
                currentSelectedQuestionList[currentIndex] = questionTextBlock.Text;
                //populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = ++currentIndex;
            }
        }

        private void insertQuestion()
        {
            var questionListInQuestion = (currentSelectedTestType == TestTypeSelection.Teacher)
                ? teacherQuestionList : parentQuestionList;

            if (questionIsValid())
            {
                questionListInQuestion.Insert(currentIndex, questionTextBlock.Text);
                int temp = ++currentIndex;
                populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = temp;
            }
        }

        private void removeQuestion()
        {
            var currentQuestionList = (currentSelectedTestType == TestTypeSelection.Teacher) ?
                teacherQuestionList : parentQuestionList;

            if (questionIsValid())
            {
                int temp = currentIndex;
                currentQuestionList.RemoveAt(currentIndex);
                populatequestionComboBox();
                currentQuestionComboBox.SelectedIndex = temp;
            }

        }

        List<String> generateDefaultQuestionList(string whichTest)
        {
            List<String> preloadQuestionList = new List<String>();

            if (File.Exists(Directory.GetCurrentDirectory() + "/Questions/" + whichTest + ".txt"))
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory()
                    + "/Questions/" + whichTest + ".txt");
                foreach (string line in lines)
                {
                    int dot = line.IndexOf('.');
                    preloadQuestionList.Add(line.Substring(dot + 1));
                }
                currentQuestionComboBox.SelectedIndex = 0;

            }
            return preloadQuestionList;
        }

        void saveQuestionList2File()
        {
            string whichFile2Save = (teacherRadioButton.IsChecked == true) ?
                "TeacherPreloadQuestions" : "ParentPreloadQuestions";
            var questionListInQuestion = (teacherRadioButton.IsChecked == true) ?
                teacherQuestionList : parentQuestionList;

            using (StreamWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "/Questions/"
                + whichFile2Save + ".txt"))
                for (int i = 0; i < questionListInQuestion.Count; i++)
                {
                    writer.WriteLine(String.Format("{0}. {1}", i + 1, questionListInQuestion[i]));
                }
            if (teacherRadioButton.IsChecked == true)
                teacherQuestionListSaved = true;
            else
                parentQuestionListSaved = true;
        }

        void updateQuestionTextBlock()
        {
           
            var questionListInQuestion = (currentSelectedTestType == TestTypeSelection.Teacher)
                ? teacherQuestionList : parentQuestionList;
            questionNumberLabel.Content = String.Format("Input question #{0}:", currentIndex + 1);
            questionTextBlock.Text = (currentIndex < questionListInQuestion.Count && currentIndex >= 0) ?
                questionListInQuestion[currentIndex] : "";
        }

        /*  Stuff pertaining to the combobox */

        private void questionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (currentQuestionComboBox.HasItems)
                currentIndex = currentQuestionComboBox.SelectedIndex;
            updateQuestionTextBlock();
            toggleButtonVisibility();

        }

        private void populatequestionComboBox()
        {
            currentQuestionComboBox.Items.Clear();
            //currentIndex = 0;

            List<String> questionListInQuestion =
                (currentSelectedTestType == TestTypeSelection.Teacher) ? teacherQuestionList :
                parentQuestionList;

            for (int i = 0; i <= questionListInQuestion.Count; i++)
                currentQuestionComboBox.Items.Add((i + 1).ToString());

            if (currentIndex > questionListInQuestion.Count)
                currentIndex = questionListInQuestion.Count;
            currentQuestionComboBox.SelectedIndex = currentIndex;
            updateQuestionTextBlock();
            toggleButtonVisibility();

        }

        /* Methods pertaining to Firebase Documents */
        void addPatientInfoDocument()
        {
            //var jsonText = JsonDocument.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json")).RootElement.GetProperty("private_key_id");
            //MessageBox.Show(jsonText.ToString());

            string patientString = String.Format("{0}, {1} ({2})", patient!.lastname, patient!.firstname, patient!.patientCode);

            Google.Cloud.Firestore.DocumentReference collection = K.database.Collection("Patients").Document(patientString);
            //MessageBox.Show(patient!.patientCode);
            Dictionary<string, object> patientData = new()
            {
                { "patientCode", patient!.patientCode },
                { "lastName", patient!.lastname },
                { "firstName", patient!.firstname },
                { "dateOfBirth", patient!.dateOfBirth.ToString() },
                { "age", patient.age },
                { "Gender", patient.gender.ToString() },
                { "Questions", new Dictionary<string, List<string>>() {
                    { "teacherQuestions", teacherQuestionList },
                    { "parentQuestions", parentQuestionList }
                }},
                { "AdministratorCode", K.adminKey.ToString() },
                { "teacherCanViewParentAnswers", false }
            };
            collection.SetAsync(patientData);

            MessageBox.Show("data added successfully");
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack(); 
            //Really, this probably won't work either and we're going to want to
            //just go to the home page after naviagtion is done.
            //then we just need to figure out at what point it destroys the old navigation pages
            //so that we aren't using up all the memory on peoples' machines
        }


        //Additional functions
        bool questionIsValid()
        {
            if (questionTextBlock.Text.Length >= 0)
                return true;
            MessageBox.Show("Error! Invalid value detected in the question block");
            return false;
        }

    }
}
