namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    public class SentPage : MainPage
    {
        #region BaseElements
        private readonly BaseElement deleteMail = new BaseElement(By.XPath("//div[@gh='tm']//div[@role='button' and @title='Удалить']")); //div[@role='button' and @title='Удалить']"));
        #endregion

        #region Methods
        /// <summary>
        /// Deletes the mail by specified subject
        /// </summary>
        /// <param name="subject"></param>
        public override void DeleteMailBySubject(string subject)
        {
            SelectMailBySubject(subject);
            this.deleteMail.Click();
        }
        #endregion
    }
}
