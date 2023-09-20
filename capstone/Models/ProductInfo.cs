using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class ProductInfo
    {
        [Key]
        public int productNum { get; set; }
        public int productcategory { get; set; }
            
        public string name { get; set; }
            
        public int manufacturer{ get; set; }
            
        public int price { get; set; }
            
        public bool bundledeliveryfee{ get; set; }
            
        public bool onSale { get; set; }
        public bool soldOut { get; set; }

        public string image { get; set; }

        public string descriptionHtml { get; set; }
        
    }
}
