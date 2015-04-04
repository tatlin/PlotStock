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
            List<double> stockHighList = new List<double>();
            List<List<double>> stockList = new List<List<double>>();

            //System.DateTime fromDate = new DateTime();
            //System.DateTime.TryParse("1/1/2005 12:00:00 AM" , out fromDate );
            //System.DateTime toDate = System.DateTime.Today;
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
                    string id = qd.ID;
                    //result = qd.ID;
                    foreach (MaasOne.Finance.HistQuotesData qx in qd)
                    {
                        stockHighList.Add(qx.High);
                    }
                    
                    //MaasOne.Finance.HistQuotesData last = qd[qd.Count - 1];
                    stockList.Add(stockHighList);
                }

            }
            return stockList;
        }
    }
}
