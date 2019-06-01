using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace WaitUntilExample.WaitUntil
{
    public static class WaitUntilFactory
    {
        public static T WaitUntil<T>(IWebDriver webDriver, Func<IWebDriver, T> condition)
        {
            return WaitUntil(webDriver, condition, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(500), null);
        }
        public static T WaitUntil<T>(IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout)
        {
            return WaitUntil(webDriver, condition, timeout, TimeSpan.FromSeconds(500), null);
        }
        public static T WaitUntil<T>(IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout, TimeSpan pollingTime, params Type[] exceptionTypes)
        {
            var waitDriver = new WebDriverWait(webDriver, timeout)
            {
                PollingInterval = pollingTime
            };

            if (exceptionTypes != null)
            {
                waitDriver.IgnoreExceptionTypes(exceptionTypes);
            }

            return waitDriver.Until(condition);
        }
    }
}
