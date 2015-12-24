using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class ShortLichChieu
    {
        [DisplayName("Lịch Chiếu")]
        public int LichChieuID { get; set; }     

        [DisplayName("Ngày Chiếu")]
        public string NgayChieu { get; set; }

        [DisplayName("Giờ Chiếu")]
        public string GioChieu { get; set; }
    }
}