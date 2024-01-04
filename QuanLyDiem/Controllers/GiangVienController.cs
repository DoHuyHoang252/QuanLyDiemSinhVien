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
    public class GiangVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public GiangVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GiangVien
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.GiangVien.Include(g => g.ChuyenNganh).Include(g => g.Khoa).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.TenGiangVien.Contains(searchText) || d.MaChuyenNganh.Contains(searchText) | d.MaKhoa.Contains(searchText));
            }
            var giangVien = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = giangVien.Count;
            }

            var totalCount = giangVien.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(giangVien.ToPagedList(pageNumber, actualPageSize));
        }

        // GET: GiangVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GiangVien == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangVien
                .Include(g => g.ChuyenNganh)
                .Include(g => g.Khoa)
                .FirstOrDefaultAsync(m => m.MaGiangVien == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // GET: GiangVien/Create
        public IActionResult Create()
        {
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh");
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: GiangVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGiangVien,TenGiangVien,GioiTinh,NgaySinh,MaKhoa,MaChuyenNganh")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", giangVien.MaChuyenNganh);
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: GiangVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GiangVien == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangVien.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", giangVien.MaChuyenNganh);
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // POST: GiangVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGiangVien,TenGiangVien,GioiTinh,NgaySinh,MaKhoa,MaChuyenNganh")] GiangVien giangVien)
        {
            if (id != giangVien.MaGiangVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giangVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangVienExists(giangVien.MaGiangVien))
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
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", giangVien.MaChuyenNganh);
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: GiangVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GiangVien == null)
            {
                return NotFound();
            }

            var giangVien = await _context.GiangVien
                .Include(g => g.ChuyenNganh)
                .Include(g => g.Khoa)
                .FirstOrDefaultAsync(m => m.MaGiangVien == id);
            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // POST: GiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GiangVien == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GiangVien'  is null.");
            }
            var giangVien = await _context.GiangVien.FindAsync(id);
            if (giangVien != null)
            {
                _context.GiangVien.Remove(giangVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiangVienExists(string id)
        {
          return (_context.GiangVien?.Any(e => e.MaGiangVien == id)).GetValueOrDefault();
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
                            var giangVien = new GiangVien()
                            {
                                MaGiangVien = dt.Rows[i][0].ToString(),
                                TenGiangVien = dt.Rows[i][1].ToString(),
                                GioiTinh = dt.Rows[i][2].ToString(),
                                NgaySinh = Convert.ToDateTime(dt.Rows[i][3]),
                                MaKhoa = dt.Rows[i][4].ToString(),
                                MaChuyenNganh = dt.Rows[i][5].ToString(),
                            };

                            _context.Add(giangVien);
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
            var fileName = "giangvien" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaGiangVien";
                worksheet.Cells["B1"].Value = "TenGiangVien";
                worksheet.Cells["C1"].Value = "GioiTinh";
                worksheet.Cells["D1"].Value = "NgaySinh";
                worksheet.Cells["E1"].Value = "Khoa";
                worksheet.Cells["F1"].Value = "ChuyenNganh";

                // Get only the properties you want to include
                var giangVienList = _context.GiangVien
                    .Select(b => new
                    {
                        b.MaGiangVien,
                        b.TenGiangVien,
                        b.GioiTinh,
                        NgaySinh = b.NgaySinh.ToString("dd/MM/yyyy"),
                        b.Khoa.MaKhoa,
                        b.ChuyenNganh.TenChuyenNganh,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(giangVienList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
