using Moq;
using Roomex.Interview.Core.Services.Interfaces;
using Roomex.Interview.Core.Services;

namespace Roomex.Interview.Core.Tests
{
    public class RegionInfoResolverTests
    {
        [Fact]
        public void When_Valid_Header_Is_Provided_Returns_RegionInfo()
        {
            // Arrange
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            configurationFacadeMock.Setup(x => x.GetDefaultLocale()).Returns("ro-RO");

            var resolver = new RegionInfoResolver(configurationFacadeMock.Object);
            var header = "en-US;en";

            // Act
            var result = resolver.Resolve(header);

            // Assert
            Assert.Equal("US", result.Name);
        }

        [Fact]
        public void When_Null_Header_Is_Provided_Returns_Default_RegionInfo()
        {
            // Arrange
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            configurationFacadeMock.Setup(x => x.GetDefaultLocale()).Returns("ro-RO");

            var resolver = new RegionInfoResolver(configurationFacadeMock.Object);

            // Act
            var result = resolver.Resolve(null);

            // Assert
            Assert.Equal("RO", result.Name);
        }

        [Fact]
        public void When_Invalid_Header_Is_Provided_Throws_Argument_Exception()
        {
            // Arrange
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            configurationFacadeMock.Setup(x => x.GetDefaultLocale()).Returns("en-US");

            var resolver = new RegionInfoResolver(configurationFacadeMock.Object);
            var invalidHeader = "InvalidHeader";

            // Act and Assert
            Assert.Throws<ArgumentException>(() => resolver.Resolve(invalidHeader));
        }
    }
}
