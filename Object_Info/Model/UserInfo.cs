using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool GA_Confirmed { get; set; } 
        public List<string> List_User_Functions { get; set; }
    }
}