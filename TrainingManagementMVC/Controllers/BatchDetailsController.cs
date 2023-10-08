using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.EF;

namespace TrainingManagementMVC.Controllers
{
    public class BatchDetailsController : Controller
    {
        private readonly TrainingManagement2Context _context;
        //constructor 
        public BatchDetailsController(TrainingManagement2Context context)
        {
            _context = context;
        }

        // GET: BatchDetails
        public async Task<IActionResult> Index()
        {
            var trainingManagement2Context = _context.BatchDetails.Include(b => b.Batch);
            return View(await trainingManagement2Context.ToListAsync());
        }

        // GET: BatchDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BatchDetails == null)
            {
                return NotFound();
            }

            var batchDetail = await _context.BatchDetails
                .Include(b => b.Batch)
                .FirstOrDefaultAsync(m => m.BatchDetailId == id);
            if (batchDetail == null)
            {
                return NotFound();
            }

            return View(batchDetail);
        }

        // GET: BatchDetails/Create
        public IActionResult Create()
        {
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchName");
            return View();
        }

        // POST: BatchDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchDetailId,BatchId,Date,HoursTaken,TopicsTaken,Remarks")] BatchDetail batchDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(batchDetail);
                await _context.SaveChangesAsync();
                var batch = await _context.Batches.FindAsync(batchDetail.BatchId);
                if (batch != null)
                {
                    var totalDuration = _context.BatchDetails
                        .Where(bd => bd.BatchId == batchDetail.BatchId)
                        .Sum(bd => bd.HoursTaken);

                    batch.Duration = totalDuration;
                    _context.Update(batch);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchId", batchDetail.BatchId);
            return View(batchDetail);
        }

        // GET: BatchDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BatchDetails == null)
            {
                return NotFound();
            }

            var batchDetail = await _context.BatchDetails.FindAsync(id);
            if (batchDetail == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchId", batchDetail.BatchId);
            return View(batchDetail);
        }

        // POST: BatchDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchDetailId,BatchId,Date,HoursTaken,TopicsTaken,Remarks")] BatchDetail batchDetail)
        {
            if (id != batchDetail.BatchDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batchDetail);
                    await _context.SaveChangesAsync();

                    var batch = await _context.Batches.FindAsync(batchDetail.BatchId);
                    if (batch != null)
                    {
                        var totalDuration = _context.BatchDetails
                            .Where(bd => bd.BatchId == batchDetail.BatchId)
                            .Sum(bd => bd.HoursTaken);

                        batch.Duration = totalDuration;
                        _context.Update(batch);
                        await _context.SaveChangesAsync();
                    }
                }
                
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchDetailExists(batchDetail.BatchDetailId))
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
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchId", batchDetail.BatchId);
            return View(batchDetail);
        }

        // GET: BatchDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BatchDetails == null)
            {
                return NotFound();
            }

            var batchDetail = await _context.BatchDetails
                .Include(b => b.Batch)
                .FirstOrDefaultAsync(m => m.BatchDetailId == id);
            if (batchDetail == null)
            {
                return NotFound();
            }

            return View(batchDetail);
        }

        // POST: BatchDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BatchDetails == null)
            {
                return Problem("Entity set 'TrainingManagement2Context.BatchDetails'  is null.");
            }
            var batchDetail = await _context.BatchDetails.FindAsync(id);
            if (batchDetail != null)
            {
                _context.BatchDetails.Remove(batchDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchDetailExists(int id)
        {
          return (_context.BatchDetails?.Any(e => e.BatchDetailId == id)).GetValueOrDefault();
        }
    }
}
