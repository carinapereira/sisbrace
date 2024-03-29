﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadBanco.aspx.cs" Inherits="Admin.cadBanco" %>
    <%@ MasterType VirtualPath="~/home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div id="content">
        <div class="container half left">
            <div class="conthead">
                <h2>Cadastro de Bancos</h2>
            </div>
            <div class="contentbox">
                 <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
                <table>
                    <tr>
                        <td style="width: 140px">* Código:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="inputboxRight" 
                                ToolTip="Informe o código do banco" Width="100px" AutoPostBack="True" 
                                ontextchanged="txtCodigo_TextChanged" MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtCodigo" CssClass="validacao" 
                                ErrorMessage="*Informe o código" ValidationGroup="salvar">*</asp:RequiredFieldValidator>
                            <asp:Label ID="lblInformacao" runat="server" CssClass="validacao"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">* Descrição:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="inputbox" Width="335px" 
                                MaxLength="70" ToolTip="Informe a descrição do banco"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDescricao" ErrorMessage="*Informe a descrição" 
                                ValidationGroup="salvar" CssClass="validacao">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 140px">
                            
                        </td>
                        <td style="width: 400px">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn" 
                                onclick="btnVoltar_Click" ToolTip="Volta para a página de consulta" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn" 
                                onclick="btnSalvar_Click" ValidationGroup="salvar" 
                                ToolTip="Salva as informações do banco" />                            
                        </td>
                    </tr>
                </table>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                CssClass="validacao" ValidationGroup="salvar" />
            <asp:HiddenField ID="hfId" runat="server" />
        </div>
        <div class="status">
        </div>
    </div>    
</asp:Content>
