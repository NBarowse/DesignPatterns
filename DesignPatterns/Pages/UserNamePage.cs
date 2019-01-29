namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    using Configuration = DesignPatterns.WebDriver.Configuration;

    public class UserNamePage
    {
        #region BaseElements
        private readonly BaseElement userName = new BaseElement(By.Id("identifierId"));
        private readonly BaseElement userNameNext = new BaseElement(By.Id("identifierNext"));
        #endregion

        #region Methods
        /// <summary>
        /// Populates username and clicks 'Next'
        /// </summary>
        /// <returns>Password Page after username is submitted</returns>
        public PasswordPage GoToPasswordPage()
        {
            this.userName.SendKeys(Configuration.Mail);
            this.userNameNext.Click();
            return new PasswordPage();
        }
        #endregion
    }
}
