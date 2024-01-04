using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuanLyDiem.Data;
using QuanLyDiem.Models;
using QuanLyDiem.Models.Process;
using X.PagedList;

namespace QuanLyDiem.Controllers
{
    [Authorize]
    public class LopHocPhanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public LopHocPhanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LopHocPhan
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query =_context.LopHocPhan.Include(l => l.BangDiem).Include(l => l.GiangVien).Include(l => l.HocKy).Include(l => l.HocPhan).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.HocPhan.TenHocPhan.Contains(searchText) || d.TenLopHocPhan.Contains(searchText)|| d.MaGiangVien.Contains(searchText));
            }
            var LopHocPhan = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = LopHocPhan.Count;
            }

            var totalCount = LopHocPhan.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(LopHocPhan.ToPagedList(pageNumber, actualPageSize));
        }

        // GET: LopHocPhan/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.LopHocPhan == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhan
                .Include(b => b.BangDiem)
                .Include(l => l.GiangVien)
                .Include(l => l.HocKy)
                .Include(l => l.HocPhan)
                .FirstOrDefaultAsync(m => m.MaLopHocPhan == id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return View(lopHocPhan);
        }

        // GET: LopHocPhan/Create
        public IActionResult Create()
        {
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien");
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy");
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan");
            return View();
        }

        // POST: LopHocPhan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLopHocPhan,TenLopHocPhan,MaHocPhan,MaGiangVien,MaHocKy")] LopHocPhan lopHocPhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lopHocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", lopHocPhan.MaHocKy);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", lopHocPhan.MaHocPhan);
            return View(lopHocPhan);
        }

        // GET: LopHocPhan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LopHocPhan == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhan.FindAsync(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", lopHocPhan.MaHocKy);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", lopHocPhan.MaHocPhan);
            return View(lopHocPhan);
        }

        // POST: LopHocPhan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLopHocPhan,TenLopHocPhan,MaHocPhan,MaGiangVien,MaHocKy")] LopHocPhan lopHocPhan)
        {
            if (id != lopHocPhan.MaLopHocPhan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lopHocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopHocPhanExists(lopHocPhan.MaLopHocPhan))
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
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", lopHocPhan.MaHocKy);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", lopHocPhan.MaHocPhan);
            return View(lopHocPhan);
        }

        // GET: LopHocPhan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LopHocPhan == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhan
                .Include(l => l.GiangVien)
                .Include(l => l.HocKy)
                .Include(l => l.HocPhan)
                .FirstOrDefaultAsync(m => m.MaLopHocPhan == id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return View(lopHocPhan);
        }

        // POST: LopHocPhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LopHocPhan == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LopHocPhan'  is null.");
            }
            var lopHocPhan = await _context.LopHocPhan.FindAsync(id);
            if (lopHocPhan != null)
            {
                _context.LopHocPhan.Remove(lopHocPhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopHocPhanExists(string id)
        {
          return (_context.LopHocPhan?.Any(e => e.MaLopHocPhan == id)).GetValueOrDefault();
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var lopHocPhan = new LopHocPhan()
                            {
                                MaLopHocPhan = dt.Rows[i][0].ToString(),
                                TenLopHocPhan = dt.Rows[i][1].ToString(),
                                MaHocPhan = dt.Rows[i][1].ToString(),
                                MaGiangVien = dt.Rows[i][5].ToString(),
                                MaHocKy = dt.Rows[i][5].ToString(),
                            };

                            _context.Add(lopHocPhan);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
        public IActionResult Download()
        {
            var fileName = "lophocphan" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaLopHocPhan";
                worksheet.Cells["B1"].Value = "TenLopHocPhan";
                worksheet.Cells["C1"].Value = "HocPhan";
                worksheet.Cells["D1"].Value = "GiangVien";
                worksheet.Cells["D1"].Value = "HocKy";

                // Get only the properties you want to include
                var lopHocPhanList = _context.LopHocPhan
                    .Select(b => new
                    {
                        b.MaLopHocPhan,
                        b.TenLopHocPhan,
                        b.HocPhan.TenHocPhan,
                        b.GiangVien.TenGiangVien,
                        b.HocKy.TenHocKy,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(lopHocPhanList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult DownloadBangDiemLop(string id)
        {
            var fileName = "bangdiemlop" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaSinhVien";
                worksheet.Cells["B1"].Value = "TenSinhVien";
                worksheet.Cells["C1"].Value = "DiemChuyenCan";
                worksheet.Cells["D1"].Value = "DiemKiemTra";
                worksheet.Cells["E1"].Value = "DiemThi";
                worksheet.Cells["F1"].Value = "DiemTong";
                worksheet.Cells["G1"].Value = "DiemTongHe4";
                worksheet.Cells["H1"].Value = "DiemChu";

                // Get only the properties you want to include
                var lopHocPhanList = _context.BangDiem
                    .Where(b => b.MaLopHocPhan == id)
                    .Select(b => new
                    {
                        b.MaSinhVien,
                        b.TenSinhVien,
                        b.DiemChuyenCan,
                        b.DiemKiemTra,
                        b.DiemThi,
                        b.DiemTong,
                        b.DiemTongHe4,
                        b.DiemChu,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(lopHocPhanList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
