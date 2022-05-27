<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myreport.aspx.cs" Inherits="MvcApplication1.Reports.myreport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student count</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; text-decoration:underline">
    <%--<h1 style="color:blue;font-family:Cambria">Subject-wise count of students.</h1>--%>
    </div>
        <asp:ScriptManager runat="server"> </asp:ScriptManager>
        <div align="center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True">
            <LocalReport ReportPath="Reports\Rptvw.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
           </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Studentcontext %>" SelectCommand="SELECT [Sub_PCode], [Reg], [Arr], [Total], [Campus_ID] FROM [Count]"></asp:SqlDataSource>
           
    </form>
</body>
</html>
