using NUnit.Framework;

namespace addressbooktests
{
    public class TestBase
    {
        protected ApplicationManager app;
        [SetUp]
        public virtual void SetUp()
        {
            app = ApplicationManager.GetInstance();
            app.LoginAsAdmin();
        }

        [TearDown]
        public void TearDown() => app.LoginHelper.OpenHomePage();
    }

    public class LoginTestBase : TestBase
    {
        [SetUp]
        public override void SetUp()
        {
            app = ApplicationManager.GetInstance();
        }
    }
}