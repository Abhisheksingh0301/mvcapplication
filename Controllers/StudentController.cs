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
    public class StudentController : Controller
    {
        //
        // GET: /Student/
 Studentcontext sC = new Studentcontext();
        List<j2t> model = new List<j2t>();
       
        public ActionResult Index()
        {
            Studentcontext stdcntxt = new Studentcontext();
            var data = stdcntxt.counts;
            return View(data.ToList());
        }
        public ActionResult showResult()
        {
            //private MovieDBContext db = new MovieDBContext();
           
            //return View(db.Movies.ToList());
            return View(sC.counts.ToList());
        }
        public ActionResult showMe()
        {
           // string ss = sC.CurrentsessionS;
            //var q = (from s in sC.counts
            //         join sa in sC.Script_counts
            //             on s.Sub_PCode equals sa.Subcode
            //             where sa.Sessn=="April - June, 2018"
            //             orderby sa.Dept,s.Sub_PCode
            //         select new  { 
            //             Dept=sa.Dept,
            //             Sub_PCode=s.Sub_PCode,
            //             Total=s.Total,
            //             Campus_ID=s.Campus_ID
            //             }).ToList();
            ViewBag.SSN = (from N in sC.CurrentsessionS select N.Current_sess).FirstOrDefault();
            var q = (from s in sC.Script_counts
                     join sa in sC.counts  on s.Subcode equals sa.Sub_PCode
                     join ss in sC.CurrentsessionS on s.Sessn equals ss.Current_sess
                     where s.Sessn == ss.Current_sess
                     orderby s.Dept, sa.Sub_PCode
                     select new
                     {
                         Dept = s.Dept,
                         Sub_PCode = sa.Sub_PCode,
                         Total = sa.Total,
                         Campus_ID = sa.Campus_ID
                     }).ToList().Distinct();
            foreach (var item in q)
            {
                model.Add(new j2t()
                {
                    Dept = item.Dept,
                    Sub_PCode = item.Sub_PCode,
                    Total = item.Total,
                    Campus_ID=item.Campus_ID
                });
            }
            return View(model);
            //return View();
        }
        public ActionResult DisplayResult(string Subject_Code)
        {
            //return View(sC.counts.ToList());
            
            //IEnumerable<SelectListItem> items = sC.counts.Select(c => new SelectListItem
            //{
            //    Value = c.Sub_PCode,
            //    Text = c.Sub_PCode
            //});
            //ViewBag.Subj = items;
            //return View(); 
            
            ViewBag.Subj = (from r in sC.counts
                            select r.Sub_PCode).Distinct();
            var model = (from r in sC.counts
                        orderby r.Sub_PCode
                        where r.Sub_PCode == Subject_Code || Subject_Code == null || Subject_Code == ""
                        select r).Distinct();
            return View(model);
        }
        public ActionResult WebGrid()
        {
            //Count model = new Count();
            //model.PageSize = 4;
            // model.TotalCount = 50;
           var CN=new List<Count>();
           CN = sC.counts.ToList();
            return View(CN);
        }
        public ActionResult TT()
        {
            Studentcontext sC = new Studentcontext();
            var TTTbl = new List<Time_Table>();
           //TTTbl = sC.TimeTables.ToList();
            var TTTx = (from xn in sC.TimeTables
                        orderby xn.Department , xn.DOE
                        where xn.Sessn=="April - June, 2018"
                        select xn);
            return View(TTTx.ToList());
           //return View(TTTbl);
        }
        public List<Time_Table> getTT(string search, string sort, string sortdir, int skip, int pageSize, out int TotalRecord)
        {
            bool x = true;
            if (search == "Practical") { x = true; } else if (search == "Theory") { x = false; }
            using (Studentcontext dc = new Studentcontext())
            {
                var v = (from a in dc.TimeTables
                         where
                         ((a.Department.Contains(search) ||
                         a.Duration.Contains(search) ||
                         a.Semester.Equals(search)||
                        // a.Practical.Equals(x)||
                         //a.DOE.Equals(search))||
                        // a.Semester.Contains(search) ||
                         a.Subject.Contains(search)) 
                         &&
                         a.Sessn.Contains("APRIL - JUNE, 2018"))
                         select a
                       );
                TotalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                //if (pageSize > 0)
                //{
                //    v = v.Skip(skip).Take(pageSize);
                //}
                
                return v.ToList();
                    

            }
        }
        public ActionResult ShowTT(int page = 1, string sort = "Department", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecords = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            //int skip = 100;
            var data = getTT(search, sort, sortdir, skip, pageSize, out totalRecords);
            ViewBag.TotalRows = totalRecords;
            ViewBag.search = search;
            return View(data);
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
            //return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xls");
        }
        //public ActionResult CountView()
        //{
        //    return View();
        //}
        //[HttpPost]
        public ActionResult CountView(string searchText)
        {
            ViewBag.SbjC = searchText;
            ViewBag.Subj = (from r in sC.counts
                            select r.Sub_PCode).Distinct();
            return View();
            //return Json(db.Countries.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
            
        }
    [HttpPost]    
     public ActionResult AutocompleTeSgn(string prefix)
        {
            //var sJ = (from c in sC.counts
            //          where c.Sub_PCode.StartsWith(prefix)
            //          select c.Sub_PCode  ).Distinct().ToArray();

            //return Json(sJ, JsonRequestBehavior.AllowGet);
            //return View(sJ);
            var sJ = (from TT in sC.TimeTables
                           join Sn in sC.CurrentsessionS on
                           new { X1 = TT.Sessn }
                           equals
                            new { X1 = Sn.Current_sess }
                           where (TT.Subject.StartsWith(prefix))
                           orderby TT.Subject
                           select TT.Subject).Distinct().ToArray();
            return Json(sJ);
        }
    [HttpPost]
    public ActionResult AutocompleteCertNo(string prefix)
    {
        var CT = (from Cert in sC.PassedOutStudentsListS where Cert.CertificateNo.StartsWith(prefix) orderby Cert.CertificateNo select Cert.CertificateNo).Distinct().ToArray();
        return Json(CT);
    }
    [HttpPost]
    public ActionResult Suggst(String searchText)
    {
        return Json(sC.counts.Where(c => c.Sub_PCode.StartsWith(searchText)).Select(a => new { label = a.Sub_PCode }), JsonRequestBehavior.AllowGet);
    }
    }
  
}
