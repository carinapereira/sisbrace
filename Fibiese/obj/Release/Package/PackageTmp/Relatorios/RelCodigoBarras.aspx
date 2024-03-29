﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelCodigoBarras.aspx.cs" Inherits="FIBIESA.Relatorios.RelCodigoBarras" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relatório de Código de Barras</title>
    <style type="text/css" media="all">
        html, body
        {
	        margin: 0px;
	        padding: 0px;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div id="divRelatorio" runat="server">
            <rsweb:ReportViewer ID="rpvCodigoBarras" runat="server" Height="600px" 
                Width="1120px" ShowRefreshButton="False" ShowPromptAreaButton="False" 
                ShowDocumentMapButton="False" ShowFindControls="False" 
                Font-Names="Arial" Font-Size="8pt" >
                <LocalReport ReportPath="Relatorios\rptCodigoBarras.rdlc">
                </LocalReport>                
            </rsweb:ReportViewer>
        </div>
        <div id="divMensagem" runat="server" visible="false">
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
