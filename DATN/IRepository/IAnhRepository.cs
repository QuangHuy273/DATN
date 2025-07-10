using Data.Models;

namespace API.IRepository
{
    public interface  IAnhRepository
    {
        // Lấy danh sách ảnh
        Task<IEnumerable<Anh>> GetAllAsync();

        // Lấy 1 ảnh theo id
        Task<Anh> GetByIdAsync(Guid id);

        // Thêm ảnh (tải lên)
        Task<Anh> UploadAsync(IFormFile file, string tenAnh);

        // Sửa thông tin ảnh (không đổi file)
        Task<Anh> UpdateAsync(Anh anh);

        // Sửa file ảnh (có đổi file)
        Task<Anh> UpdateFileAsync(Guid id, IFormFile file, string tenAnh);

        // Xóa ảnh (và file vật lý)
        Task<bool> DeleteAsync(Guid id);
    }
}
