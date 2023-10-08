using DataAccess.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TrainingManagementMVC.Controllers
{
    public class BatchDetailController : Controller
    {
        TrainingManagement2Context trainingManagement2Context = new TrainingManagement2Context();

       
        public ActionResult Index()
        {
            var batchDetails = trainingManagement2Context.BatchDetails.ToList();
            // GET: BatchDetail
            return View();
        }

        // GET: BatchDetail/Details/5
        public ActionResult Details(int id)
        {
            
            var batch = trainingManagement2Context.BatchDetails.FirstOrDefault(b => b.BatchId == id);

            if (id <= 0 || trainingManagement2Context.Batches == null)
            {
                return (NotFound());
            }


            if (batch == null)
            {
                return (NotFound());
            }
            return View(trainingManagement2Context.BatchDetails);
        }

        // GET: BatchDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatchDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BatchDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BatchDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BatchDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BatchDetail/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
