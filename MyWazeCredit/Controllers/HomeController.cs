using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyWazeCredit.Models;
using MyWazeCredit.Models.ViewModels;
using MyWazeCredit.Service;
using MyWazeCredit.Utility.AppsettingsClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWazeCredit.Controllers
{
    public class HomeController : Controller
    {

        public HomeVM homeVM { get; set; }
        private readonly IMarketForecaster _marketForecaster;
        private readonly StripeSettings _stripeSettings;
        private readonly WazeForecastSettings _wazeForecastSettings;
        private readonly TwilioSettings _twilioSettings;
        private readonly SendGridSettings _sendGridSettings;
        public HomeController(IMarketForecaster marketForecaster, 
            IOptions<StripeSettings> stripeSettings,
            IOptions<WazeForecastSettings> wazeForecastSettings,
            IOptions<TwilioSettings> twilioSettings,
            IOptions<SendGridSettings> sendGridSettings
            )
        {
            homeVM = new HomeVM();

            _sendGridSettings = sendGridSettings.Value;
            _stripeSettings = stripeSettings.Value;
            _twilioSettings = twilioSettings.Value;
            _wazeForecastSettings = wazeForecastSettings.Value;

            _marketForecaster = marketForecaster;
        }

        public IActionResult Index()
        {           
            
            MarketResult currentMarket = _marketForecaster.GetMarketPrediction();
            switch (currentMarket.MarketCondition)
            {
                case MarketCondition.StableDown:
                    homeVM.MarketForecast = "Market shows signs to go down in a stable state! It is a not a good sign to apply for credit applications! But extra credit is always piece of mind if you have handy when you need it.";
                    break;
                case MarketCondition.StableUp:
                    homeVM.MarketForecast = "Market shows signs to go up in a stable state! It is a great sign to apply for credit applications!";
                    break;
                case MarketCondition.Volatile:
                    homeVM.MarketForecast = "Market shows signs of volatility. In uncertain times, it is good to have credit handy if you need extra funds!";
                    break;
                default:
                    homeVM.MarketForecast = "Apply for a credit card using our application!";
                    break;
            }

            return View( homeVM );
        }

        public IActionResult AllConfigSettings() 
        {
            List<string> messages = new List<string>();
            messages.Add($"Waze config - Forecast Tracker: " + _wazeForecastSettings.ForecastTrackerEnabled.ToString());
            messages.Add($"Stripe Publishable Key: " + _stripeSettings.PublishableKey);
            messages.Add($"Stripe Secret Key: " + _stripeSettings.SecretKey);
            messages.Add($"Send Grid Key: " + _sendGridSettings.SendGridKey);
            messages.Add($"Twilio Phone: " + _twilioSettings.PhoneNumber);
            messages.Add($"Twilio SID: " + _twilioSettings.AccountSid);
            messages.Add($"Twilio Token: " + _twilioSettings.AuthToken);
            return View(messages);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
