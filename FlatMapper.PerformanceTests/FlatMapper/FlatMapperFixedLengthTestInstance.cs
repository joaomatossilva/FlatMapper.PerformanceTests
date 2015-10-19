using System.Collections.Generic;
using System.IO;

namespace FlatMapper.PerformanceTests.FlatMapper
{
    public class FlatMapperFixedLengthTestInstance : TestInstance<TestObject>
    {
        private Layout<TestObject> _layout;

        public FlatMapperFixedLengthTestInstance(string name) : base(name)
        {

        }


        protected override void Setup()
        {
            _layout = new Layout<TestObject>.FixedLengthLayout()
                .WithMember(m => m.Id, settings => settings.WithLength(10).WithLeftPadding('0'))
                .WithMember(m => m.Description, settings => settings.WithLength(10).WithRightPadding(' '))
                .WithMember(m => m.NullableInt, settings => settings.WithLength(1).AllowNull("N"));
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
