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
    [Authorize]
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SinhVien
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query =_context.SinhVien.Include(s => s.ChuyenNganh).Include(s => s.KhoaHoc).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.TenSinhVien.Contains(searchText) || d.MaSinhVien.Contains(searchText));
            }
            var SinhVien = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = SinhVien.Count;
            }

            var totalCount = SinhVien.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(SinhVien.ToPagedList(pageNumber, actualPageSize));
        }


        // GET: SinhVien/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.SinhVien == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .Include(cn => cn.ChuyenNganh)
                .Include(b => b.BangDiem)
                    .ThenInclude(bd => bd.LopHocPhan)
                        .ThenInclude(lhp => lhp.HocKy)
                .Include(drl => drl.DiemRenLuyen)
                    .ThenInclude(hk => hk.HocKy)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhVien/Create
        public IActionResult Create()
        {
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh");
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "TenKhoaHoc");
            return View();
        }

        // POST: SinhVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,TenSinhVien,GioiTinh,NgaySinh,TinhTrang,MaChuyenNganh,MaKhoaHoc")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", sinhVien.MaChuyenNganh);
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "TenKhoaHoc", sinhVien.MaKhoaHoc);
            return View(sinhVien);
        }

        // GET: SinhVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SinhVien == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", sinhVien.MaChuyenNganh);
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "TenKhoaHoc", sinhVien.MaKhoaHoc);
            return View(sinhVien);
        }

        // POST: SinhVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,TenSinhVien,GioiTinh,NgaySinh,TinhTrang,MaChuyenNganh,MaKhoaHoc")] SinhVien sinhVien)
        {
            if (id != sinhVien.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSinhVien))
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
            ViewData["MaChuyenNganh"] = new SelectList(_context.ChuyenNganh, "MaChuyenNganh", "TenChuyenNganh", sinhVien.MaChuyenNganh);
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "TenKhoaHoc", sinhVien.MaKhoaHoc);
            return View(sinhVien);
        }

        // GET: SinhVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SinhVien == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .Include(s => s.ChuyenNganh)
                .Include(s => s.KhoaHoc)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SinhVien == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SinhVien'  is null.");
            }
            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhVien.Remove(sinhVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(string id)
        {
          return (_context.SinhVien?.Any(e => e.MaSinhVien == id)).GetValueOrDefault();
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
                            var sinhVien = new SinhVien()
                            {
                                MaSinhVien = dt.Rows[i][0].ToString(),
                                TenSinhVien = dt.Rows[i][1].ToString(),
                                GioiTinh = dt.Rows[i][2].ToString(),
                                NgaySinh = Convert.ToDateTime(dt.Rows[i][3]),
                                TinhTrang = dt.Rows[i][4].ToString(),
                                MaChuyenNganh = dt.Rows[i][5].ToString(),
                                MaKhoaHoc = dt.Rows[i][6].ToString(),
                            };

                            _context.Add(sinhVien);
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
                worksheet.Cells["C1"].Value = "GioiTinh";
                worksheet.Cells["D1"].Value = "NgaySinh";
                worksheet.Cells["E1"].Value = "TinhTrang";
                worksheet.Cells["F1"].Value = "ChuyenNganh";
                worksheet.Cells["G1"].Value = "KhoaHoc";

                // Get only the properties you want to include
                var sinhVienList = _context.SinhVien
                    .Select(b => new
                    {
                        b.MaSinhVien,
                        b.TenSinhVien,
                        b.GioiTinh,
                        NgaySinh = b.NgaySinh.ToString("dd/MM/yyyy"),
                        b.TinhTrang,
                        b.ChuyenNganh.TenChuyenNganh,
                        b.KhoaHoc.TenKhoaHoc,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(sinhVienList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
