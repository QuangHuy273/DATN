using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.IRepository.Repository
{
    public class ChiTietHoaDonRepository : IChiTietHoaDonRepository
    {
        private readonly DbContextApp _contextApp;

        public ChiTietHoaDonRepository(DbContextApp contextApp)
        {
            _contextApp = contextApp;
        }

        public async Task<IEnumerable<HoaDonChiTiet>> GetAllHDCTAsync()
        {
            return await _contextApp.HoaDonChiTiets
                .Include(x => x.Giays)
                .Include(x => x.HoaDons)
                .ToListAsync();
        }

        public async Task<IEnumerable<HoaDonChiTiet>> GetByHoaDonChiTietIdAsync(Guid hdctID)
        {
            return await _contextApp.HoaDonChiTiets
                .Where(x => x.HoaDonChiTietId == hdctID)
                .Include(x => x.Giays)
                .Include(x => x.HoaDons)
                .ToListAsync();
        }

        public async Task<HoaDonChiTiet?> GetByIdHDCTAsync(Guid IdCTHD)
        {
            return await _contextApp.HoaDonChiTiets
                .Include(x => x.Giays)
                .Include(x => x.HoaDons)
                .FirstOrDefaultAsync(x => x.HoaDonChiTietId == IdCTHD);
        }

        public async Task<HoaDonChiTiet?> GetByHoaDonChiTietvaGiayAsync(Guid hoaDonId, Guid giayId)
        {
            return await _contextApp.HoaDonChiTiets
                .Include(x => x.Giays)
                .Include(x => x.HoaDons)
                .FirstOrDefaultAsync(x => x.HoaDonId == hoaDonId && x.GiayId == giayId);
        }
    }
}
