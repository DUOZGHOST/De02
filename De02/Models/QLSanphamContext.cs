using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace De02.Models
{
    public class QLSanphamContext : DbContext
    {
        public QLSanphamContext() : base("name=QLSanphamConnection") { }

        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<Sanpham> Sanphams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiSP>()
                .HasKey(l => l.MaLoai);

            modelBuilder.Entity<Sanpham>()
                .HasKey(s => s.MaSP);

            modelBuilder.Entity<Sanpham>()
                .HasRequired(s => s.LoaiSP)
                .WithMany(l => l.Sanphams)
                .HasForeignKey(s => s.MaLoai);
        }
    }
}
