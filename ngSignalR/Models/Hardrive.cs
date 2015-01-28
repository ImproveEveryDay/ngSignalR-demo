namespace AngularSignal.Models
{
    public class Hardrive
    {
        public long UsedSpace { get; set; }
        public long TotalSpace { get; set; }
        public long UsedPercent { get; set; }
        public string DriveLetter { get; set; }
        public string DriveName { get; set; }
        public string FileSystem { get; set; }
    }
}