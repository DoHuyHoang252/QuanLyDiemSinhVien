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
    public class HocPhanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public HocPhanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HocPhantroller
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.HocPhan.Include(h => h.ChuyenNganh).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.MaHocPhan.Contains(searchText) || d.TenHocPhan.Contains(searchText) || d.MaChuyenNganh.Contains(searchText));
            }
            var hocPhan = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = hocPhan.Count;
            }

            var totalCount = hocPhan.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(hocPhan.ToPagedList(pageNumber, actualPageSize));
        }


        // GET: HocPhantroller/Create
        public IActionResult Create()
        {
            ViewData["MaChuyenNganh"] = new SelectList(_context.Set<ChuyenNganh>(), "MaChuyenNganh", "TenChuyenNganh");
            return View();
        }

        // POST: HocPhantroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHocPhan,TenHocPhan,SoTinChi,MaChuyenNganh")] HocPhan hocPhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.Set<ChuyenNganh>(), "MaChuyenNganh", "TenChuyenNganh", hocPhan.MaChuyenNganh);
            return View(hocPhan);
        }

        // GET: HocPhantroller/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HocPhan == null)
            {
                return NotFound();
            }

            var hocPhan = await _context.HocPhan.FindAsync(id);
            if (hocPhan == null)
            {
                return NotFound();
            }
            ViewData["MaChuyenNganh"] = new SelectList(_context.Set<ChuyenNganh>(), "MaChuyenNganh", "TenChuyenNganh", hocPhan.MaChuyenNganh);
            return View(hocPhan);
        }

        // POST: HocPhantroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHocPhan,TenHocPhan,SoTinChi,MaChuyenNganh")] HocPhan hocPhan)
        {
            if (id != hocPhan.MaHocPhan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocPhanExists(hocPhan.MaHocPhan))
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
            ViewData["MaChuyenNganh"] = new SelectList(_context.Set<ChuyenNganh>(), "MaChuyenNganh", "TenChuyenNganh", hocPhan.MaChuyenNganh);
            return View(hocPhan);
        }

        // GET: HocPhantroller/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HocPhan == null)
            {
                return NotFound();
            }

            var hocPhan = await _context.HocPhan
                .Include(h => h.ChuyenNganh)
                .FirstOrDefaultAsync(m => m.MaHocPhan == id);
            if (hocPhan == null)
            {
                return NotFound();
            }

            return View(hocPhan);
        }

        // POST: HocPhantroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HocPhan == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HocPhan'  is null.");
            }
            var hocPhan = await _context.HocPhan.FindAsync(id);
            if (hocPhan != null)
            {
                _context.HocPhan.Remove(hocPhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocPhanExists(string id)
        {
          return (_context.HocPhan?.Any(e => e.MaHocPhan == id)).GetValueOrDefault();
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
                            var hocPhan = new HocPhan()
                            {
                                MaHocPhan = dt.Rows[i][0].ToString(),
                                TenHocPhan = dt.Rows[i][1].ToString(),
                                SoTinChi = Convert.ToInt32(dt.Rows[i][4]),
                                MaChuyenNganh = dt.Rows[i][5].ToString(),
                            };

                            _context.Add(hocPhan);
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
            var fileName = "hocphan" + ".xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaHocPhan";
                worksheet.Cells["B1"].Value = "TenHocPhan";
                worksheet.Cells["C1"].Value = "SoTinChi";
                worksheet.Cells["D1"].Value = "ChuyenNganh";

                // Get only the properties you want to include
                var hocPhanList = _context.HocPhan
                    .Select(b => new
                    {
                        b.MaHocPhan,
                        b.TenHocPhan,
                        b.SoTinChi,
                        b.ChuyenNganh.TenChuyenNganh,
                    })
                .ToList();
                worksheet.Cells["A2"].LoadFromCollection(hocPhanList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
