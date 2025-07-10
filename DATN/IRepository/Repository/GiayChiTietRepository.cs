using API.IRepository;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GiayChiTietRepository : IGiayChiTietRepository
    {
        private readonly DbContextApp _context;

        public GiayChiTietRepository(DbContextApp context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiayChiTiet>> GetAllAsync()
        {
            return await _context.GiayChiTiets
                .Include(x => x.Giay)
                .Include(x => x.KichCo)
                .Include(x => x.MauSac)
                .ToListAsync();
        }

        public async Task<IEnumerable<GiayChiTiet>> GetByGiayIdAsync(Guid giayId)
        {
            return await _context.GiayChiTiets
                .Where(x => x.GiayId == giayId)
                .Include(x => x.Giay)
                .Include(x => x.KichCo)
                .Include(x => x.MauSac)
                .ToListAsync();
        }

        public async Task<GiayChiTiet> GetByIdAsync(Guid id)
        {
            return await _context.GiayChiTiets
                .Include(x => x.Giay)
                .Include(x => x.KichCo)
                .Include(x => x.MauSac)
                .FirstOrDefaultAsync(x => x.GiayChiTietId == id);
        }

        public async Task<GiayChiTiet> AddAsync(GiayChiTiet chiTiet)
        {
            chiTiet.GiayChiTietId = Guid.NewGuid();
            chiTiet.NgayTao = DateTime.Now;
            chiTiet.NgaySua = DateTime.Now;

            _context.GiayChiTiets.Add(chiTiet);
            await _context.SaveChangesAsync();
            return chiTiet;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.GiayChiTiets.FindAsync(id);
            if (entity == null) return false;

            _context.GiayChiTiets.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddMultipleAsync(List<GiayChiTiet> chiTietList)
        {
            foreach (var item in chiTietList)
            {
                item.GiayChiTietId = Guid.NewGuid();
                item.NgayTao = DateTime.Now;
                item.NgaySua = DateTime.Now;
                _context.GiayChiTiets.Add(item);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
