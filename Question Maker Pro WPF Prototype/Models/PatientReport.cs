using Google.Cloud.Firestore;
using Question_Maker_Pro_WPF_Prototype.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Question_Maker_Pro_WPF_Prototype
{
    public class PatientReport
    {
        private Dictionary<string, Answers> _reportAnswers = new Dictionary<string, Answers>();
        private string _lastName, _firstName, _parentOrTeacher, _documentPath;
        private TimeOfDay _timeOfDay;
        private DateTime _timestamp;

        public Dictionary<string, Answers> reportAnswers { get => _reportAnswers; private set => _reportAnswers = value; }
        public string lastName { get => _lastName; private set => _lastName = value; }
        public string firstName { get => _firstName; private set => _firstName = value; }
        public string parentOrTeacher { get => _parentOrTeacher; private set => _parentOrTeacher = value; }
        public string documentPath { get => _documentPath; private set => _documentPath = value; }
        public TimeOfDay timeOfDay { get => _timeOfDay; private set => _timeOfDay = value; }
        public DateTime timestamp { get => _timestamp; private set => _timestamp = value; }


        public PatientReport(DocumentSnapshot document) {
            //this.reportAnswers = document.GetValue<Dictionary<string, Answers>>("Answers");
            this.lastName = document.GetValue<string>("answererLastName");
            this.firstName = document.GetValue<string>("answererFirstName");
            this.parentOrTeacher = document.GetValue<string>("parentOrTeacher");
            this.timestamp = document.GetValue<Timestamp>("Timestamp").ToDateTime();
            this.documentPath = document.Id;

            if (Enum.TryParse(typeof(TimeOfDay), document.GetValue<string>("timeOfDay"), out var timeOfDayString))
            {
                this.timeOfDay = (TimeOfDay)timeOfDayString!;
            } else
            {
                this.timeOfDay = TimeOfDay.morning; //this should theoretically be unreachable. But we'll put this here just in case.
            }

            populateReportAnswers(document.GetValue<Dictionary<string, string>>("Answers"));
        }

        private void populateReportAnswers(Dictionary<string, string> answerDict)
        {
            foreach (var answerString in answerDict.Keys)
            {
                
                if (Enum.TryParse<Answers>(answerDict[answerString], out var answer))
                {
                    reportAnswers.Add(answerString, answer);
                }
            }
        }

    }
}
