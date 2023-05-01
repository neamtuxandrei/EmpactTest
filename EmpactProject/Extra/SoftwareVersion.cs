namespace EmpactProject.Extra
{
    public class SoftwareVersion
    {
        public int MajorNumber { get; set; }
        public int MinorNumber { get; set;}
        public int BugFixNumber { get; set;}
        public string BranchNameAndNumber { get; set; }

        public string VersionNumber => $"{MajorNumber}.{MinorNumber}.{BugFixNumber}-{BranchNameAndNumber}";
        public SoftwareVersion(int majorNumber,int minorNumber,int bugFixNumber,string branchNameAndNumber)
        {
            MajorNumber = majorNumber;
            MinorNumber = minorNumber;
            BugFixNumber = bugFixNumber;
            BranchNameAndNumber = branchNameAndNumber;
        }
    }
            //List<string> _versionNumbers = new List<string>();
            //var item =  new SoftwareVersion(2, 5, 0, "dev.1");
            //var item2 = new SoftwareVersion(2, 4, 2, "5354");
            //var item3 = new SoftwareVersion(2, 4, 2, "test.675");
            //var item4 = new SoftwareVersion(2, 4, 1, "integration.1");
            //_versionNumbers.Add(item.VersionNumber);
            //_versionNumbers.Add(item2.VersionNumber);
            //_versionNumbers.Add(item3.VersionNumber);
            //_versionNumbers.Add(item4.VersionNumber);
    public class TestSoftwareVersion
    {
        public string GetProductionVersion(List<SoftwareVersion> versionNumbers)
        {
            foreach(var version in versionNumbers) 
            {
                var branchNameAndNumber  = version.VersionNumber.Split('-').Last();
                if (Int32.TryParse(branchNameAndNumber, out int num))
                {
                    return version.VersionNumber;
                }
            }
            return "No prod version.";

        }

    }


}
