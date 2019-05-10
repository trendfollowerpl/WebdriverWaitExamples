namespace WaitUntilExample
{
    using OpenQA.Selenium.Chrome;
    using System;
    using WaitUntilExample.WaitUntil;

    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl("http://google.com");

            
            Console.ReadLine();
            driver.Dispose();
        }
    }
}
