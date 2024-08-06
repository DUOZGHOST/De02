using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace De02.Models
{
    public class LoaiSP
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
