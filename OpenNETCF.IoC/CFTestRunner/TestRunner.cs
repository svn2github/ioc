

namespace OpenNETCF.MsTest
{
    class TestRunner
    {
        static void Main(string[] args)
        {
            #if !DEBUG
            throw new Exception("You must run TestRunner in Debug mode");
            #endif

            var @params = Deployment.TranslateArgs(args);
            Deployment.Manager.Deploy(@params);

            // Set your custom reporter here
            // Deployment.Manager.Reporter = new MyReporter();

            Deployment.Manager.Report();
        }
    }
}
