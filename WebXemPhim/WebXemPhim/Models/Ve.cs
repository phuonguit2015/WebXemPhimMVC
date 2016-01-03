using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public enum TrangThaiThanhToan
    {        
        Chua_Thanh_Toan,
        Da_Thanh_Toan, 
        Da_Huy
    }
    public class Ve
    {
        [DisplayName("Vé")]
        public int VeID { get; set; }

        [DisplayName("Mã Vé")]
        public string Code { get; set; }

        [DisplayName("Ghế")]
        public int GheID { get; set; }

        [DisplayName("Loại Vé")]
        public int LoaiVeID { get; set; }

        [DisplayName("Lịch Chiếu")]
        public int LichChieuID { get; set; }

        [DisplayName("Ngày Đặt Vé")]
        public DateTime NgayDatVe { get; set; }

        [DisplayName("Ngày Thanh Toán")]
        public DateTime NgayThanhToan { get; set; }

        [DisplayName("Số CMND")]
        public string SoCMND { get; set; }

        [DisplayName("Số Điện Thoại")]
        public string SoDienThoai  { get; set;  }

        [DisplayName("Tên Khách Hàng")]
        public string TenKhachHang { get; set; }
        [DisplayName ("Thanh Toán")]
        public decimal ThanhToan { get; set; }
        public string ThanhTien { get; set; }


        public virtual Ghe Ghe { get; set; }
        public virtual LoaiVe LoaiVe { get; set; }
        public virtual LichChieu LichChieu { get; set; }

    }
}