using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace API.IRepository.Repository
{
    public class AnhRepository : IAnhRepository
    {
        private readonly DbContextApp _context;
        private readonly IWebHostEnvironment _env;
        private readonly string _uploadFolderPath;
        private const string _relativePath = "/uploads/images";

        public AnhRepository(DbContextApp context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            _uploadFolderPath = Path.Combine(_env.ContentRootPath, "Uploads", "Images");
        }

        public async Task<IEnumerable<Anh>> GetAllAsync()
        {
            return await _context.Anhs.ToListAsync();
        }

        public async Task<Anh?> GetByIdAsync(Guid id)
        {
            return await _context.Anhs.FindAsync(id);
        }

        public async Task<Anh?> UploadAsync(IFormFile file, string tenAnh)
        {
            if (file == null || file.Length == 0) return null;

            Directory.CreateDirectory(_uploadFolderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_uploadFolderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var anh = new Anh
            {
                TenAnh = tenAnh,
                DuongDan = $"{_relativePath}/{fileName}".Replace("\\", "/"),
                TrangThai = true
            };

            _context.Anhs.Add(anh);
            await _context.SaveChangesAsync();
            return anh;
        }

        public async Task<Anh?> UpdateAsync(Anh anh)
        {
            var existing = await _context.Anhs.FindAsync(anh.AnhId);
            if (existing == null) return null;

            existing.TenAnh = anh.TenAnh;
            existing.TrangThai = anh.TrangThai;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Anh?> UpdateFileAsync(Guid id, IFormFile file, string tenAnh)
        {
            var existing = await _context.Anhs.FindAsync(id);
            if (existing == null || file == null || file.Length == 0) return null;

            // Xóa ảnh cũ nếu tồn tại
            var oldFullPath = Path.Combine(_env.ContentRootPath, existing.DuongDan.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(oldFullPath))
                File.Delete(oldFullPath);

            // Lưu ảnh mới
            Directory.CreateDirectory(_uploadFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_uploadFolderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            existing.DuongDan = $"{_relativePath}/{fileName}".Replace("\\", "/");
            existing.TenAnh = tenAnh ?? existing.TenAnh;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var anh = await _context.Anhs.FindAsync(id);
            if (anh == null) return false;

            var fullPath = Path.Combine(_env.ContentRootPath, anh.DuongDan.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            _context.Anhs.Remove(anh);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
