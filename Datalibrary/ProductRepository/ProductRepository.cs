using Datalibrary.DataAccess;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalibrary.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public ProductRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public async Task<IEnumerable<Product>> GetProducts() => await sqlDataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetAll", new { });

        public async Task<Product?> GetProduct(long code)
        {
            var res = await sqlDataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetByCode", new { Code = code });
            return res.FirstOrDefault();
        }

        public async Task InsertProducts(List<Product> Products)
        {

            string status = "";
            foreach (var product in Products)
            {
                
                if(product.Status == Status.Imported)
                {
                    status = "imported";
                }
                if (product.Status == Status.Draft)
                {
                    status = "drafted";
                }
                try
                {
                   await sqlDataAccess.SaveData("dbo.spProduct_Insert", new
                    {
                        product.Code,
                        product.Barcode,
                        status,
                        product.Imported_t,
                        product.Quantity,
                        product.Url,
                        product.Name,
                        product.Categories,
                        product.Packaging,
                        product.Brands,
                        product.Image_Url
                    });

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }


        }




    }
}
