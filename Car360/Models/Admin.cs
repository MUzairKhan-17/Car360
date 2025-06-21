using System.ComponentModel.DataAnnotations;

namespace Car360.Models
{
    public class Admin
    {
        [Key]
        public int a_id { get; set; }
        public string a_name { get; set; }
        public string a_mail { get; set; }
        public string a_image { get; set; }
        public string a_pass { get; set; }
        public int a_status { get; set; }

        public Admin() 
        {
            a_status = 1;
        }
    }
}
