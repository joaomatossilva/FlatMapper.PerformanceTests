using System.Collections.Generic;
using System.IO;

namespace FlatMapper.PerformanceTests.FlatMapper
{
    public class FlatMapperDelimitedTestInstance : TestInstance<TestObject>
    {
        private Layout<TestObject> _layout;

        public FlatMapperDelimitedTestInstance(string name) : base(name)
        {

        }


        protected override void Setup()
        {
            _layout = new Layout<TestObject>.DelimitedLayout()
                .WithDelimiter(";")
                .WithQuote("\"")
                .WithMember(m => m.Id, _ => {})
                .WithMember(m => m.Description, _ => { })
                .WithMember(m => m.NullableInt, settings => settings.AllowNull("N"));
        }

        public override void PerformWriteTest(IEnumerable<TestObject> items)
        {
            using (var stream = File.OpenWrite(TempFile))
            {
                var file = new FlatFile<TestObject>(_layout, stream);
                file.Write(items);
            }
        }

        public override void PerformReadTest()
        {
            using (var stream = File.OpenRead(TempFile))
            {
                var file = new FlatFile<TestObject>(_layout, stream);
                var items = file.Read();
                foreach (var item in items)
                {
                    var id = item.Id;
                }
            }
        }
    }
}
