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
using Camelia.POM;

namespace Camelia
{
    internal class TestCases
    {
        IWebDriver driver;
        GeneralMethods generalMethods;
        TopMenu topMenu;
        ProductCart productCart;
        ProductList productList;
                
        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications"); // to disable notification
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Url = "https://camelia.lt";
            generalMethods = new GeneralMethods(driver);
            topMenu = new TopMenu(driver);
            productCart = new ProductCart(driver);
            productList = new ProductList(driver);
            generalMethods.ClickElementBy("//*[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']");

        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                generalMethods.TakeScreenshot();
                generalMethods.DeleteFiles();
            }
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void CheckProductCartExists()
        {
            //tikrina, ar TopMenu yra prekių krepšelis
            topMenu.CheckCartExists();

        }
        [Test]
        public void SearchFieldByText()
        {
            //tikrina, ar paieškoje įvedus žodį jis matomas eilutėje "Paieškos rezultatai"
            topMenu.SearchFieldInput("kremas");
            topMenu.CheckSearchInputText("kremas");
        }
        [Test]
        public void SearchFieldByName()
        {
            //tikrina, ar paieškos žodis yra visuose produktų pavadinimuose
            topMenu.SearchFieldInput("omega");
            topMenu.CheckProductName("Omega");
            
        }
        [Test]
        public void CheckLoginNonExistingAccount()
        {
            topMenu.ClickLoginButton();
            topMenu.InputTextGeneratedDate();
            topMenu.InputFromFile();
            topMenu.ClickSubmit();
            Assert.AreEqual(topMenu.CheckAlertExists(), "Identifikavimas nepavyko".ToLower());
        }
        [Test]
        public void AddProductToCart()
        {
            productCart.FirstProductPrice();
            productCart.ClickAddToCart();
            Assert.AreEqual(productCart.FirstProductPrice(), productCart.PriceCart());
        }

        [Test]
        public void AddToCartAndRemove()
        {
            productCart.ClickAddToCart();
            productCart.ClickTrash();
            Assert.AreEqual(productCart.EmptyTrashAlert(), "Jūsų krepšelyje nėra prekių".ToLower());

        }
       
        [Test]
        public void CheckSorting()
        {
            productList.ClickCategory(2);
            productList.ClickSorting(6);
            productList.CheckSortAscending(productList.PriceList());
        }
        [Test]
        public void CheckSortAndBack()
        {
            productList.ClickCategory(3);
            productList.ClickSorting(6);
            productList.CheckSortAscending(productList.PriceList());
            productList.ClickProductAndGoBack();
            productList.CheckSortAscending(productList.PriceList());

        }
       


    }
}
