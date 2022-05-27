using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MvcApplication1.Models;


namespace MvcApplication1.Reports
{
    public partial class myreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string searchText = string.Empty;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Request.QueryString["searchText"].ToString();
                    
                }
                //searchText = "MECA3202";
                //List<Customer> customers = null;
                //using (var _context = new EmployeeManagementEntities())
                //{
                //    customers = _context.Customers.Where(t => t.FirstName.Contains(searchText) || t.LastName.Contains(searchText)).OrderBy(a => a.CustomerID).ToList();
                //    CustomerListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Report1.rdlc");
                //    CustomerListReportViewer.LocalReport.DataSources.Clear();
                //    ReportDataSource rdc = new ReportDataSource("CustomerDataSet", customers);
                //    CustomerListReportViewer.LocalReport.DataSources.Add(rdc);
                //    CustomerListReportViewer.LocalReport.Refresh();
                //    CustomerListReportViewer.DataBind();
                //}
                Studentcontext sC = new Studentcontext();
                ReportViewer1.LocalReport.DataSources.Clear();
                var Cnts=(from cnt in sC.counts where cnt.Sub_PCode==searchText  select cnt);
                //var Cxx=(from ss in DataSet1TableAdapters.CountTableAdapter select ss);


               // List<Models.Count> Counts = null;
                //using (var _context = new Models.Studentcontext())
                //{
                    //Cnts = _context.counts.Where(t => t.Sub_PCode.Contains(searchText)).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rptvw.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    //ReportDataSource rdc = new ReportDataSource("DataSet1",Cnts);

                    var rdc = new ReportDataSource("DataSet1", Cnts);

                   ReportViewer1.LocalReport.DataSources.Add(rdc);
                    
                    ReportViewer1.LocalReport.Refresh();
                    //ReportViewer1.DataBind();
                   

                //}
            }  
        }
    }
}