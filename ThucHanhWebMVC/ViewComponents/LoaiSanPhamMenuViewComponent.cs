using ThucHanhWebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Repository;

namespace ThucHanhWebMVC.ViewComponents
{
    public class LoaiSanPhamMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSanPhamRepository _loaiSanPham;
        public LoaiSanPhamMenuViewComponent(ILoaiSanPhamRepository loaisanpham) 
        {
            _loaiSanPham = loaisanpham;        
        }

        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSanPham.GetAllLoaiSP().OrderBy(x => x.Loai);
            return View(loaisp);
        }
    }
}
