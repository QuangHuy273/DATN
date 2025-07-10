using System.ComponentModel.DataAnnotations;

namespace DATN.Model
{
    public class TaiKhoan
    {
        [Key]
        public Guid TaikhoanId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Ngaytaotaikhoan { get; set; }

        public virtual ICollection<HoaDon> hoaDons { get; set; } = new List<HoaDon>();
        public virtual NhanVien? NhanVien { get; set; }

    }
    public class KhachHang
    {
        [Key]
        public Guid KhachHangId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s]+$", ErrorMessage = "Họ tên chỉ chứa chữ cái và khoảng trắng")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(09|03)\d{8}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 09 hoặc 03 và có đúng 10 số")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhatCuoiCung { get; set; }
        public Guid? TaiKhoanId { get; set; }
        public virtual TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public virtual ICollection<DiaChiKhachHang> DiaChiKhachHangs { get; set; } = new List<DiaChiKhachHang>();
        public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
    }
    public class DiaChiKhachHang
    {
        [Key]
        public Guid DiaChiKhachHangId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenDiaChi { get; set; }
        public Guid khachHangId { get; set; }
        public bool TrangThai { get; set; } = true;

        public virtual KhachHang KhachHang { get; set; }


    }
    public class NhanVien
    {
        [Key]
        public Guid NhanVienId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s]+$", ErrorMessage = "Họ tên chỉ chứa chữ cái và khoảng trắng")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(09|03)\d{8}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 09 hoặc 03 và có đúng 10 số")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        public DateTime NgaySinh { get; set; }
        public bool TrangThai { get; set; }
        public Guid? ChucVuId { get; set; }
        public Guid? TaikhoanId { get; set; }

        public DateTime NgayCapNhatCuoiCung { get; set; }

        public ChucVu? ChucVu { get; set; }
        public virtual TaiKhoan? TaiKhoan { get; set; }
    }
    public class ChucVu
    {
        [Key]
        public Guid ChucVuId { get; set; }


        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]

        public string TenChucVu { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
        public string MotaChucVu { get; set; }

        [Required]
        public int TrangThai { get; set; }
        public virtual ICollection<NhanVien> nhanViens { get; set; }

    }
}
