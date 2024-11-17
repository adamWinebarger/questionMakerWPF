using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Json;

namespace Question_Maker_Pro_WPF_Prototype
{
    public static class K
    {

        public static FirestoreDb? firestoreDB = null;

        //public static string firestorePrivKeyID = (string)JObject.Parse(AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json")["private_key_id"]!;
        public static string adminKey = File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json") ? 
            JsonDocument.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"cloudfire_schoolquestiontester.json")).RootElement.GetProperty("private_key_id").ToString() : "";
    }
}
