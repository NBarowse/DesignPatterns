namespace DesignPatterns.WebDriver
{
    using System;

    using OpenQA.Selenium;

    public class Browser
    {
        private static Browser _currentInstane;
        private static IWebDriver _driver;
        public static BrowserFactory.BrowserType CurrentBrowser;
        public static int ImplWait;
        public static double TimeoutForElement;
        private static string _browser;

        private Browser()
        {
            InitParamas();
            _driver = BrowserFactory.GetDriver(CurrentBrowser, ImplWait);
        }

        /// <summary>
        /// Initializes fields of Browser class
        /// </summary>
        private static void InitParamas()
        {
            ImplWait = Convert.ToInt32(Configuration.ElementTimeout);
            TimeoutForElement = Convert.ToDouble(Configuration.ElementTimeout);
            _browser = Configuration.Browser;
            Enum.TryParse(_browser, out CurrentBrowser);
        }

        public static Browser Instance => _currentInstane ?? (_currentInstane = new Browser());

        /// <summary>
        /// Maximizes window of browser
        /// </summary>
        public static void WindowMaximise()
        {
            _driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Navigates to specified url
        /// </summary>
        /// <param name="url"></param>
        public static void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Gets driver
        /// </summary>
        /// <returns>Driver</returns>
        public static IWebDriver GetDriver()
        {
            return _driver;
        }

        //Quits the driver, closing every associated window, set fields to null
        public static void Quit()
        {
            _driver.Quit();
            _currentInstane = null;
            _driver = null;
            _browser = null;
        }
    }
}
