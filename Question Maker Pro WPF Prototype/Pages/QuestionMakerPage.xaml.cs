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

namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for QuestionMakerPage.xaml
    /// </summary>
    public partial class QuestionMakerPage : Page
    {
        private Patient? patient = null;
        //bool preload; //this will be so that we can use the same Page for creating question survey and generating new questions
        List<String> questionList = new List<String>();
        bool preloadQuestion;
        int currentIndex = 0;

        public QuestionMakerPage(Patient patient)
        {
            InitializeComponent();
            this.patient = (Patient) patient;
            //MessageBox.Show(patient.lastname);
            this.preloadQuestion = false;
            questionList = generateDefaultQuestionList();
            populatequestionComboBox();
        }

        public QuestionMakerPage(bool preloadQuestion = true)
        {
            InitializeComponent();
            this.preloadQuestion = preloadQuestion;
            questionList = generateDefaultQuestionList();
            if (preloadQuestion)
            {
                titleLabel.Content = "Pre-build Question List";
            }
            populatequestionComboBox();
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
            }
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
                using (StreamWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "/PreloadQuestions.txt"))
                    for (int i = 0; i < questionList.Count; i++)
                    {
                        writer.WriteLine(String.Format("{0}. {1}", i+1, questionList[i]));
                    }
            } else
            {
                //This is where we will be making a call to load data into our firebase database
                addPatientInfoDocument();

            }
            
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

        /*  Stuff pertaining to the combobox */

        private void questionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentIndex = currentQuestionComboBox.SelectedIndex;
            questionNumberLabel.Content = String.Format("Input question #{0}:", currentIndex + 1);
            questionTextBlock.Text = (currentIndex < questionList.Count && currentIndex >= 0) ?
                questionList[currentIndex] : "";
            toggleButtonVisibility();

        }

        private void populatequestionComboBox()
        {
            currentQuestionComboBox.Items.Clear();

            for (int i = 0; i <= questionList.Count; i++)
                currentQuestionComboBox.Items.Add((i + 1).ToString());

            toggleButtonVisibility();

        }

        /* Methods pertaining to Firebase Documents */
        void addPatientInfoDocument()
        {
            //var jsonText = JsonDocument.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json")).RootElement.GetProperty("private_key_id");
            //MessageBox.Show(jsonText.ToString());

            Google.Cloud.Firestore.DocumentReference collection = K.database.Collection("Patients")
                .Document( String.Format("{0}, {1} ({2})", patient!.lastname, 
                patient!.firstname, patient!.patientCode));
            //MessageBox.Show(patient!.patientCode);
            Dictionary<string, object> patientData = new()
            {
                { "patientCode", patient!.patientCode },
                { "lastName", patient!.lastname },
                { "firstName", patient!.firstname },
                { "dateOfBirth", patient!.dateOfBirth.ToString() },
                { "age", patient.age },
                { "Gender", patient.gender.ToString() },
                { "Questions", questionList },
                { "AdministratorCode", K.adminKey.ToString() }
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
            if (questionTextBlock.Text.Length > 0)
                return true;
            MessageBox.Show("Error! Invalid value detected in the question block");
            return false;
        }

        List<String> generateDefaultQuestionList()
        {
            List<String> preloadQuestionList = new List<String>();

            if (File.Exists(Directory.GetCurrentDirectory() + "/PreloadQuestions.txt"))
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/PreloadQuestions.txt");
                foreach (string line in lines)
                {
                    int dot = line.IndexOf('.');
                    preloadQuestionList.Add(line.Substring(dot + 1));
                }
                currentQuestionComboBox.SelectedIndex = 0;

            }
            return preloadQuestionList;
        }

    }
}
