using System.ComponentModel.DataAnnotations;

namespace Car360.Models
{
    public class Product
    {
        [Key]
        public int p_id { get; set; }
        public string p_company { get; set; }
        public string p_name { get; set; }
        public string p_price { get; set; }
        public string p_model { get; set; }
        public string p_image { get; set; }
        public int p_status { get; set; }

        public Product() 
        {
            p_status = 1;
        }
    }
}
