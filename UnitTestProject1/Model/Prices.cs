using System.Collections.Generic;

namespace OlxUaTests.Model
{
    public class Range
    {
        public int avgPrice { get; set; }
        public int maxPrice { get; set; }
        public int minPrice { get; set; }
        public int minstay { get; set; }
        public string startDate { get; set; }
        public int week { get; set; }
    }

    public class Ranges
    {
        public bool is_error { get; set; }
        public string message { get; set; }
        public string messageCode { get; set; }
        public string currency { get; set; }
        public List<Range> range { get; set; }
        public int propertyId { get; set; }
    }

    public class Prices
    {
        public Ranges ranges { get; set; }
    }
}