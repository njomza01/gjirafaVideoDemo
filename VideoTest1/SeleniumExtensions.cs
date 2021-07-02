using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoTest1
{
    public static class SeleniumExtensions
    {

        #region SwitchTo

        public static void SwitchTo(this IWebDriver driver, IWebElement element)
        {
            driver.SwitchTo().Frame(element);
        }
        #endregion

        #region Waits

        public static bool WaitUntilUrlContains(this IWebDriver driver, string keyword, int seconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

                wait.Until(d =>
                {
                    var url = driver.Url;
                    return url.Contains(keyword);
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        #endregion

        public static void ScrollToTheBottom(this IWebDriver webDriver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)webDriver);
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            
        }

        public static void ScrollToElement(this IWebDriver webDriver, IWebElement element)
        {
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}
