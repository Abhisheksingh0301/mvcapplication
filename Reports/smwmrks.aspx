<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="smwmrks.aspx.cs" Inherits="MvcApplication1.Reports.smwmrks" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
        
            <%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MvcApplication1.Reports.DataSet1TableAdapters.SemwiseMarksTableAdapter"></asp:ObjectDataSource>--%>
           
           <div align="center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True">
            <LocalReport ReportPath="Reports\SemWmarks.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
               </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Studentcontext %>" SelectCommand="SELECT DISTINCT [Roll], [Name], [Sess], [Subject], [SemCode], [TOTAL], [CREDIT], [Campus_id], [Crdt], [RegNo], [PASS], [Department] FROM [Student_Information]"></asp:SqlDataSource>
    </form>
</body>
</html>
