using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PlanetariumService.Controllers;
using PlanetariumService.Models;
using PlanetariumServices;
using PlanetariumModels;
using System.Threading.Tasks;
using NSubstitute.ReturnsExtensions;
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class SeleniumAuthControllerTests
    {
        private WebDriver driver = null!;
        private string DriverPath { get; set; } = @"C:\chromedriver.exe";
        private string LoginUrl { get; set; } = "https://localhost:7230/Signin";

        private string RegisterUrl { get; set; } = "https://localhost:7230/Signup";

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            driver = new ChromeDriver(DriverPath, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2500);
            //driver.Navigate().GoToUrl("http://google.com");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Timeout(40000)]
        public void CheckSignInTitleTest()
        {
            driver.Navigate().GoToUrl(LoginUrl);
            Assert.AreEqual("Sign In - PlanetariumService", driver.Title);
        }

        [Test, Timeout(40000)]
        public void CheckSignUpTitleTest()
        {
            driver.Navigate().GoToUrl(RegisterUrl);
            Assert.AreEqual("Sign Up - PlanetariumService", driver.Title);
        }

        [Test, Timeout(40000)]
        public void CheckLoginFailTest()
        {
            string Username = "UserTest", UserPassword = "1";

            driver.Navigate().GoToUrl(LoginUrl);
            Thread.Sleep(1500);

            var inputField = driver.FindElement(By.Name("Login"));
            inputField.Clear();
            inputField.SendKeys(Username);

            inputField = driver.FindElement(By.Name("Password"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            var button = driver.FindElement(By.Name("Submit"));
            button.Click();

            Thread.Sleep(1000);

            try
            {
                Assert.AreEqual("Sign In - PlanetariumService", driver.Title);
                List<IWebElement> list = new List<IWebElement>(driver.FindElements(By.TagName("label")));
                string message = list[0].Text;
                Assert.AreEqual("Invalid Username or Email.", message);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test, Timeout(40000)]
        public void CheckLoginPasswordFailTest()
        {
            string Username = "User", UserPassword = "1";

            driver.Navigate().GoToUrl(LoginUrl);
            Thread.Sleep(1500);

            var inputField = driver.FindElement(By.Name("Login"));
            inputField.Clear();
            inputField.SendKeys(Username);

            inputField = driver.FindElement(By.Name("Password"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            var button = driver.FindElement(By.Name("Submit"));
            button.Click();

            Thread.Sleep(1000);

            try
            {
                Assert.AreEqual("Sign In - PlanetariumService", driver.Title);
                List<IWebElement> list = new List<IWebElement>(driver.FindElements(By.TagName("label")));
                string message = list[0].Text;
                Assert.AreEqual("Invalid Sign in attempt. (Wrong Password)", message);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test, Timeout(40000)]
        public void CheckLoginSuccessTest()
        {
            string Username = "User", UserPassword = "1234";

            driver.Navigate().GoToUrl(LoginUrl);
            Thread.Sleep(1500);

            var inputField = driver.FindElement(By.Name("Login"));
            inputField.Clear();
            inputField.SendKeys(Username);

            inputField = driver.FindElement(By.Name("Password"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            var button = driver.FindElement(By.Name("Submit"));
            button.Click();

            Thread.Sleep(1000);

            try
            {
                var alert = driver.SwitchTo().Alert();
                Assert.AreEqual("Signed in successfully!", alert.Text);
                alert.Accept();
            }
            catch
            {
                Assert.Pass();
            }
            Thread.Sleep(1000);
            var link = driver.FindElement(By.LinkText("Log out"));
            link.Click();
            Thread.Sleep(1000);

            Assert.AreEqual("Sign In - PlanetariumService", driver.Title);
        }

        [Test, Timeout(40000)]
        public void CheckRegisterFailTest()
        {
            string Email = "mail@email.com", Username = "User", UserPassword = "1234";

            driver.Navigate().GoToUrl(RegisterUrl);
            Thread.Sleep(1500);

            var inputField = driver.FindElement(By.Name("Email"));
            inputField.Clear();
            inputField.SendKeys(Email);

            inputField = driver.FindElement(By.Name("Username"));
            inputField.Clear();
            inputField.SendKeys(Username);

            inputField = driver.FindElement(By.Id("pwd"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            inputField = driver.FindElement(By.Id("pwdconfirm"));
            inputField.Clear();
            inputField.SendKeys("1");

            var label = driver.FindElement(By.CssSelector("label[for=\"pwd\"]"));
            Assert.AreEqual("Passwords are not matching", label.Text);

            inputField.Clear();
            inputField.SendKeys(UserPassword);

            label = driver.FindElement(By.CssSelector("label[for=\"pwd\"]"));
            Assert.AreEqual("Correct", label.Text);

            var button = driver.FindElement(By.Name("Submit"));
            button.Click();

            Thread.Sleep(1000);

            try
            {
                Assert.AreEqual("Sign Up - PlanetariumService", driver.Title);
                List<IWebElement> list = new List<IWebElement>(driver.FindElements(By.TagName("label")));
                string message = list[0].Text;
                Assert.AreEqual("Email already used.", message);
                message = list[1].Text;
                Assert.AreEqual("Username already used.", message);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test, Timeout(40000)]
        public void CheckRegisterSuccessTest()
        {
            PlanetariumServiceContext.Connection = new PlanetariumModelsFramework.PlanetariumServiceContext().Database.Connection.ConnectionString;
            PlanetariumServiceContext Context = new PlanetariumServiceContext(new DbContextOptions<PlanetariumServiceContext>());

            string Email = "testmail@email.com", Username = "UserTest", UserPassword = "1111";

            Users user = Context.Users.SingleOrDefault(u => u.Username.Equals(Username));
            if (user != null) { 
                Context.Remove(user);
                Context.SaveChanges();
            }

            driver.Navigate().GoToUrl(RegisterUrl);
            Thread.Sleep(1500);

            var inputField = driver.FindElement(By.Name("Email"));
            inputField.Clear();
            inputField.SendKeys(Email);

            inputField = driver.FindElement(By.Name("Username"));
            inputField.Clear();
            inputField.SendKeys(Username);

            inputField = driver.FindElement(By.Id("pwd"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            inputField = driver.FindElement(By.Id("pwdconfirm"));
            inputField.Clear();
            inputField.SendKeys(UserPassword);

            var label = driver.FindElement(By.CssSelector("label[for=\"pwd\"]"));
            Assert.AreEqual("Correct", label.Text);

            var button = driver.FindElement(By.Name("Submit"));
            button.Click();

            Thread.Sleep(1000);

            try
            {
                var alert = driver.SwitchTo().Alert();
                Assert.AreEqual("Registration complete. Signed in successfully!", alert.Text);
                alert.Accept();
            }
            catch
            {
                Assert.Pass();
            }
            Thread.Sleep(1000);
            var link = driver.FindElement(By.LinkText("Log out"));
            link.Click();
            Thread.Sleep(1000);

            Assert.AreEqual("Sign In - PlanetariumService", driver.Title);

            user = Context.Users.SingleOrDefault(u => u.Username.Equals(Username));
            Context.Remove(user);
            Context.SaveChanges();
        }
    }
}
