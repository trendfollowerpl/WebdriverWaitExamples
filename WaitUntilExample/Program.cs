using OpenQA.Selenium.Chrome;
using System;

namespace WaitUntilExample
{
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
