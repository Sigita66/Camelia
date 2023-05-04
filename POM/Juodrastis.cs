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
using System.Security.Cryptography.X509Certificates;

namespace Camelia.POM
{
    internal class Juodrastis
    {
        IWebDriver driver;
        GeneralMethods generalMethods;
        public Juodrastis(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        //string priceClub = "//*[@id='js-product-list']/div[1]/div/article["+index+"]/div[2]/div[1]/div[2]/div/div/div[1]/div[1]/div[1]/div";
        //string priceClub = "(//div[@class='discount_percent_container club_price'])";
        //string priceRegular = "//*[@id=\"js-product-list\"]/div[1]/div/article["+index+ "]/div/div[1]/div[2]/div/div/div[1]/div[1]/div[2]/span[2]";
        string priceRegular = "(//span[@class='price regular-price disable-text-decoration old-price no-strike'])";
        //string priceRegular = "(//div[@class='first-prices d-flex flex-wrap align-items-center'])";
        string ad = "//div[@class='ad-container']";
        string priceRed = "(//div[@class='first-prices d-flex flex-wrap align-items-center']//span[@class='price product-price '])";
        string firstPruduct = "//div[@class='first-block'][1]";
        string priceClub = "(//div[@class='cm-club-price-list']/div)";

        
        //public double ParsePrice(string kazkas)
        //{

        //    var split = kazkas.Replace(" ","");
        //    split = kazkas.Replace("€", "");
        //    split = kazkas.Replace(",", "");
        //    return double.Parse(split);
        //}
        //public void GetListPrices()
        //{
        //    List<double> listName = new List<double>();
        //    for (int i = 1; i < 49; i++)
        //    {

        //        if (generalMethods.CheckElementExists(priceClub + "[" + i + "]"))
        //        {
        //            listName.Add(ParsePrice(generalMethods.GetText(priceClub + "[" + i + "]")));
        //        }
        //        else if (generalMethods.CheckElementExists(priceRed + "[" + i + "]"))
        //        {
        //            listName.Add(ParsePrice(generalMethods.GetText(priceRed + "[" + i + "]")));
        //        }
        //        else if (generalMethods.CheckElementExists(priceRegular + "[" + i + "]"))
        //        {
        //            listName.Add(ParsePrice(generalMethods.GetText(priceRegular + "[" + i + "]")));
        //        }
        //        else break;
        //    }
        //    Console.WriteLine(  listName.Count);
        //    for (int i = 1; i < listName.Count-1; i++)
        //    {
        //        Console.WriteLine(i+" "+listName[i]);
        //    }

        //}

        //public double[] Bandymas()
        //{

        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //    //Console.WriteLine(generalMethods.CountElements(priceRegular));
        //    //Console.WriteLine(generalMethods.CountElements(priceClub));
        //    //Console.WriteLine(generalMethods.CountElements(priceRed));
        //    //Console.WriteLine();
        //    double[] masyvas = new double[49];

        //    for (int i = 1; i < 49; i++)
        //    {

        //        if (generalMethods.CheckElementExists(priceClub + "[" + i + "]"))
        //        {
        //            masyvas[i] = ParsePrice(generalMethods.GetText(priceClub + "[" + i + "]"));

        //        }
        //        else if (generalMethods.CheckElementExists(priceRed + "[" + i + "]"))
        //        {
        //            masyvas[i] = ParsePrice(generalMethods.GetText(priceRed + "[" + i + "]"));

        //        }
        //        else if (generalMethods.CheckElementExists(priceRegular + "[" + i + "]"))
        //        {
        //            masyvas[i] = ParsePrice(generalMethods.GetText(priceRegular + "[" + i + "]"));

        //        }
        //        else break;

        //    }
        //    return masyvas;
            
        //}
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
        public void Kazkas()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> elementList = driver.FindElements(By.XPath(priceClub));
            foreach (IWebElement element in elementList) 
            {
                //Console.WriteLine(  element);
                
                Console.WriteLine(element.Text);
            }
            Console.WriteLine(elementList.Count);
        }





        //public List<double> GetListText()
        //{
        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //    Console.WriteLine(generalMethods.CountElements(priceRegular));
        //    Console.WriteLine(generalMethods.CountElements(priceClub));

        //    Console.WriteLine();
        //    //Console.WriteLine(ParsePrice(priceRegular + "[" + 1 + "]"));

        //    //IList<IWebElement> elementList = driver.FindElements(By.XPath(priceClub));
        //    List<double> listName = new List<double>();

        //    for (int i = 1; i < 49; i++)
        //    {
        //        listName.Add(ParsePrice(generalMethods.GetText(priceRegular+"["+i+"]")));
        //        Console.Write(i + " ");
        //        Console.WriteLine(ParsePrice(generalMethods.GetText(priceRegular + "[" + i + "]")));

        //    }
        //    return listName;
        //    Console.WriteLine(  );
        //    Console.WriteLine(listName.Count);

        //foreach (IWebElement element in elementList)
        //    {
        //        listName.Add(ParsePrice(generalMethods.GetText(priceClub+"["+i+"]")));


        //    }
        //    return listName;


        //for (int i = 1; i < listName.Count; i++)
        //{
        //    Console.WriteLine(listName[i]);
        //}


        //}
        //public void GetPrices()
        //{
        //    Console.WriteLine(generalMethods.CountElements(priceRegular));
        //    Console.WriteLine(generalMethods.CountElements(priceClub));
        //    Console.WriteLine(CountAds());
        //    GetListText(prices);
        //    for (int i = 1; i < GetListText(prices).Count; i++)
        //    {
        //        Console.WriteLine();
        //    }

        //}




    }
}

