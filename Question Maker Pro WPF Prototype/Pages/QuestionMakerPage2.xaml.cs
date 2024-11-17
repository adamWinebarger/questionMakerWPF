using Question_Maker_Pro_WPF_Prototype.Enums;
using Question_Maker_Pro_WPF_Prototype.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Google.Cloud.Firestore;
using Firebase.Auth;

namespace Question_Maker_Pro_WPF_Prototype.Pages
{
    /// <summary>
    /// Interaction logic for QuestionMakerPage2.xaml
    /// </summary>
    public partial class QuestionMakerPage2 : Page
    {
        private Patient? patient = null;
        FirebaseAuthClient _firebaseAuthClient = ((App)Application.Current).firebaseAuthProvider();

        //this will be so that we can use the same Page for creating question survey and generating new questions
        List<String> teacherQuestionList = new List<String>(),
            parentQuestionList = new List<string>();

        TestTypeSelection currentSelection = TestTypeSelection.Parent;
        int currentIndex = -1;
        bool parentQuestionListSaved = false, teacherQuestionListSaved = false;
        bool updatingExistingPatient;

        public QuestionMakerPage2(Patient patient, bool updatingExistingPatient = false)
        {
            this.patient = patient;
            this.updatingExistingPatient = updatingExistingPatient;
            InitializeComponent();
            teacherQuestionList = generateDefaultQuestionList("TeacherPreloadQuestions");
            parentQuestionList = generateDefaultQuestionList("ParentPreloadQuestions");
            populateQuestionList();
        }

        public QuestionMakerPage2()
        {
            InitializeComponent();
            teacherQuestionList = generateDefaultQuestionList("TeacherPreloadQuestions");
            parentQuestionList = generateDefaultQuestionList("ParentPreloadQuestions");
            populateQuestionList();
        }

        /* So what is there still left to do here?
        - On loading this page, we first need to check for preloaded questions (parent and/or teacher).
        - If there are none, then the lists for the questions simply sit empty - in which case people will have to add them in manually
            (We are going to want a separate Window for adding in questions, I think.)
        -  In the event that there are questions, the parent and teacher question DataGrid Views will need to be populated
            (will need catchments for 0, 1, and 2 preloaded questionnaires. Should we allow them to pick their own questionnaires while we're at it?)
        
        - on top of that, there will need to be options to add, edit, rearrange, and delete questions from both questionnaires
        - there will need to be catchments to ensure which questionnaire is selected and have edit, rearrange, and delete reflect accordingly
            - I think that add will just have it's own window which will ask people to specifiy which questionnaire they are wanting to use.
        - Not really sure what the behavior should be for finish when someone is generating their own preloaded questionnaires... maybe we should
            give people the option to make multiple ones... cross that bridge when we come to it. 
        */

        /* 
         * TODO - we need to rework the logic of how our question cells are populating since a questionList can be 0 but it will still cause the 
         * datagridview to populate with the other questionList. So we might need to rework the logic from the top-down in order to make sure everything
         * makes sense and we won't have that issue. 
         */

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == editQuestionButton.Content)
            {
                //Case for Edit Question Button right here. May involve our new window with an input parameter in place
                //here we can use the same Window with a different constructor to allow us our reuse of components and shit
                editQuestion();

            }
            else if (tag == addQuestionButton.Content)
            {
                //Add question case
                //I think we need a new window here that will then pop the question string here. 
                addQuestion();

            }
            else if (tag == removeQuestionButton.Content)
            {
                //Remove question case
                deleteQuestion();

            }
            else if (tag == saveQuestionnaireButton.Content)
            {
                //Should we also make another window for this one? At the very least, we need some kind of prompt to let the machine know
                // which questionnaire to sace
                saveQuestionnaireWindow saveQuestionnaireWindow = new(currentSelection);

                var result = saveQuestionnaireWindow.ShowDialog();

                if (result == true)
                {
                    if (saveQuestionnaireWindow.bothRadioButton.IsChecked == true)
                    {
                        saveQuestionnaire();
                    }
                    else if (saveQuestionnaireWindow.parentRadioButton.IsChecked == true)
                    {
                        saveQuestionnaire(TestTypeSelection.Parent);
                    }
                    else if (saveQuestionnaireWindow.teacherRadioButton.IsChecked == true)
                    {
                        saveQuestionnaire(TestTypeSelection.Teacher);
                    }
                    else
                    {
                        MessageBox.Show("This should have theoritically been unreachable. If you're seeing this. Then something is wrong with the save window logic");
                    }
                }

            }
            else if (tag == moveUpButton.Content)
            {
                //move up/move down might be a bit dicey. Do we want to rearrange the numbers or do we want to swap out the question strings
                //is the thing
                moveCellUpInDataGrid();

            }
            else if (tag == moveDownButton.Content)
            {
                //Move down
                moveCellDownInDataGrid();
            }
            else if (tag == resetQuestionnaireButton.Content)
            {
                //Reloads the saved questionnaires... might want to have a little pop-up prompt that makes it so they can load, one, the other, or both
                //MessageBoxResult result = MessageBox.Show("Banana", "Warning", MessageBoxButton.YesNo);

                if (MessageBox.Show("This will reset your progress on both questionnaires. Are you sure you want to proceed?",
                    "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    teacherQuestionList = generateDefaultQuestionList("TeacherPreloadQuestions");
                    parentQuestionList = generateDefaultQuestionList("ParentPreloadQuestions");
                    populateQuestionList();
                }
                else
                {
                    return;
                }

            }
            else if (tag == submitButton.Content)
            {
                //Condition for when the submit button is hit. We will need to have separate things happen whether QMP2 is selected to edit 
                //questionnaires or if we're creating a new tester. Frankly, I'm not even sure I want the submit button to be here when 
                // we're creating our preloaded questionnaires - and I might have it be something else in that case. 

                if (patient != null)
                {
                    if (!updatingExistingPatient)
                    {
                        if (addPatientDocument2Database() == true)
                        {

                            NavigationService.Navigate(new PatientDataPage(), UriKind.Relative);

                        }
                    } else
                    {
                        //Need to put an additional async method here so that we can update the questionnaires in the database

                    }

                } else
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("I don't know how you got this error message to show up. But please do not do it again");
            }
        }

        private void parentQuestionsDataGrid_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGrid = sender as DataGrid;

            if (selectedGrid != null)
            {
                if (selectedGrid.Name == "parentQuestionsDataGrid")
                {
                    currentSelection = TestTypeSelection.Parent;
                } else if (selectedGrid.Name == "teacherQuestionsDataGrid")
                {
                    currentSelection = TestTypeSelection.Teacher;
                }
                currentIndex = selectedGrid.SelectedIndex;
                //MessageBox.Show(String.Format("{0} - {1}", currentSelection.ToString(), currentIndex.ToString()));
            }
        }

        private void onRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            editQuestion();
        }

        private void onRowSelection(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();

            
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
            }
            return preloadQuestionList;
        }

        private void populateQuestionList(TestTypeSelection? testTypeSelection = null, ButtonSelection bs = ButtonSelection.Reset)
        {
            /* So what to we need to do here?
             * 
             * First, on the initial load, we need to load one or both of the question lists. If there's only one question list, do we want 
             * to ask the user if they want to load in for both? Or should we just autoload for both on the initial load? Probably should do the latter
             * 
             *  - We need to have some sort of case for if there are no questionLists to speak of.
             *  - In the event that all questions are deleted out of one of the lists... should we give them a MessageBox prompt to ask them if they 
             *      to copy in the other?
             *  - 
             */

            if (bs == ButtonSelection.Reset)
            {
                if (parentQuestionList.Count == 0 && teacherQuestionList.Count > 0)
                    parentQuestionList = new(teacherQuestionList);  

                if (teacherQuestionList.Count == 0 && parentQuestionList.Count > 0)
                    teacherQuestionList = new(parentQuestionList);
            }

            if (bs == ButtonSelection.Remove)
            {
                if (testTypeSelection == TestTypeSelection.Parent &&  parentQuestionList.Count == 0)
                {
                    var result = MessageBox.Show("It appears you have deleted all of the questions from your Parent Questionnaire. Would you like to populate " +
                      "this questionnaire with your teacher quetsions", "Empty questionnaire detected", MessageBoxButton.YesNo);

                    if (result ==  MessageBoxResult.Yes) {
                        parentQuestionList = new List<string>(teacherQuestionList);
                    }
                }

                if (testTypeSelection == TestTypeSelection.Teacher && teacherQuestionList.Count == 0)
                {
                    var result = MessageBox.Show("It appears you have deleted all of the questions from your teacher Questionnaire." +
                        " Would you like to populate it with the questions from the parent questionnaire?", "Empty quetsionnaire detected", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        teacherQuestionList = new List<String>(parentQuestionList);
                    }
                }
            }

            ObservableCollection<QuestionCell> parentQuestionCells = new(), teacherQuestionCells = new();
            int index = 1;

            if (testTypeSelection != TestTypeSelection.Teacher) {
                foreach (String question in parentQuestionList)
                {
                    parentQuestionCells.Add(new(question, index++));
                }
                parentQuestionsDataGrid.ItemsSource = parentQuestionCells;
            }

            index = 1;

            if (testTypeSelection != TestTypeSelection.Parent) {
                foreach (String question in teacherQuestionList)
                {
                    teacherQuestionCells.Add(new(question, index++));
                }
                teacherQuestionsDataGrid.ItemsSource = teacherQuestionCells;
            }
        }

        private void moveCellUpInDataGrid()
        {
            if (currentIndex > 0)
            {
                List<String> questionList = currentSelection == TestTypeSelection.Parent ? parentQuestionList : teacherQuestionList;

                if (questionList.Count > 0)
                {
                    var temp = questionList[currentIndex - 1];
                    questionList[currentIndex - 1] = questionList[currentIndex];
                    questionList[currentIndex] = temp;

                    if (currentSelection == TestTypeSelection.Teacher) {
                        teacherQuestionList = questionList;
                        int index = currentIndex > 0 ? --currentIndex : 0;
                        populateQuestionList(currentSelection);
                        teacherQuestionsDataGrid.SelectedIndex = index;
                        teacherQuestionsDataGrid.Focus();

                    } else {
                        parentQuestionList = questionList;
                        int index = currentIndex > 0 ? --currentIndex : 0;
                        populateQuestionList(currentSelection);
                        parentQuestionsDataGrid.SelectedIndex = index;
                        parentQuestionsDataGrid.Focus();

                    }                  
                }
            }
        }

        private void moveCellDownInDataGrid()
        {
            List<String> questionList = (currentSelection == TestTypeSelection.Parent) ? parentQuestionList : teacherQuestionList;

            if (currentIndex < questionList.Count - 1) {
                var temp = questionList[currentIndex + 1];
                questionList[currentIndex + 1] = questionList[currentIndex];
                questionList[currentIndex] = temp;

                if (currentSelection == TestTypeSelection.Teacher) {
                    teacherQuestionList = questionList;
                    int index = currentIndex < questionList.Count - 1 ? ++currentIndex : questionList.Count - 1;
                    populateQuestionList(currentSelection);
                    teacherQuestionsDataGrid.SelectedIndex = index;
                    teacherQuestionsDataGrid.Focus();

                } else {
                    parentQuestionList = questionList;
                    int index = currentIndex < questionList.Count - 1 ? ++currentIndex : questionList.Count - 1;
                    populateQuestionList(currentSelection);
                    parentQuestionsDataGrid.SelectedIndex = index;
                    parentQuestionsDataGrid.Focus();

                }
            }
        }

        private void addQuestion()
        {
            AddQuestionWindow addQuestionWindow = new(currentSelection);
            bool? result = addQuestionWindow.ShowDialog();

            if (result == true)
            {
                string newQuestion = addQuestionWindow.questionText;
                if (addQuestionWindow.parentRadioButton.IsChecked == true || addQuestionWindow.bothRadioButton.IsChecked == true) {
                    parentQuestionList.Add(newQuestion);
                    populateQuestionList(TestTypeSelection.Parent);
                    parentQuestionsDataGrid.SelectedIndex = currentIndex = parentQuestionList.Count - 1;
                    
                    parentQuestionsDataGrid.Focus();
                }
                
                if (addQuestionWindow.teacherRadioButton.IsChecked == true || addQuestionWindow.bothRadioButton.IsChecked == true) {
                    teacherQuestionList.Add(newQuestion);
                    populateQuestionList(TestTypeSelection.Teacher);
                    teacherQuestionsDataGrid.SelectedIndex = currentIndex = teacherQuestionList.Count - 1;
                    teacherQuestionsDataGrid.Focus();
                }
            }
            
        }

        private void editQuestion()
        {

            int tempIndex = currentIndex;

            if (tempIndex < 0)
                return;

            string selectedQuestion = currentSelection == TestTypeSelection.Parent ? parentQuestionList[tempIndex] : teacherQuestionList[tempIndex];
            AddQuestionWindow editQuestionWindow = new(selectedQuestion, tempIndex+1);

            bool? result = editQuestionWindow.ShowDialog();
            if (result == true)
            {
                string editedQuestion = editQuestionWindow.questionText;
                if (currentSelection == TestTypeSelection.Parent)
                {
                    parentQuestionList[currentIndex] = editedQuestion;
                    populateQuestionList(TestTypeSelection.Parent);
                    parentQuestionsDataGrid.SelectedIndex = currentIndex = tempIndex;
                    parentQuestionsDataGrid.Focus();
                } else if (currentSelection == TestTypeSelection.Teacher)
                {
                    teacherQuestionList[currentIndex] = editedQuestion;
                    populateQuestionList(TestTypeSelection.Teacher);
                    teacherQuestionsDataGrid.SelectedIndex = currentIndex = tempIndex;
                    parentQuestionsDataGrid.Focus();
                }
             } else
            {
                if (currentSelection == TestTypeSelection.Parent)
                {
                    parentQuestionsDataGrid.SelectedIndex = currentIndex = tempIndex;
                    parentQuestionsDataGrid.Focus();
                } else if (currentSelection == TestTypeSelection.Teacher)
                {
                    teacherQuestionsDataGrid.SelectedIndex = currentIndex = tempIndex;
                    parentQuestionsDataGrid.Focus();
                }
            }
        }

        private void deleteQuestion()
        {
            int tempIndex = currentIndex;

            if (currentIndex < 0)
                return;

            if (currentSelection == TestTypeSelection.Parent)
            {
                if (parentQuestionList[currentIndex] != null)
                {
                    parentQuestionList.RemoveAt(currentIndex);
                    populateQuestionList(TestTypeSelection.Parent, ButtonSelection.Remove);
                    parentQuestionsDataGrid.SelectedIndex = (tempIndex >= parentQuestionList.Count) ? parentQuestionList.Count - 1 : tempIndex;
                    parentQuestionsDataGrid.Focus();
                }
            } else if (currentSelection == TestTypeSelection.Teacher)
            {
                if (teacherQuestionList[currentIndex] != null)
                {
                    teacherQuestionList.RemoveAt(currentIndex);
                    populateQuestionList(TestTypeSelection.Teacher, ButtonSelection.Remove);
                    teacherQuestionsDataGrid.SelectedIndex = (tempIndex >= teacherQuestionList.Count) ? teacherQuestionList.Count - 1 : tempIndex;
                    teacherQuestionsDataGrid.Focus();
                }
            }
        }

        private void saveQuestionnaire(TestTypeSelection? testTypeSelection = null)
        {
            string parentQuestionPath = Directory.GetCurrentDirectory() + "/Questions/ParentPreloadQuestions.txt",
                teacherQuestionsPath = Directory.GetCurrentDirectory() + "/Questions/TeacherPreloadQuestions.txt";

            //Case for saving parent questions 
            if (testTypeSelection != TestTypeSelection.Teacher)
            {
                using (StreamWriter writer = File.CreateText(parentQuestionPath))
                {
                    for (int i = 0; i < parentQuestionList.Count; i++)
                    {
                        writer.WriteLine(String.Format("{0}. {1}", i + 1, parentQuestionList[i]));
                    }
                }

                //Should we put some sort of thing here so that we can detect whether questionnaires have been changed since their last save?
            }


            //Case for saving teacher quetsions
            if (testTypeSelection != TestTypeSelection.Parent)
            {
                using (StreamWriter writer = File.CreateText(teacherQuestionsPath))
                {
                    for (int j = 0; j < teacherQuestionList.Count; j++)
                    {
                        writer.WriteLine(String.Format("{0}. {1}", j+1, teacherQuestionList[j]));
                    }
                }

                //Same for this one.
            }
        }

        bool addPatientDocument2Database()
        {

            try
            {
                Google.Cloud.Firestore.DocumentReference docref = K.firestoreDB!.Collection("Patients").Document(patient!.patientPath);

                Dictionary<string, object> docData = new()
                {
                    { "patientCode", patient.patientCode },
                    { "parentCode",  patient.parentCode},
                    {"teacherCode", patient.teacherCode},
                    { "lastName", patient.lastname },
                    { "firstName", patient.firstname },
                    { "dateOfBirth", patient.dateOfBirth.ToString() },
                    { "age", patient.age },
                    { "Gender", patient.gender.ToString() },
                    { "teacherQuestions", teacherQuestionList },
                    { "parentQuestions", parentQuestionList },
                    { "AdministratorCode", _firebaseAuthClient.User.Uid }, 
                    { "teacherCanViewParentAnswers", false }
                };

                docref.SetAsync(docData);
                MessageBox.Show("Patient Created Successfully!", "Success");
                return true;

            } catch (Exception ex)
            {
                MessageBox.Show(String.Format("Failed to create new patient. Error: {0}", ex), "Error");
                return false;
            }

        }

        bool updateQuestionnaire()
        {


            return false;
        }

        internal enum ButtonSelection
        {
            Add,
            Edit,
            Remove,
            Reset
        }

        internal class QuestionCell
        {
            public string questionText { get; set; }
            public int questionNumber { get; set; }
            public QuestionCell(string questionText, int questionNumber)
            {
                this.questionText = questionText;
                this.questionNumber = questionNumber;
            }
        }
    }
}
