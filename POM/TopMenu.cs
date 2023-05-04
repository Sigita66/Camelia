using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Camelia.POM
{
    internal class TopMenu
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string cart = "//ul[@class='cart-header']";
        string searchField = "//input[contains(@class,'form-control')][1]";
        string searchButton = "//button[@class='page-link']";
        string expectedTextXpath= "//section[@id='main']/h1";
        string productName = "(//h5[@itemprop='name'])";
        string loginButton = "//div[@class='user-info']//div[@class='account-link']";
        string emailField = "//input[@name='email']";
        string passwordField = "//input[@name='password']";
        string submit = "//button[@id='submit-login']";
        string alertLogin = "//li[@class='alert alert-danger']";


        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public void CheckCartExists()
        {
           generalMethods.ClickElementBy(cart);
            
        }

       
        public void SearchFieldInput(string actualText)
        {
            //įveda žodį į paieškos lauką
            generalMethods.SendKeys(searchField, actualText);
            generalMethods.ClickElementBy(searchButton);
                        
        }
       
        public void CheckSearchInputText(string actualText)
        {
            //tikrina, ar atsiranda eilutė su įvestu žodžiu
            generalMethods.CheckTextContains(expectedTextXpath, actualText);
            
        }
        
        public void CheckProductName(string actualText)
        {
                        
            //tikrina, ar įvestas žodis yra produktų pavadinimuose
            for (int i = 1; i < 49; i++)
            {
               generalMethods.CheckTextContains((productName + "[" + i + "]"), actualText);
               
            }
            
        }
        
        public void InputTextGeneratedDate()
        {
            //įveda sugeneruotą pagal datą email 
            generalMethods.SendKeys(emailField, generalMethods.GetEmail());
        }
        public void InputFromFile()
        {
            //įveda slaptažodį iš failo
            generalMethods.SendKeys(passwordField, generalMethods.ReadFromFile());
        }
       
        public void ClickLoginButton()
        {
            
            generalMethods.ClickElementBy(loginButton);
            
        }
        public void ClickSubmit()
        {
            generalMethods.ClickElementBy(submit);
        }
        public string CheckAlertExists()
        {
            //tikrina, ar yra tekstas apie nepavykusį prisijungimą
            return generalMethods.GetText(alertLogin);

        }


    }
}
