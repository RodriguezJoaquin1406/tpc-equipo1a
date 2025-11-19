<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorCategorias.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Administrador Marcas</h2>


    <div>
        <asp:GridView runat="server" ID="dgvCategorias" AutoGenerateColumns="false" CssClass="table table-dark">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre Categoria" />
                <%--<asp:HyperLinkField Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AdministradorCategoria.aspx?Id={0}" />--%>
            </Columns>
        </asp:GridView>
    </div>

    <%--<div>
        <asp:GridView runat="server" ID="dgvUsuarios" AutoGenerateColumns="false" CssClass="table table-dark">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="Rol" HeaderText="Tipo Usuario" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AdministradorProducto.aspx?Id={0}" />
            </Columns>
        </asp:GridView>
    </div>--%>

</asp:Content>
