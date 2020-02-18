using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace Wordpress.Tests
{
    static class Helper
    {
        public static void ClickElem(this IWebElement element)
        {
            element.Click();
        }

        public static void WaitBeforeClickElem(this string waiter, IWebElement element)
        {
            waiter.WaitUntilElementPresent();
            element.ClickElem();
        }

        public static void WaitBeforeAdvClickElem(this string waiter, IWebElement element)
        {
            waiter.WaitUntilElementPresent();
            element.AdvClickElem();
        }

        public static void ClickWithActions(this IWebElement element)
        {
            Actions action = new Actions(WordpressTests._browserDriver);
            action.MoveToElement(element).Click().Build().Perform();
        }
        public static void AdvClickElem(this IWebElement element)
        {
            IJavaScriptExecutor ex = (IJavaScriptExecutor)WordpressTests._browserDriver;
            ex.ExecuteScript("arguments[0].click();", element);
        }

        public static string SelectBrowser(string browserName)
        {
            if(browserName == "CHROME")
            {
                return "CHROME";
            }
            if(browserName == "FIREFOX")
            {
                return "CHROME";
            }
            if(browserName == "IE")
            {
                return "IE";
            }
            else{
                return "CHROME";
            }
        }

        public static void EnterText(this IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static void WaitBeforeEnteringText(this string waiter, IWebElement element, string value)
        {
            waiter.WaitUntilElementPresent();
            element.EnterText(value);
        }

        public static void SelectDDText(this IWebElement element, string value)
        {
            WaitBeforeAction(2);
            new SelectElement(element).SelectByText(value); 
        }

        public static IList<string> GetAllDDOptions(this IWebElement element)
        {
            SelectElement selectElem = new SelectElement(element);
            IList<IWebElement> selectOptions = selectElem.Options;
            IList<string> optionNames = new List<string>();
            
            foreach(IWebElement elem in selectOptions)
            {
                optionNames.Add(elem.GetText());
            }
            return optionNames;  
        }

        public static string SelectRandomDDOption(this IWebElement currentTxt, IWebElement dropdown_elem, bool place_holder)
        {
            string current_option = currentTxt.GetText();
            IList<string> all_options = dropdown_elem.GetAllDDOptions();
            if(place_holder)
            {
                all_options.RemoveAt(0);
            }
            all_options.Remove(current_option);
            int new_index = all_options.Count.GenerateRandomNumber(0);
            string new_option = all_options[new_index];
            dropdown_elem.SelectDDText(new_option);
            return new_option;
        }
        
        public static String GetText(this IWebElement element)
        {
            return element.Text;
        }
        public static String GetTextByValue(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static String GetTextByInnerHTML(this IWebElement element)
        {
            return element.GetAttribute("innerHTML");
        }

        public static void ValPageHeaderText(this IWebElement element, string value)
        {
            string page_text = element.Text;
            bool result = page_text.Contains(value);
            if (result)
            {
                Console.WriteLine("Page Header is: " + page_text);
            }
            else
            {
                Console.WriteLine("Page Header does not contain: " + value);
                //failure routine
            }
        }

        public static void ValPageTitle(this string page_title)
        {
            string _pagetitle = WordpressTests._browserDriver.Title;
            bool result = _pagetitle.Contains(page_title);
            if (result)
            {
                Console.WriteLine($"Page Title is: {_pagetitle}");
            }
            else
            {
                Console.WriteLine($"Page Title does not contain: {page_title}");
                Console.WriteLine($"Page Title should be: {_pagetitle}");
                Assert.True(result);
            }
        }

        public static void WaitBeforeAction(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static void WaitUntilElementPresent(this string elem)
        {
            WebDriverWait wait = new WebDriverWait(WordpressTests._browserDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(elem)));
        }

        public static void WaitUntilElementClickable(this string elem)
        {
            WebDriverWait wait = new WebDriverWait(WordpressTests._browserDriver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(elem)));
        }


        public static int GenerateRandomNumber(this int max_num, int min_num)
        {
            Random rnd = new Random();
            return rnd.Next(min_num, max_num);
        }

        public static bool IsAlertAccepted(this IWebDriver driver)
        {
            bool presentFlag = false;
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                presentFlag = true;
                alert.Accept();
            } 
            catch (NoAlertPresentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return presentFlag;
        }

        public static bool IsEqual(string str1, string str2)
        {
            return str1.Equals(str2);
        }

        public static string GetPreviousDate()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            return yesterday.ToString("dd/MM/yyyy");
        }

        public static void SwitchToCurrentWindow(this IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void SwitchToPreviousWindow(this IWebDriver driver, string page_title)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.SwitchTo().Window(driver.WindowHandles.First());
            wait.Until(_driver => driver.Title.Contains(page_title));
        }

        public static void ElemContainsText(this IWebElement element, string exp_text)
        {
            string actual_text = element.Text;
            if (actual_text.Contains(exp_text)){
                 Console.WriteLine("Page contains: " + exp_text);
            }
            else
            {
                 Console.WriteLine(exp_text + " is not found");
            }
        }

        public static void TextContainsText(this string elem_text, string exp_text)
        {
            if (elem_text.Contains(exp_text)){
                 Console.WriteLine("Page contains: " + exp_text);
            }
            else
            {
                 Console.WriteLine(exp_text + " is not found");
            }
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+"); 
        public static string ReplaceWhitespace(this string input, string replacement) 
        { 
            return sWhitespace.Replace(input, replacement); 
        }

        public static string GetData(this string data_key)
        {
            return WordpressTests._config[data_key];
        }

        public static string GetSectionData(this string section, string data_key)
        {
            return WordpressTests._config.GetSection(section)[data_key];
        }
    }

}