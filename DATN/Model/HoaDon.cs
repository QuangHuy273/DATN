using System.ComponentModel.DataAnnotations;

namespace DATN.Model
{
    public class HoaDon
    {
        [Key]
        public Guid HoaDonId { get; set; } = Guid.NewGuid();

        public Guid TaiKhoanId { get; set; }
        public Guid? KhachHangId { get; set; }
        public Guid HinhThucThanhToanId { get; set; }
        public Guid? VoucherId { get; set; }


        [MaxLength(50)]
        public string TenCuaKhachHang { get; set; }

        [MaxLength(20)]
        public string SDTCuaKhachHang { get; set; }

        [MaxLength(50)]
        public string EmailCuaKhachHang { get; set; }

        public DateTime NgayTao { get; set; }
        public DateTime NgayNhanHang { get; set; }

        public float TongTienSauKhiGiam { get; set; }

        public string TrangThai { get; set; }

        [MaxLength(200)]
        public string GhiChu { get; set; }

        public Voucher? voucher { get; set; }
        public TaiKhoan taiKhoan { get; set; }
        public HinhThucThanhToan hinhThucThanhToan { get; set; }
        public KhachHang? khachHang { get; set; }
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    }
    public class HoaDonChiTiet
    {
        [Key]
        public Guid HoaDonChiTietId { get; set; } = Guid.NewGuid();

        public Guid GiayId { get; set; }

        public Guid HoaDonId { get; set; }

        [Range(1, int.MaxValue)]
        public int SoLuongSanPham { get; set; }

        public decimal Gia { get; set; }

        // Đánh dấu đây là hàng mua hay trả lại
        public bool TrangThai { get; set; } = false;

        // Ghi nhận thời điểm khách trả hàng (chỉ có nếu TrangThai = true)
        public DateTime? NgayTraHang { get; set; }

        // Trạng thái dòng chi tiết (nếu cần phân biệt logic nâng cao)
        [MaxLength(20)]
        public string? TrangThaiChiTiet { get; set; }

        // Ghi chú nếu có
        [MaxLength(255)]
        public string? GhiChu { get; set; }

        // Navigation properties
        public virtual HoaDon HoaDons { get; set; }
        public virtual Giay Giays { get; set; }
    }
    public class GioHangChiTiet
    {
        [Key]
        public Guid GioHangChiTietId { get; set; }

        public Guid GioHangId { get; set; }

        public Guid GiayId { get; set; }

        public int SoLuongSanPham { get; set; }


        public decimal Gia { get; set; }

        public decimal ThanhTien => Gia * SoLuongSanPham;

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public bool TrangThai { get; set; }
        public virtual GioHang GioHang { get; set; }
        public virtual Giay Giays { get; set; }
    }
    public class GioHang
    {
        [Key]
        public Guid GioHangId { get; set; }
        public DateTime NgayTaoGioHang { get; set; }
        public DateTime NgayCapNhatCuoiCung { get; set; }
        public bool TrangThai { get; set; }
        public Guid KhachHangId { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();

    }
    public class HinhThucThanhToan
    {

        [Key]
        public Guid HinhThucThanhToanId { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯàáâãèéêìíòóôõùúăđĩũơưẠ-ỹ\s0-9]+$", ErrorMessage = "Tên chỉ được chứa chữ cái tiếng Việt, số và khoảng trắng")]
        public string TenHinhThuc { get; set; }
        public string MoTa { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
    public class Voucher
    {
        [Key]
        public Guid VoucherId { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVoucher { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Range(0, 100)]
        public float PhanTram { get; set; }

        public bool TrangThai { get; set; }

        public int SoLuong { get; set; }

        public Guid? IdTaiKhoan { get; set; }

        public void Validate()
        {
            if (NgayBatDau.Date < DateTime.Today)
                throw new Exception("Ngày bắt đầu không hợp lệ.");
            if (NgayKetThuc <= NgayBatDau)
                throw new Exception("Ngày kết thúc phải sau ngày bắt đầu.");
        }
        public virtual TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
