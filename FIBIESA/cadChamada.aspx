﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="cadChamada.aspx.cs" Inherits="Admin.cadChamada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div id="content">
        <div class="container half left">
            <div class="conthead">
                <h2>Chamada</h2>
            </div>
            <div class="contentbox">
                <table>
                    <tr>
                        <td style="width: 140px">Turma do Aluno:</td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="_tumaAluno" runat="server">
                                <asp:ListItem>Ativo</asp:ListItem>
                                <asp:ListItem>Desativado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Presença:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="_presenca" runat="server" CssClass="inputbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px">Data:</td>
                        <td style="width: 400px">
                            <asp:TextBox ID="_data" runat="server" CssClass="inputbox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 140px">
                        </td>
                        <td style="width: 400px">
                            <input type="submit" value="Enviar" class="btn" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="status">
        </div>
    </div>    
</asp:Content>
