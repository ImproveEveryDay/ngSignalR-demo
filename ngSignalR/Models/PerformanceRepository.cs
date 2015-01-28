using System.Collections.Generic;
using System.Diagnostics;

namespace AngularSignal.Models
{
    public class PerformanceRepository
    {
        private readonly PerformanceCounterCategory[] categories;

        public PerformanceRepository()
        {
            categories = PerformanceCounterCategory.GetCategories();
        }

        public PerformanceCounterCategory[] Categories
        {
            get { return categories; }
        }

        public List<Hardrive> GetDrivesInfo()
        {
            var drives = new List<Hardrive>();
            var driveInfo = System.IO.DriveInfo.GetDrives();
            foreach (var info in driveInfo)
            {
                if (info.IsReady)
                {
                    drives.Add(new Hardrive
                    {
                        DriveName = info.VolumeLabel,
                        FileSystem = info.DriveFormat,
                        UsedSpace = info.TotalSize - info.AvailableFreeSpace,
                        TotalSpace = info.TotalSize,
                        UsedPercent = (info.AvailableFreeSpace / info.TotalSize),
                        DriveLetter = info.Name
                    });
                }
             
            }

            return drives;
        }
    }
}