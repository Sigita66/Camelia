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
            //tikrina, gali prisijungti neegzistuojantis klientas
            topMenu.ClickLoginButton();
            topMenu.InputTextGeneratedDate();
            topMenu.InputFromFile();
            topMenu.ClickSubmit();
            Assert.AreEqual(topMenu.CheckAlertExists(), "Identifikavimas nepavyko".ToLower());
        }
        [Test]
        public void AddProductToCart()
        {
            //tikrina, ar pavyksta įdėti produktą į krepšelį, ar krepšelio kaina lygi produkto kainai
            productCart.FirstProductPrice();
            productCart.ClickAddToCart();
            Assert.AreEqual(productCart.FirstProductPrice(), productCart.PriceCart());
        }

        [Test]
        public void AddToCartAndRemove()
        {
            //tikrina, ar pavyksta produktą įdėti į krepšelį ir po to išmesti
            productCart.ClickAddToCart();
            productCart.ClickTrash();
            Assert.AreEqual(productCart.EmptyTrashAlert(), "Jūsų krepšelyje nėra prekių".ToLower());

        }
       
        [Test]
        public void CheckSorting()
        {
            //tikrina, ar geras kainų rikiavimas didėjimo tvarka
            productList.ClickCategory(2);
            productList.ClickSorting(6);
            productList.CheckSortAscending(productList.PriceList());
        }
        [Test]
        public void CheckSortAndBack()
        {
            //tikrina, ar kainų rikiavimas išlieka paspaudus produktą ir grįžus
            productList.ClickCategory(3);
            productList.ClickSorting(6);
            productList.CheckSortAscending(productList.PriceList());
            productList.ClickProductAndGoBack();
            productList.CheckSortAscending(productList.PriceList());

        }
       


    }
}
