using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class NguoiDung
    {
        public int NguoiDungID { get; set; }

        [DisplayName ("Tên Người Dùng")]
        public string TenNguoiDung { get; set; }

        [DisplayName ("Tên Tài Khoản")]
        public string TaiKhoan { get; set; }

        [DisplayName ("Mật Khẩu")]
        public string MatKhau { get; set; }
    }
}