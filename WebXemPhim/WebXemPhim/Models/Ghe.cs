using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebXemPhim.Models
{
    public class Ghe
    {
        [DisplayName("Ghế")]
        public int GheID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số ghế.")]
        [StringLength(200, ErrorMessage = "Số ký tự nhập tối đa là 200.")]
        [DisplayName("Số Ghế")]
        public string SoGhe { get; set; }
        public virtual ICollection<Ve> Ves { get; set; }

    }
}