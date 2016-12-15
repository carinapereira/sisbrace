<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true" CodeBehind="viewRelEventos.aspx.cs" Inherits="FIBIESA.viewRelFrequencia" %>
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
                <div class="container half left">
                    <div class="conthead">
                        <h2>Relatórios dos Eventos</h2>
                    </div>
                    <div class="contentbox">
                        <table>
                            <tr>
                                <td style="width: 140px">
                                    Evento:
                                </td>
                                <td style="width: 530px" colspan="3">
                                    <asp:DropDownList ID="ddlEvento" runat="server" CssClass="dropdownlist" 
                                        ToolTip="Selecione o evento" AppendDataBoundItems="true" AutoPostBack="true"
                                        onselectedindexchanged="ddlEvento_SelectedIndexChanged">                                                                                                                            
                                    </asp:DropDownList>                                                                 
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 140px">
                                    Turma:
                                </td>
                                <td style="width: 530px" colspan="3">
                                    <asp:DropDownList ID="ddlTurma" runat="server" CssClass="dropdownlist" 
                                        ToolTip="Selecione a turma" AppendDataBoundItems="false" AutoPostBack="true"
                                        onselectedindexchanged="ddlTurma_SelectedIndexChanged">                                                                                
                                    </asp:DropDownList>                                                              
                                </td>
                            </tr>                           
                            <tr>
                                <td style="width: 140px">
                                    Participante:
                                </td>
                                <td style="width: 530px" colspan="3">
                                    <asp:DropDownList ID="ddlParticipante" runat="server" CssClass="dropdownlist" 
                                        ToolTip="Selecione o participante" AppendDataBoundItems="false">                                                        
                                    </asp:DropDownList>                                    
                                </td>
                            </tr>
                            <td style="width: 140px">
                                    Período:
                                </td>
                                <td style="width: 400px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataInicio" runat="server" CssClass="inputbox" Width="100px" 
                                                    ToolTip="Informe a data inicial do evento"></asp:TextBox><asp:CalendarExtender Format="dd/MM/yyyy"
                                                    ID="txtDataIni_CalendarExtender" runat="server" TargetControlID="txtDataInicio"
                                                    Enabled="True">
                                                </asp:CalendarExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="*" ToolTip="Não Válido" SetFocusOnError="true" 
        ControlToValidate="txtDataInicio" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
                                                    Display="Dynamic" validationgroup="grupo" ForeColor="Red"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                &nbsp;a&nbsp;&nbsp;
                                            </td>
                                            <td>    
                                                <asp:TextBox ID="txtDataFim" runat="server" CssClass="inputbox" Width="100px" 
                                                    ToolTip="Informe a data inicial do evento"></asp:TextBox><asp:CalendarExtender Format="dd/MM/yyyy"
                                                    ID="txtDataIniF_CalendarExtender" runat="server" TargetControlID="txtDataFim"
                                                    Enabled="True">
                                                </asp:CalendarExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ToolTip="Não Válido" SetFocusOnError="true" 
        ControlToValidate="txtDataFim" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" 
                                                    Display="Dynamic" validationgroup="grupo" ForeColor="Red"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                    
                            </tr>                                                     
                             <tr>
                                <td style="width: 140px">
                                    Relatório:
                                </td>
                                <td  colspan="3">                                   
                                        <asp:RadioButton ID="rbEventos" GroupName="relatorios" runat="server" CssClass="input" Text="Eventos">
                                        </asp:RadioButton>
                                        <asp:RadioButton ID="rbTurmas" GroupName="relatorios" runat="server" CssClass="input" Text="Turmas">                                                                                    
                                        </asp:RadioButton>  
                                        <asp:RadioButton ID="rbFrequencias" GroupName="relatorios" runat="server" CssClass="input" Text="Frequências">                                                                                    
                                        </asp:RadioButton>  
                                </td>                                                                      
                            </tr>
                             <tr>
                                <td></td>
                                <td  colspan="3">                                   
                                        <asp:RadioButton ID="rbSemPreenchimento" GroupName="Preenchimento" runat="server" CssClass="input" Text="Sem Preenchimento">
                                        </asp:RadioButton>
                                        <asp:RadioButton ID="rbComPreenchimento" GroupName="Preenchimento" runat="server" CssClass="input" Text="Com Preenchimento">                                                                                    
                                        </asp:RadioButton>  
                                </td>                                                                      
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="3">
                                    <asp:Button ID="btnVoltar" runat="server" CssClass="btn" Text="Voltar" 
                                    onclick="btnVoltar_Click" ToolTip="Volta para página principal" />
                                            &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="btn" Text="Relatório" 
                                        onclick="btnRelatorio_Click" validationgroup="grupo"/>
                                </td>                                    
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="status">
                </div>                
                <asp:HiddenField ID="hfIdEvento" runat="server" Value="0" />
                <asp:HiddenField ID="hfIdTurma" runat="server" Value="0" />
                <asp:HiddenField ID="hfIdInstrutor" runat="server" Value="0" />
                <asp:HiddenField ID="hfIdParticipante" runat="server" Value="0" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
