using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

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

    public class WaitUntilBuilder<T>
    {
        private TimeSpan timeout;
        private TimeSpan pollingTime;
        private Type[] exceptionTypes;
        private Func<IWebDriver, T> condition;
        private IWebDriver webDriver;

        private WaitUntilBuilder(IWebDriver webDriver, Func<IWebDriver, T> condition)
        {
            this.webDriver = webDriver;
            this.condition = condition;
            this.timeout = TimeSpan.FromSeconds(5);
            this.pollingTime = TimeSpan.FromMilliseconds(500);
            this.exceptionTypes = Enumerable.Empty<Type>().ToArray();
        }

        public static WaitUntilBuilder<T> Start(IWebDriver webDriver, Func<IWebDriver, T> condition)
        {
            return new WaitUntilBuilder<T>(webDriver, condition);
        }

        public WaitUntilBuilder<T> WithTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public WaitUntilBuilder<T> WithPollingTime(TimeSpan pollingTime)
        {
            this.pollingTime = pollingTime;
            return this;
        }

        public WaitUntilBuilder<T> WithIgnoreExceptionTypes(Type[] exceptionTypes)
        {
            this.exceptionTypes = exceptionTypes;
            return this;
        }

        public T Build()
        {
            return WaitUntilFactory.WaitUntil<T>(webDriver, condition, timeout, pollingTime, exceptionTypes);
        }
    }
}
