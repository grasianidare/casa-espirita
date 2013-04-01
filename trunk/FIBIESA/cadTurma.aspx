﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadTurma.aspx.cs" Inherits="Admin.cadTurma" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div id="content">
        <div class="container half3 left">
            <div class="conthead">
                <h2>
                    Cadastro de Turmas</h2>
            </div>
            <div class="contentbox">
                <table>
                    <tr>
                        <td style="width: 140px">                           
                            Código:
                        </td>
                        <td style="width: 400px" colspan="3">
                            <asp:Label ID="lblcodigo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">
                            *
                            Descrição:
                        </td>
                        <td style="width: 400px" colspan="3">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="inputbox" 
                                MaxLength="40" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDescricao" ErrorMessage="Informe da descrição" 
                                ValidationGroup="salvar" CssClass="validacao" Enabled="False">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 140px">
                            * Evento:
                        </td>
                        <td style="width: 400px" colspan="3">   
                            <asp:DropDownList ID="ddlEvento" runat="server" CssClass="dropdownlist">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="ddlEvento" ErrorMessage="Informe o evento" 
                                ValidationGroup="salvar" CssClass="validacao">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">
                            Instrutor:
                        </td>
                        <td style="width: 400px" colspan="3">                            
                            <asp:DropDownList ID="ddlInstrutor" runat="server" CssClass="dropdownlist">
                            </asp:DropDownList>                         
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">* Dt. Início:</td>
                        <td>
                            <asp:TextBox ID="txtDtInicio" runat="server" CssClass="inputbox" Width="110px" ></asp:TextBox>
                            <asp:CalendarExtender
                             ID="txtDtInicio_CalendarExtender" runat="server" TargetControlID="txtDtInicio"
                                    Enabled="True">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtDtInicio" CssClass="validacao" 
                                ErrorMessage="Informe da data de início" ValidationGroup="salvar">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 140px">* Dt. Fim:</td>
                        <td>
                            <asp:TextBox ID="txtDtFim" runat="server" CssClass="inputbox" Width="110px" ></asp:TextBox>
                            <asp:CalendarExtender
                             ID="txtDtFim_CalendarExtender" runat="server" TargetControlID="txtDtFim"
                                        Enabled="True">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="txtDtFim" CssClass="validacao" 
                                ErrorMessage="Informe da data final" ValidationGroup="salvar">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Hora Início:</td>
                        <td>
                            <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="inputbox" 
                                Width="80px" ></asp:TextBox>                          
                        </td>
                        <td style="width: 140px">Hora Fim:</td>
                        <td>
                            <asp:TextBox ID="txtHoraFim" runat="server" CssClass="inputbox" Width="80px" ></asp:TextBox>                          
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Sala:</td>
                        <td>
                            <asp:TextBox ID="txtSala" runat="server" CssClass="inputbox" Width="110px" ></asp:TextBox>                          
                        </td>
                        <td style="width: 140px">* Nro. Máximo:</td>
                        <td style="width: 400px" colspan="3"> 
                            <asp:TextBox ID="txtNroMax" runat="server" CssClass="inputbox" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtNroMax" CssClass="validacao" 
                                ErrorMessage="Informe o número máximo de alunos" ValidationGroup="salvar">*</asp:RequiredFieldValidator>
                        </td>                        
                    </tr>
                    <tr>                        
                        <td style="width: 140px">Dias da Semana:</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDiaSemana" runat="server" CssClass="inputbox" 
                                Width="300px" ></asp:TextBox>                          
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 140px">
                        </td>
                        <td style="width: 400px">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn" 
                                onclick="btnVoltar_Click" />                             
                             &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn" 
                                onclick="btnSalvar_Click" ValidationGroup="salvar" />  
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnParticipantes" runat="server" Text="Participantes" 
                                CssClass="btn" onclick="btnParticipantes_Click" /> 
                        </td>
                    </tr>
                </table>                                                
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    CssClass="validacao" ValidationGroup="salvar" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfIdPessoa" runat="server" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true">
            </asp:ScriptManager>
        </div>
        <div class="status">
        </div>
        
    </div>
</asp:Content>
