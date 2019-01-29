namespace DesignPatterns.Pages
{
    using System;

    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    using Configuration = DesignPatterns.WebDriver.Configuration;

    public class PasswordPage
    {
        public PasswordPage()
        {
            PageFactory.InitElements(Browser.GetDriver(), this);
        }

        #region BaseElements
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "passwordNext")]
        private IWebElement passwordNext;
        #endregion

        #region Methods
        /// <summary>
        /// Populates password and clicks 'Next' 
        /// </summary>
        /// <returns>Home Page after password is submitted</returns>
        public HomePage GoToHomePage()
        {
            new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementIsVisible(By.Name("password")));
            this.password.SendKeys(Configuration.Pswrd);
            this.passwordNext.Click();
            return new HomePage();
        }
        #endregion
    }
}
