using OlxUaTests.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using System.Web.Script.Serialization;

namespace OlxUaTests.Steps
{
    class MyBookingPalComSteps
    {
        private string _url;

        public MyBookingPalComSteps(string url = "https://www.mybookingpal.com")
        {
            _url = url;
        }

        public string GetLocationId(string[] address = null)
        {
            var locations = GetResponseDeserialized<List<MyBookingPalLocations>>(_url + "/api/location/getlocations/?term=" + address[0]);
            var label = string.Join(", ", address);
            return locations.Find(i => i.label == label).ID;
        }

        public Quote GetProduct(string productId, string product, DateTime from, DateTime to)
        {
            var products = GetResponseDeserialized<Products>(_url + "/xml/services/json/reservation/products/" + productId + "/" + from.ToString("yyyy-MM-dd") + "/" + to.ToString("yyyy-MM-dd") + "?pos=a502d2c65c2f75d3&guests=2&amenity=true&currency=USD&exec_match=false&display_inquire_only=false");
            return products.search_response.search_quotes.quote.Find(p => p.productname == product);
        }

        public Prices GetPrices(int productid, DateTime date)
        {
            var response = GetResponseDeserialized<Prices>(_url + "/xml/services/json/reservation/prices?pos=a502d2c65c2f75d3&productid=" + productid + "&fromdate=" + date + "&currency=USD");
            return response;
        }

        private static T GetResponseDeserialized<T>(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            var webResponse = request.GetResponse();
            using (var webStream = webResponse.GetResponseStream())
                if (webStream != null)
                    using (var responseReader = new StreamReader(webStream))
                    {
                        var response = responseReader.ReadToEnd();
                        return new JavaScriptSerializer().Deserialize<T>(response);
                    }
                else
                    throw new Exception("Response code: " + ((HttpWebResponse)webResponse).StatusCode);
        }
    }
}