using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Mvc.Html;

namespace MvcApplication1.Controllers
{
    public class VerificationController : Controller
    {
        //
        // GET: /Verification/
        Studentcontext sC = new Studentcontext();
        public ActionResult Index()
        {
            return View();
        }
         [Authorize]
        public ActionResult Show(string searchText)
        {            
            //var Dt=(from r in sC.PassedOutStudentsListS where r.CertificateNo=="050800001" select r).Distinct();
            var Dt = sC.PassedOutStudentsListS.Where(s => s.CertificateNo == searchText).FirstOrDefault();
            ViewBag.Ct = searchText;
            return View(Dt);
        }

    }
}
