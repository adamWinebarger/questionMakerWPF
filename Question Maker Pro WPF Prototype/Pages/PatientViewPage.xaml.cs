using Google.Cloud.Firestore;
using Question_Maker_Pro_WPF_Prototype.Enums;
using Question_Maker_Pro_WPF_Prototype.Windows;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PatientViewPage.xaml
    /// </summary>
    public partial class PatientViewPage : Page
    {
        private Patient patient;
        private DateRange selectedDateRange;
        List<PatientReport>  viewablePatientReports;

        private Dictionary<String, DateRange> dateRangeMap = new Dictionary<String, DateRange>
        {
            { "Today", DateRange.Today },
            { "This Week", DateRange.Week },
            { "This Month", DateRange.Month },
            { "All Time", DateRange.AllTime },
            { "Specify Date Range", DateRange.SpecifyRange }
        };

        public PatientViewPage(Patient patient)
        {
            this.patient = patient;
            selectedDateRange = DateRange.AllTime;
            InitializeComponent();

            //So this is apparently where we decided to set up the date-range map... I might come refactor this at some point.
            setupComboBoxes();
            fromDateStackPanel.Visibility = Visibility.Collapsed;
            toDateStackPanel.Visibility = Visibility.Collapsed;

            //populateReportsDataGrid();
            grabReportsFromDatabase();
            populateDataGrid2();
        }

        //Methods tied to the xaml stuff
        private void button_click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Content;

            if (tag == backButton.Content)
            {
                NavigationService.GoBack();
            }
            else if (tag == searchButton.Content)
            {
                // This is where we'll handle our logic for the search logic for when the search button is clicked.
                // Basically, we'll want it to look and see if one of our presets is selected, and if it is, to load reports based on the specifed
                // criteria. But if the "specific dates" option is selected, then we'll also need to check for valid dates in the thing and then do our 
                // report search based on that.
                switch (filterOptionsComboBox.SelectedIndex)
                {
                    case 0:
                        selectedDateRange = DateRange.Today; break;
                    case 1:
                        selectedDateRange = DateRange.Week; break;
                    case 2:
                        selectedDateRange = DateRange.Month; break;
                    case 3:
                        selectedDateRange = DateRange.AllTime; break;
                    case 4:
                        selectedDateRange = DateRange.SpecifyRange; break;
                    default:
                        MessageBox.Show("This should theoretically be unreachable");
                        selectedDateRange = DateRange.Week;
                        break;
                }
                populateDataGrid2();

            }
            else if (tag == viewReportButton.Content)
            {
                // This button will look at what report has been selected within our list and then will have to load some kind of page/window to show
                // us the contents of that specific report. We'll also probably want some additional info in there like the date, time of day, who took
                // the questionnaire, and what their relationship to the child is. 
                viewQuestionnaire();

            }
            else if (tag == questionDataButton.Content)
            {
                /* This will do a similar thing... actually, no it won't. With this one, we're going to be loading the aggregate data for all the 
                 * questionnaires and looking at trends in the data (filter by parent/teacher, time of day, and recency). Kind of like what we did with
                 * the dataviewer in the Flutter application. The main difference here is that we can have multiple infographics on the same scrollable
                 * window.
                 * 
                 * The question is do we just want to push the reports that we have from the current page, or should we re-make the calls to the database.
                 * I'm leaning towards the latter even though it's a little more expensive. At least that way we can allow them to filter by the things the
                 * reports were filtered for on the previous page and I think it will be a bit more intuitive for the end-user
                */
                NavigationService.Navigate(new ReportDataVizPage(patient, 
                    filterOptionsComboBox.SelectedIndex, parentOrTeacherCombobox.SelectedIndex, timeOfDayCombobox.SelectedIndex),
                    UriKind.Relative);

            }
            else
            {
                MessageBox.Show("This should have been theoretically unreachable. If you're seeing this message then there is a problem");
            }
        }

        //Combobox helper functions. I think only the first one should be needed but I guess I'll leave them all in for the time being.
        private void options_combobox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(filterOptionsComboBox.SelectedItem.ToString());
            selectedDateRange = dateRangeMap[filterOptionsComboBox.SelectedItem.ToString()!];

            if (selectedDateRange == DateRange.SpecifyRange)
            {
                fromDateStackPanel.Visibility = Visibility.Visible;
                toDateStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                fromDateStackPanel.Visibility = Visibility.Collapsed;
                toDateStackPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void timeOfDayCombobox_selection_changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void parentOrTeacherCombobox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void patientReportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void onRowSelection(object sender, RoutedEventArgs e)
        {

        }

        private void onRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewQuestionnaire();
        }


        /* So I think we should rework the logic here a little bit...
         *  Instead of making an async call for whatever criteria is requested from the database, I think we should instead make a call for all
         *  of the questionnaires and then just filter things out of the datagrid based on the criteria specified on the right side of the screen
         *  
         *  It's a little bit more expensive upfront (and may end up not being the best solution if a single child has a massive amount of reports
         *  over the course of multiple years or something). But if we make that one call to the database when it's time to load this thing, then
         *  we can just filter out what doesn't meet the criteria so it's stored in memory but not viewable and then we won't have to make any 
         *  additional function calls here *or* in the data visualizer and everything will just be... chillin.
         */

        async void grabReportsFromDatabase()
        {
            QuerySnapshot querySnapshot = await K.firestoreDB!.Collection("Patients").Document(patient.patientPath)
                .Collection("Answers").GetSnapshotAsync();

            List<PatientReport> patientReportList = new();

            foreach (DocumentSnapshot document in querySnapshot)
            {
                //PatientReport report = new(document);
                patientReportList.Add(new PatientReport(document));
            }

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(patientReportList);
            cv.SortDescriptions.Add(new SortDescription("timestamp", ListSortDirection.Descending));
            patient.patientReportList = patientReportList;
            //viewablePatientReports = patientReportList;
            patientReportDataGrid.ItemsSource = cv;

        }

        void populateDataGrid2()
        {
            List<PatientReport> viewableReports = new(patient.patientReportList);
            if (viewableReports.Count == 0) 
                return;

            //So here, we'll remove anything from viewable reports that aren't within the specified dateRange
            if (selectedDateRange != DateRange.AllTime || filterOptionsComboBox.SelectedIndex == -1)
            {
                if (selectedDateRange == DateRange.SpecifyRange)
                {
                    //TODO: come back and refactor this so that only one of the date pickers have to have a value to them.
                    if(!(fromDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue))
                    {
                        MessageBox.Show("One or more of the date-select boxes has an invalid value");
                        return;
                    }

                    DateTime fromDateTime = fromDatePicker.SelectedDate.Value,
                        toDateTime = toDatePicker.SelectedDate.Value;

                    viewableReports.RemoveAll(report => report.timestamp < fromDateTime || report.timestamp > toDateTime);
                    
                } else
                {
                    //int lookback = selectedDateRange switch
                    //{
                    //    DateRange.Today => 1,
                    //    DateRange.Week => 7,
                    //    DateRange.Month => 30,
                    //    _ => int.MaxValue
                    //};

                    DateTime fromDate = selectedDateRange switch
                    {
                        DateRange.Today => DateTime.Now.AddDays(-1),
                        DateRange.Week => DateTime.Now.AddDays(-7),
                        DateRange.Month => DateTime.Now.AddMonths(-1),
                        _ => DateTime.MinValue //sure, why not?
                    };

                    viewableReports.RemoveAll(report => report.timestamp < fromDate);
                }
            }

            //Alright. Now we need to set up the filter for timeofday
            if (timeOfDayCombobox.SelectedIndex != -1 && timeOfDayCombobox.SelectedIndex != timeOfDayCombobox.Items.Count - 1)
            {
                if(Enum.TryParse(timeOfDayCombobox.SelectedItem.ToString()!.ToLower(), out TimeOfDay timeOfDay)) {
                    viewablePatientReports.RemoveAll(report => report.timeOfDay != timeOfDay);
                }
            }

            //And then this will be for parent or teacher
            if (parentOrTeacherCombobox.SelectedIndex != -1 && parentOrTeacherCombobox.SelectedIndex != parentOrTeacherCombobox.Items.Count -1 )
            {
                viewableReports.RemoveAll(report => !report.parentOrTeacher
                    .ToLower().Equals(parentOrTeacherCombobox.SelectedItem.ToString()!.ToLower()));
            }

            //This should organize our list after we have removed everything we don't want. Might not be necessary tbh
            patientReportDataGrid.ItemsSource = viewableReports;

        }

        async void populateReportsDataGrid()
        {
            CollectionReference cr = K.firestoreDB!.Collection("Patients").Document(patient.patientPath).Collection("Answers");
            Query query;

            switch (selectedDateRange)
            {
                case DateRange.Today:
                    query = cr.WhereGreaterThanOrEqualTo("TimeStamp", Timestamp.FromDateTime(DateTime.Today.ToUniversalTime()));
                    break;
                case DateRange.Week:
                    query = cr.WhereGreaterThanOrEqualTo("TimeStamp", Timestamp.FromDateTime(DateTime.Today.ToUniversalTime().AddDays(-7)));
                    break;
                case DateRange.Month:
                    query = cr.WhereGreaterThanOrEqualTo("Timestamp", Timestamp.FromDateTime(DateTime.Today.ToUniversalTime().AddMonths(-1)));
                    break;
                case DateRange.AllTime:
                    //I guess nothing should happen with this one since we want all of them.
                    query = cr;
                    break;
                case DateRange.SpecifyRange:
                    //This one is the only one that will have to be slightly different. But it shouldn't be too much extra work.
                    if (!(fromDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue))
                    {
                        MessageBox.Show("One or more of the date-select boxes has an invalid value");
                        return;
                    }

                    Timestamp fromTimeStamp = Timestamp.FromDateTime(fromDatePicker.SelectedDate.Value.ToUniversalTime()),
                        toTimeStamp = Timestamp.FromDateTime(toDatePicker.SelectedDate.Value.ToUniversalTime());

                    query = cr.WhereGreaterThanOrEqualTo("TimeStamp", fromTimeStamp).WhereLessThanOrEqualTo("TimeStamp", toTimeStamp);
                    break;
                default:
                    //This should theoretically be unreachable
                    MessageBox.Show("Invalid Selection detected");
                    return;
            }

            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<PatientReport> patientReportList = new();

            MessageBox.Show(Timestamp.FromDateTime(DateTime.Today.ToUniversalTime().AddDays(-7)).ToString());

            foreach (DocumentSnapshot document in querySnapshot)
            {
                //PatientReport report = new(document);
                patientReportList.Add(new PatientReport(document));
            }

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(patientReportList);
            cv.SortDescriptions.Add(new SortDescription("timestamp", ListSortDirection.Descending));
            patient.patientReportList = patientReportList;
            patientReportDataGrid.ItemsSource = cv;
        }

        private void setupComboBoxes()
        {

            filterOptionsComboBox.ItemsSource = dateRangeMap.Keys.ToList();
            fromDateStackPanel.Visibility = Visibility.Collapsed;
            toDateStackPanel.Visibility = Visibility.Collapsed;
            timeOfDayCombobox.ItemsSource = new List<String> { "Morning", "Afternoon", "Evening", "All" };
            parentOrTeacherCombobox.ItemsSource = new List<String> { "Parent", "Teacher", "Both" };
        }

        void viewQuestionnaire()
        {
            if (patientReportDataGrid.SelectedItem != null)
            {
                PatientReport? patientReport = patientReportDataGrid.SelectedItem as PatientReport;
                new QuestionnaireViewerWindow(patient, patientReport!).ShowDialog();

            }
        }
    }
}
