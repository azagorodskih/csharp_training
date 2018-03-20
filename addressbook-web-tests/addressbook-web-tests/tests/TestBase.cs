using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            //app = new ApplicationManager();
            //app.Navigator.OpenHomePage();
            //app.Auth.Login(new AccountData("admin", "secret"));

            app = ApplicationManager.GetInstance();
            //app.Auth.Login(new AccountData("admin", "secret"));
        }

        //[TearDown]
        //public void TeardownTest()
        //{
        //    app.Stop();
        //}  

        public static Random rnd = new Random();

        public static string GenerateRandomString(int length)
        {            
            int l = Convert.ToInt32((rnd.NextDouble() * length));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(rnd.NextDouble() * 65 + 32)));

            }
            return builder.ToString();
        }
    }
}
