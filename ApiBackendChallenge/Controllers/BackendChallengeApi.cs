using ApiBackendChallenge.Scraper;
using ApiBackendChallenge.Utility;
using Datalibrary.ProductRepository;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using System.Net;

namespace ApiBackendChallenge.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BackendChallengeApi : ControllerBase
    {
        private readonly IProductRepository _product;

        private readonly UtilityClass utilityClass;

        public BackendChallengeApi(IProductRepository product, UtilityClass utilityClass)
        {
            _product = product;
            this.utilityClass = utilityClass;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return ((int)HttpStatusCode.OK) + " Fullstack Challenge 20201026";
        }

        private async Task<List<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = await _product.GetProducts();
            return products.ToList();
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            await CheckDate();
            try
            {
                var res = await GetAllProducts();
                if (res.Count() == 0)
                {
                   await CheckDate();
                }
                return Ok(res);
            }
            catch(Exception ex)
            {
                utilityClass.GetLog().Error(ex, "Exception caught at BackendChallengeApi in GetProducts Method");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("products/{code}")]
        public async Task<ActionResult<IEnumerable<Product>>> Get(long code)
        {
            await CheckDate();
            try
            {
                var res = await _product.GetProduct(code);
                return Ok(res);
            }
            catch(Exception ex)
            {
                utilityClass.GetLog().Error(ex, "Exception caught at BackendChallengeApi in Get Method");
                return BadRequest(ex.Message);
            }
            
            
        }

        private async Task InsertProducts(List<Product> products)
        {
            try{
               await _product.InsertProducts(products);
            }
            catch(Exception ex)
            {
                utilityClass.GetLog().Error(ex, "Exception caught at BackendChallengeApi in ScrapeProducts Method");
            }
            
        }

        private async Task CheckDate()
        {
            DateTime updateTime = new DateTime(2000, 1, 1, 02, 00, 00);
            DateTime now = DateTime.Now;
            int res = TimeSpan.Compare(updateTime.TimeOfDay, now.TimeOfDay);
            var allProds = await GetAllProducts();
            if (res == 0)          
                await UpdateDatabase(allProds);
            
        }

        private async Task UpdateDatabase(List<Product> products)
        {
            UtilityClass utility = new UtilityClass();
            WebScraper scraper = new WebScraper(utility.GetSettings());
            List<Product> newProducts = scraper.GenerateProducts();
            newProducts = utility.FormatLists(newProducts);
            products = utility.FormatLists(products);
            var newlyFetched = newProducts.Except(newProducts.Where(y => products.Any(x => x.Code == y.Code))).ToList();
            await InsertProducts(newlyFetched);

        }

        
        

    }
}
