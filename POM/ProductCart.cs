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

namespace Camelia.POM
{
    internal class ProductCart
    {
        IWebDriver driver;
        GeneralMethods generalMethods;
        string firstProduct = "//div[@class='first-block'][1]";
        string priceProduct = "//span[contains(@class,'price regular')][1]";
        string priceCart = "//*[@id='js-cart-sidebar']//span[@class='product-price']";
        string cartButton = "//a[contains(@class,'add-to-cart')][1]";
        string remove="(//a[@class='remove-from-cart'])[2]";
        string empty = "(//div[@class='no-items'])[2]";

        public ProductCart(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
               
        public string FirstProductPrice()
        {
            //paima pirmo produkto kainą
            generalMethods.ScrollToElement(firstProduct);
            return generalMethods.GetText(priceProduct);
        }
        public void ClickAddToCart()
        {
            //įdeda prekę į krepšelį
            generalMethods.ClickElementBy(cartButton);
        }
        public string PriceCart()
        {
           //paima kainą iš krepšelio
            return generalMethods.GetText(priceCart);
        }
        public void ClickTrash()
        {
            //išmeta prekę iš krepšelio
            generalMethods.ClickElementBy(remove);
        }
        public string EmptyTrashAlert()
        {
            //tikrina, ar atsiranda tuščio krepšelio užrašas
            return generalMethods.GetText(empty);            

        }
    }
}
