namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    public class DraftsPage : MainPage
    {
        #region BaseElements
        private readonly BaseElement deleteMail = new BaseElement(By.XPath("//div[@role='button']//div[@class='Bn']"));
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
