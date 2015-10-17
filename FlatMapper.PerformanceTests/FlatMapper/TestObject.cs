using System.Collections.Generic;
using System.Linq;

namespace FlatMapper.PerformanceTests.FlatMapper
{
    public partial class TestObject
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? NullableInt { get; set; }

        public static IEnumerable<TestObject> GenerateItems(int count)
        {
            return Enumerable.Range(1, count).Select(x => new TestObject
            {
                Id = x,
                Description = "desript",
                NullableInt = x%5 == 0 ? null : (int?)x % 5
            });
        } 
    }
}