using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp4.Models
{
    public class Client
    {
        public int id { get; set; }
        public string firstname { get; set; } = "";
        public string lastname { get; set; } = "";
        public string email { get; set; } = "";
        public string phone { get; set; } = "";
        public string address { get; set; } = "";
        public string created_at { get; set; } = "";
    }
}
