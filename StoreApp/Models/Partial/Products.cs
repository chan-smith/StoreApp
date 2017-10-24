using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreApp.Models
{
    [MetadataType(typeof(MetadataProducts))]
    public partial class Products
    {
        public class MetadataProducts
        {
            [DisplayName("產品編號")]
            public int ProductID { get; set; }
            
            [DisplayName("分類編號")]
            public int CategoryID { get; set; }

            [DisplayName("產品型號")]
            public string ModelNumber { get; set; }

            [DisplayName("產品名稱")]
            public string ModelName { get; set; }

            [DisplayName("產品圖片")]
            public string ProductImage { get; set; }

            [DisplayFormat(DataFormatString = "{0:c0}")]
            [DisplayName("產品價格")]
            public Nullable<decimal> UnitCost { get; set; }

            [DisplayName("產品描述")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
            public byte[] BytesImage { get; set; }
        }
    }
}