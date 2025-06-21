using System.ComponentModel.DataAnnotations;

namespace Car360.Models
{
    public class Sign
    {
        [Key]
        public int s_id { get; set; }
        public string s_name { get; set; }
        public string s_user { get; set; }
        public string s_mail { get; set; }
        public string s_phone { get; set; }
        public string s_image { get; set; }
        public string s_pass { get; set; }
        public int s_status { get; set; }
        public List<Buy> Buyone { get; set; }

        public Sign() 
        {
            s_status = 1;
        }

    }
}
