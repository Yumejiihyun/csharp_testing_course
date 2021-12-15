using NUnit.Framework;

namespace addressbooktests
{
    public class TestBase
    {
        protected ApplicationManager app;
        [SetUp]
        public void SetUp()
        {
            app = new ApplicationManager();
            app.LoginAsAdmin();
        }
        [TearDown]
        protected void TearDown()
        {
            app.Close();
        }
    }
}