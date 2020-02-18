using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras;
using System;
using Microsoft.Extensions.Configuration;

namespace Wordpress.Tests
{
    class HomePagePO
    {
        public HomePagePO()
        {
            PageFactory.InitElements(WordpressTests._browserDriver, this);
        }

        //Elements

        [FindsBy(How = How.XPath, Using = "//*[@title='Log In']")]
        public IWebElement loginMenuLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@title='Get Started']")]
        public IWebElement getStartedMenuLink { get; set; }

        //Waiters

        public EntryPO GetStarted()
        {
            //verify pagetitle and click link
            "landingPagetitle".GetData().ValPageTitle();
            getStartedMenuLink.ClickElem();
            return new EntryPO();
        }

        public EntryPO Login()
        {
            //verify pagetitle and click link
            "landingPagetitle".GetData().ValPageTitle();
            loginMenuLink.ClickElem();
            return new EntryPO();
        }

    }
}