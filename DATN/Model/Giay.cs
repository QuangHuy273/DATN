using System.ComponentModel.DataAnnotations;

namespace DATN.Model
{
    public class Giay
    {
        [Key]
        public Guid GiayId { get; set; }
        public Guid? ChatLieuId { get; set; }
        public Guid? ThuongHieuId { get; set; }
        public Guid? KieuDangId { get; set; }
        public Guid? TheLoaiGiayId { get; set; }
        public Guid? DeGiayId { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenGiay { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public virtual ThuongHieu? ThuongHieu { get; set; }
        public virtual KieuDang? KieuDang { get; set; }
        public virtual DeGiay? DeGiay { get; set; }
        public virtual TheLoaiGiay? TheLoaiGiay { get; set; }
        public virtual ChatLieu? ChatLieu { get; set; }
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
        public virtual ICollection<GiayDotGiamGia> GiayDotGiamGias { get; set; } = new List<GiayDotGiamGia>();
        public virtual ICollection<GiayChiTiet> GiayChiTiets { get; set; } = new List<GiayChiTiet>();
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();
    }
    public class GiayChiTiet
    {
        [Key]
        public Guid GiayChiTietId { get; set; }

        [Required]
        public Guid GiayId { get; set; }


        public Guid? KichCoId { get; set; }
        public Guid? MauSacId { get; set; }


        public int SoLuongCon { get; set; }
        public string AnhGiay { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgaySua { get; set; }
        public float Gia { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; }

        public virtual Giay Giay { get; set; }
        public virtual KichCo? KichCo { get; set; }
        public virtual MauSac? MauSac { get; set; }
        public virtual ICollection<Anh> Anhs { get; set; } = new List<Anh>();
    }
    public class GiayDotGiamGia
    {
        [Key]
        public Guid GiayDotGiamGiaId { get; set; }
        public Guid? GiamGiaId { get; set; }
        public Guid? GiayId { get; set; }
        public virtual Giay Giay { get; set; }
        public virtual GiamGia GiamGia { get; set; }

    }
    public class GiamGia : IValidatableObject
    {
        [Key]
        public Guid GiamGiaId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenGiamGia { get; set; }

        public string? SanPhamKhuyenMai { get; set; } // ✅ cho phép null

        public float PhanTramKhuyenMai { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public bool TrangThai { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayBatDau.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Ngày bắt đầu không được ở trong quá khứ.", new[] { nameof(NgayBatDau) });
            }
        }
        public virtual ICollection<GiayDotGiamGia> GiayDotGiamGias { get; set; } = new List<GiayDotGiamGia>();

    }
    public class ChatLieu
    {
        [Key]
        public Guid ChatLieuId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenChatLieu { get; set; }

        public string MoTa { get; set; }

        public bool TrangThai { get; set; }
        public virtual ICollection<Giay> Giays { get; set; } = new List<Giay>();
    }
    public class TheLoaiGiay
    {
        [Key]
        public Guid TheLoaiGiayId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string TenTheLoai { get; set; }

        public string MoTa { get; set; }

        public bool TrangThai { get; set; }
        public virtual ICollection<Giay> Giays { get; set; } = new List<Giay>();
    }
    public class DeGiay
    {
        [Key]
        public Guid DeGiayId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenDeGiay { get; set; }

        public string MoTa { get; set; }

        public bool TrangThai { get; set; }
        public virtual ICollection<Giay> Giays { get; set; } = new List<Giay>();

    }
    public class KieuDang
    {
        [Key]
        public Guid KieuDangId { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Tên không được chứa ký tự đặc biệt.")]
        public string TenKieuDang { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
        public virtual ICollection<Giay> Giays { get; set; } = new List<Giay>();
    }
    public class Anh
    {
        [Key]
        public Guid AnhId { get; set; }
        public Guid GiayChiTietId { get; set; }
        public string DuongDan { get; set; }
        public string TenAnh { get; set; }
        public bool TrangThai { get; set; }


        public Anh()
        {
            AnhId = Guid.NewGuid();
            TrangThai = true;
        }
        public virtual GiayChiTiet GiayChiTiet { get; set; }

    }
    public class ThuongHieu
    {
        [Key]
        public Guid ThuongHieuId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Tên không được chứa ký tự đặc biệt.")]
        public string TenThuongHieu { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string SDT { get; set; }

        public string DiaChi { get; set; }

        public string MoTa { get; set; }

        public bool TrangThai { get; set; }
        public virtual ICollection<Giay> Giays { get; set; } = new List<Giay>();
    }
    public class KichCo
    {
        [Key]
        public Guid KichCoId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenKichCo { get; set; }
        public int size { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
        public virtual ICollection<GiayChiTiet> GiayChiTiets { get; set; } = new List<GiayChiTiet>();
    }
    public class MauSac
    {
        [Key]
        public Guid MauSacId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
        public string TenMau { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string MoTa { get; set; }

        public bool TrangThai { get; set; }
        public virtual ICollection<GiayChiTiet> GiayChiTiets { get; set; } = new List<GiayChiTiet>();

    }
}
