<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadUsuario.aspx.cs" Inherits="Admin.cadUsuario" Culture="auto" UICulture="auto" %>

<%@ MasterType VirtualPath="~/home.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlPesquisa" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="content">
                <div class="container half3 left">
                    <div class="conthead">
                        <h2>
                            Cadastro de Usuários</h2>
                    </div>
                    <div class="contentbox">
                        <table>
                            <tr>                                
                                <td style="width: 80px">
                                    * Categoria:
                                </td>
                                <td style="width: 300px">
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="dropdownlist" ToolTip="Selecione a categoria">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCategoria"
                                        CssClass="validacao" ErrorMessage="*Preenchimento Obrigatório" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 80px">
                                    * Status:
                                </td>
                                <td style="width: 250px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="95px" CssClass="dropdownlist"
                                        ToolTip="Selecione o status">
                                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    Nome:
                                </td>
                                <td style="width: 300px" colspan="3">
                                    <asp:TextBox ID="txtNome" runat="server" CssClass="inputbox" MaxLength="70" Width="300px"
                                        ToolTip="Informe o nome da pessoa"></asp:TextBox>
                                </td>
                               
                            </tr>
                            <tr>
                                <td style="width: 130px; height: 62px;">
                                    * E-mail:
                                </td>
                                <td style="width: 300px; height: 62px;" colspan="5">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="inputbox" MaxLength="100" Width="300px"
                                        ToolTip="Informe o e-mail"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*E-mail com formato errado"
                                        ToolTip="Não Válido" SetFocusOnError="true" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic" ValidationGroup="salvar" CssClass="validacao"></asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                        CssClass="validacao" ErrorMessage="*Preenchimento Obrigatório" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                                </td>
                               
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    * Login:
                                </td>
                                <td style="width: 300px" colspan="5">
                                    <asp:TextBox ID="txtLogin" runat="server" CssClass="inputbox" MaxLength="20" ToolTip="Informe o login"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin"
                                        ErrorMessage="*Preenchimento Obrigatório" ValidationGroup="salvar" CssClass="validacao"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    * Senha:
                                </td>
                                <td style="width: 300px" colspan="3">
                                    <asp:TextBox ID="txtSenha" runat="server" CssClass="inputbox" MaxLength="100" Width="300px"
                                        TextMode="Password" ToolTip="Informe a senha"></asp:TextBox>
                                    <asp:Label ID="lblInformacao" runat="server" CssClass="validacao" 
                                                        Font-Size="Smaller"></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    * Confirmar Senha:
                                </td>
                                <td style="width: 300px" colspan="3">
                                    <asp:TextBox ID="txtConfirmarSenha" runat="server" CssClass="inputbox" MaxLength="100"
                                        Width="300px" TextMode="Password" ToolTip="Confirmar a senha"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtSenha"
                                        ControlToValidate="txtConfirmarSenha" ErrorMessage="Os valores dos campo Senha e Confirmar Senha devem ser iguais"
                                        ValidationGroup="salvar" CssClass="validacao"></asp:CompareValidator>
                                </td>
                            </tr>                           
                        </table>
                        <table>
                            <tr>
                                <td style="width: 130px">
                                </td>
                                <td style="width: 300px">
                                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn" OnClick="btnVoltar_Click"
                                        ToolTip="Volta para página de consulta" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn" OnClick="btnSalvar_Click"
                                        ValidationGroup="salvar" ToolTip="Valida e salva as informações" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="hfId" runat="server" />
                  </div>
                <div class="status">
                </div>                             
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
