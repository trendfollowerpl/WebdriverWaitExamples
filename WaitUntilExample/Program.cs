namespace WaitUntilExample
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using WaitUntilExample.WaitUntil;
    using SeleniumExtras.WaitHelpers;

    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl("https://google.pl/maps");

            var wheatherClassFindBy = By.CssSelector(".section-area-weather");
            var temperetureFindBy = By.CssSelector("[class*='area-weather-temperature']");

            var wheatherElement = WaitUntilFactory.WaitUntil<IWebElement>(driver, ExpectedConditions.ElementExists(wheatherClassFindBy));
            Console.WriteLine($"wheather element found: {wheatherElement.Size}");

            var temperatureElement = WaitUntilFactory.WaitUntil(driver, ExpectedConditions.ElementIsVisible(temperetureFindBy));

            try
            {
                WaitUntilFactory.WaitUntil<bool>(driver, (d) =>
                {
                    return temperatureElement.Text.Contains("17");
                });

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
