using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Product
    {
            public long Code { get; set; }
            public string Barcode { get; set; }
            public Status Status { get; set; }
            public DateTime Imported_t { get; set; }
            public string Quantity { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string Categories { get; set; }
            public string Packaging { get; set; }
            public string Brands { get; set; }
            public string Image_Url { get; set; }

        

       
    }
    public enum Status
    {
        Draft,
        Imported
    }
}
