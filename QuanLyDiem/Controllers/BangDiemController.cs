using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Slicer.Style;
using QuanLyDiem.Data;
using QuanLyDiem.Models;
using QuanLyDiem.Models.Process;
using X.PagedList;

namespace QuanLyDiem.Controllers
{
    [Authorize]
    public class BangDiemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public BangDiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BangDiem
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.BangDiem.Include(d => d.HocPhan).Include(d => d.LopHocPhan).Include(d => d.SinhVien).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.MaHocPhan.Contains(searchText) || d.TenHocPhan.Contains(searchText) || d.MaSinhVien.Contains(searchText) || d.TenSinhVien.Contains(searchText));
            }
            var bangDiem = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = bangDiem.Count;
            }

            var totalCount = bangDiem.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(bangDiem.ToPagedList(pageNumber, actualPageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BangDiem == null)
            {
                return NotFound();
            }

            var bangDiem = await _context.BangDiem
                .Include(d => d.HocPhan)
                .Include(d => d.LopHocPhan)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaBangDiem == id);
            if (bangDiem == null)
            {
                return NotFound();
            }

            return View(bangDiem);
        }

        // GET: BangDiem/Create
        public IActionResult Create()
        {
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan");
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhan, "MaLopHocPhan", "TenLopHocPhan");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien");
            return View();
        }

        // POST: Diem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBangDiem,MaSinhVien,MaHocPhan,MaLopHocPhan,DiemChuyenCan,DiemKiemTra,DiemThi")] BangDiem bangDiem)
        { 
            if (ModelState.IsValid)
            {
                var selectedHocPhan = await _context.HocPhan.FindAsync(bangDiem.MaHocPhan);
                if (selectedHocPhan != null)
                {
                    bangDiem.SoTinChi = selectedHocPhan.SoTinChi;
                    bangDiem.TenHocPhan = selectedHocPhan.TenHocPhan;
                }
                var selectedSinhVien = await _context.SinhVien.FindAsync(bangDiem.MaSinhVien);
                if (selectedHocPhan != null)
                {
                    bangDiem.TenSinhVien = selectedSinhVien.TenSinhVien;
                }
                if (!_context.BangDiem.Any(b => b.MaSinhVien == bangDiem.MaSinhVien && b.MaHocPhan == bangDiem.MaHocPhan || b.MaSinhVien == bangDiem.MaSinhVien && b.MaLopHocPhan == bangDiem.MaLopHocPhan))
                {
                    _context.Add(bangDiem);
                    await _context.SaveChangesAsync();
                    await TinhDiemTrungBinhTichLuyAsync(bangDiem.MaSinhVien);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = $"Bản ghi đã tồn tại!";
                }
            }
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", bangDiem.MaHocPhan);
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhan, "MaLopHocPhan", "TenLopHocPhan", bangDiem.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", bangDiem.MaSinhVien);
            return View(bangDiem);
        }

        // GET: BangDiem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BangDiem == null)
            {
                return NotFound();
            }

            var bangDiem = await _context.BangDiem.FindAsync(id);
            if (bangDiem == null)
            {
                return NotFound();
            }
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", bangDiem.MaHocPhan);
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhan, "MaLopHocPhan", "TenLopHocPhan", bangDiem.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", bangDiem.MaSinhVien);
            return View(bangDiem);
        }

        // POST: BangDiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBangDiem,MaSinhVien,MaHocPhan,MaLopHocPhan,DiemChuyenCan,DiemKiemTra,DiemThi")] BangDiem bangDiem)
        {
            if (id != bangDiem.MaBangDiem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var selectedHocPhan = await _context.HocPhan.FindAsync(bangDiem.MaHocPhan);
                if (selectedHocPhan != null)
                {
                    bangDiem.SoTinChi = selectedHocPhan.SoTinChi;
                    bangDiem.TenHocPhan = selectedHocPhan.TenHocPhan;
                }
                var selectedSinhVien = await _context.SinhVien.FindAsync(bangDiem.MaSinhVien);
                if (selectedHocPhan != null)
                {
                    bangDiem.TenSinhVien = selectedSinhVien.TenSinhVien;
                }
                try
                {
                    _context.Update(bangDiem);
                    await _context.SaveChangesAsync();
                    await TinhDiemTrungBinhTichLuyAsync(bangDiem.MaSinhVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BangDiemExists(bangDiem.MaBangDiem))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", bangDiem.MaHocPhan);
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhan, "MaLopHocPhan", "TenLopHocPhan", bangDiem.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", bangDiem.MaSinhVien);
            return View(bangDiem);
        }

        // GET: Diem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BangDiem == null)
            {
                return NotFound();
            }

            var bangDiem = await _context.BangDiem
                .Include(d => d.HocPhan)
                .Include(d => d.LopHocPhan)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaBangDiem == id);
            if (bangDiem == null)
            {
                return NotFound();
            }

            return View(bangDiem);
        }

        // POST: Diem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BangDiem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BangDiem'  is null.");
            }
            var bangDiem = await _context.BangDiem.FindAsync(id);
            if (bangDiem != null)
            {
                _context.BangDiem.Remove(bangDiem);
            }
            
            await _context.SaveChangesAsync();
            await TinhDiemTrungBinhTichLuyAsync(bangDiem.MaSinhVien);
            return RedirectToAction(nameof(Index));
        }

        private bool BangDiemExists(int id)
        {
          return (_context.BangDiem?.Any(e => e.MaBangDiem == id)).GetValueOrDefault();
        }
        private async Task TinhDiemTrungBinhTichLuyAsync(string maSinhVien)
        {
            var bangDiem = await _context.SinhVien.FindAsync(maSinhVien);
            if (bangDiem != null)
            {
                var chiTiet = await _context.BangDiem.Where(b => b.MaSinhVien == maSinhVien).ToListAsync();
                if (chiTiet.Count > 0)
                {
                    double tongDiemHe4 = 0;
                    double tongDiem = 0;
                    int tongTinChi = 0;

                    foreach (var ct in chiTiet)
                    {
                        tongDiem += ct.DiemTong * ct.SoTinChi;
                        tongDiemHe4 += ct.DiemTongHe4 * ct.SoTinChi;
                        tongTinChi += ct.SoTinChi;
                    }
                    bangDiem.SoTinChiTichLuy = tongTinChi;
                    bangDiem.DTBTLHe10 = Math.Round(tongDiem / tongTinChi,2);
                    bangDiem.DTBTLHe4 = Math.Round(tongDiemHe4 / tongTinChi,2);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    bangDiem.DTBTLHe10 = 0;
                    bangDiem.DTBTLHe4 = 0;
                    bangDiem.SoTinChiTichLuy = 0;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                bangDiem.DTBTLHe10 = 0;
                bangDiem.DTBTLHe4 = 0;
                bangDiem.SoTinChiTichLuy = 0;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IActionResult> Upload(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file){

            if(file != null){
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension != ".xls" && fileExtension != ".xlsx"){
                    ModelState.AddModelError("","Please choose excel file to upload!");
                }
                else
                {
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        var danhSachBangDiem = new List<BangDiem>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var bangDiem = new BangDiem
                            {
                                MaSinhVien = dt.Rows[i][0].ToString(),
                                TenSinhVien = dt.Rows[i][1].ToString(),
                                MaHocPhan = dt.Rows[i][2].ToString(),
                                TenHocPhan = dt.Rows[i][3].ToString(),
                                SoTinChi = Convert.ToInt32(dt.Rows[i][4]),
                                MaLopHocPhan = dt.Rows[i][5].ToString(),
                                DiemChuyenCan = Convert.ToDouble(dt.Rows[i][6]),
                                DiemKiemTra = Convert.ToDouble(dt.Rows[i][7]),
                                DiemThi = Convert.ToDouble(dt.Rows[i][8])
                            };

                            danhSachBangDiem.Add(bangDiem);
                        }

                        // Thêm tất cả các đối tượng BangDiem vào DbContext
                        _context.AddRange(danhSachBangDiem);
                        await _context.SaveChangesAsync();
                        foreach (var bangDiem in danhSachBangDiem)
                        {
                            await TinhDiemTrungBinhTichLuyAsync(bangDiem.MaSinhVien);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        public IActionResult Download()
        {
            var fileName = "bangdiem" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaSinhVien";
                worksheet.Cells["B1"].Value = "TenSinhVien";
                worksheet.Cells["C1"].Value = "MaHocPhan";
                worksheet.Cells["D1"].Value = "TenHocPhan";
                worksheet.Cells["E1"].Value = "SoTinChi";
                worksheet.Cells["F1"].Value = "MaLopHocPhan";
                worksheet.Cells["G1"].Value = "DiemChuyenCan";
                worksheet.Cells["H1"].Value = "DiemKiemTra";
                worksheet.Cells["I1"].Value = "DiemThi";
                worksheet.Cells["J1"].Value = "DiemTong";
                worksheet.Cells["K1"].Value = "DiemTongHe4";
                worksheet.Cells["L1"].Value = "DiemChu";

                // Get only the properties you want to include
                var bangDiemList = _context.BangDiem
                    .Select(b => new
                    {
                        b.MaSinhVien,
                        b.TenSinhVien,
                        b.MaHocPhan,
                        b.TenHocPhan,
                        b.SoTinChi,
                        b.MaLopHocPhan,
                        b.DiemChuyenCan,
                        b.DiemKiemTra,
                        b.DiemThi,
                        b.DiemTong,
                        b.DiemTongHe4,
                        b.DiemChu
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(bangDiemList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
