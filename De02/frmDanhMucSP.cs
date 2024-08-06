using De02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De02
{
    public partial class frmDanhMucSP : Form
    {
        private QLSanphamContext dbContext;
        private Sanpham originalProduct;

        public frmDanhMucSP()
        {
            InitializeComponent();
            dbContext = new QLSanphamContext();
            LoadProducts();
            LoadLoaiSP();

            buttonSave.Visible = false;
            buttonCancel.Visible = false;
        }

        private void LoadProducts()
        {
            listViewProducts.Items.Clear();
            var products = dbContext.Sanphams.ToList();

            foreach (var product in products)
            {
                var item = new ListViewItem(product.MaSP);
                item.SubItems.Add(product.TenSP);
                item.SubItems.Add(product.Ngaynhap.ToShortDateString());
                var loaiSP = dbContext.LoaiSPs.FirstOrDefault(l => l.MaLoai == product.MaLoai);
                item.SubItems.Add(loaiSP != null ? loaiSP.TenLoai : "Không xác định");
                listViewProducts.Items.Add(item);
            }
        }

        private void LoadLoaiSP()
        {
            comboBoxLoaiSP.DataSource = dbContext.LoaiSPs.ToList();
            comboBoxLoaiSP.DisplayMember = "TenLoai";
            comboBoxLoaiSP.ValueMember = "MaLoai";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var searchTerm = textBoxSearch.Text;
            var products = dbContext.Sanphams
                .Where(p => p.TenSP.Contains(searchTerm))
                .ToList();

            listViewProducts.Items.Clear();

            foreach (var product in products)
            {
                var item = new ListViewItem(product.MaSP);
                item.SubItems.Add(product.TenSP);
                item.SubItems.Add(product.Ngaynhap.ToShortDateString());
                var loaiSP = dbContext.LoaiSPs.FirstOrDefault(l => l.MaLoai == product.MaLoai);
                item.SubItems.Add(loaiSP != null ? loaiSP.TenLoai : "Không xác định");
                listViewProducts.Items.Add(item);
            }
        }

        private void listViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count > 0)
            {
                var selectedItem = listViewProducts.SelectedItems[0];
                var productId = selectedItem.Text;
                var product = dbContext.Sanphams.FirstOrDefault(p => p.MaSP == productId);

                if (product != null)
                {
                    originalProduct = new Sanpham
                    {
                        MaSP = product.MaSP,
                        TenSP = product.TenSP,
                        Ngaynhap = product.Ngaynhap,
                        MaLoai = product.MaLoai
                    };

                    textBoxMaSP.Text = product.MaSP;
                    textBoxTenSP.Text = product.TenSP;
                    dateTimePickerNgayNhap.Value = product.Ngaynhap;
                    dateTimePickerNgayNhap.CustomFormat = "dd/MM/yyyy";
                    dateTimePickerNgayNhap.Format = DateTimePickerFormat.Custom;
                    comboBoxLoaiSP.SelectedValue = product.MaLoai;

                    buttonSave.Visible = false;
                    buttonCancel.Visible = false;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newProduct = new Sanpham
            {
                MaSP = textBoxMaSP.Text,
                TenSP = textBoxTenSP.Text,
                Ngaynhap = dateTimePickerNgayNhap.Value,
                MaLoai = comboBoxLoaiSP.SelectedValue.ToString()
            };

            dbContext.Sanphams.Add(newProduct);

            buttonSave.Visible = true;
            buttonCancel.Visible = true;

            LoadProducts();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var productId = textBoxMaSP.Text;
            var product = dbContext.Sanphams.FirstOrDefault(p => p.MaSP == productId);

            if (product != null)
            {
                originalProduct = new Sanpham
                {
                    MaSP = product.MaSP,
                    TenSP = product.TenSP,
                    Ngaynhap = product.Ngaynhap,
                    MaLoai = product.MaLoai
                };

                product.TenSP = textBoxTenSP.Text;
                product.Ngaynhap = dateTimePickerNgayNhap.Value;
                product.MaLoai = comboBoxLoaiSP.SelectedValue.ToString();

                buttonSave.Visible = true;
                buttonCancel.Visible = true;

                LoadProducts();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var productId = textBoxMaSP.Text;
            var product = dbContext.Sanphams.FirstOrDefault(p => p.MaSP == productId);

            if (product != null)
            {
                dbContext.Sanphams.Remove(product);
                
                buttonSave.Visible = true;
                buttonCancel.Visible = true;

                LoadProducts();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            dbContext.SaveChanges();
            buttonSave.Visible = false;
            buttonCancel.Visible = false;
            LoadProducts();

            MessageBox.Show("Dữ liệu đã được lưu!");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (originalProduct != null)
            {
                var product = dbContext.Sanphams.FirstOrDefault(p => p.MaSP == originalProduct.MaSP);
                if (product != null)
                {
                    product.TenSP = originalProduct.TenSP;
                    product.Ngaynhap = originalProduct.Ngaynhap;
                    product.MaLoai = originalProduct.MaLoai;

                    textBoxTenSP.Text = product.TenSP;
                    dateTimePickerNgayNhap.Value = product.Ngaynhap;
                    comboBoxLoaiSP.SelectedValue = product.MaLoai;
                }
            }

            buttonSave.Visible = false;
            buttonCancel.Visible = false;
            LoadProducts();
            MessageBox.Show("Đã huỷ thao tác!");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
