using SeleniumExtras.WaitHelpers;

namespace WaitUntilExample
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using WaitUntilExample.WaitUntil;

    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver(".");
            driver.Manage().Timeouts().ImplicitWait = default(TimeSpan);
            driver.Navigate().GoToUrl("https://google.pl/maps");

            var temperetureFindBy = By.CssSelector("[class*='area-weather-temperature']");

            var temperatureElement = WaitUntilFactory.WaitUntil<IWebElement>(driver, d => d.FindElement(temperetureFindBy));
            //var temperatureElement =
            //    WaitUntilFactory.WaitUntil(driver, ExpectedConditions.ElementExists(temperetureFindBy));

            try
            {
                WaitUntilFactory.WaitUntil<bool>(driver,
                    (d) => temperatureElement.Text.Contains("17"),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMinutes(500),
                    typeof(StaleElementReferenceException)
                    );

                Console.WriteLine("termperature match!!!");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($" current temperature {temperatureElement.Text}");
            }


            Console.ReadLine();
            driver.Dispose();
        }
    }
}
