namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;

    public class EmailPage
    {
        #region BaseElements
        private readonly BaseElement nameBox = new BaseElement(By.XPath("//*[@name='to']"));
        private readonly BaseElement nameBoxInDraft = new BaseElement(By.XPath("//*[@enctype='multipart/form-data']//table//span[@email]"));
        private readonly BaseElement subjectBox = new BaseElement(By.CssSelector("input[name='subjectbox']"));
        private readonly BaseElement subjectInDraft = new BaseElement(By.XPath("//*[@class='aYF']"));
        private readonly BaseElement bodyBox = new BaseElement(By.CssSelector("div[aria-label='Тело письма']"));
        private readonly BaseElement btnCloseDraft = new BaseElement(By.CssSelector("*[alt='Закрыть']"));
        private readonly BaseElement btnSend = new BaseElement(By.XPath("//div[@role='button' and contains(.,'Отправить')]"));
        #endregion

        #region Methods
        /// <summary>
        /// Populates fields of the mail with specified parameters
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="subject"></param>
        /// <param name="text"></param>
        public void CreateDraft(string mailTo, string subject, string text)
        {
            this.nameBox.SendKeys(mailTo);
            this.subjectBox.SendKeys(subject);
            this.bodyBox.SendKeys(text);
        }

        /// <summary>
        /// Saves and closes the opened draft
        /// </summary>
        public void SaveAndCloseDraft()
        {
            this.btnCloseDraft.Click();
            this.SwitchToMainPage();
        }

        /// <summary>
        /// Sends the opened draft
        /// </summary>
        public void SendDraft()
        {
            this.btnSend.Click();
            this.SwitchToMainPage();
        }

        /// <summary>
        /// Gets text of the email specified in 'To' field of the draft
        /// </summary>
        /// <returns>Text of the email specified in 'To' field</returns>
        public string GetDraftMailToText()
        {
            return this.nameBox.GetAttribute("value");
        }

        /// <summary>
        /// Gets the subject text specified in 'Subject' field of the draft 
        /// </summary>
        /// <returns>Subject text specified in 'Subject' field</returns>
        public string GetDraftSubjectText()
        {
            return this.subjectInDraft.GetText();
        }

        /// <summary>
        /// Gets the body text specified in body of the draft 
        /// </summary>
        /// <returns>Body text specified in body</returns>
        public string GetBodyText()
        {
            return this.bodyBox.GetText();
        }

        /// <summary>
        /// Switches from the email frame to the main frame
        /// </summary>
        private void SwitchToMainPage()
        {
            Browser.GetDriver().SwitchTo().DefaultContent();
        }
        #endregion
    }
}
