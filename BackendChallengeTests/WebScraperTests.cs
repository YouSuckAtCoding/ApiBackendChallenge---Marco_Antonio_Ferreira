using ApiBackendChallenge.Scraper;
using ApiBackendChallenge.Utility;

namespace BackendChallengeTests
{
    public class WebScraperTests
    {
        //Teste demora muito pra concluir, melhor opção é debugar e ver se a lista é populada coretamente.
        [Fact]
        public void TestWebScraperGenerateProductList_ExpectedToWork()
        {
            UtilityClass utility = new UtilityClass();
            var configuration = utility.GetSettings();
            WebScraper webScraper = new WebScraper(configuration);
            Assert.NotEmpty(webScraper.GenerateProducts());
        }
    }
}