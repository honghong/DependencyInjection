using MyWazeCredit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWazeCredit.Service
{
    public class MarketForecaster : IMarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            return new MarketResult
            {
                MarketCondition = MarketCondition.StableUp
            };

        }
    }

    public class MarketResult
    {
        public MarketCondition MarketCondition { get; set; }
    }
}
