using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace De02.Models
{
    public class Sanpham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; } 
        public DateTime Ngaynhap { get; set; }
        public string MaLoai { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
