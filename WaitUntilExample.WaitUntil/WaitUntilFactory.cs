using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace WaitUntilExample.WaitUntil
{
    public sealed class WaitUntilFactory
    {
        static WebDriverWait Create(IWebDriver webDriver)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
        }
        static WebDriverWait Create(IWebDriver webDriver, TimeSpan timeout)
        {
            return new WebDriverWait(webDriver, timeout);
        }
        static WebDriverWait Create(IWebDriver webDriver, TimeSpan timeout, params Type[] exceptionTypes)
        {
            var waitDriver = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            waitDriver.IgnoreExceptionTypes(exceptionTypes);
            return waitDriver;
        }

        public T WaitUntil<T>(WebDriverWait webDriverWait, Func<IWebDriver, T> condition)
        {
            return webDriverWait.Until<T>(condition);
        }
    }
}
