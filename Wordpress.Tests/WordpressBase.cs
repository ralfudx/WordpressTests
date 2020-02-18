using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using Xunit;

namespace Wordpress.Tests
{
    class WordpressBase: IDisposable
    {
        //auto-implemented property
        public static IWebDriver _browserDriver { get; set; }
        public static IConfiguration _config { get; set; }
        
		public WordpressBase()
        {
            this.InitializeBrowser();
        }

        public void InitializeBrowser()
        {
            _browserDriver = new ChromeDriver("./");
            _config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            _browserDriver.Manage().Window.Maximize();
			_browserDriver.Navigate().GoToUrl(_config["appurl"]);
        }

        public void Dispose()
        {
            Helper.WaitBeforeAction(10);
            //_browserDriver.Quit();
        }
    }
}
