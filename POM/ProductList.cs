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
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace Camelia.POM
{
    internal class ProductList
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string category = "//div[@id='main-menu']//li";
        string sortButton = "//button[@data-toggle='dropdown']";
        string sorting = "(//a[contains(@class,'dropdown-item')])[6]";
        string ad = "//div[@class='ad-container']";
        string firstPruduct = "//div[@class='first-block'][1]";
        string priceClub = "(//div[@class='cm-club-price-list']/div)";
        string priceRed = "(//div[@class='first-prices d-flex flex-wrap align-items-center']//span[@class='price product-price '])";
        string priceRegular = "(//span[@class='price regular-price disable-text-decoration old-price no-strike'])";


        public ProductList(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
        public void ClickCategory(int k)
        {
            generalMethods.ClickElementBy(category + "[" + k + "]");
        }
        public void ClickSorting(int k)
        {
            generalMethods.ClickElementBy(sortButton);
            generalMethods.ScrollToElement(sorting);
            generalMethods.ClickElementBy(sorting);
        }
        
        public double ParsePrice(string kazkas)
        {
            var split = kazkas.Split(' ')[0];
            return double.Parse(split);
        }
        public double[] PriceList()
        {
            return generalMethods.GetArray(priceClub, priceRed, priceRegular);
        }

        public void CheckSortAscending(double[] priceList)
        {

            Console.WriteLine(priceList.Length);
            Console.WriteLine();

            for (int i = (generalMethods.CountElements(ad)) + 1; i < priceList.Length - 1; i++)
            {

                Console.Write(i + " ");
                Console.WriteLine(priceList[i]);
                if (priceList[i] > priceList[i + 1])
                {
                    Assert.Fail("Prices are not sorted");
                }
            }
        }



        public void ClickProductAndGoBack()
        {
            generalMethods.ClickElementBy(firstPruduct);
            driver.Navigate().Back();
        }




    }
}

