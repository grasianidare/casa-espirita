﻿<%@ Page Title="" Language="C#" MasterPageFile="~/home.Master" AutoEventWireup="true"
    CodeBehind="viewTipoObra.aspx.cs" Inherits="Admin.viewTipoObra" %>
    <%@ MasterType VirtualPath="~/home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div id="content">
        <div class="container">
            <div class="conthead">
                <h2>Cadastro de Categorias de Obras</h2>
            </div>
            <div class="contentbox">
                <asp:TextBox ID="txtBusca" runat="server" CssClass="inputbox" 
                    ToolTip="Pesquisar por"></asp:TextBox>
                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="dropdownlist">
                    <asp:ListItem Value="CODIGO">Código</asp:ListItem>
                    <asp:ListItem Value="DESCRICAO">Descrição</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="btn" 
                    OnClick="btnBusca_Click" />                 
                &nbsp;&nbsp;&nbsp
                <asp:Button ID="btnInserir" runat="server" Text="Inserir" CssClass="btn" 
                    onclick="btnInserir_Click" />

                <!-- grid modelo começa aqui -->
                <div class="contentbox">
                    <table width="100%">
                       <tr>
                            <td>                       
                               <asp:GridView ID="dtgTiposObras" runat="server" AutoGenerateColumns="False" 
                                   BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                   CellPadding="3" AllowPaging="True" DataKeyNames="ID" 
                                   onrowdeleting="dtgTiposObras_RowDeleting" 
                                   onselectedindexchanged="dtgTiposObras_SelectedIndexChanged" 
                                    AllowSorting="True" GridLines="None" 
                                    onpageindexchanging="dtgTiposObras_PageIndexChanging" 
                                    onrowdatabound="dtgTiposObras_RowDataBound" onsorting="dtgTiposObras_Sorting" 
                                    ShowHeaderWhenEmpty="True">
                                   <Columns>
                                       <asp:CommandField SelectText="Editar" ShowSelectButton="True">
                                            <HeaderStyle CssClass="grd_cmd_header" />
                                            <ItemStyle CssClass="grd_edit" />
                                       </asp:CommandField>
                                       <asp:CommandField DeleteText="Excluir" ShowDeleteButton="True">
                                            <HeaderStyle CssClass="grd_cmd_header" />
                                            <ItemStyle CssClass="grd_delete" />
                                       </asp:CommandField>
                                       <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                       <asp:BoundField DataField="CODIGO" HeaderText="Código" SortExpression="CODIGO" />
                                       <asp:BoundField DataField="DESCRICAO" HeaderText="Descrição" SortExpression="DESCRICAO" />
                                       <asp:BoundField DataField="QTDDIAS" HeaderText="Qtde. Dias" SortExpression="QTDDIAS"/>
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
                <!-- grid modelo finaliza aqui -->               
            </div>
        </div>
        <div class="status">
        </div>
    </div>   
</asp:Content>
