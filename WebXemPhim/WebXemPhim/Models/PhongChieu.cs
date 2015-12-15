using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class PhongChieu
    {
        public int PhongChieuID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên phòng chiếu.")]
        [StringLength(200, ErrorMessage = "Số ký tự nhập tối đa là 200.")]
        [DisplayName("Phòng Chiếu")]
        public string TenPhongChieu { get; set; }

        [DisplayName("Số Lượng Ghế")]
        public int SoLuongGhe { get; set; }

        [DisplayName("Thông Tin")]
        public string ThongTin { get; set; }
    }
}