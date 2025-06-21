using System.ComponentModel.DataAnnotations;

namespace Car360.Models
{
    public class Buy
    {
        [Key]
        public int b_id { get; set; }
        public string b_company { get; set; }
        public string b_name { get; set; }
        public string b_price { get; set; }
        public string b_model { get; set; }
        public string b_date { get; set; }
        public int User_ID { get; set; }
        public Sign Signone { get; set; }
    }
}
