using System;
using System.Globalization;

namespace CurrencyConverterApp
{
    class CurrencyConverter
    {
        /// <summary>
        /// Gets all available currency tags
        /// </summary>
        public static string[] GetCurrencyTags()
        {

            // Hardcoded currency tags neccesairy to parse the ecb xml's
            return new string[] {"eur", "usd", "jpy", "bgn", "czk", "dkk", "gbp", "huf", "ltl", "lvl"
            , "pln", "ron", "sek", "chf", "nok", "hrk", "rub", "try", "aud", "brl", "cad", "cny", "hkd", "idr", "ils"
            , "inr", "krw", "mxn", "myr", "nzd", "php", "sgd", "zar"};
        }

        /// <summary>
        /// Get currency exchange rate in euro's 
        /// </summary>
        public static float GetCurrencyRate(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
  
            try
            {
                // Create valid RSS url to european central bank
                string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");

                // Create & Load New Xml Document
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                // Create XmlNamespaceManager for handling XML namespaces.
                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                // Get list of daily currency exchange rate between selected "currency" and the EURO
                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                // Loop Through all XMLNODES with daily exchange rates
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    try
                    {
                        // Get currency exchange rate with EURO from XMLNODE
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any, 
                            ci);

                        return exchangeRate;
                    }
                    catch { }
                }

                // currency not parsed!! 
                // return default value
                return 0;
            }
            catch
            {
                // currency not parsed!! 
                // return default value
                return 0;
            }
        }

        /// <summary>
        /// Get The Exchange Rate Between 2 Currencies
        /// </summary>
        public static float GetExchangeRate(string from, string to, float amount)
        {
            // If currency's are empty abort
            if (from == null || to == null)
                return 0;

            try
            {
                // Convert Same Exchange Rate
                if (from.ToLower() == to.ToLower())
                {
                    return amount;
                }

                // Get the exchange rate of both currencies in euro
                float toRate = GetCurrencyRate(to) / 1;
                float fromRate = GetCurrencyRate(from) / 1;
                // Calculate exchange rate From A to B
                return (amount * toRate) / fromRate;
            }
            catch { return 0; }
        }
    }
}
