using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class LoaiVe
    {
        [DisplayName("Loại Vé")]
        public int LoaiVeID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên loại vé.")]
        [StringLength(200, ErrorMessage = "Số ký tự nhập tối đa là 200.")]
        [DisplayName("Loại Vé")]
        public string TenLoaiVe { get; set; }

        [DisplayName("Giá Vé")]
        public decimal GiaTri { get; set; }

        public virtual ICollection<Ve> Ves { get; set; }

    }
}