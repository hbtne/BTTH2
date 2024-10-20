using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.Authentication;
using ThucHanhWebMVC.ViewModels;
using X.PagedList;

namespace ThucHanhWebMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private QlbanVaLiContext db = new QlbanVaLiContext();

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		//[Authentication]
		public IActionResult Index(int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lsp = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> newLsp = new PagedList<TDanhMucSp>(lsp, pageNumber, pageSize);
			return View(newLsp);
		}

		public IActionResult SanPhamTheoLoai(string maloai, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lsp = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> newLsp = new PagedList<TDanhMucSp>(lsp, pageNumber, pageSize);
			ViewBag.maloai = maloai;
			return View(newLsp);
		}

		public IActionResult ChiTietSanPham(string maSp)
		{
			var sanpham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
			ViewBag.anhSanPham = anhSanPham;
			return View(sanpham);
		}

		public IActionResult ProductDetail(string maSp)
		{
			var sanpham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
			var homeProductDetailViewModel = new HomeProductDetailViewModel
			{
				danhMucSp = sanpham,
				anhSps = anhSanPham
			};
			return View(homeProductDetailViewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}