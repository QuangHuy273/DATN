using Data.Models;

namespace API.IRepository
{
    public interface IGiayChiTietRepository
    {
        Task<IEnumerable<GiayChiTiet>> GetAllAsync();
        Task<IEnumerable<GiayChiTiet>> GetByGiayIdAsync(Guid giayId);
        Task<GiayChiTiet> GetByIdAsync(Guid id);
        Task<GiayChiTiet> AddAsync(GiayChiTiet chiTiet);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddMultipleAsync(List<GiayChiTiet> chiTietList);
    }
}
