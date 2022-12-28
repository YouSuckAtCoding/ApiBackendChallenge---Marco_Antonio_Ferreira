using ApiBackendChallenge.Controllers;
using ApiBackendChallenge.Utility;
using Datalibrary.ProductRepository;
using FakeItEasy;

namespace BackendChallengeTests
{
    public class ApiTests
    {

       
        [Fact]
        public async Task TestDatabaseUpdate_ExpectedToWorkAsync() {

            UtilityClass utility = new UtilityClass();
            var fakeRepo = A.Fake<IProductRepository>();
            var controller = new BackendChallengeApi(fakeRepo, utility);
            //Necessário tornar métodos públicos na api para testar, porém isso faz a api parar de rodar.
            await controller.CheckDate();


        }

        [Fact]
        public void ScrapeProducts_ExpectedToWork()
        {
            UtilityClass utility = new UtilityClass();
            var fakeRepo = A.Fake<IProductRepository>();
            var controller = new BackendChallengeApi(fakeRepo, utility);
            //Necessário tornar métodos públicos na api para testar, porém isso faz a api parar de rodar.
            //controller.ScrapeProducts();


        }

        [Fact]
        public void TestGetAll_ExpectedToWork()
        {
            UtilityClass utility = new UtilityClass();
            var fakeRepo = A.Fake<IProductRepository>();
            var controller = new BackendChallengeApi(fakeRepo, utility);
            var res = controller.GetProducts();
            Assert.NotNull(res);


        }
        [Fact]
        public void TestGet_ExpectedToWork()
        {
            UtilityClass utility = new UtilityClass();
            var fakeRepo = A.Fake<IProductRepository>();
            var controller = new BackendChallengeApi(fakeRepo, utility);
            var res = controller.Get(80135463);
            Assert.NotNull(res);


        }

        [Fact]
        public void TestGet_ExpectedToWork_ShouldReturnStatusCode()
        {
            UtilityClass utility = new UtilityClass();
            var fakeRepo = A.Fake<IProductRepository>();
            var controller = new BackendChallengeApi(fakeRepo, utility);
            var res = controller.Get();
            Assert.NotNull(res);


        }

       

    }

   

   

    
}
