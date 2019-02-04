namespace DesignPatterns
{
    using DesignPatterns.Pages;
    using DesignPatterns.WebDriver;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GmailTest : BaseTest
    {
        #region Variables

        private string mailTo = "anastasiya.maniak@gmail.com";

        private string subject = "Subject " + DateTime.Now;

        private string text = "Text...";
        
        #endregion

        /// <summary>
        /// The test verifies that draft is created and sent.
        /// Test Scenario:
        /// 1. Login to the mail box.
        /// 2. Assert, that the login is successful: If there is button "Написать" login is succeessful.
        /// 3. Create a new mail(fill addressee, subject and body fields).
        /// 4. Save the mail as a draft.
        /// 5. Verify, that the mail presents in ‘Drafts’ folder.
        /// 6. Verify the draft content(addressee, subject and body – should be the same as in 3).  
        /// 7. Send the mail.
        /// 8. Verify, that the mail disappeared from ‘Drafts’ folder.
        /// 9. Verify, that the mail is in ‘Sent’ folder.
        /// 10. Log out.
        /// </summary>
        [Test]
        public void TestCreateAndSendDraft()
        {
            // Login to the mail box.
            HomePage homePage = Login();
            string writeBtnText = homePage.GetWriteBtnText();

            // Assert, that the login is successful: Если появляется кнопка Написать, значит вход выполнен успешно
            StringAssert.Contains("Написать", writeBtnText, "Login failed.");

            // Create a new mail(fill addressee, subject and body fields).
            EmailPage emailPage = homePage.ClickWriteBtn();
            emailPage.CreateDraft(this.mailTo, this.subject, this.text);

            // Save the mail as a draft.
            emailPage.SaveAndCloseDraft();

            // Verify, that the mail presents in ‘Drafts’ folder.
            DraftsPage draftsPage = homePage.OpenDrafts();
            string subjectOfCreatedDraft = draftsPage.GetMailSubjectText(this.subject);
            Assert.AreEqual(this.subject, subjectOfCreatedDraft, "Draft wasn't saved");

            // Verify the draft content(addressee, subject and body – should be the same as in 3). 
            emailPage = draftsPage.OpenMailBySubject(this.subject);
            string draftBody = emailPage.GetBodyText();
            string draftMailTo = emailPage.GetDraftMailToText();
            string draftSubject = emailPage.GetDraftSubjectText();

            StringAssert.Contains(this.mailTo, draftMailTo, "TO value is not expected");
            StringAssert.Contains(this.subject, draftSubject, "Subject value is not expected");
            StringAssert.Contains(this.text, draftBody,  "Body value is not expected");

            // Send the mail.
            emailPage.SendDraft();

            // Verify, that the mail disappeared from ‘Drafts’ folder.
            draftsPage.RefreshPage();
            bool isDraftDisplayed = draftsPage.IsMailDisplayed(this.subject);
            Assert.IsFalse(isDraftDisplayed);

            // Verify, that the mail is in ‘Sent’ folder.
            SentPage sentPage = draftsPage.OpenSent();
            string subjectOfSentMail = sentPage.GetMailSubjectText(this.subject);
            Assert.AreEqual(this.subject, subjectOfSentMail, "Mail is not in 'Sent' folder");

            // Log out.
            sentPage.Logout();
        }
     
        /// <summary>
        /// The test verifies that the mail is sent and deleted.
        /// Test Scenario:
        /// 1. Login to the mail box.
        /// 2. Send a mail.
        /// 3. Delete sent mail.
        /// 4. Verify, that the mail disappeared from ‘Sent’ folder.
        /// 5. Verify, that the mail is in ‘Trash’ folder
        /// 6. Log out.
        /// </summary>
        [Test]
        public void TestSendAndDeleteMail()
        {
            // Login to the mail box.
            HomePage homePage = Login();

            // Send a mail.
            EmailPage emailPage = homePage.ClickWriteBtn();
            emailPage.CreateDraft(this.mailTo, this.subject, this.text);
            emailPage.SendDraft();
            
            // Delete sent mail.
            SentPage sentPage = homePage.OpenSent();
            sentPage.SelectMailBySubject(this.subject);
            sentPage.DeleteSelectedMail();

            // Verify, that the mail disappeared from ‘Sent’ folder.
            sentPage.RefreshPage();
            bool isMailDisplayed = sentPage.IsMailDisplayed(this.subject);
            Assert.IsFalse(isMailDisplayed);

            // Verify, that the mail is in ‘Trash’ folder
            TrashPage trashPage = sentPage.OpenTrash();
            string subjectOfDeletedMail = trashPage.GetMailSubjectText(this.subject);
            Assert.AreEqual(this.subject, subjectOfDeletedMail, "The mail wasn't deleted");

            // Log out.
            trashPage.Logout();
        }
    
    /// <summary>
    /// The test verifies that search in drafts works and searched mail is deleted.
    /// Test Scenario:
    /// 1. Login to the mail box.
    /// 2. Create a new mail.
    /// 3. Save the mail as a draft.
    /// 4. Repeat steps 2-3 4 times.
    /// 5. Perform search in drafts.
    /// 6. Verify, that found draft matches searched term 
    /// 7. Delete searched draft.
    /// 8. Verify, that the mail disappeared from ‘Drafts’ folder.
    /// 9. Verify, that the mail is in ‘Trash’ folder
    /// 10. Log out.
    /// </summary>
    [Test]
        public void TestSearchAndDeleteDraftMail()
        {
            // Login to the mail box.
            HomePage homePage = Login();
            
            EmailPage emailPage;
            int lengthOfSubject = subject.Length;
            int num = 5;
            // Create a new mail.
            // Save the mail as a draft.
            // Repeat steps 2-3 4 times.
            do
            {
                string sub = subject.Substring(0, lengthOfSubject);
                emailPage = homePage.ClickWriteBtn();
                emailPage.CreateDraft(this.mailTo, sub, this.text);
                emailPage.SaveAndCloseDraft();
                lengthOfSubject--;
                num--;
            } while (num != 0);


            // Perform search in drafts.
            DraftsPage draftsPage = homePage.OpenDrafts();
            draftsPage.TypeInSearchBox(this.subject);
            draftsPage.ClickSearchBtn();
            string subjectOfFoundMail = draftsPage.GetMailSubjectText(this.subject);
            int numberOfFoundMails = draftsPage.GetNumberOfMailsDisplayed();

            // Verify, that found draft matches searched term 
            Assert.AreEqual(1, numberOfFoundMails, "The wrong number of mails was found");
            Assert.AreEqual(this.subject, subjectOfFoundMail, "The wrong mail was found");

            // Delete searched draft.
            homePage.SelectMailBySubject(this.subject);
            homePage.DeleteSelectedMail();

            // Verify, that the mail disappeared from ‘Drafts’ folder.
            draftsPage.RefreshPage();
            bool isMailDisplayed = draftsPage.IsMailDisplayed(this.subject);
            Assert.IsFalse(isMailDisplayed);

            // Verify, that the mail is in ‘Trash’ folder
            TrashPage trashPage = draftsPage.OpenTrash();
            string subjectOfDeletedMail = trashPage.GetMailSubjectText(this.subject);
            Assert.AreEqual(this.subject, subjectOfDeletedMail, "The mail wasn't deleted");

            // Log out.
            trashPage.Logout();
        }


        /// <summary>
        /// The test verifies that search in drafts works and searched mail is deleted.
        /// Test Scenario:
        /// 1. Login to the mail box.
        /// 2. Create a new mail.
        /// 3. Save the mail as a draft.
        /// 4. Repeat steps 2-3 4 times.
        /// 5. Perform search in drafts by clicking Enter key
        /// 6. Verify, that found draft matches searched term 
        /// 7. Delete searched draft via context menu.
        /// 8. Verify, that the mail disappeared from ‘Drafts’ folder.
        /// 9. Verify, that the mail is in ‘Trash’ folder
        /// 10. Log out.
        /// </summary>
        [Test]
        public void TestSearchAndDeleteDraftMailWithContextMenu()
        {
            // Login to the mail box.
            HomePage homePage = Login();

            EmailPage emailPage;
            int lengthOfSubject = subject.Length;
            int num = 5;
            // Create a new mail.
            // Save the mail as a draft.
            // Repeat steps 2-3 4 times.
            do
            {
                string sub = subject.Substring(0, lengthOfSubject);
                emailPage = homePage.ClickWriteBtn();
                emailPage.CreateDraft(this.mailTo, sub, this.text);
                emailPage.SaveAndCloseDraft();
                lengthOfSubject--;
                num--;
            } while (num != 0);


            // Perform search in drafts by clicking Enter key.
            DraftsPage draftsPage = homePage.OpenDrafts();
            draftsPage.TypeInSearchBox(this.subject);
            draftsPage.ClickEnterKey();
            string subjectOfFoundMail = draftsPage.GetMailSubjectText(this.subject);
            int numberOfFoundMails = draftsPage.GetNumberOfMailsDisplayed();

            // Verify, that found draft matches searched term 
            Assert.AreEqual(1, numberOfFoundMails, "The wrong number of mails was found");
            Assert.AreEqual(this.subject, subjectOfFoundMail, "The wrong mail was found");

            // Delete searched draft.
            homePage.DeleteMailWithContextMenu(this.subject);

            // Verify, that the mail disappeared from ‘Drafts’ folder.
            draftsPage.RefreshPage();
            bool isMailDisplayed = draftsPage.IsMailDisplayed(this.subject);
            Assert.IsFalse(isMailDisplayed);

            // Verify, that the mail is in ‘Trash’ folder
            TrashPage trashPage = draftsPage.OpenTrash();
            string subjectOfDeletedMail = trashPage.GetMailSubjectText(this.subject);
            Assert.AreEqual(this.subject, subjectOfDeletedMail, "The mail wasn't deleted");

            // Log out.
            trashPage.Logout();
        }
    }
}
