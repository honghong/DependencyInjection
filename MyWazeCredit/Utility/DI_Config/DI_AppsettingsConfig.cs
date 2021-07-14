using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWazeCredit.Utility.AppsettingsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWazeCredit.Utility.DI_Config
{
    public static class DI_AppsettingsConfig
    {
        public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services, IConfiguration configuration)         
        {

            services.Configure<WazeForecastSettings>(configuration.GetSection("WazeForecast"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));

            return services;
        }
    }
}
