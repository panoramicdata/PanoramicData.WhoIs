using EmailLookup;
using Xunit;

namespace EmailLookupTests
{
    public class GoogleSearchTests
    {
        [Fact]
        public async void ValidEmailSearch_ShouldReturnSomething()
        {
            LinkedinGoogleSearchResponse response = new LinkedinGoogleSearchResponse();
            response = await Program.GoogleSearch("example", "profile", "company.com", "1501e4a4050ced422", "AIzaSyBaI60DXdj9q03JcDz3fq8uGlUd5fLvPhA");

            Assert.NotNull(response);
            Assert.NotNull(response.Title);
            Assert.NotNull(response.Url);
            Assert.NotNull(response.Desc);
        }
    }
}