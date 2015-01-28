namespace AngularSignal.Models.Dto
{
    public class HardriveDto
    {
        public long UsedSpace { get; set; }
        public long TotalSpace { get; set; }
        public long UsedPercent { get; set; }
        public string DriveLetter { get; set; }
        public string DriveName { get; set; }
        public string FileSystem { get; set; }

        public static HardriveDto Map(Hardrive entity)
        {
            var dto = new HardriveDto
                {
                    DriveLetter = entity.DriveLetter,
                    DriveName = entity.DriveName,
                    FileSystem = entity.FileSystem,
                    UsedSpace = entity.UsedSpace,
                    TotalSpace = entity.TotalSpace,
                    UsedPercent = entity.UsedPercent
                };
            return dto;
        }
    }
}