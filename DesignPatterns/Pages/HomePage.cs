namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    public class HomePage : MainPage
    {
        #region BaseElements
        private readonly BaseElement deleteMail = new BaseElement(By.XPath("//div[@gh='tm']//div[@role='button' and @title='Удалить']"));
        #endregion

        #region Methods
        /// <summary>
        /// Deletes the mail by specified subject
        /// </summary>
        public override void DeleteSelectedMail()
        {
            this.deleteMail.Click();
        }
        #endregion
    }
}
