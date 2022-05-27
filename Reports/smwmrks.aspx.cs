using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MvcApplication1.Models;
using Microsoft.Reporting.WebForms;

namespace MvcApplication1.Reports
{
    public partial class smwmrks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Dep = string.Empty;
                string Sessn = string.Empty;
                if (Request.QueryString["Dep"] != null)
                {
                    Dep = Request.QueryString["Dep"].ToString();

                }
                if (Request.QueryString["Sessn"] != null)
                {
                    Sessn = Request.QueryString["Sessn"].ToString();
                }
            //}
                Studentcontext sC = new Studentcontext();
                //ReportViewer1.LocalReport.DataSources.Clear();
                
                var Datax = (from D in sC.Student_InformationS where D.Sess == Sessn && D.Department == Dep orderby D.Roll select D);
                
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/SemWmarks.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", Datax);
                //var rdc = new ReportDataSource("Dataset1", Datax);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}