using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras;

namespace Wordpress.Tests
{
    class CheckoutPO
    {
        public CheckoutPO()
        {
            PageFactory.InitElements(WordpressTests._browserDriver, this);
        }

        //Elements

        [FindsBy(How = How.Id, Using = "first-name")]
        public IWebElement firstNameTextField { get; set; }

        [FindsBy(How = How.Id, Using = "last-name")]
        public IWebElement lastNameTextField { get; set; }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement emailTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@class='phone-input__country-select form-country-select']")]
        public IWebElement countryCodeDDBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='tel']")]
        public IWebElement phoneTextField { get; set; }

        [FindsBy(How = How.Id, Using = "country-code")]
        public IWebElement countryDDBox { get; set; }

        [FindsBy(How = How.Id, Using = "address-1")]
        public IWebElement address1TextField { get; set; }

        [FindsBy(How = How.Id, Using = "city")]
        public IWebElement cityTextField { get; set; }

        [FindsBy(How = How.Id, Using = "state")]
        public IWebElement stateTextField { get; set; }

        [FindsBy(How = How.Id, Using = "postal-code")]
        public IWebElement postalCodeTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement continueButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[@class='formatted-header__title']")]
        public IWebElement paymentPageHeader { get; set; }

        
        //Waiters
        private static string paymentTypeXPath = "//div[@class='checkout__provider']";
        private static string firstNameXPath = "//*[@id='first-name']";


        public void CompleteContactInfo()
        {
            //verify page
            firstNameXPath.WaitUntilElementPresent();
            paymentPageHeader.ValPageHeaderText("checkoutHeaderText".GetData());

            firstNameTextField.EnterText("personalDetails".GetSectionData("firstName"));
            lastNameTextField.EnterText("personalDetails".GetSectionData("lastName"));
            emailTextField.EnterText("personalDetails".GetSectionData("googleEmail"));
            countryCodeDDBox.SelectDDText("personalDetails".GetSectionData("country"));
            phoneTextField.EnterText("personalDetails".GetSectionData("phoneNumber"));
            countryDDBox.SelectDDText("personalDetails".GetSectionData("country"));
            address1TextField.EnterText("personalDetails".GetSectionData("address"));
            cityTextField.EnterText("personalDetails".GetSectionData("city"));
            stateTextField.EnterText("personalDetails".GetSectionData("state"));
            postalCodeTextField.EnterText("personalDetails".GetSectionData("postalCode"));
            continueButton.ClickElem();

            //wait for element to verify page
            paymentTypeXPath.WaitUntilElementPresent();
        }


    }
}