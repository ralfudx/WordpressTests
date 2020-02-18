using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras;
using System.Collections.Generic;

namespace Wordpress.Tests
{
    class GetStartedPO
    {
        public GetStartedPO()
        {
            PageFactory.InitElements(WordpressTests._browserDriver, this);
        }

        //Elements

        [FindsBy(How = How.XPath, Using = "//button[@data-e2e-title='blog']")]
        public IWebElement blogOptionButton { get; set; }

        [FindsBy(How = How.Id, Using = "siteTopic")]
        public IWebElement siteTopicTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@title='Continue']")]
        public IWebElement topicContinueButton { get; set; }

        [FindsBy(How = How.Id, Using = "title")]
        public IWebElement siteTitleTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement titleContinueButton { get; set; }

        [FindsBy(How = How.Id, Using = "search-component-2")]
        public IWebElement domainNameTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='button domain-suggestion__action is-primary']")]
        public IWebElement selectButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Start with Personal']")]
        public IWebElement personalPlanButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Start with Premium']")]
        public IWebElement premiumPlanButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Start with Business']")]
        public IWebElement businessPlanButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()='Start with eCommerce']")]
        public IWebElement eCommercePlanButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[@class='formatted-header__title']")]
        public IWebElement PageHeader { get; set; }


        //Waiters
        private static string siteTypeXPath = "//div[@class='card site-type__wrapper']";
        private static string siteTopicXPath = "//*[@id='siteTopic']";
        private static string topicContinueButtonXPath = "//button[@title='Continue']";
        private static string siteTitleXPath = "//*[@id='title']";
        private static string titleContinueButtonXPath = "//button[@type='submit']";
        private static string domainNameXPath = "//*[@id='search-component-2']";
        private static string selectButtonXPath = "//button[@class='button domain-suggestion__action is-primary']";
        private static string paymentbuttonsXPath = "//div[@class='plan-features__actions-buttons']";
        private static string personalPlanXPath = "//button[text()='Start with Personal']";
        private static string premiumPlanXPath = "//button[text()='Start with Premium']";
        private static string businessPlanXPath = "//button[text()='Start with Business']";
        private static string eCommercePlanXPath = "//button[text()='Start with eCommerce']";



        public void ContinueSiteSetUp()
        {
            //verify page
            siteTypeXPath.WaitUntilElementPresent();
            PageHeader.ValPageHeaderText("siteTypePageHeaderText".GetData());
            blogOptionButton.ClickElem();
            
            //verify page
            siteTopicXPath.WaitUntilElementPresent();
            PageHeader.ValPageHeaderText("blogTopicHeaderText".GetData());
            siteTopicTextField.EnterText("blogTopicText".GetData());
            topicContinueButtonXPath.WaitBeforeClickElem(topicContinueButton);
            
            //verify page
            siteTitleXPath.WaitUntilElementPresent();
            PageHeader.ValPageHeaderText("blogTitleHeaderText".GetData());
            siteTitleTextField.EnterText("blogTitleText".GetData());
            titleContinueButtonXPath.WaitBeforeClickElem(titleContinueButton);
            
            //verify page
            domainNameXPath.WaitUntilElementPresent();
            PageHeader.ValPageHeaderText("blogDomainHeaderText".GetData());
            domainNameTextField.EnterText("blogDomainText".GetData());
            selectButtonXPath.WaitBeforeClickElem(selectButton);
            
        }

        public CheckoutPO CompleteSiteSetUp(Enum enum_item)
        {
            //verify page
            paymentbuttonsXPath.WaitUntilElementClickable();
            PageHeader.ValPageHeaderText("selectPlanHeaderText".GetData());

            SelectPaymentPlan(enum_item);
            return new CheckoutPO();
        }

        public void SelectPaymentPlan(Enum enum_item)
        {
            string enum_name = enum_item.ToString();
            Console.WriteLine($"Selected payment plan is -> {enum_name}");
            Helper.WaitBeforeAction(2);
            switch(enum_name)
            {
                case "Personal":
                    personalPlanXPath.WaitBeforeAdvClickElem(personalPlanButton);
                    break;
                case "Premium":
                    premiumPlanXPath.WaitBeforeAdvClickElem(premiumPlanButton);
                    break;
                case "Business":
                    businessPlanXPath.WaitBeforeAdvClickElem(businessPlanButton);
                    break;
                case "eCommerce":
                    eCommercePlanXPath.WaitBeforeAdvClickElem(eCommercePlanButton);
                    break;
            }
        }  
    }
}