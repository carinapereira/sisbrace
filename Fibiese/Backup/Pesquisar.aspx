﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="StarFestaEventos.Pesquisar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/default.css" rel="stylesheet" type="text/css" />
     <link href="styles/main.css" rel="stylesheet" type="text/css" />
     <link href="themes/blue/styles.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtPesquisa" runat="server" CssClass="inputbox" Width="180px"></asp:TextBox>                
                </td>
                <td>
                    <asp:ImageButton ID="btnPesquisa" runat="server"  CssClass="btnPes"
                        onclick="btnPesquisa_Click" ImageUrl="~/images/icons/icone_lupa.jpg" />  
                </td>
            </tr>
             <tr>
                <td colspan ="2">
                 <asp:GridView ID="grdPesquisa" runat="server" CellPadding="3" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        onselectedindexchanged="grdPesquisa_SelectedIndexChanged" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                        BorderWidth="1px" GridLines="None" 
                        onrowdatabound="grdPesquisa_RowDataBound">
                     <Columns>
                         <asp:CommandField ShowSelectButton="True">
                           <HeaderStyle CssClass="grd_cmd_header" />
                           <ItemStyle CssClass="grd_select" />
                         </asp:CommandField>
                         <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                         <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                         <asp:BoundField DataField="DESCRICAO" HeaderText="Descrição" />
                     </Columns>
                     <FooterStyle BackColor="White" ForeColor="#000066" />
                     <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                     <RowStyle ForeColor="#000066" />
                     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                     <SortedAscendingCellStyle BackColor="#F1F1F1" />
                     <SortedAscendingHeaderStyle BackColor="#007DBB" />
                     <SortedDescendingCellStyle BackColor="#CAC9C9" />
                     <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                </td>
            </tr>
        </table>      
    
    </div>
    </form>
</body>
</html>
