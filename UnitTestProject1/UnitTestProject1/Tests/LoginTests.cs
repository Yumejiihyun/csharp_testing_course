using NUnit.Framework;

namespace addressbooktests
{
    public class LoginTests : LoginTestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            app.LoginHelper.LogOut();

            var account = new AccountData("admin", "secret");
            app.LoginHelper.Login(account);

            Assert.IsTrue(app.LoginHelper.IsLoggedIn());
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.LoginHelper.LogOut();

            var account = new AccountData("admin2", "123");
            app.LoginHelper.Login(account);

            Assert.IsFalse(app.LoginHelper.IsLoggedIn());
        }
    }
}
