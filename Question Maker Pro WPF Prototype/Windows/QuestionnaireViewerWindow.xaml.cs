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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Question_Maker_Pro_WPF_Prototype.Windows
{
    /// <summary>
    /// Interaction logic for QuestionnaireViewerWindow.xaml
    /// </summary>
    public partial class QuestionnaireViewerWindow : Window
    {
        Patient patient;
        PatientReport patientReport;

        Dictionary<Answers, string> answermap = new Dictionary<Answers, string>();

        public QuestionnaireViewerWindow(Patient patient, PatientReport patientReport)
        {
            this.patient = patient;
            this.patientReport = patientReport;
            populateAnswerMap();
            InitializeComponent();
            loadDataGridItemSource();
            reportTitleTextBlock.Text = String.Format("Report by {0} ({1})\nSubmitted on {2}\nReport for time of day: {3}", 
                patientReport.firstName + " " + patientReport.lastName, patientReport.parentOrTeacher, patientReport.timestamp, 
                patientReport.timeOfDay);
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void populateAnswerMap()
        {
            answermap.Add(Answers.notAtAll, "Not at all");
            answermap.Add(Answers.sometimes, "Sometimes");
            answermap.Add(Answers.alot, "Alot");
            answermap.Add(Answers.always, "Always");
        }

        void loadDataGridItemSource()
        {
            Dictionary<string, string> reportAnswers = new();

            foreach (string key in patientReport.reportAnswers.Keys)
            {
                reportAnswers.Add(key, answermap[patientReport.reportAnswers[key]]);
            }

            questionnaireDataGrid.ItemsSource = reportAnswers;
        }
    }
}
