using System;
using System.Runtime.Serialization;
using System.Xml;
using LearningPlatform.Data;
using LearningPlatform.Models;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Services
{
    public static class AdminService
    {
        public static void ExportDbToXml(ApplicationDbContext db)
        {
            SerializationHelper.CurrentDbContext = db;
            var dcs = new DataContractSerializer(typeof(EmulatedDb));
            var xmlw = XmlWriter.Create($"{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}" + "_db_backup.xml");
            dcs.WriteObject(xmlw, EmulatedDb.Instance);
            xmlw.Close();
        }

        public static void WipeDatabase(ApplicationDbContext db)
        {
            SerializationHelper.CurrentDbContext = db;
            SerializationHelper.WipeDatabase();
        }

        public static void RestoreDatabase(ApplicationDbContext db)
        {
            SerializationHelper.CurrentDbContext = db;
             var dcs = new DataContractSerializer(typeof(EmulatedDb));
             var xmlr = XmlReader.Create("2020-11-29_11-34-11-PM_db_backup.xml");
             var emulatedDb = (EmulatedDb) dcs.ReadObject(xmlr);
             xmlr.Close();           
            SerializationHelper.SaveAllToDatabase();
        }
    }
    
}