using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Runtime.CompilerServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;
using System.Xml.Linq;

namespace Camelia
{
    internal class GeneralMethods
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;
        

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
        }

        public void ClickElementBy(string xpath)
        {
            IWebElement element = wait.Until(x => x.FindElement(By.XPath(xpath)));
            element.Click();
        }
        
        public void SendKeys(string xpath,string text)
        {
            IWebElement element = wait.Until(x => x.FindElement(By.XPath(xpath)));
            element.SendKeys(text);
        }

        public string GetText(string xpath)
        {
            IWebElement element = wait.Until(x => x.FindElement(By.XPath(xpath)));
            return element.Text.ToLower();                        
        }
                
        public void CheckTextContains(string xpath,string actualText)
        {
            if (!GetText(xpath).ToLower().Contains(actualText.ToLower()))
            {
                Assert.Fail("Wrong expected text");
            }
        }

        public bool CheckElementExists(string xpath)
        {
            try
            {
                By element = By.XPath(xpath);
                driver.FindElement(element);
                return element != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }
        
        public string ReadFromFile()
        {
            StreamReader sr = new StreamReader("TextFile.txt");
            return sr.ReadLine();
        }

        public void ScrollToElement(string xpath)
        {
            IWebElement element = driver.FindElement(By.XPath(xpath));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            
        }

        public void TakeScreenshot()
        {
            DateTime time = DateTime.Now;
            Screenshot TakeScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
            TakeScreenshot.SaveAsFile("screenshots\\error" + time.ToString("yyyy_MM_dd_HH_mm_ss")+"." + System.Drawing.Imaging.ImageFormat.Png);
            File.WriteAllText("screenshots\\error" + time.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt", TestContext.CurrentContext.Result.Message);
            
        }
        public void DeleteFiles()
        {
            var countToLeave = 4;
            var files = Directory.GetFiles("screenshots").ToList();
            files.Sort();
            if (files.Count > countToLeave)
            {
                for (int i = 0; i < files.Count - countToLeave; i++)
                {
                    File.Delete(files[i]);
                }
            }
        }

        public int CountElements(string xpath)
        {
            By element = By.XPath(xpath);
            return driver.FindElements(element).Count();
        }
        

        public string GetEmail()
        {
            DateTime time = DateTime.Now;
            return "test_" + time.ToString("yyyy_MM_dd_HH_mm_ss") + "@gmail.com";
        }

        
        public double ParsePrice(string text)
        {
            var splitedText = text.Split(' ')[0];
            return double.Parse(splitedText.Replace(',', '.'));
        }
        public double[] GetArray(string xpath1,string xpath2,string xpath3)
        {

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            double[] array = new double[49];

            for (int i = 1; i < 49; i++)
            {

                if (CheckElementExists(xpath1 + "[" + i + "]"))
                {
                    array[i] = ParsePrice(GetText(xpath1 + "[" + i + "]"));

                }
                else if (CheckElementExists(xpath2 + "[" + i + "]"))
                {
                    array[i] = ParsePrice(GetText(xpath2 + "[" + i + "]"));

                }
                else if (CheckElementExists(xpath3 + "[" + i + "]"))
                {
                    array[i] = ParsePrice(GetText(xpath3 + "[" + i + "]"));

                }
                else break;

            }
            return array;

        }







    }
}
