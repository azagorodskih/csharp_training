using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [Binding]
    public class LoginSteps
    {
        private ApplicationManager app
        {
            get
            {
                return ApplicationManager.GetInstance();
            }
        }

        [Given(@"A user is logged out")]
        public void GivenAUserIsLoggedOut()
        {
            app.Auth.Logout();
        }

        //[When(@"I login with valid credentials")]
        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithValidCredentials(string username, string password)
        {
            AccountData account = new AccountData(username, password);
            ScenarioContext.Current.Add("account", account);
            app.Auth.Login(account);
        }
        
        //[When(@"I login with invalid credentials")]
        //public void WhenILoginWithInvalidCredentials()
        //{
        //    AccountData account = new AccountData("admin", "12345");
        //    ScenarioContext.Current.Add("account", account);
        //    app.Auth.Login(account);
        //}
        
        [Then(@"I have logged in")]
        public void ThenIHaveLoggedIn()
        {
            AccountData account = ScenarioContext.Current.Get<AccountData>("account");
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }
        
        [Then(@"I have not logged in")]
        public void ThenIHaveNotLoggedIn()
        {
            AccountData account = ScenarioContext.Current.Get<AccountData>("account");
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
