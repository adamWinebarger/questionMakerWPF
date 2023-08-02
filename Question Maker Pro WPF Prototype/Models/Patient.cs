using System;
using System.Collections.Generic;
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
        public int age { get; private set; }
        public List<string> testQuestions { get; set; }

        public Patient(string lastname, string firstname, Gender gender, DateOnly dateOfBirth)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            age = DateOnly.FromDateTime(DateTime.Now).Year - dateOfBirth.Year;
            if (DateOnly.FromDateTime(DateTime.Now).DayOfYear < dateOfBirth.DayOfYear)
                age--;
            testQuestions = new();
            patientCode = sha256Hash();
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
