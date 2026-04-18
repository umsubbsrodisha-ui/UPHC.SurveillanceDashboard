namespace UPHC.SurveillanceDashboard.Models
{
    //public class Notification
    //{
    //    public int Id { get; set; }
    //    public string UPHCName { get; set; }    
    //    public string DiseaseName { get; set; } = "";
    //    public bool IsChecked { get; set; }
    //    public DateTime Timestamp { get; set; } = DateTime.Now;
    //    public int patientId { get; set; }

    //    public int UPHCId { get; set; }
    //}


    public class Notification
    {
        public int Id { get; set; }

        public string DiseaseName { get; set; } = "";

        public bool IsChecked { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string Type { get; set; } = "";
        // "NewCase", "Update", "LabConfirmed", "Outbreak"

        // 🔗 FK → CaseRecord
        public int CaseRecordId { get; set; }
        public CaseRecord CaseRecord { get; set; }

        // 🔗 FK → UPHC (IMPORTANT for analytics)
        public int UPHCId { get; set; }
        public UPHC UPHC { get; set; }
    }

}
