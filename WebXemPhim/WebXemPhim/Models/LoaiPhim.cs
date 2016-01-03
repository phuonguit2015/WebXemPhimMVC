using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebXemPhim.Models
{
    public class LoaiPhim
    {
        [DisplayName("Loại Phim")]
        public int LoaiPhimID { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập tên loại phim.")]
        [StringLength(200,ErrorMessage ="Số ký tự nhập tối đa là 200.")]
        [DisplayName ("Loại Phim")]
        //[Index(IsUnique = true)]
        public string TenLoaiPhim { get; set; }

        public virtual ICollection<Phim> Phims { get; set; }
    }
}