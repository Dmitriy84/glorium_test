using OlxUaTests.Model;

namespace OlxUaTests.Utils
{
    static class CategoryDataFactory
    {
        public static CategoryData GetSimple()
        {
            return new CategoryData() { Title = Util.GetRandomString(), Address = new[] { "Винница", "Винницкая область", "Замостянский" }, Phone = "0634596992", Description = Util.GetRandomString(20) };
        }
    }
}