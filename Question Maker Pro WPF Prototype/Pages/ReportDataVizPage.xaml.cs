using LiveCharts;
using LiveCharts.Wpf;
using Question_Maker_Pro_WPF_Prototype.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for ReportDataVizPage.xaml
    /// </summary>
    public partial class ReportDataVizPage : Page
    {
        private Patient patient;
        private int reportDataIndex = -1;
        private Dictionary<Answers, string> answerMap = new Dictionary<Answers, string>
        {
            { Answers.notAtAll, "Not at all" },
            { Answers.sometimes, "Sometimes" },
            { Answers.alot, "A lot" },
            { Answers.always, "Always" }
        };

        private Dictionary<String, DateRange> dateRangeMap = new Dictionary<String, DateRange>
        {
            { "Today", DateRange.Today },
            { "This Week", DateRange.Week },
            { "This Month", DateRange.Month },
            { "All Time", DateRange.AllTime },
            { "Specify Date Range", DateRange.SpecifyRange }
        };

        CompiledReportData reportData;
        AnswerData? selectedAnswerData;

        public ReportDataVizPage(Patient patient, int dateComboBoxIndex, int timeOfDayIndex, int ptIndex)
        {
            this.patient = patient;
            reportData = new(this.patient);
            selectedAnswerData = reportData.reportData.Count > 0 ? reportData.reportData[0] : null;
            reportDataIndex = reportData.reportData.Count > 0 ? 0 : -1;
            InitializeComponent();

            setupDataViz();
            setupComboBoxes();

            dateComboBox.SelectedIndex = dateComboBoxIndex;
            timeOfDayComboBox.SelectedIndex = timeOfDayIndex;
            parentOrTeacherComboBox.SelectedIndex = ptIndex;

            //MessageBox.Show($"{selectedAnswerData != null}", "Report Data");
        
        }

        private void button_click(object sender, RoutedEventArgs e)
        {

            var tag = ((Button)sender).Content;

            if (tag == return2DataViewButton.Content)
            {
                //Might change the content of this button at some point. But this is the simiplest thing that we'll need for right now
                NavigationService.GoBack();
            } else if (tag == nextButton.Content)
            {
                //We have nothing to put here just yet. But this will be the button functionality for changing between questions for 
                // the piechart... starting to wonder if we should use the WPF organic chart stuff for this. But I guess we can stick with this
                // for now.
                nextPressed();

            } else if (tag == backButton.Content)
            {
                prevPressed();
            } else if (tag == searchButton.Content)
            {
                searchPressed();
            } else 
            {
                MessageBox.Show("This should theoretically be unreachable. If you're seeing this, then something has gone wrong");
            }

        }

        private void comboBox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            /* So I think that we should have this method handle all the legwork when it comes to changing things in all of the comboboxes.
             * Not sure if I want it to auto-update or if we should have the search button update the parameters... tough to say right now
             * But with this we can have all 3 comboboxes do their thing in here. 
             * 
             * I'm realizing I might've overthought this. I might leave it alone for the moment other than making it so that the 
             * stack panel for custom date ranges appears when the requesite option is selected
             */

            var comboBoxName = ((ComboBox)sender).Name;

            if (comboBoxName == dateComboBox.Name) {

                //Date combobox has been changed. So essentially, we need to consider special cases for when something isn't selected,
                //or when the SelectDateRange option is selected. 
                if (dateComboBox.SelectedIndex == dateComboBox.Items.Count - 1)
                {
                    customDateRangeStackPanel.Visibility = Visibility.Visible;
                } else
                {
                    customDateRangeStackPanel.Visibility = Visibility.Collapsed;
                }

            } else if (comboBoxName == timeOfDayComboBox.Name) { 



            } else if (comboBoxName == parentOrTeacherComboBox.Name) {

            } else {
                //Theoretically unreachable
                MessageBox.Show("ComboBox Selection Changed: This should be unreachable");
            }

        }

        private void questionComboBox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            if (questionComboBox.SelectedIndex != -1)
            {
                selectedAnswerData = reportData.reportData[questionComboBox.SelectedIndex];
                reportDataIndex = questionComboBox.SelectedIndex;
                setupDataViz();
                questionComboBox.SelectedIndex = -1;
            }
        }

        //Button helper functions
        private void nextPressed()
        {
            try
            {
                if (reportDataIndex != -1 && reportDataIndex < reportData.reportData.Count - 1)
                {
                    selectedAnswerData = reportData.reportData[++reportDataIndex];
                    setupDataViz();
                } else
                {
                    //for now, let's just have nothing happen here
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prevPressed()
        {
            try
            {
                if (reportDataIndex > 0)
                {
                    selectedAnswerData = reportData.reportData[--reportDataIndex];
                    setupDataViz();
                } else
                {
                    //Probably also going to have nothing here
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchPressed()
        {
            /*
             * So this one is going to be a complex setup since we essentially need to go down the line of our various comboboxes and strip out
             * the reports we don't want in our compiled report data. Frankly, this is going to look very convoluted in hindsight. But doing it
             * this way will give us the least amount of rewrites.
             * 
             * So, first thing, we should check if the selected Indices for all of our combobox are at -1 or at combobox.Items.Count -1 and just
             * "recompile" with patient.reportdata if that's the case.
             * 
             * Barring that, we'll first want to make a shallow copy of patient.reportdata. Then go down the line checking each combobox to figure 
             * out what is going to get stripped out of our copied list. Since date will likely always be the first on to be stripped out, I think
             * that's the one to start with. After that, may as well go down the line (time of day, then teacher) doing the same thing.
             * 
             * Once that's been completed, we then run our compiledata function in from our CompiledReportData object that we have instantiated,
             * but with the list we created in here and then show our dataviz with that new list. The reason for doing it this way is that we 
             * will still have an untarnished copy of patient.reportdata to fall back on (so things removed aren't gone for good/won't require
             * exiting and re-entering this page) without us having to make additional calls to the Firestore database to re-grab that information.
             */

            if (allSelectionsAreSelected())
            {
                reportData.reportData = reportData.compileData(reportData.patient.patientReportList);
            }

            List<PatientReport> viewableReportList = new(reportData.patient.patientReportList);

            //Case for custom date-range selected in the combobox
            if (dateComboBox.SelectedIndex == dateComboBox.Items.Count - 1) {
                if (fromDateBox.SelectedDate == null && toDateBox.SelectedDate == null)
                {
                    MessageBox.Show("Both date selections appear to be empty. Please select a valid date-range for your search");
                    return;
                } else
                {
                    //Doing it this way makes it so that only one of the date boxes needs a value and it won't fuck everything up if the other is
                    // empty. Remember to go back and change the one on PatientView to be like this too. 
                    if (fromDateBox.SelectedDate != null)
                        viewableReportList.RemoveAll(report => report.timestamp < fromDateBox.SelectedDate.Value);
                    if (toDateBox.SelectedDate != null)
                        viewableReportList.RemoveAll(report => report.timestamp > toDateBox.SelectedDate.Value);
                }
            } else if (dateComboBox.SelectedIndex != -1 && dateComboBox.SelectedIndex != dateComboBox.Items.Count -2)
            {
                //bit of a weird way of doing it. But -1 indicates nothing has been selected and Count - 2 will always be "All time". So we don't 
                // need to do anything in this case. This catchment is basically looking to grab the -1, -7, -30 trend
                DateTime fromDate;

                switch (dateComboBox.SelectedIndex)
                {
                    case 0:
                        fromDate = DateTime.Now.AddDays(-1);
                        break;
                    case 1:
                        fromDate = DateTime.Now.AddDays(-7);
                        break;
                    case 2:
                        fromDate = DateTime.Now.AddMonths(-1);
                        break;
                    default:
                        //This should theoretically be unreachable if we do this right. 
                        fromDate = DateTime.Now; 
                        break;
                }

                viewableReportList.RemoveAll(report => report.timestamp < fromDate);
            } //this should be the end of our logic from the date combobox

            //Now we need to do our logic for... I guess let's do timeofday first
            if (timeOfDayComboBox.SelectedIndex > -1 && timeOfDayComboBox.SelectedIndex < timeOfDayComboBox.Items.Count -1)
            {

                if (Enum.TryParse(timeOfDayComboBox.SelectedItem.ToString()!.ToLower(), out TimeOfDay timeOfDay))
                {
                    //It's working. Just needed to make our string lowercase to make it match the enum values. 
                    viewableReportList.RemoveAll(report => report.timeOfDay != timeOfDay);
                }
            }

            //And lastly, we gotta do our logic for parents and teachers
            if (parentOrTeacherComboBox.SelectedIndex > -1 && parentOrTeacherComboBox.SelectedIndex < parentOrTeacherComboBox.Items.Count - 1) { 
                viewableReportList.RemoveAll(report => !report.parentOrTeacher
                    .ToLower().Equals(parentOrTeacherComboBox.SelectedItem.ToString()!.ToLower()));
            }

            //Now we move to the part where we are changing the report data and then reloading our dataviz thing.
            reportData.reportData = reportData.compileData(viewableReportList);
            selectedAnswerData = reportData.reportData.Count > 0 ? reportData.reportData[0] : null;
            reportDataIndex = reportData.reportData.Count > 0 ? 0 : -1;
            setupDataViz();
        }

        //Combobox helper methods
        private void setupComboBoxes()
        {
            //Loads all of our questions into the question combobox
            questionComboBox.ItemsSource = reportData.reportData.Select(item => item.question).ToList();

            dateComboBox.ItemsSource = dateRangeMap.Keys.ToList();
            customDateRangeStackPanel.Visibility = Visibility.Collapsed;
            timeOfDayComboBox.ItemsSource = new List<String> { "Morning", "Afternoon", "Evening", "All"};
            parentOrTeacherComboBox.ItemsSource = new List<String> { "Parent", "Teacher", "Both" };
        }

        private bool allSelectionsAreSelected()
        {
            bool allDatesSelected = dateComboBox.SelectedIndex == -1 || dateComboBox.SelectedIndex == dateComboBox.Items.Count - 1,
                allTimesOfDaySelected = timeOfDayComboBox.SelectedIndex == -1 ||
                    timeOfDayComboBox.SelectedIndex == timeOfDayComboBox.Items.Count - 1,
                allAnswerersSelected = parentOrTeacherComboBox.SelectedIndex == -1 ||
                    parentOrTeacherComboBox.SelectedIndex == parentOrTeacherComboBox.Items.Count - 1;

            return allDatesSelected && allTimesOfDaySelected && allAnswerersSelected;
        }

        //Data Viz setup
        private void setupDataViz()
        {

            dataVizPieChart.Series = new();
            if (selectedAnswerData == null)
            {
                questionLabel.Text = "There is nothing to display here";
                return;
            }

            questionLabel.Text = selectedAnswerData.question;
            

            foreach (var item in selectedAnswerData!.answerValues)
            {
                dataVizPieChart.Series.Add(new PieSeries
                {
                    Title = answerMap[item.answer],
                    Values = new ChartValues<int> { item.count },
                    DataLabels = item.count > 0 //should make it so data labels only show up for values over 0
                });
            }
        }

        internal class AnswerValues
        {
            public Answers answer { get; private set; }
            public int count = 0;

            public AnswerValues(Answers answer)
            {
                this.answer = answer;
            }

        }

        internal class AnswerData
        {
            public string question { get; private set; }
            public List<AnswerValues> answerValues { get; private set; }

            public AnswerData(string question)
            {
                this.question = question;
                this.answerValues = new();
                setupAnswerValues();
            }

            private void setupAnswerValues()
            {
                answerValues.Add(new(Answers.notAtAll));
                answerValues.Add(new(Answers.sometimes));
                answerValues.Add(new(Answers.alot));
                answerValues.Add(new(Answers.always));
            }

            public void add1(Answers answer)
            {
                var answerValue = answerValues.FirstOrDefault(av => av.answer == answer);

                if (answerValue != null)
                {
                    answerValue.count++;
                }
                else
                {
                    //this should be theoretically unreachable...
                }
            }
        }

        internal class CompiledReportData
        {
            public Patient patient { get; private set; }
            //public Dictionary<string, Dictionary<Answers, int>> reportdata { get; private set; }
            public List<AnswerData> reportData { get; set; }

            public CompiledReportData(Patient patient)
            {
                this.patient = patient;
                this.reportData = compileData(patient.patientReportList);
                //compileData();

            }

            public string printReportData()
            {
                StringBuilder sb = new();

                foreach (var data in reportData)
                {
                    sb.Append($"{data.question}\n");
                    foreach (var value in data.answerValues)
                    {
                        sb.Append($"\t{value.answer} : {value.count}\n");
                    }
                }

                return sb.ToString();
            }

            public List<AnswerData> compileData(List<PatientReport> patientReportList)
            {
                /*
                 * Alright. So let's write this out since this was a bit of a bear of a task doing it on the flutter app...
                 * Essentially, we need to take all of the reports associated with the patient in question for a given timeframe (may need to
                 * re-evaluate what we're passing in, here), then, for each report we need to look at every individual question. If the question exists
                 * in our current dictionary, then all we need to do is increment the corresponding answer value according to what's been input on the 
                 * report that we're looking at; and if the question isn't in our current keys, then we need to add it in as a key, then each answer 
                 * to the "value keys", and then 0 for the value values. Writing it out, this sounds a lot simpler than it was the first time. 
                 */

                List<AnswerData> answerDataList = new();

                foreach (PatientReport patientReport in patientReportList)
                {
                    foreach (var question in patientReport.reportAnswers.Keys)
                    {
                        var foundElement = answerDataList.FirstOrDefault(x => x.question == question);
                        if (foundElement == null)
                        {
                            AnswerData temp = new(question);
                            answerDataList.Add(temp);
                            foundElement = temp;
                        }

                        foundElement.add1(patientReport.reportAnswers[question]);

                    }
                }

                return answerDataList;
            }

            //protected Dictionary<string, Dictionary<Answers, int>> compileData()
            //{
            //    /*
            //     * Alright. So let's write this out since this was a bit of a bear of a task doing it on the flutter app...
            //     * Essentially, we need to take all of the reports associated with the patient in question for a given timeframe (may need to
            //     * re-evaluate what we're passing in, here), then, for each report we need to look at every individual question. If the question exists
            //     * in our current dictionary, then all we need to do is increment the corresponding answer value according to what's been input on the 
            //     * report that we're looking at; and if the question isn't in our current keys, then we need to add it in as a key, then each answer 
            //     * to the "value keys", and then 0 for the value values. Writing it out, this sounds a lot simpler than it was the first time. 
            //     */
            //    Dictionary<string, Dictionary<Answers, int>> reportdata = new();

            //    foreach (PatientReport patientReport in patient.patientReportList)
            //    {
            //        foreach (var question in patientReport.reportAnswers.Keys)
            //        {
            //            //Case for if we've not found an existing key within our reportdata
            //            if (reportdata.ContainsKey(question) == false)
            //            {
            //                reportdata.Add(question, new Dictionary<Answers, int>());
            //                reportdata[question] = Enum.GetValues(typeof(Answers)).Cast<Answers>().ToDictionary(answer => answer, value => 0);
            //            }

            //            if (reportdata.ContainsKey(question) == true){
            //                var innerDict = reportdata[question];
            //                Answers answer = patientReport.reportAnswers[question];
            //                innerDict[answer]++;
            //            }
            //        }
            //    }

            //    return reportdata;

            //}
        }
    }
}
