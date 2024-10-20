using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Repository
{
    public class LoaiSanPhamRepository : ILoaiSanPhamRepository
    {
        private readonly  QlbanVaLiContext _context;

        public LoaiSanPhamRepository(QlbanVaLiContext context)
        {
            _context = context;
        }

        public TLoaiSp Add(TLoaiSp loaiSp)
        {
            _context.TLoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public TLoaiSp Delete(TLoaiSp maloaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSP()
        {
           return _context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(TLoaiSp maloaiSp)
        {
            return _context.TLoaiSps.Find(maloaiSp);
        }

        public TLoaiSp Update(TLoaiSp loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
