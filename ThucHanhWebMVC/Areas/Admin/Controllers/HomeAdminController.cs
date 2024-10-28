using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.Authentication;
using X.PagedList;

namespace ThucHanhWebMVC.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{
		private QlbanVaLiContext db = new QlbanVaLiContext();

		[Route("")]
		[Route("index")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("danhmucsanpham")]
		public IActionResult DanhMucSanPham(int? page)
		{
			int pageSize = 12;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lsp = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> newLsp = new PagedList<TDanhMucSp>(lsp, pageNumber, pageSize);
			return View(newLsp);
		}

		[Route("ThemSanPhamMoi")]
		[HttpGet]
		public IActionResult ThemSanPhamMoi()
		{
			ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
			ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
			ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
			ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
			ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
			return View();
		}

		[Route("ThemSanPhamMoi")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
		{
			if (ModelState.IsValid)
			{
				db.TDanhMucSps.Add(sanPham);
				db.SaveChanges();
				return RedirectToAction("DanhMucSanPham");
			}
			return View(sanPham);
		}

		[Route("SuaSanPham")]
		[HttpGet]
		public IActionResult SuaSanPham(string maSp)
		{
			ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
			ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
			ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
			ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
			ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
			var sanPham = db.TDanhMucSps.Find(maSp);
			return View(sanPham);
		}

		[Route("SuaSanPham")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SuaSanPham(TDanhMucSp sanPham)
		{
			if (ModelState.IsValid)
			{
				db.Entry(sanPham).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("DanhMucSanPham", "HomeAdmin");
			}
			return View(sanPham);
		}

		[Route("XoaSanPham")]
		[HttpGet]
		public IActionResult XoaSanPham(string maSp)
		{
			TempData["Message"] = "";
			var chiTietSP = db.TChiTietSanPhams.Where(x => x.MaSp == maSp).ToList();
			if (chiTietSP.Count() > 0)
			{
				TempData["Message"] = "Không thề xóa sản phẩm sản này";
				return RedirectToAction("DanhMucSanPham", "HomeAdmin");
			}
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp);
			if (anhSanPham.Any())
				db.RemoveRange(anhSanPham);
			db.Remove(db.TDanhMucSps.Find(maSp));
			db.SaveChanges();
			TempData["Message"] = "Đã xóa thành công";
			return RedirectToAction("DanhMucSanPham", "HomeAdmin");
		}
        [Route("DanhSachNguoiDung")]
        public IActionResult DanhSachNguoiDung(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var users = db.TUsers.AsNoTracking().OrderBy(x => x.Username);
            PagedList<TUser> pagedUsers = new PagedList<TUser>(users, pageNumber, pageSize);
            return View(pagedUsers);
        }

        // Create User - GET
        [Route("ThemNguoiDungMoi")]
        [HttpGet]
        public IActionResult ThemNguoiDungMoi()
        {
            return View();
        }

        // Create User - POST
        [Route("ThemNguoiDungMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguoiDungMoi(TUser user)
        {
            if (ModelState.IsValid)
            {
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("DanhSachNguoiDung");
            }
            return View(user);
        }

        // Edit User - GET
        [Route("SuaNguoiDung")]
        [HttpGet]
        public IActionResult SuaNguoiDung(string id)
        {
            var user = db.TUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit User - POST
        [Route("SuaNguoiDung")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNguoiDung(TUser user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachNguoiDung");
            }
            return View(user);
        }

        // Delete User - GET
        [Route("XoaNguoiDung")]
        [HttpGet]
        public IActionResult XoaNguoiDung(string id)
        {
            var user = db.TUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Delete User - POST
        [Route("XoaNguoiDung")]
        [HttpPost, ActionName("XoaNguoiDung")]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoaNguoiDung(string id)
        {
            var user = db.TUsers.Find(id);
            if (user != null)
            {
                db.TUsers.Remove(user);
                db.SaveChanges();
                TempData["Message"] = "Người dùng đã được xóa thành công";
            }
            return RedirectToAction("DanhSachNguoiDung");
        }
    }
}