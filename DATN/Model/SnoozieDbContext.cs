using Microsoft.EntityFrameworkCore;

namespace DATN.Model
{
    public class SnoozieDbContext : DbContext
    {
        public SnoozieDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GiayDotGiamGia>()
      .HasKey(gdg => gdg.GiayDotGiamGiaId);
            modelBuilder.Entity<GioHangChiTiet>()
        .Property(ghct => ghct.Gia)
        .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<GiayDotGiamGia>()
                .HasOne(gdg => gdg.Giay)
                .WithMany(g => g.GiayDotGiamGias)
                .HasForeignKey(gdg => gdg.GiayId);

            // Quan hệ GiayDotGiamGia - GiamGia (N-1)
            modelBuilder.Entity<GiayDotGiamGia>()
                .HasOne(gdg => gdg.GiamGia)
                .WithMany(g => g.GiayDotGiamGias)
                .HasForeignKey(gdg => gdg.GiamGiaId);
            modelBuilder.Entity<ChucVu>()
        .HasMany(cv => cv.nhanViens)
        .WithOne(nv => nv.ChucVu)
        .HasForeignKey(nv => nv.ChucVuId)
        .OnDelete(DeleteBehavior.Restrict);
            //abc

            // Seed dữ liệu cứng: Admin và Nhân viên
            modelBuilder.Entity<ChucVu>().HasData(
                new ChucVu
                {
                    ChucVuId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    TenChucVu = "Admin",
                    MotaChucVu = "Quản trị hệ thống",
                    TrangThai = 1
                },
                new ChucVu
                {
                    ChucVuId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    TenChucVu = "NhanVien",
                    MotaChucVu = "Nhân viên bán hàng",
                    TrangThai = 1
                });
            var adminTaiKhoanId = Guid.Parse("99999999-9999-9999-9999-999999999999");

            modelBuilder.Entity<TaiKhoan>().HasData(new TaiKhoan
            {
                TaikhoanId = adminTaiKhoanId,
                Username = "admin",
                Password = "admin123",
                Ngaytaotaikhoan = DateTime.Now
            });





            modelBuilder.Entity<NhanVien>().HasData(new NhanVien
            {
                NhanVienId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                HoTen = "Nguyễn Văn Quản Trị",
                SoDienThoai = "0987654321",
                Email = "admin@shop.com",
                NgaySinh = new DateTime(1995, 1, 1),
                NgayCapNhatCuoiCung = DateTime.Now,
                TrangThai = true,
                TaikhoanId = adminTaiKhoanId,
                ChucVuId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });


            // Dữ liệu mẫu kích cỡ từ 35 đến 50
            var kichCoList = new List<KichCo>();
            for (int size = 35; size <= 50; size++)
            {
                kichCoList.Add(new KichCo
                {
                    KichCoId = Guid.NewGuid(),
                    TenKichCo = $"Size {size}",
                    size = size,
                    MoTa = $"Cỡ giày {size}",
                    TrangThai = true
                });
            }
            modelBuilder.Entity<KichCo>().HasData(kichCoList.ToArray());

            modelBuilder.Entity<MauSac>().HasData(
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Đỏ", Color = "#FF0000", MoTa = "Màu đỏ cơ bản", TrangThai = true },
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Xanh dương", Color = "#0000FF", MoTa = "Màu xanh dương cơ bản", TrangThai = true },
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Xanh lá", Color = "#00FF00", MoTa = "Màu xanh lá cây", TrangThai = true },
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Vàng", Color = "#FFFF00", MoTa = "Màu vàng", TrangThai = true },
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Đen", Color = "#000000", MoTa = "Màu đen", TrangThai = true },
                new MauSac { MauSacId = Guid.NewGuid(), TenMau = "Trắng", Color = "#FFFFFF", MoTa = "Màu trắng", TrangThai = true }
            );

            // NhanVien - ChucVu
            modelBuilder.Entity<NhanVien>()
      .HasOne(nv => nv.ChucVu)
      .WithMany(cv => cv.nhanViens)
      .HasForeignKey(nv => nv.ChucVuId);



            // NhanVien - TaiKhoan


            //hoadon-taikhoan

            modelBuilder.Entity<HoaDon>()

                .HasOne(h => h.taiKhoan)
                .WithMany(t => t.hoaDons)
                .HasForeignKey(h => h.TaiKhoanId)
                .OnDelete(DeleteBehavior.Restrict);
            //hoadon-Khachhang
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.khachHang)
                .WithMany(t => t.HoaDons)
                .HasForeignKey(h => h.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()

                .HasOne(h => h.hinhThucThanhToan)
                .WithMany(t => t.HoaDons)

                .HasForeignKey(h => h.HinhThucThanhToanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()

                .HasOne(h => h.voucher)
                .WithMany(t => t.HoaDons)

                .HasForeignKey(h => h.VoucherId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<KhachHang>()
           .HasOne(kh => kh.TaiKhoan)
           .WithMany()
           .HasForeignKey(kh => kh.TaiKhoanId)
           .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HoaDonChiTiet>()
    .Property(x => x.Gia)
    .HasColumnType("decimal(18,2)");

        }
        public DbSet<KhachHang> KhachHangs { get; set; }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }

        public DbSet<Giay> Giays { get; set; }
        public DbSet<GiayChiTiet> GiayChiTiets { get; set; }
        public DbSet<ChatLieu> ChatLieus { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<KichCo> KichCos { get; set; }
        public DbSet<DeGiay> DeGiays { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<GiamGia> GiamGias { get; set; }
        public DbSet<GiayDotGiamGia> GiayDotGiamGias { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<KieuDang> KieuDangs { get; set; }
        public DbSet<TheLoaiGiay> TheLoaiGiays { get; set; }
        public DbSet<Anh> Anhs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<HinhThucThanhToan> hinhThucThanhToans { get; set; }
        public DbSet<DiaChiKhachHang> diaChiKhachHangs { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
    }
}
