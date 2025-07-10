using Data.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.IRepository.Repository
{
    public class GiamGiaRepository : IGiamGiaRepository
    {
        private readonly DbContextApp _context;

        public GiamGiaRepository(DbContextApp context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiamGia>> GetAllAsync()
        {
            return await _context.GiamGias
                .Include(x => x.GiayDotGiamGias)
                .ThenInclude(y => y.Giay)
                .ToListAsync();
        }

        public async Task<GiamGia> GetByIdAsync(Guid id)
        {
            return await _context.GiamGias
                .Include(x => x.GiayDotGiamGias)
                .ThenInclude(y => y.Giay)
                .FirstOrDefaultAsync(x => x.GiamGiaId == id);
        }

        public async Task<GiamGia> AddAsync(GiamGia giamGia)
        {
            giamGia.GiamGiaId = Guid.NewGuid();
            _context.GiamGias.Add(giamGia);
            await _context.SaveChangesAsync();
            return giamGia;
        }

        public async Task<GiamGia> UpdateAsync(GiamGia giamGia)
        {
            var existing = await _context.GiamGias.FindAsync(giamGia.GiamGiaId);
            if (existing == null) return null;

            existing.TenGiamGia = giamGia.TenGiamGia;
            existing.SanPhamKhuyenMai = giamGia.SanPhamKhuyenMai;
            existing.PhanTramKhuyenMai = giamGia.PhanTramKhuyenMai;
            existing.NgayBatDau = giamGia.NgayBatDau;
            existing.NgayKetThuc = giamGia.NgayKetThuc;
            existing.TrangThai = giamGia.TrangThai;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var giamGia = await _context.GiamGias.FindAsync(id);
            if (giamGia == null) return false;
            var links = _context.GiayDotGiamGias.Where(x => x.GiamGiaId == id);
            _context.GiayDotGiamGias.RemoveRange(links);
            _context.GiamGias.Remove(giamGia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddGiayToDotGiamGia(Guid giamGiaId, Guid giayId)
        {
            var giamGia = await _context.GiamGias.FindAsync(giamGiaId);
            var giay = await _context.Giays.FindAsync(giayId);
            if (giamGia == null || giay == null) return false;

            var exists = await _context.GiayDotGiamGias
                .AnyAsync(x => x.GiamGiaId == giamGiaId && x.GiayId == giayId);
            if (exists) return false;

            var link = new GiayDotGiamGia
            {
                GiamGiaId = giamGiaId,
                GiayId = giayId
            };
            _context.GiayDotGiamGias.Add(link);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveGiayFromDotGiamGia(Guid giamGiaId, Guid giayId)
        {
            var link = await _context.GiayDotGiamGias
                .FirstOrDefaultAsync(x => x.GiamGiaId == giamGiaId && x.GiayId == giayId);
            if (link == null) return false;

            _context.GiayDotGiamGias.Remove(link);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
