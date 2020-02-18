using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras;
using System;
using Microsoft.Extensions.Configuration;

namespace Wordpress.Tests
{
    class EntryPO
    {
        public EntryPO()
        {
            PageFactory.InitElements(WordpressTests._browserDriver, this);
        }

        //Elements

        [FindsBy(How = How.XPath, Using = "//*[@title='Log In']")]
        public IWebElement loginMenuLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@title='Get Started']")]
        public IWebElement getStartedMenuLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='login__form-header']")]
        public IWebElement loginPageHeader { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h1[@class='formatted-header__title']")]
        public IWebElement PageHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text()='Continue with Google']")]
        public IWebElement continueWithGoogleButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text()='Continue with Apple']")]
        public IWebElement continueWithAppleButton { get; set; }

        [FindsBy(How = How.Id, Using = "identifierId")]
        public IWebElement googleEmailTextField { get; set; }

        [FindsBy(How = How.Id, Using = "identifierNext")]
        public IWebElement emailNextButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement googlePasswordTextField { get; set; }

        [FindsBy(How = How.Id, Using = "passwordNext")] 
        public IWebElement passwordNextButton { get; set; }
        
        [FindsBy(How = How.Id, Using = "account_name_text_field")] 
        public IWebElement appleIDTextField { get; set; }

        [FindsBy(How = How.Id, Using = "password_text_field")] 
        public IWebElement applePasswordTextField { get; set; }

        [FindsBy(How = How.Id, Using = "sign-in")] 
        public IWebElement appleArrowButton { get; set; }

        [FindsBy(How = How.Id, Using = "usernameOrEmail")] 
        public IWebElement usernameTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")] 
        public IWebElement continueButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email']")] 
        public IWebElement emailTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='username']")] 
        public IWebElement signUpUsernameTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")] 
        public IWebElement passwordTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")] 
        public IWebElement createAccountButton { get; set; }
        
        

        //Waiters
        private static string usernameBoxXPath = "//*[@id='usernameOrEmail']";
        private static string emailBoxXPath = "//*[@id='email']";
        private static string passwordBoxXPath = "//*[@id='password']";
        private static string googleButtonXPath = "//span[text()='Continue with Google']";
        private static string apppleButtonXPath = "//span[text()='Continue with Apple']";
        private static string googleEmailXPath = "//*[@id='identifierId']";
        private static string googlePasswordXPath = "//input[@name='password']";
        private static string searchBoxXPath = "//input[@id='search-component-1']";
        private static string appleIDBoxXPath = "//input[@id='account_name_text_field']";
        private static string applePasswordBoxXPath = "//input[@id='password_text_field']";
        private static string appleIDArrowXPath = "//button[@id='sign-in']";

        public void Login(Enum entry_type)
        {
            //verify page
            usernameBoxXPath.WaitUntilElementPresent();
            "loginPagetitle".GetData().ValPageTitle();
            loginPageHeader.ValPageHeaderText("loginHeaderText".GetData());
            
            SelectEntryType(entry_type, EntryMode.SignIn);

            //for google login, switch to main window and verify pagetitle
            if (entry_type.Equals(EntryType.Google))
            {
                WordpressTests._browserDriver.SwitchToPreviousWindow("loginPagetitle".GetData());
                "loginPagetitle".GetData().ValPageTitle();
                searchBoxXPath.WaitUntilElementPresent();
            }
            if (entry_type.Equals(EntryType.Email))
            {
                searchBoxXPath.WaitUntilElementPresent();
            }
        }

        public GetStartedPO SignUp(Enum entry_type)
        {
           //verify page
            emailBoxXPath.WaitUntilElementPresent();
            "getStartedPagetitle".GetData().ValPageTitle();
            PageHeader.ValPageHeaderText("getStartedHeaderText".GetData());

            SelectEntryType(entry_type, EntryMode.SignUp);

            //for google signup, switch to main window and verify pagetitle
            WordpressTests._browserDriver.SwitchToPreviousWindow("getStartedPagetitle".GetData());
            "getStartedPagetitle".GetData().ValPageTitle();
            return new GetStartedPO();
        }

        public void SelectEntryType(Enum entry_type, Enum entry_mode)
        {
            string enum_name = entry_type.ToString();
            Console.WriteLine($"Selected Login Type is -> {enum_name}");
            Helper.WaitBeforeAction(2);
            switch(entry_type)
            {
                case EntryType.Email:
                    EmailEntry(entry_mode);
                    break;
                case EntryType.Google:
                    GoogleEntry();
                    break;
                case EntryType.Apple:
                    AppleEntry();
                    break;
            }
        }

        public void EmailEntry(Enum entry_mode)
        {
            //enter details and click create
            if (entry_mode.Equals(EntryMode.SignUp))
            {
                string user = BuildUserDetail("user");
                emailBoxXPath.WaitBeforeEnteringText(emailTextField, $"{user}@gmail.com");
                signUpUsernameTextField.EnterText(user);
                passwordTextField.EnterText("firstUserDetails".GetSectionData("password"));
                createAccountButton.ClickElem();
            }
            else{
                //enter username and click continue
                usernameBoxXPath.WaitBeforeEnteringText(usernameTextField, "firstUserDetails".GetSectionData("email"));
                continueButton.ClickElem();

                //enter password and click continue
                passwordBoxXPath.WaitBeforeEnteringText(passwordTextField, "firstUserDetails".GetSectionData("password"));
                continueButton.ClickElem();
            }

        }

        public void GoogleEntry()
        {
            //click and switch to login window
            googleButtonXPath.WaitBeforeClickElem(continueWithGoogleButton);
            WordpressTests._browserDriver.SwitchToCurrentWindow();

            //enter details
            googleEmailXPath.WaitBeforeEnteringText(googleEmailTextField, "personalDetails".GetSectionData("googleEmail"));
            emailNextButton.ClickElem();
            googlePasswordXPath.WaitBeforeEnteringText(googlePasswordTextField, "personalDetails".GetSectionData("googlePassword"));
            passwordNextButton.ClickElem();
        }

        public void AppleEntry()
        {
            //click and switch to login window
            apppleButtonXPath.WaitBeforeClickElem(continueWithAppleButton);
            appleIDBoxXPath.WaitUntilElementPresent();
            //"appleSignInPagetitle".GetData().ValPageTitle();

            //enter details
            appleIDBoxXPath.WaitBeforeEnteringText(appleIDTextField, "personalDetails".GetSectionData("appleId"));
            appleIDArrowXPath.WaitBeforeClickElem(appleArrowButton);
            applePasswordBoxXPath.WaitBeforeEnteringText(applePasswordTextField, "personalDetails".GetSectionData("applePassword"));
            appleIDArrowXPath.WaitBeforeClickElem(appleArrowButton);
        }

        public string BuildUserDetail(string detail)
        {
            int first_part = 5000.GenerateRandomNumber(1000);
            int second_part = 9000.GenerateRandomNumber(5000);
            string user_detail = $"{detail}{first_part.ToString()}{second_part.ToString()}";
            Console.WriteLine($"Built detail is: {user_detail}");
            return user_detail;
        }

    }
}