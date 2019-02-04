namespace DesignPatterns.Pages
{
    using DesignPatterns.WebDriver;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public abstract class MainPage
    {
        private string draftMask = "//*[@role='main']//div[@role='link']//span[contains(.,'{0}')]/span";

        private string selectMailMask =
            "(//*[@role='main']//div[@role='link']//span[contains(.,'{0}')]/span//ancestor::tr)/td/div[@role='checkbox']";


        #region BaseElements
        private readonly BaseElement btnWrite = new BaseElement(By.XPath("//div[@role='button' and contains(.,'Написать')]"));
        private readonly BaseElement drafts = new BaseElement(By.XPath("//*[@title='Черновики']"));
        private readonly BaseElement sent = new BaseElement(By.XPath("//*[@title='Отправленные']"));
        private readonly BaseElement more = new BaseElement(By.XPath("//*[@class='ait']"));
        private readonly BaseElement trash = new BaseElement(By.XPath("//*[@title='Корзина']"));
        private readonly BaseElement profileIcon = new BaseElement(By.XPath("//*[@class='gb_cb gbii']"));
        private readonly BaseElement btnExit = new BaseElement(By.XPath("//a[text()='Выйти']"));
        private readonly BaseElement searchBox = new BaseElement(By.XPath("//input[@aria-label='Поиск в почте']"));
        private readonly BaseElement btnSearch = new BaseElement(By.XPath("//button[@aria-label='Поиск в почте']"));
        private readonly By mailsDisplayed = By.XPath("//*[@role='main']//div[@role='link']");

        private BaseElement mailBySubject;

        private BaseElement selectMailBySubject;
        #endregion

        #region Methods
        /// <summary>
        /// Deletes the mail by specified subject
        /// </summary>
        public abstract void DeleteSelectedMail();

        /// Deletes the mail by specified subject via context menu
        /// </summary>
        /// <param name="subject"></param>
        public void DeleteMailWithContextMenu(string subject)
        {
            this.GetMailBySubject(subject);
            this.mailBySubject.DeleteWithContextMenu();
        }

        /// <summary>
        /// Gets text of 'Write' button
        /// </summary>
        /// <returns>Text of 'Write' button</returns>
        public string GetWriteBtnText()
        {
            return this.btnWrite.GetText();
        }

        /// <summary>
        /// Refreshes the current page
        /// </summary>
        public void RefreshPage()
        {
           Browser.GetDriver().Navigate().Refresh();
        }

        /// <summary>
        /// Clicks 'Write' button
        /// </summary>
        /// <returns>Email Page</returns>
        public EmailPage ClickWriteBtn()
        {
            this.btnWrite.Click();
            return new EmailPage();
        }

        /// <summary>
        /// Clicks 'Drafts' button 
        /// </summary>
        /// <returns>Drafts Page</returns>
        public DraftsPage OpenDrafts()
        {
            this.drafts.Click();
            return new DraftsPage();
        }

        /// <summary>
        /// Clicks 'Sent' button
        /// </summary>
        /// <returns>Sent Page</returns>
        public SentPage OpenSent()
        {
            this.sent.Click();
            return new SentPage();
        }

        /// <summary>
        /// Clicks 'More' and 'Trash' buttons
        /// </summary>
        /// <returns>Trash Page</returns>
        public TrashPage OpenTrash()
        {
            if (!this.more.Displayed)
            {
                this.more.Click();
            }

            this.trash.Click();
            return new TrashPage();
        }

        /// <summary>
        /// Log out of the Gmail
        /// </summary>
        public void Logout()
        {
            this.profileIcon.Click();
            this.btnExit.Click();
        }

        /// <summary>
        /// Gets number of the mails displayed on the page
        /// </summary>
        /// <returns>Mails count</returns>
        public int GetNumberOfMailsDisplayed()
        {
            return Browser.GetDriver().FindElements(mailsDisplayed).Count;
        }

        /// <summary>
        /// Gets the subject text of the mail that matches specified subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>Subject text of the mail</returns>
        public string GetMailSubjectText(string subject)
        {
            this.GetMailBySubject(subject);
            return this.mailBySubject.GetText();
        }

        /// <summary>
        /// Highlights mail by subject
        /// </summary>
        /// <param name="subject"></param>
        public void HighlightMailBySubject(string subject)
        {
            this.GetMailBySubject(subject);
            this.mailBySubject.JsHighlight();
        }

        /// <summary>
        /// Determines whether the mail that matches specified subject is displayed on the page
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>True if the mail is displayed</returns>
        public bool IsMailDisplayed(string subject)
        {
            this.GetMailBySubject(subject);
            return this.mailBySubject.Displayed;
        }

        /// <summary>
        /// Clicks the mail by specified subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>Email Page</returns>
        public EmailPage OpenMailBySubject(string subject)
        {
            this.GetMailBySubject(subject);
            this.mailBySubject.Click();
            return new EmailPage();
        }

        /// <summary>
        /// Selects the mail by specified subject
        /// </summary>
        /// <param name="subject"></param>
        public void SelectMailBySubject(string subject)
        {
            this.GetMailBySubject(subject);
            this.selectMailBySubject.Click();
        }

        /// <summary>
        /// Types search term in search box
        /// </summary>
        /// <param name="subject"></param>
        public void TypeInSearchBox(string subject)
        {
            this.searchBox.SendKeys(subject);
        }

        /// <summary>
        /// Clicks Search buttom
        /// </summary>
        public void ClickSearchBtn()
        {
            this.btnSearch.Click();
        }

        /// <summary>
        /// Clicks Enter on the Keyboard
        /// </summary>
        public void ClickEnterKey()
        {
            new Actions(Browser.GetDriver())
                .SendKeys(Keys.Enter)
                .Build().Perform();
        }       

        /// <summary>
        /// Determines elements Subject of the mail and Select by specified subject
        /// </summary>
        /// <param name="subject"></param>
        private void GetMailBySubject(string subject)
        {
            this.mailBySubject = new BaseElement(By.XPath(string.Format(this.draftMask, subject)));
            this.selectMailBySubject = new BaseElement(By.XPath(string.Format(this.selectMailMask, subject)));
        }
        #endregion
    }
}
