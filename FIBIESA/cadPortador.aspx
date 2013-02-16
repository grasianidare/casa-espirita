﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadPortador.aspx.cs" Inherits="Admin.cadPortador" %>
<%@ MasterType VirtualPath="~/home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div id="content">
        <div class="container half left">
            <div class="conthead">
                <h2>Cadastro de Portadores</h2>
            </div>
            <div class="contentbox">
                <table>
                    
                    <tr>
                        <td style="width: 140px">Código:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="inputbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCodigo" ErrorMessage="*Preenchimento Obrigatório" 
                                ForeColor="#CC0000" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Descrição:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="inputbox" 
                                MaxLength="70" Width="335px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDescricao" ErrorMessage="*Preenchimento Obrigatório" 
                                ForeColor="#CC0000" ValidationGroup="salvar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 140px">Banco:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtBanco" runat="server" CssClass="inputbox"></asp:TextBox>
                            <asp:Button ID="btnPesBanco" runat="server" Text="..."  CssClass="btn" onclick="btnPesBanco_Click" 
                                 />
                            <asp:Label ID="lblDesBanco" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Agência:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="txtAgencia" runat="server" CssClass="inputbox"></asp:TextBox>
                            <asp:Button ID="btnPesAgencia" runat="server" Text="..."  CssClass="btn" onclick="btnPesAgencia_Click" 
                                 />
                            <asp:Label ID="lblDesAgencia" runat="server"></asp:Label>
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
                            
                        </td>
                    </tr>
                </table>
               
            </div>
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfIdBanco" runat="server" />
            <asp:HiddenField ID="hfIdAgencia" runat="server" />
        </div>
        <div class="status">
        </div>
    </div>   
</asp:Content>
