using System;
using Xunit;
using Xunit.Abstractions;
using XUnitElegantTesting.Utils;


namespace XunitElegantTestingTests.Utils
{
    [CollectionDefinition(name:"Guid generator")]
    public class GuidGeneratorDefition : ICollectionFixture<GuidGenerator>{}


    [Collection(name: "Guid generator")]
    public class GuidGeneratorTestsOne : IDisposable
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;

        public GuidGeneratorTestsOne(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }

        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}"); 
        }

        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }

        public void Dispose()
        {
            _output.WriteLine("The class was disposed");
        }

    }


    [Collection(name: "Guid generator")]
    public class GuidGeneratorTestsTwo : IDisposable
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;

        public GuidGeneratorTestsTwo(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }

        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }

        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }

        public void Dispose()
        {
            _output.WriteLine("The class was disposed");
        }

    }

}