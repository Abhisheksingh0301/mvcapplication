using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Helpers;
using System.Linq.Dynamic;
using System.Text;

namespace MvcApplication1.Controllers
{
    public class TimeTableController : Controller
    {
        Studentcontext sC = new Studentcontext();
        List<Time_Table_Cnt> model = new List<Time_Table_Cnt>();
        //
        // GET: /TimeTable/
        public ActionResult DepSm(string Dep, string Sem)
        {
            var Dp = (from TT in sC.TimeTables
                      join Sn in sC.CurrentsessionS on
                      new { X1 = TT.Sessn }
                      equals
                       new { X1 = Sn.Current_sess }
                       select TT.Department).ToList().Distinct();
            var Sm = (from TT in sC.TimeTables
                      join Sn in sC.CurrentsessionS on
                      new { X1 = TT.Sessn }
                      equals
                       new { X1 = Sn.Current_sess }
                       //where(TT.Department==Dep)
                      select TT.Semester).ToList().Distinct();
            var TblData = (from TT in sC.TimeTables
                      join Sn in sC.CurrentsessionS on
                      new { X1 = TT.Sessn }
                      equals
                       new { X1 = Sn.Current_sess }
                       where (TT.Department==Dep && TT.Semester==Sem)
                       orderby TT.Subject,TT.DOE
                      select TT).ToList();
            ViewBag.Dpt = Dp;
            ViewBag.Smt = Sm;
            ViewBag.Rcnt = TblData.Count();
            return View(TblData.ToList());

        }
        public ActionResult SubjectWise(string searchText)
        {
            var tbldaTa = (from TT in sC.TimeTables
                           join Sn in sC.CurrentsessionS on
                           new { X1 = TT.Sessn }
                           equals
                            new { X1 = Sn.Current_sess }
                           where (TT.Subject == searchText)
                           orderby TT.DOE
                           select TT).ToList();
            ViewBag.Sb = searchText;
            ViewBag.cnt = tbldaTa.Count();
            return View(tbldaTa.ToList());
        }
        public ActionResult DateWiseTT(DateTime? start, DateTime? end)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            int AllTotal = 0;
            ViewBag.St = start;
            ViewBag.En = end;
            //var TblData = (from TT in sC.TimeTables
            //               join Sn in sC.CurrentsessionS on
            //               new { X1 = TT.Sessn }
            //               equals
            //                new { X1 = Sn.Current_sess }
            //               where (TT.DOE >= start && TT.DOE <= end)
            //               //where (TT.DOE==start)
            //               orderby TT.DOE, TT.Department, TT.Subject
            //               select TT).ToList();

            var ttx=(from TT in sC.TimeTables
                     join Sn in sC.CurrentsessionS on TT.Sessn equals Sn.Current_sess
                     join Ct in sC.counts on TT.Subject equals Ct.Sub_PCode into Cnt
                     where (TT.DOE >= start && TT.DOE <= end)
                     orderby TT.DOE, TT.Department, TT.Subject
                     from cnTT in Cnt.DefaultIfEmpty()
                     select new
                    {
                        Department = TT.Department,
                        Semester = TT.Semester,
                        Subject = TT.Subject,
                        SubTitle = TT.SubTitle,
                        DOE = TT.DOE,
                        Time_From = TT.Time_From,
                        Time_To = TT.Time_To,
                        Duration = TT.Duration,
                        Practical = TT.Practical,
                        Sessn = TT.Sessn,
                        T_Count=(cnTT.Total==null )?0:cnTT.Total
                    }).ToList().Distinct();
            
            foreach (var item in ttx)
            {
                model.Add(new Time_Table_Cnt()
               {
                   Department = item.Department,
                   Semester = item.Semester,
                   Subject = item.Subject,
                   SubTitle = item.SubTitle,
                   DOE = item.DOE,
                   Time_From = item.Time_From,
                   Time_To = item.Time_To,
                   Duration = item.Duration,
                   Practical = item.Practical,
                   Sessn = item.Sessn,
                   T_Count = item.T_Count
               });
                AllTotal = AllTotal + item.T_Count;
            }
                    
            ViewBag.Rcnt = ttx.Count();
            ViewBag.AToT = AllTotal;
            //return View(TblData);
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
            
        }

        //
        // GET: /TimeTable/Details/5
        
        public ActionResult Details(string id)
        {
            var model = (from SM in sC.SubjectMasterS
                         where SM.Subcode == id
                         select SM).ToList();
            //Subject_Master model = sC.SubjectMasterS.Find(id);
            //if (model == null)
            //{
            //    return HttpNotFound();
            //}
            
            var Sbj = (from TT in sC.TimeTables
                      join Sn in sC.CurrentsessionS on
                      new { X1 = TT.Sessn }
                      equals
                       new { X1 = Sn.Current_sess }
                      select TT.Subject).ToList().Distinct();
            //ViewBag.SJ = Sbj;
            
            
            //Time_Table tt = sC.TimeTables.Find(Subj);
            //Subject_Master SM = sC.SubjectMasterS.Find(id);
            //if (SM == null)
            //{
            //    return HttpNotFound();
            //}

            return View("Details",model);
        }

        //
        // GET: /TimeTable/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeTable/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TimeTable/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TimeTable/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TimeTable/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TimeTable/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
