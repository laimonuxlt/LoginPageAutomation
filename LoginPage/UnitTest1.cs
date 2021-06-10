using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace LoginPage
{
    public class Tests
    {

        readonly string guardianLoginUrl = "https://barnehage.testaws.visma.com/SeleniumTestAutomation";
        readonly string employeeLoginUrl = "https://manage.barnehage.testaws.visma.com/SeleniumTestAutomation";
        readonly string guardian = "Laimonas Samalius";
        readonly string employee = "Laimonas Samalius";
        readonly string warningGuardianIsRequired = "Feltet må fylles inn.";



        IWebDriver chromeDriver;


        [SetUp]
        public void Setup()
        {
            chromeDriver = new ChromeDriver(@"C:\selenium-drivers");

        }

        [Test]
        public void LogInSelectedGuardian()

        {

            chromeDriver.Navigate().GoToUrl(guardianLoginUrl);

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);


            IWebElement ddlGuardian = chromeDriver.FindElement(By.XPath("//select[@id='employee']"));

            SelectElement ddlSelection = new SelectElement(ddlGuardian);

            ddlSelection.SelectByText(guardian);

            IWebElement btnLogin = chromeDriver.FindElement(By.XPath("//button[@title='Sign in'  or @title='Logg inn']"));


            btnLogin.Click();

            Thread.Sleep(1000);

            string actualUrl = chromeDriver.Url;

            string expectedUrl = "https://barnehage.testaws.visma.com/children/list";


            Assert.AreEqual(expectedUrl, actualUrl);


        }
        [Test]
        public void LogInWithoutSelectedGuardian()
        {

            chromeDriver.Navigate().GoToUrl(guardianLoginUrl);

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement btnLogin = chromeDriver.FindElement(By.XPath("//button[@title='Sign in'  or  @title='Logg inn']"));

            btnLogin.Click();

            IWebElement warning = chromeDriver.FindElement(By.XPath("//kid-validation-message/span"));

            string displayedWarningText = warning.Text;

            Assert.AreEqual(warningGuardianIsRequired, displayedWarningText);

        }



        [Test]
        public void LoginSelectedEmployee()
        {

            chromeDriver.Navigate().GoToUrl(employeeLoginUrl);

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement ddlEmployee = chromeDriver.FindElement(By.XPath("//*[@id='employee']"));

            SelectElement ddlSelection = new SelectElement(ddlEmployee);

            ddlSelection.SelectByText(employee);

            IWebElement btnLogin = chromeDriver.FindElement(By.XPath("//button[@class='btn green']"));


            btnLogin.Click();

            Thread.Sleep(1000);

            string actualUrl = chromeDriver.Url;

            string expectedUrl = "https://manage.barnehage.testaws.visma.com/children/list";

            Assert.AreEqual(expectedUrl, actualUrl);
        }


        [Test]
        public void CreateApplication()
        {
            chromeDriver.Navigate().GoToUrl(employeeLoginUrl);

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement ddlEmployee = chromeDriver.FindElement(By.XPath("//*[@id='employee']"));

            SelectElement ddlSelection = new SelectElement(ddlEmployee);

            ddlSelection.SelectByText(employee);

            IWebElement btnLogin = chromeDriver.FindElement(By.XPath("//button[@class='btn green']"));

            btnLogin.Click();

            Thread.Sleep(1000);

            chromeDriver.Navigate().GoToUrl("https://manage.barnehage.testaws.visma.com/admission/application");

            Thread.Sleep(5000);

            //IWebElement btnAddAplication = chromeDriver.FindElement(By.XPath("//button[contains(text(), 'Add application' or  'Ny søknad ')]"));

            //Thread.Sleep(2000);

            //btnAddAplication.Click();

            //IWebElement chkGenerateNin = chromeDriver.FindElement(By.XPath("//*[@id='generateIdentityNumber']"));

            //chkGenerateNin.Click();




        }


        [TearDown]
        public void CloseBrowser()
        {
            chromeDriver.Quit();
        }


    }


}