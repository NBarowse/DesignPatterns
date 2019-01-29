namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    public class DraftsPage : MainPage
    {
        #region BaseElements
        private readonly BaseElement deleteMail = new BaseElement(By.XPath("//div[@role='button']//div[@class='Bn']"));
        private readonly BaseElement deleteMailAfterSeacrh = new BaseElement(By.XPath("//div[@role='button' and @aria-label='Удалить']"));
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

        /// <summary>
        /// Deletes the mail by specified subject after search was performed
        /// </summary>
        /// <param name="subject"></param>
        public void DeleteMailBySubjectAfterSearch(string subject)
        {
            SelectMailBySubject(subject);
            this.deleteMailAfterSeacrh.Click();
        }
        #endregion
    }
}
