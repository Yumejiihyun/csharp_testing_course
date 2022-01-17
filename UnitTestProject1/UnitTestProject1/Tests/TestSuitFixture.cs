using NUnit.Framework;

namespace addressbooktests
{
    [SetUpFixture]
    public class TestSuitFixture
    {
        private ApplicationManager app;

        [TearDown]
        public void TearDown()
        {
            app = ApplicationManager.GetInstance();
            app.driver.Close();
        }
    }
}
