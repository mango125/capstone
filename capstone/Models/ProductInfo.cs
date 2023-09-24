using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class ProductInfo //상품의 기본 정보
    {
        [Key]
        public int productNum { get; set; }
        public int productcategory { get; set; }

        public string name { get; set; }
        public int manufacturer{ get; set; }
            
        public int price { get; set; }
            
        public bool bundledeliveryfee { get; set; }
            
        public bool onSale { get; set; }
        public bool soldOut{ get; set; }

        public string image { get; set; }

        public string descriptionHtml { get; set; }
    }
    public class Product_Mainboard //상품의 상세 정보 : 메인보드
    {
        [Key]   
        public int productNum { get; set; }
        public string productName { get; set; }

        public string productManufacturer { get; set; }
        public string productCategory { get; set; }

        public int productPrice { get; set; }

        public bool productBundledeliveryfee { get; set; }

        public bool productOnsale { get; set; }
        public bool productSoldOut { get; set; }

        public string productImage { get; set; }

        public string productDescriptionHtml { get; set; }
        public string mainboardName { get; set; }
        public string mainboardFormfactor { get; set; }
        public string mainboardCpuSocket { get; set; }
        public string mainboardRamSocket { get; set; }
        public string mainboardChipset { get; set; }
        public int mainboardMemorySlots { get; set; }

        
    }
}
