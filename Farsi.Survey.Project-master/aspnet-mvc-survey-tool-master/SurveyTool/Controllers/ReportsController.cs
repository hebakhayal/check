﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyTool.Models;

using System.Data;

namespace SurveyTool.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index(string SearchBy, string search)
        {

            if (SearchBy == "Date")
            {
                var x1 = DateTime.Parse(search);
                var y = _db.Surveys.Select(x => x.StartDate);

                return View(_db.Surveys.Where(x => x.StartDate == x1).ToList());
            }


            else  if (SearchBy == "Name")
            {
                return View(_db.Surveys.Where(x => x.Name == search || search == null).ToList());
            }


          /*  else
            {

              //  return View(_db.Surveys.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
            */
            else
            {
                return View(_db.Surveys.ToList());
            }

            
            /*
            var surveys = _db.Surveys.ToList();
            return View(surveys);*/
        }

        [HttpGet]
        public ActionResult Details(int id, int? departmentId, DateTime? startDate, DateTime? endDate)
        {
            var questions = new List<QuestionViewModel>();
            startDate = startDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate = endDate ?? DateTime.Now;

            var survey = _db.Surveys.Single(s => s.Id == id);

            _db.Questions
               .Where(q => q.SurveyId == id)
               .OrderBy(q => q.Priority)
               .Select(q => new
                   {
                       q.Title,
                       q.Body,
                       q.Type,
                       Answers = _db.Answers.Where(a => a.QuestionId == q.Id &&
                                                        a.Response.CreatedOn >= startDate.Value &&
                                                        a.Response.CreatedOn <= endDate.Value)
                   })
               .ToList()
               .ForEach(r => questions.Add(new QuestionViewModel
                   {
                       Title = r.Title,
                       Body = r.Body,
                       Type = r.Type,
                       
                       Answers = r.Answers.ToList()
                   }));

            var vm = new ReportViewModel
                {
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    Survey = survey,
                    Responses = questions
                };

            return View(vm);
        }
    }
}
