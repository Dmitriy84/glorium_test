using OlxUaTests.Model;
using OlxUaTests.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using System.Web.Script.Serialization;

namespace OlxUaTests.Steps
{
    class MyBookingPalComSteps
    {
        public string GetLocationId(string[] address = null)
        {
            var response = GetResponse("https://www.mybookingpal.com/api/location/getlocations/?term=" + address[0]);
            var locations = new JavaScriptSerializer().Deserialize<List<MyBookingPalLocations>>(response);
            var label = string.Join(", ", address);
            return locations.Find(i => i.label == label).ID;
        }

        public Quote GetProduct(string productId, string product, DateTime from, DateTime to)
        {
            var response = GetResponse("https://www.mybookingpal.com/xml/services/json/reservation/products/" + productId + "/" + from.ToString("yyyy-MM-dd") + "/" + to.ToString("yyyy-MM-dd") + "?pos=a502d2c65c2f75d3&guests=2&amenity=true&currency=USD&exec_match=false&display_inquire_only=false");
            var products = new JavaScriptSerializer().Deserialize<Products>(response);
            return products.search_response.search_quotes.quote.Find(p => p.productname == product);
        }

        public Prices GetPrices(int productid, DateTime date)
        {
            var response = GetResponse("https://www.mybookingpal.com/xml/services/json/reservation/prices?pos=a502d2c65c2f75d3&productid=" + productid + "&fromdate=" + date + "&currency=USD");
            return new JavaScriptSerializer().Deserialize<Prices>(response);
        }

        private static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse webResponse = request.GetResponse();
            using (Stream webStream = webResponse.GetResponseStream())
                if (webStream != null)
                    using (StreamReader responseReader = new StreamReader(webStream))
                        return responseReader.ReadToEnd();
                else
                    throw new Exception("Response code: " + ((HttpWebResponse)webResponse).StatusCode);
        }
    }
}