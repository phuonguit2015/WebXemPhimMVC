using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class LichChieu
    {
        [DisplayName("Lịch Chiếu")]
        public int LichChieuID { get; set; }
        
        [DisplayName("Phim")]
        public int PhimID { get; set; }

        [DisplayName("Loại Vé")]
        public int LoaiVeID { get; set; }

        [DisplayName("Phòng Chiếu")]
        public int PhongChieuID { get; set; }

        [DisplayName("Ngày Chiếu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayChieu { get; set; }

        [DisplayName("Giờ Chiếu")]
        [DisplayFormat(DataFormatString = "{0:H:mm:ss}")]
        public DateTime GioChieu { get; set; }

        public virtual Phim Phim { get; set; }
        public virtual LoaiVe LoaiVe { get; set; }
        public virtual PhongChieu PhongChieu { get; set; } 
      

        public string GioChieuToString
        {
            get
            {
                return GioChieu.ToString("H:mm");
            }
        }

        public string NgayChieuToString
        {
            get
            {
                return NgayChieu.ToString("dd/MM/yyyy");
            }
        }


    }
}