namespace DesignPatterns.WebDriver
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using OpenQA.Selenium.Interactions;

    public class BaseElement : IWebElement
    {
        protected string Name;
        protected By Locator;
        protected IWebElement Element;

        public string TagName { get; }
        public string Text { get; }
        public bool Enabled { get; }
        public bool Selected { get; }
        public Point Location { get; }
        public Size Size { get; }
        public bool Displayed { get; }

        public BaseElement(By locator, string name)
        {
            this.Locator = locator;
            this.Name = name == "" ? this.GetText() : name;
        }

        public BaseElement(By locator)
        {
            this.Locator = locator;
        }

        /// <summary>
        /// Gets text of the web element after the web element becomes visible
        /// </summary>
        /// <returns>Text of the web element</returns>
        public string GetText()
        {
            this.WaitForIsVisible();
            this.GetElement();
            return this.Element.Text;
        }

        /// <summary>
        /// Gets the first web element by locator
        /// </summary>
        /// <returns>The first web elemen by locatort</returns>
        public IWebElement GetElement()
        {
            try
            {
                this.Element = Browser.GetDriver().FindElement(this.Locator);
            }
            catch (Exception)
            {

                throw;
            }
            return this.Element;
        }

        /// <summary>
        /// Waits until the web element becomes visible on the page
        /// </summary>
        public void WaitForIsVisible()
        {
            new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementIsVisible(this.Locator));
        }

        /// <summary>
        /// Waits until the web element becomes clickable on the page
        /// </summary>
        public void WaitForIsClickable()
        {
            new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementExists(this.Locator));
        }
               
        /// <summary>
        /// Clears the content of the web element after the web element becomes visible
        /// </summary>
        public void Clear()
        {
            this.WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).Clear();
        }

        /// <summary>
        /// Clicks on the web element after it becomes visible
        /// </summary>
        public void Click()
        {
            this.WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).Click();
        }
        
        /// <summary>
        /// Clicks on the web element even if it is overlapped by others
        /// </summary>
        public void JsClick()
        {
            this.WaitForIsVisible();
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            executor.ExecuteScript("arguments[0].click();", this.GetElement());
        }

        /// <summary>
        /// Calls context menu and selects the 3rd option
        /// </summary>
        public void DeleteWithContextClick()
        {
            this.WaitForIsVisible();
            new Actions(Browser.GetDriver()).ContextClick(this.GetElement()).SendKeys(Keys.ArrowDown).SendKeys(Keys.ArrowDown).SendKeys(Keys.ArrowDown).SendKeys(Keys.Return).Build().Perform();
        }
                
        /// <summary>
        /// Finds the first web element by locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns>The first web element by locator</returns>
        public IWebElement FindElement(By by)
        {
            return Browser.GetDriver().FindElement(by);
        }

        /// <summary>
        /// Finds all the web elements by locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns>All the web elements by locator</returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Browser.GetDriver().FindElements(by);
        }

        /// <summary>
        /// Gets value of specified attribute for the web element
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns>Value of specified attribute</returns>
        public string GetAttribute(string attributeName)
        {
            return Browser.GetDriver().FindElement(this.Locator).GetAttribute(attributeName);
        }

        /// <summary>
        /// Gets value of CSS property of the web element after the web element becomes visible
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>Value of CSS property</returns>
        public string GetCssValue(string propertyName)
        {
            this.WaitForIsVisible();
            return Browser.GetDriver().FindElement(this.Locator).GetCssValue(propertyName);
        }

        /// <summary>
        /// Gets value of JavaScript property of the web element after the web element becomes visible
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>Value of JavaScript property</returns>
        public string GetProperty(string propertyName)
        {
            this.WaitForIsVisible();
            return Browser.GetDriver().FindElement(this.Locator).GetProperty(propertyName);
        }

        /// <summary>
        /// Simulates typing text into the web element after it becomes visible
        /// </summary>
        /// <param name="text"></param>
        public void SendKeys(string text)
        {
            this.WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).SendKeys(text);
        }

        /// <summary>
        /// Submits the web element to the web server after the web element becomes visible
        /// </summary>
        public void Submit()
        {
            this.WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).Submit();
        }
    }
}
