﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true" CodeBehind="viewRelMovimentacaoEstoque.aspx.cs" Inherits="FIBIESA.viewRelMovimentacaoEstoque" %>
<%@ MasterType VirtualPath="~/home.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updPrincipal" runat="server" UpdateMode="Always">
        <ContentTemplate>        
            <div id="content">
                <div class="container half left">
                    <div class="conthead">
                        <h2>Relatório de Movimentação do Estoque</h2>
                    </div>
                    <div class="contentbox">
                        <table>
                            <tr>
                                <td style="width: 140px">
                                    Item(s):
                                </td>
                                <td style="width: 530px" colspan="2">
                                    <asp:TextBox ID="txtItem" runat="server" CssClass="inputbox" Width="260px" 
                                        AutoPostBack="true" ontextchanged="txtItem_TextChanged" 
                                        ToolTip="Informe o(s) item(s) - Lista de valores disponível"></asp:TextBox>                                                                                   
                                    <asp:Button ID="btnPesItem" runat="server" CssClass="btn" Text="..." 
                                        onclick="btnPesItem_Click"  />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*" ToolTip="Não Válida" SetFocusOnError="true"
        ControlToValidate="txtItem" ValidationExpression="^\d+(,\d+)*$" Display="Dynamic" validationgroup="grupo" ForeColor="Red"  CssClass="labelValignMiddle"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 140px">
                                    Usuário(s):
                                </td>
                                <td style="width: 530px" colspan="2">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputbox" Width="260px" 
                                        AutoPostBack="true" ontextchanged="txtUsuario_TextChanged" 
                                        ToolTip="Informe o(s) usuário(s) - Lista de valores disponível"></asp:TextBox>
                                    <asp:Button ID="btnPesUsuario" runat="server" CssClass="btn" Text="..." 
                                        onclick="btnPesUsuario_Click"  />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*" ToolTip="Não Válida" SetFocusOnError="true"
        ControlToValidate="txtUsuario" ValidationExpression="^\d+(,\d+)*$" Display="Dynamic" validationgroup="grupo" ForeColor="Red"  CssClass="labelValignMiddle"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 140px">
                                    Quantidade:
                                </td>
                                <td style="width: 530px" colspan="2" >                            
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="inputboxRight" 
                                        ToolTip="Informe a quantidade "></asp:TextBox>                                                                
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ToolTip="Não Válida" SetFocusOnError="true"
ControlToValidate="txtQuantidade" ValidationExpression="^\d+$" Display="Dynamic" validationgroup="grupo" ForeColor="Red"  CssClass="labelValignMiddle"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tipo de Movimento:
                                </td>
                                <td style="width: 530px">                                   
                                        <asp:DropDownList ID="ddlTipoMov" runat="server" AppendDataBoundItems="True" CssClass="dropdownlist"                                        
                                        ToolTip="Selecione o tipo de movimento">
                                        <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Entrada" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Saída" Value="2"></asp:ListItem>
                                    </asp:DropDownList>                                 
                                </td>                                                                      
                            </tr>
                            <tr>
                                <td style="width: 140px">
                                    Data:
                                </td>
                                <td style="width: 400px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataIni" runat="server" CssClass="inputbox" Width="100px" 
                                                    ToolTip="Informe a data de movimentação"></asp:TextBox><asp:CalendarExtender Format="dd/MM/yyyy"
                                                    ID="txtDataIni_CalendarExtender" runat="server" TargetControlID="txtDataIni"
                                                    Enabled="True">
                                                </asp:CalendarExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ToolTip="Não Válido" SetFocusOnError="true" 
ControlToValidate="txtDataIni" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
                                            Display="Dynamic" validationgroup="grupo" ForeColor="Red"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                &nbsp;a&nbsp;&nbsp;
                                            </td>
                                            <td>    
                                                <asp:TextBox ID="txtDataFim" runat="server" CssClass="inputbox" Width="100px" 
                                                    ToolTip="Informe a data de movimentação"></asp:TextBox><asp:CalendarExtender Format="dd/MM/yyyy"
                                                    ID="txtDataFim_CalendarExtender" runat="server" TargetControlID="txtDataFim" 
                                                    Enabled="True">
                                                </asp:CalendarExtender>
                                                <asp:RegularExpressionValidator ID="REVdataFim" runat="server" ErrorMessage="*" ToolTip="Não Válido" SetFocusOnError="true"
ControlToValidate="txtDataFim" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
                                            Display="Dynamic" validationgroup="grupo" ForeColor="Red"></asp:RegularExpressionValidator>

                                            </td>
                                        </tr>
                                    </table>
                                </td>                                    
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnVoltar" runat="server" CssClass="btn" Text="Voltar" 
                                        OnClick="btnVoltar_Click" ToolTip="Volta para página principal" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="btn" Text="Relatório" 
                                        onclick="btnRelatorio_Click" validationgroup="grupo" 
                                        ToolTip="Imprime o relatório" />
                                </td>                                    
                            </tr>                                        
                        </table>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdUsuario" runat="server" Value="0"/>
                <asp:HiddenField ID="hfIdItem" runat="server" Value="0"/>
                <div class="status">
                </div>
                <asp:Panel runat="server" ID="pnlUsuario" Width="450px" Height="450px" CssClass="modalPopup" Style="display: none" ScrollBars="Auto">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPesquisaUsuario" runat="server" CssClass="inputbox" Width="180px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnBuscarUsu" runat="server" Text="Buscar" CssClass="btn" 
                                    onclick="btnBuscarUsu_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelUsuario" runat="server" Text="Cancelar" OnClick="btnCancelUsuario_Click"
                                    CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grdPesquisaUsuario" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                    BorderWidth="1px" GridLines="None" OnRowDataBound="grdPesquisaUsuario_RowDataBound"
                                    Width="300px">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnSelectUsuario" runat="server" ImageUrl="~/images/icons/icon_tick.png"
                                                    OnClick="btnSelectUsuario_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtenderPesquisaUsuario" runat="server" TargetControlID="hfIdUsuario"
                    PopupControlID="pnlUsuario" BackgroundCssClass="modalBackground" DropShadow="true"
                    OkControlID="btnCancel" Enabled="false" />
                <asp:Panel runat="server" ID="pnlItem" Width="450px" Height="450px" CssClass="modalPopup" ScrollBars="Auto" Style="display: none">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPesquisa" runat="server" CssClass="inputbox" Width="180px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="btn" 
                                    onclick="btnBuscar_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click"
                                    CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grdPesquisaItem" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                    BorderWidth="1px" GridLines="None" OnRowDataBound="grdPesquisaItem_RowDataBound"
                                    Width="300px">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/images/icons/icon_tick.png"
                                                    OnClick="btnSelect_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtenderPesquisaItem" runat="server" TargetControlID="hfIdItem"
                    PopupControlID="pnlItem" BackgroundCssClass="modalBackground" DropShadow="true"
                    OkControlID="btnCancel" Enabled="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>