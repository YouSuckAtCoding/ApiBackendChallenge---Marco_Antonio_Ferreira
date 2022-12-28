using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ModelLibrary;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Text;
using System.Text.RegularExpressions;


namespace ApiBackendChallenge.Scraper

{
    public class WebScraper
    {
        private readonly IConfiguration config;

        public WebScraper(IConfiguration config)
        {
            this.config = config;
        }
        public List<Product> GenerateProducts()
        {
            var links = GetLinks();
            List<Product> products = GenerateProductList(links);
            return products;
        }
        private List<string> GetLinks()
        {
            var html = GetHtml(config.GetValue<string>("FoodUrl"));
            var links = html.CssSelect("div > ul > li");
            return PopulateLinkList(links);
        }

        private List<String> PopulateLinkList(IEnumerable<HtmlNode> links)
        {
            List<string> ProductLinks = new List<string>();
            foreach (var link in links)
            {
                var mainUrl = link.CssSelect("a");
                foreach (var main in mainUrl)
                {
                    string href = main.Attributes["href"].Value;
                    if (href.Contains("/product"))
                    {
                        ProductLinks.Add("https://world.openfoodfacts.org" + href);
                    }
                }

            }

            return ProductLinks;
        }

        private HtmlNode GetHtml(string Url)
        {
            ScrapingBrowser _scrapBrowser = new ScrapingBrowser();
            _scrapBrowser.IgnoreCookies = true;
            _scrapBrowser.Timeout = TimeSpan.FromMinutes(15);
            _scrapBrowser.Headers["User-Agent"] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_5)" +
                                                    "AppleWebKit / 605.1.15(KHTML, like Gecko)" +
                                                        "Version / 12.1.1 Safari / 605.1.15";
            WebPage _webpage = _scrapBrowser.NavigateToPage(new Uri(Url));
            return _webpage.Html;

        }

        private List<Product> GenerateProductList(List<string> Links)
        {
            List<Product> Products = new List<Product>();
            foreach (var link in Links)
            {

                var html = GetHtml(link);
                var nodes = GetProductNodes(html);
                Product newProduct = new Product();
                foreach (var node in nodes)
                {
                    GetProductProperty(newProduct, node);
                }

                newProduct.Imported_t = DateTime.Now;
                newProduct.Status = Status.Imported;
                newProduct.Url = link;
                newProduct.Image_Url = GetImageUrl(newProduct.Code);
                newProduct.Name = CheckNullName(newProduct, html);
                Products.Add(newProduct);
            }

            return Products;
        }

        private string CheckNullName(Product newProduct, HtmlNode? html)
        {
            if(newProduct.Name is null)
            {
                var elements = html.CssSelect("div#prodInfos");
                var prod = elements.CssSelect("div.row");
                var prodElements = prod.CssSelect("div.medium-8");
                var productInfo = prodElements.CssSelect("h2.title-1");

                foreach(var node in productInfo)
                {
                    
                        string text = node.FirstChild.InnerText;
                        return text;
                    
                }


            }
            return newProduct.Name;

        }

        private void GetProductProperty(Product newProduct, HtmlNode node)
        {
            if (node.Id == "barcode_paragraph")
            {
                string[] separator = { "Barcode: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Barcode = strings[1];
                var code = node.CssSelect("span");
                newProduct.Code = Convert.ToInt64(code.First().InnerText);
            }

            if (node.Id == "field_generic_name")
            {
                string[] separator = { "Common name: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Name = strings[1];
                
            }

            if (node.Id == "field_quantity")
            {
                string[] separator = { "Quantity: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Quantity = strings[1];
            }

            if (node.Id == "field_packaging")
            {
                string[] separator = { "Packaging: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Packaging = strings[1];
            }

            if (node.Id == "field_brands")
            {
                string[] separator = { "Brands: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Brands = strings[1];
            }

            if (node.Id == "field_categories")
            {
                string[] separator = { "Categories: " };
                string[] strings = node.InnerText.Split(separator, StringSplitOptions.TrimEntries);
                newProduct.Categories = strings[1];
            }
        }

        private IEnumerable<HtmlNode> GetProductNodes(HtmlNode html)
        {
            var elements = html.CssSelect("div#prodInfos");
            var prod = elements.CssSelect("div.row");
            var prodElements = prod.CssSelect("div.medium-8");
            var productInfo = prodElements.CssSelect("p");

            return productInfo;
        }

        private string GetImageUrl(long code)
        {         
            string url = config.GetValue<string>("ImagesUrl");
            string check = CheckIfUrlLenghtIs8(code, url);
            if (check.Length > 0)
            {
                return check;
            }
            string parsedCode = GenerateParsedImageUrl(code, url);
            return parsedCode;

        }

        private string CheckIfUrlLenghtIs8(long code, string url)
        {
            if (code.ToString().Length == 8)
            {
                url = url + code.ToString();
                return url;
            }
            else
            {
                return "";
            }
        }

        private string GenerateParsedImageUrl(long code, string url)
        {
            string pattern = @"^(...)(...)(...)(.*)$";
            Regex rg = new Regex(pattern);
            string[] parsedCode = rg.Split(code.ToString()).Where(x => x != String.Empty).ToArray();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= parsedCode.Length - 1; i++)
            {
                if (i == parsedCode.Length - 1)
                {
                    str.Append(parsedCode[i]);
                    break;
                }

                str.Append(parsedCode[i]);
                str.Append("/");

            }
            url = url + str;
            return url;
        }

        
    }
}
