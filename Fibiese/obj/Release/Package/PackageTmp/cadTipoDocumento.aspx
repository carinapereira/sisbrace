﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadTipoDocumento.aspx.cs" Inherits="Admin.cadTipoDocumento" %>
<%@ MasterType VirtualPath="~/home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div id="content">
        <div class="container half left">
            <div class="conthead">
                <h2>
                    Cadastro de Tipos de Documentos</h2>
            </div>
            <div class="contentbox">
                <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
                <table>
                    <tr>
                        <td style="width: 140px">
                            Código:
                        </td>
                        <td style="width: 400px">
                           <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">
                            * Descrição:
                        </td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="inputbox" 
                                MaxLength="40" Width="335px" ToolTip="Informe a descrição"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDescricao" ErrorMessage="*Preenchimento Obrigatório" 
                                ForeColor="#CC0000" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">
                            *
                            Aplicação:
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="ddlAplicacao" runat="server" CssClass="dropdownlist" 
                                ToolTip="Selecione a aplicação">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="CP">Contas a Pagar</asp:ListItem>
                                <asp:ListItem Value="CR">Contas a Receber</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="ddlAplicacao" ErrorMessage="*Preenchimento Obrigatório" 
                                ForeColor="#CC0000" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 140px">
                        </td>
                        <td style="width: 400px">
                            <asp:Button ID="btnVoltar" runat="server" CssClass="btn" Text="Voltar" 
                                onclick="btnVoltar_Click" ToolTip="Volta para a página de consulta" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnInserir" runat="server" CssClass="btn" Text="Salvar" 
                                onclick="btnInserir_Click" ValidationGroup="salvar" 
                                ToolTip="Valida e salva as informações" />                            
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hfId" runat="server" />
        </div>
        <div class="status">
        </div>
    </div>   
</asp:Content>
