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
                    (d) => temperatureElement.Text.StartsWith("1"),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromMilliseconds(500),
                    typeof(StaleElementReferenceException)
                    );

                Console.WriteLine("termperature match!!!");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($" current temperature {temperatureElement.Text}");
            }

            WaitUntilBuilder<bool>
                .Start(driver, (d) => temperatureElement.Text.StartsWith("1"))
                .WithIgnoreExceptionTypes(typeof(StaleElementReferenceException))
                .WithPollingTime(TimeSpan.FromSeconds(1))
                .WithTimeout(TimeSpan.FromSeconds(3))
                .Build();

            Console.ReadLine();
            driver.Dispose();
        }
    }
}
