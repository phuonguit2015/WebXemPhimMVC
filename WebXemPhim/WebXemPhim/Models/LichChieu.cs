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

        [DisplayName("Phòng Chiếu")]
        public int PhongChieuID { get; set; }

        [DisplayName("Ngày Chiếu")]
        public DateTime NgayChieu { get; set; }

        [DisplayName("Giờ Chiếu")]
        public DateTime GioChieu { get; set; }

        public virtual Phim Phim { get; set; }
        public virtual PhongChieu PhongChieu { get; set; }


    }
}