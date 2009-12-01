
namespace OpenNETCF.MsTest
{
    public class DeployParameters
    {
        internal DeployParameters(string testFolder)
        {
            TestAssemblyFolder = testFolder;
        }

        public DeployParameters()
        {
        }

        public string TestAssemblyFolder { get; set; }
        public string[] TestAssemblies { get; set; }
    }
}
