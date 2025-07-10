using Data.IRepository;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DiaChiKhachHangRepository : IDiaChiKhachHangRepository
    {
        private readonly DbContextApp _context;

        public DiaChiKhachHangRepository(DbContextApp context)
        {
            _context = context;
        }

        public async Task<List<DiaChiKhachHang>> GetAllAsync()
        {
            return await _context.diaChiKhachHangs
                .Include(d => d.KhachHang)
                .Where(d => d.TrangThai == true)
                .ToListAsync();
        }

        public async Task<DiaChiKhachHang> GetByIdAsync(Guid id)
        {
            return await _context.diaChiKhachHangs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.DiaChiKhachHangId == id && d.TrangThai == true);
        }

        public async Task<List<DiaChiKhachHang>> GetByKhachHangIdAsync(Guid khachHangId)
        {
            return await _context.diaChiKhachHangs
                .Where(d => d.khachHangId == khachHangId && d.TrangThai == true)
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(DiaChiKhachHang diaChi)
        {
            try
            {
                await _context.diaChiKhachHangs.AddAsync(diaChi);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(DiaChiKhachHang diaChi)
        {
            try
            {
                _context.diaChiKhachHangs.Update(diaChi);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var diaChi = await _context.diaChiKhachHangs.FindAsync(id);
            if (diaChi == null) return false;

            // Xóa mềm bằng cách đổi trạng thái
            diaChi.TrangThai = false;

            _context.diaChiKhachHangs.Update(diaChi);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
