using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public enum TrangThaiPhim
    {
        Da_Chieu,
        Sap_Chieu,
        Dang_Chieu
    }

    public class Phim
    {
        public int PhimID { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên  phim.")]
        [StringLength(200, ErrorMessage = "Số ký tự nhập tối đa là 200.")]
        [DisplayName("Tên Phim")]
        public string TenPhim { get; set; }

        [DisplayName("Đạo Diễn")]
        public string DaoDien { get; set; }

        [DisplayName("Diễn Viên")]
        public string DienVien { get; set; }

        [DisplayName("Nội Dung")]
        public string NoiDung { get; set; }

        [DisplayName("Poster")]
        public byte[] Poster { get; set; }

        [DisplayName("Thời Lượng")]
        public string ThoiLuong { get; set; }

        [DisplayName("Trailer URL")]
        public string TrailerURL { get; set; }

        [DisplayName("Trạng Thái")]
        public TrangThaiPhim TrangThai { get; set; }

        [DisplayName("Ngày Chiếu")]
        public DateTime NgayChieu { get; set; }

        [DisplayName("Loại Phim")]
        public int LoaiPhimID { get; set; }

        public virtual LoaiPhim LoaiPhim { get; set; }
    }
}