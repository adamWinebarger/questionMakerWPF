using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Question_Maker_Pro_WPF_Prototype
{
    public class Patient
    {
        public string lastname { get; private set; }
        public string firstname { get; private set; }
        public string patientCode { get; private set; }
        public Gender gender { get; private set; }
        public DateOnly dateOfBirth { get; private set; }
        public int age { get; set; }
        public List<string> parentQuestions { get; set; }
        public List<string> teacherQuestions { get; set; }
        public string patientPath { get; private set; }
        public string parentCode { get; private set; }
        public string teacherCode { get; private set; }

        public List<PatientReport> patientReportList { get; set; } = new();

        public Patient(string lastname, string firstname, Gender gender, DateOnly dateOfBirth)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            age = DateOnly.FromDateTime(DateTime.Now).Year - dateOfBirth.Year;
            if (DateOnly.FromDateTime(DateTime.Now).DayOfYear < dateOfBirth.DayOfYear)
                age--;
            parentQuestions = new();
            teacherQuestions = new();
            patientCode = sha256Hash();
            this.patientPath = String.Format("{0}, {1} ({2})", lastname, firstname, patientCode);

            Random rand = new();

            this.parentCode = firstname[0].ToString().ToUpper() + lastname[0].ToString().ToUpper() + patientCode.Substring(0, rand.Next(6, 11));

            int randomLength2 = rand.Next(6, 11);

            this.teacherCode = firstname[0].ToString().ToUpper() + lastname[0].ToString().ToUpper() + 
                patientCode.Substring(patientCode.Length - randomLength2, randomLength2);
        }

        public Patient(DocumentSnapshot document)
        {
            //Make sure to have that document.Exists catchment before instantiating these classes out in the PatientDataPage thing
            //gotta make sure that the document we're throwing in actually exists. 
            firstname = document.GetValue<string>("firstName");
            lastname = document.GetValue<string>("lastName");
            dateOfBirth = DateOnly.Parse(document.GetValue<string>("dateOfBirth"));
            gender = document.GetValue<string>("Gender").Equals("male") ? Gender.male : Gender.female;

            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age--;
            }

            parentCode = document.GetValue<string>("parentCode");
            teacherCode = document.GetValue<string>("teacherCode");
            patientCode = document.GetValue<string>("patientCode");
            patientPath = document.Id.ToString();

            parentQuestions = document.GetValue<List<String>>("parentQuestions");
            teacherQuestions = document.GetValue<List<String>>("teacherQuestions");

            //Will we want to be able to view whether teachers can view parent questionnaires?

            //We may want to incorporate our questionnaire stuff into this... I don't know... we'll come back to that.
        }

        public string sha256Hash()
        {
            string plainText = string.Format("{0}, {1} - {2}, {3} {4}", lastname,
                firstname, gender.ToString(), dateOfBirth.ToString(), DateTime.UtcNow
                .ToString("yyyy'-'MMM'-'dd HH':'mm':'ss 'GMT' "));
            StringBuilder sb = new();

            using (var hash = SHA1.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] output = hash.ComputeHash(enc.GetBytes(plainText));

                foreach (byte b in output)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

    }
}
