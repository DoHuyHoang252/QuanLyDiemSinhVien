using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Data.Common;
using System.IO.Compression;
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
    public class DiemRenLuyenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public DiemRenLuyenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiemRenLuyen
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.DiemRenLuyen.Include(d => d.HocKy).Include(d => d.SinhVien).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.SinhVien.TenSinhVien.Contains(searchText) || d.MaSinhVien.Contains(searchText));
            }
            var diemRenLuyen = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = diemRenLuyen.Count;
            }

            var totalCount = diemRenLuyen.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(diemRenLuyen.ToPagedList(pageNumber, actualPageSize));
        }

        // GET: DiemRenLuyen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiemRenLuyen == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyen
                .Include(d => d.HocKy)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaDiemRenLuyen == id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }

            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Create
        public IActionResult Create()
        {
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "MaSinhVien");
            return View();
        }

        // POST: DiemRenLuyen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDiemRenLuyen,DiemRL,MaSinhVien,MaHocKy")] DiemRenLuyen diemRenLuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diemRenLuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "MaSinhVien", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiemRenLuyen == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyen.FindAsync(id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "MaSinhVien", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // POST: DiemRenLuyen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDiemRenLuyen,DiemRL,MaSinhVien,MaHocKy")] DiemRenLuyen diemRenLuyen)
        {
            if (id != diemRenLuyen.MaDiemRenLuyen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diemRenLuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiemRenLuyenExists(diemRenLuyen.MaDiemRenLuyen))
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
            ViewData["MaHocKy"] = new SelectList(_context.HocKy, "MaHocKy", "TenHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "MaSinhVien", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiemRenLuyen == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyen
                .Include(d => d.HocKy)
                .Include(d => d.SinhVien)
                .FirstOrDefaultAsync(m => m.MaDiemRenLuyen == id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }

            return View(diemRenLuyen);
        }

        // POST: DiemRenLuyen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiemRenLuyen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DiemRenLuyen'  is null.");
            }
            var diemRenLuyen = await _context.DiemRenLuyen.FindAsync(id);
            if (diemRenLuyen != null)
            {
                _context.DiemRenLuyen.Remove(diemRenLuyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiemRenLuyenExists(int id)
        {
          return (_context.DiemRenLuyen?.Any(e => e.MaDiemRenLuyen == id)).GetValueOrDefault();
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
                            var diemRenLuyen = new DiemRenLuyen()
                            {
                                MaSinhVien = dt.Rows[i][0].ToString(),
                                MaHocKy = dt.Rows[i][1].ToString(),
                                DiemRL = Convert.ToInt32(dt.Rows[i][2])
                            };

                            _context.Add(diemRenLuyen);
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
            var fileName = "sinhvien" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaSinhVien";
                worksheet.Cells["B1"].Value = "TenSinhVien";
                worksheet.Cells["C1"].Value = "HocKy";
                worksheet.Cells["D1"].Value = "DiemRL";

                // Get only the properties you want to include
                var diemRenLuyenList = _context.DiemRenLuyen
                    .Select(b => new
                    {
                        b.MaSinhVien,
                        b.SinhVien.TenSinhVien,
                        b.HocKy.TenHocKy,
                        b.DiemRL,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(diemRenLuyenList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
