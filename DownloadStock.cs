/*
 * A Dynamo zero-touch library to pull stock values from the Yahoo Finance web service.
 * This code is loosely based on this yahoo finance managed code sample:
 * https://code.google.com/p/yahoo-finance-managed/wiki/sampleYahooManagedAPIHistQuotesDownload
 * 
 * Copyright (c) 2015 Matt Jezyk
 * Licened under the MIT License, see project README.md file
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaasOne;

namespace PlotStock
{
    public class DownloadStock
    {

        public static List<List<double>> DownloadData(List<string> stockNameList, System.DateTime fromDate,
            System.DateTime toDate)
        {
            //Parameters
            IEnumerable<string> ids = stockNameList;

            //new string[] {"ADSK", "APPL",};

            string result = "";

            List<List<double>> stockList = new List<List<double>>();


            MaasOne.Finance.YahooFinance.HistQuotesInterval interval =
                MaasOne.Finance.YahooFinance.HistQuotesInterval.Monthly;

            //Download
            MaasOne.Finance.YahooFinance.HistQuotesDownload dl = new MaasOne.Finance.YahooFinance.HistQuotesDownload();
            MaasOne.Base.Response<MaasOne.Finance.YahooFinance.HistQuotesResult> resp = dl.Download(ids, fromDate,
                toDate, interval);
            //Response/Result
            if (resp.Connection.State == MaasOne.Base.ConnectionState.Success)
            {
                foreach (MaasOne.Finance.HistQuotesDataChain qd in resp.Result.Chains)// just one stock for now
                {
                    List<double> stockHighList = new List<double>();
                    string id = qd.ID;
                    //result = qd.ID;
                    foreach (MaasOne.Finance.HistQuotesData qx in qd)
                    {
                        stockHighList.Add(qx.High);
                    }

                    stockList.Add(stockHighList);
                }

            }
            return stockList;
        }
    }
}
