using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace TrainingManagementMVC.Controllers
{
    public class BatchesController : Controller
    {

       /* public  TrainingManagement2Context _context;

        //constructor 

        public BatchesController(TrainingManagement2Context context)
        {
            _context = context;
        }*/

        TrainingManagement2Context trainingManagement2Context =new TrainingManagement2Context();    
        // GET: BatchesController1
        public ActionResult Index()
        {

            var batches = trainingManagement2Context.Batches.ToList();
           // var batches = _context.Batches.ToList(); 

            return View(batches);
            
            
        }
 


        // GET: BatchesController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BatchesController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatchesController1/Create
        [HttpPost] // This attribute specifies that this method should only respond to HTTP POST requests.
        [ValidateAntiForgeryToken]// This attribute helps prevent cross-site request forgery (CSRF) attacks.
        public async Task<IActionResult> Create([Bind("BatchId,BatchName,StartDate,TentativeEndDate,EndDate,Fees,FeesPaid,Duration,HoursTaken,Status,Details,Remarks")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                // ModelState.IsValid checks if the data submitted in the form passes validation rules.

                // trainingManagement2Context is an instance of the DbContext for the application.
                // It is used to interact with the database.

                // Add the 'batch' object to the DbContext. This stages the object for database insertion.
                trainingManagement2Context.Add(batch);
                // Save changes to the database asynchronously. This will actually insert the data into the database.
                await trainingManagement2Context.SaveChangesAsync();
                // After successfully inserting the data, redirect to the Index action method.

                await trainingManagement2Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // If ModelState is not valid, return the 'batch' object along with validation errors to the view.

            return View(batch);
        }

        // GET: BatchesController1/Edit/5
        public async Task< IActionResult> Edit(int id)
        {
            if (id <=0 || trainingManagement2Context.Batches==null)
            {
                return(NotFound());
            }
            var batch = await trainingManagement2Context.FindAsync<Batch>(id);
            if (batch == null)
            {
                return (NotFound());
            }
            return View(batch);
        }

        // POST: BatchesController1/Edit/5
         [HttpPost]
          [ValidateAntiForgeryToken]
          public  async Task<IActionResult> Edit(int id, [Bind("BatchName,StartDate,TentativeEndDate,EndDate,Fees,FeesPaid,Duration,HoursTaken,Status,Details,Remarks")] Batch batch)
          {

            if (batch.BatchId != batch.BatchId)
            {

                return (NotFound());
            }


            if (ModelState.IsValid)
              {
                  try
                  {
                      trainingManagement2Context.Update(batch);

                      await trainingManagement2Context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      if (!BatchExists(batch.BatchId))
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
              return View(batch);
          }




        // GET: Batches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || trainingManagement2Context.Batches == null)
            {
                return NotFound();
            }

            var batch = await trainingManagement2Context.Batches
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (trainingManagement2Context.Batches == null)
            {
                return Problem("Entity set 'TrainingManagementGeminiContext.Batches'  is null.");
            }
            var batch = await trainingManagement2Context.Batches.FindAsync(id);
            if (batch != null)
            {
                trainingManagement2Context.Batches.Remove(batch);
            }

            await trainingManagement2Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BatchExists(int id)
        {
            return (trainingManagement2Context.Batches?.Any(e => e.BatchId == id)).GetValueOrDefault();
        }
    }
}
