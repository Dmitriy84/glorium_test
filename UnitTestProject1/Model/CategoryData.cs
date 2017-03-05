using System;

namespace OlxUaTests.Model
{
    class CategoryData
    {
        public string Title { get; set; }
        public Action FillCategory { get; set; }
        public string Description { get; set; }
        public string[] Address { get; set; }
        public string Phone { get; set; }
    }
}