using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Mvc.Html;
using System.IO;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        Studentcontext sC = new Studentcontext();
        List<Measurement_Evaluation> model = new List<Measurement_Evaluation>();
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
        public ActionResult Info(string Sessn,  string roll, string Dep,string sort = "SemCode", string sortdir = "asc")
        {
            List<Student_Info> model = new List<Student_Info>();
            ViewBag.SSn = (from r in sC.Student_MasterS orderby r.Sess descending select r.Sess).ToList().Distinct();
            ViewBag.DpT = (from DT in sC.Student_MasterS orderby DT.Sess ascending  select DT.Department).ToList().Distinct();
            ViewBag.Name=((from nm in sC.Student_MasterS where ((nm.Sess==Sessn) && (nm.Department==Dep) && (nm.Roll==roll)) select nm.Name).FirstOrDefault());
            ViewBag.RegdNo = ((from nm in sC.Student_MasterS where ((nm.Sess == Sessn) && (nm.Department == Dep) && (nm.Roll == roll)) select nm.RegistrationNo).FirstOrDefault());
            ViewBag.CurYr = ((from nm in sC.Student_MasterS where ((nm.Sess == Sessn) && (nm.Department == Dep) && (nm.Roll == roll)) select nm.Current_Year).FirstOrDefault());
            //ViewBag.FullMks = ((from fM in sC.Marks_SetupS where ((fM.Sess == Sessn) && (fM.Department == Dep)) select fM.FullMarks).FirstOrDefault());
            ViewBag.roll = roll;
            double FullTotal = 0;
            string Regn_Number = "";
            string SubList = "";
            int PassCount=0, ArrearCount=0;
            //var qry = (from st in sC.Student_MasterS
            //           join exRg in sC.Exam_Marks_ActuaLS on st.RegistrationNo equals exRg.RegNo
            //           join exC in sC.Exam_Marks_ActuaLS on st.Current_Year equals exC.Current_Year
            //           join exD in sC.Exam_Marks_ActuaLS on st.Department equals exD.Department
            //           join exSn in sC.Exam_Marks_ActuaLS on st.Sess equals exSn.Sess
            //           join exRl in sC.Exam_Marks_ActuaLS on st.Roll equals exRl.Roll
            //           where st.Department == Dep && st.Roll == roll && st.Sess == Sessn
            //           orderby exC.SemCode, exC.Subject
            //           select new
            //           {
            //               Name=st.Name,
            //               Regno=st.RegistrationNo,
            //               SemCode = exC.SemCode,
            //               Subject = exC.Subject,
            //               CA = exC.CA,
            //               wTg = exC.WEIGHTAGE,
            //               Plus = exC.PLUS,
            //               TtL = exC.TOTAL,
            //               Pass = exC.PASS,
            //               Credit=exC.CREDIT,
            //               Remarks = exC.Remarks,
            //               Department=Dep,
            //               Roll=roll,
            //               Sess=Sessn,
            //               Current_year = st.Current_Year
            //           }).ToList();
            //            foreach (var item in qry)
            //            {
            //                model.Add(new Student_Info()
            //                {
            //                    Name=item.Name,
            //                    RegNo=item.Regno,
            //                    Subject=item.Subject,
            //                    CA=item.CA,
            //                    WEIGHTAGE=item.wTg,
            //                    PLUS=item.Plus,
            //                    TOTAL=item.TtL,
            //                    PASS=item.Pass,
            //                    CREDIT=item.Credit,
            //                    Department=item.Department,
            //                    Current_Year=item.Current_year,
            //                    Sess=item.Sess,
            //                    Roll=item.Roll,
            //                    SemCode=item.SemCode,
            //                    Remarks=item.Remarks
            //                });
            //            }
            ////////////////////////////////////////////////////////////////
            var qry=(from st in sC.Student_MasterS
                     join ex in sC.Exam_Marks_ActuaLS on new{X1=st.Roll, X2=st.Sess,X3=st.RegistrationNo, X4=st.Department}
                     equals
                     new {X1=ex.Roll, X2=ex.Sess, X3=ex.RegNo, X4=ex.Department}
                     where (st.Department == Dep && st.Roll == roll && st.Sess == Sessn)
                     orderby ex.SemCode, ex.Subject
                      select new
                      {
                          Name = st.Name,
                          Regno = st.RegistrationNo,
                          SemCode = ex.SemCode,
                          Subject = ex.Subject,
                          CA = ex.CA,
                          WTG = ex.WEIGHTAGE,
                          Plus = ex.PLUS,
                          TtL = ex.TOTAL,
                          Pass = ex.PASS,
                          Credit = ex.CREDIT,
                          Remarks = ex.Remarks,
                          Department = Dep,
                          Roll = roll,
                          Sess = Sessn,
                          Current_year = st.Current_Year
                      }).ToList();
            foreach (var item in qry)
            {
                model.Add(new Student_Info()
                {
                    Name = item.Name,
                    RegNo = item.Regno,
                    Subject = item.Subject,
                    CA = item.CA,
                    WTG = item.WTG,
                    PLUS = item.Plus,
                    TOTAL = item.TtL,
                    PASS = item.Pass,
                    CREDIT = item.Credit,
                    Department = item.Department,
                    Current_Year = item.Current_year,
                    Sess = item.Sess,
                    Roll = item.Roll,
                    SemCode = item.SemCode,
                    Remarks = item.Remarks
                }); 
                FullTotal = item.TtL + FullTotal;
                Regn_Number = item.Regno;
                if (item.Pass == "PASS")
                    PassCount += 1;
                else if (item.Pass == "ARREAR")
                    ArrearCount += 1;
            }
            int TotalRecords = qry.Count();
            ViewBag.TRec=TotalRecords;
            ViewBag.FT = FullTotal;
            int FullMks=0;
            var FM = (from EM in sC.Exam_Marks_ActuaLS
                      join MS in sC.Marks_SetupS on
                      new { X1 = EM.Sess, X2 = EM.Department, X3 = EM.Subject, X4=EM.Current_Year }
                      equals
                       new { X1 = MS.Sess, X2 = MS.Department, X3 = MS.Subject_Code, X4=MS.Current_Year }
                      where (MS.Department == Dep && MS.Sess == Sessn && EM.Roll == roll)
                      orderby EM.SemCode, EM.Subject
                      select new { MS.FullMarks }).ToList();
            foreach (var item in FM)
            {
                FullMks = item.FullMarks + FullMks;
            };
            var SubApprd = (from SMks in sC.SemMarkS
                            where SMks.Regn_No == Regn_Number
                            orderby SMks.Sem_No, SMks.Sub_PCode
                            select new { SMks.Sub_PCode }).ToList();
            foreach (var SItem in SubApprd)
            {
                SubList=SubList+SItem.Sub_PCode+", ";
            }
            ViewBag.SubjectList=(SubList==null || SubList=="")?"Not Appeared":SubList;
            ViewBag.FullMks = FullMks;
            double Perc = (FullTotal * 100) / FullMks;
            string Percvalue = Perc.ToString("0.00");
            ViewBag.Prc = Percvalue;
            ViewBag.Pass = PassCount;
            ViewBag.Arrear = ArrearCount;
            return View(model.ToList());
        }

        public ActionResult SemWiseMarks(string Sessn, string Dep)
        {
            ViewBag.SSn = (from r in sC.Student_MasterS orderby r.Sess descending select r.Sess).ToList().Distinct();
            ViewBag.DpT = (from DT in sC.Student_MasterS orderby DT.Sess ascending select DT.Department).ToList().Distinct();
            ViewBag.SSnC = Sessn;
            ViewBag.DepC = Dep;
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [Authorize(Users="Abhishek")]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }

        public ActionResult Measurement_Report()
        {
           ViewBag.Sessn=((from Sn in sC.CurrentsessionS select Sn.Current_sess).FirstOrDefault());
            int full_total = 0;
            var QryMe = (from Msc in sC.Script_counts
                         join Mtt in sC.TimeTables
                         on new { X1 = Msc.Dept, X2 = Msc.Subcode, X3 = Msc.Sessn }
                         equals
                         new { X1 = Mtt.Department, X2 = Mtt.Subject, X3 = Mtt.Sessn }
                         join Css in sC.CurrentsessionS on Mtt.Sessn equals Css.Current_sess
                         //where Msc.Sessn=="Nov - Dec, 2019"                                             
                         //group new { Msc, Mtt } by new { Msc.Dept, Mtt.Semester } into g
                         group new { Msc, Mtt } by new { Mtt.Department, Mtt.Semester } into g
                         from Msc in g.DefaultIfEmpty()
                         orderby g.Key.Department, g.Key.Semester
                         select new
                         {
                             Department = g.Key.Department,
                             Semester = g.Key.Semester,
                             Total_Subjects = g.Select(x => x.Mtt.Subject).Distinct().Count(),
                             Total_Scripts = g.Sum(x => x.Msc.No_of_Scripts),
                             Last_Exam=g.Max(x=>x.Mtt.DOE)
                         }).ToList().Distinct();
            foreach (var item in QryMe)
            {
                model.Add(new Measurement_Evaluation()
                {
                    Department=item.Department,
                    Semester=item.Semester,
                    Total_Subjects=item.Total_Subjects,
                    Total_Scripts=item.Total_Scripts,
                    Last_Exam = item.Last_Exam
                });
                full_total = full_total + item.Total_Scripts;
            }                  
            //return View();
            ViewBag.FTot = full_total;
            return View(model.ToList());
        }
    }

    
}
