using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThucChien.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            //Danh sách các thuộc tính
            [DisplayName("Mã thành viên")]
            public int MaThanhVien { get; set; }
            [DisplayName("Tài khoản")]
            [Required(ErrorMessage = "</br>Nhập tài khoản")]
            [StringLength(10, ErrorMessage = "Không quá 10 kí tự")]
            //[Range(5, 10 , ErrorMessage)]
            public string TaiKhoan { get; set; }
            [DisplayName("Mật khẩu")]
            public string MatKhau { get; set; }
            [DisplayName("Họ tên")]
            public string HoTen { get; set; }
            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }
            [DisplayName("Email")]
            public string Email { get; set; }
            [DisplayName("Số điện thoại")]
            public string SoDienThoai { get; set; }
            [DisplayName("Câu hỏi")]
            public string CauHoi { get; set; }
            [DisplayName("Câu trả lời")]
            public string CauTraLoi { get; set; }
            [DisplayName("Mã loại thành viên")]
            public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}