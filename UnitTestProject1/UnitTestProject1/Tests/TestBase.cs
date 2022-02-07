using NUnit.Framework;
using System;
using System.Text;

namespace addressbooktests
{
    public class TestBase
    {
        protected ApplicationManager app;
        private static readonly Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomIntString(int max)
        {
            return rnd.Next(max).ToString();
        }

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