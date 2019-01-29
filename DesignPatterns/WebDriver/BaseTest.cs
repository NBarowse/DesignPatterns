namespace DesignPatterns.WebDriver
{
    using DesignPatterns.Pages;
    using NUnit.Framework;

    public class BaseTest
    {
        protected static Browser Browser = Browser.Instance;

        /// <summary>
        /// Opens browser, maximizes window and navigates to start url
        /// </summary>
        [SetUp]
        public virtual void InitTest()
        {
            Browser = Browser.Instance;
            Browser.WindowMaximise();
            Browser.NavigateTo(Configuration.StartUrl);
        }

        /// <summary>
        /// //Quits the driver, closing every associated window
        /// </summary>
        [TearDown]
        public virtual void CleanTest()
        {
            Browser.Quit();
        }


        /// <summary>
        /// Login to Gmail
        /// </summary>
        /// <returns>Home Page</returns>
        protected static HomePage Login()
        {
            UserNamePage userNamePage = new UserNamePage();
            PasswordPage passwordPage = userNamePage.GoToPasswordPage();
            HomePage homePage = passwordPage.GoToHomePage();
            return homePage;
        }
    }
}
