<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorCategorias.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager runat="server" />

    <h2>Administrador Marcas</h2>



    <asp:UpdatePanel ID="upCategorias" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <div class="mb-3 text-end">
                <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría"
                    CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
            </div>

            <div>
                <asp:GridView runat="server" ID="dgvCategorias" AutoGenerateColumns="false" CssClass="table table-dark">
                    <Columns>

                        <asp:BoundField DataField="Id" HeaderText="ID" />

                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Categoria" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>

                                <asp:Button Text="Editar" runat="server" CssClass="btn btn-warning btn-sm"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnEditar_Click" />


                                <asp:Button Text="Eliminar" runat="server" CssClass="btn btn-danger btn-sm ms-2"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnEliminar_Click"
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>

            <div class="modal fade" id="modalEdicion" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary text-white">
                            <h5 class="modal-title">Editar Categoría</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">

                            <div class="mb-3">
    <label class="form-label">Nombre de la Categoría</label>
    <asp:TextBox ID="txtNombreEditar" runat="server" CssClass="form-control" />

    <!-- Label para mostrar errores -->
    <asp:Label ID="lblErrorEditar" runat="server" CssClass="text-danger" />
</div>


                            <!-- Campo oculto para guardar el ID que estamos editando -->
                            <asp:HiddenField ID="hfIdCategoria" runat="server" />

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <!-- Botón Guardar: Dispara evento de servidor -->
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
            </div>


            <!-- Modal de Confirmación de Eliminación -->
            <div class="modal fade" id="modalConfirmaEliminar" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title">Confirmar Eliminación</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
    <asp:Label ID="lblMensajeEliminar" runat="server" Text="¿Estás seguro de eliminar esta categoría?" />
    <asp:HiddenField ID="hfIdEliminar" runat="server" />

    <!-- Label para mostrar errores -->
    <asp:Label ID="lblErrorEliminar" runat="server" CssClass="text-danger" />
</div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                            <asp:Button ID="btnConfirmaEliminar" runat="server" Text="Eliminar Definitivamente"
                                CssClass="btn btn-danger" OnClick="btnConfirmaEliminar_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modalNuevo" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-success text-white">
                            <h5 class="modal-title">Nueva Categoría</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">

                            <div class="mb-3">
    <label class="form-label">Nombre</label>
    <asp:TextBox ID="txtNombreNuevo" runat="server" CssClass="form-control" />

    <asp:RequiredFieldValidator ErrorMessage="El nombre es requerido"
        ControlToValidate="txtNombreNuevo" runat="server"
        Display="Dynamic" ForeColor="Red" ValidationGroup="Nuevo" />

    <!-- Label para mostrar errores -->
    <asp:Label ID="lblErrorNuevo" runat="server" CssClass="text-danger" />
</div>


                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                            <asp:Button ID="btnGuardarNuevo" runat="server" Text="Agregar"
                                CssClass="btn btn-success" OnClick="btnGuardarNuevo_Click"
                                ValidationGroup="Nuevo" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>
