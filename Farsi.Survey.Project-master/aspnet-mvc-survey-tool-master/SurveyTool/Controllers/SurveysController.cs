 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyTool.Models;
using System.IO;

namespace SurveyTool.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SurveysController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult GetAllServies()
        {
            var surveys = _db.Surveys.ToList();
            return Json(surveys,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //3awza ady default value 3lshan al8y el exception 
        // Index(string SearchBy, string search,5/5/2015);
        public ActionResult Index(string SearchBy, string search)
        {
            DateTime date;
            if (SearchBy == "Date")
            { 
                if (DateTime.TryParse(search, out date))
                {
                    var x1 = DateTime.Parse(search);
                    var y = _db.Surveys.Select(x => x.StartDate);
                    return View(_db.Surveys.Where(x => x.StartDate == x1).ToList());
                }
                return View(_db.Surveys.ToList());
            }


            else if (SearchBy == "Name")
            {
                return View(_db.Surveys.Where(x => x.Name == search || search == null).ToList());
            }
            else
            {
                return View(_db.Surveys.ToList());
            }

            
        }

        [HttpGet]
        public ActionResult Create()
        {
            var survey = new Survey
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1)
                };

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Survey survey, string action, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                survey.Questions.ForEach(q => q.CreatedOn = q.ModifiedOn = DateTime.Now);

                //saving image filepath
                //if (file.ContentLength > 0)
                //{
                //    string _FileName = Path.GetFileName(file.FileName);
                //    string _path = Path.Combine(Server.MapPath("~/uploadedfiles"), _FileName);
                //    file.SaveAs(_path);
                //}

                //saving image binary
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(Request.Files["file"].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Request.Files["file"].ContentLength);
                }
                survey.Thumbnail = fileData;
                _db.Surveys.Add(survey);
                _db.SaveChanges();
                //_db.Questions.AddRange(survey.Questions);
                //_db.SaveChanges();
                TempData["success"] = "The survey was successfully created!";


                return RedirectToAction("Edit", new {id = survey.Id});
            }
            else
            {
                TempData["error"] = "An error occurred while attempting to create this survey.";
                return View(survey);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var survey = _db.Surveys.Include("Questions").Single(x => x.Id == id);
            survey.Questions = survey.Questions.OrderBy(q => q.Priority).ToList();
            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Survey model)
        {
            foreach (var question in model.Questions)
            {
                question.SurveyId = model.Id;

                if (question.Id == 0)
                {
                    question.CreatedOn = DateTime.Now;
                    question.ModifiedOn = DateTime.Now;
                    _db.Entry(question).State = EntityState.Added;
                }
                else
                {
                    question.ModifiedOn = DateTime.Now;
                    _db.Entry(question).State = EntityState.Modified;
                    _db.Entry(question).Property(x => x.CreatedOn).IsModified = false;
                }
            }

            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Edit", new {id = model.Id});
        }

        [HttpPost]
        public ActionResult Delete(Survey survey)
        {
            _db.Entry(survey).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
