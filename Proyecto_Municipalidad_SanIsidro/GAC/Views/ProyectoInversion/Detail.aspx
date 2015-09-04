<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dominio.Core.Entities.ProyectoInversion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Proyectos de Inversión
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .field-validation-error
    {
        color: #ff0000;
    }
    .validation-summary-errors
    {
    }
</style>
    <div id="loading">
        <div id="loadingcontent">
            <p id="loadingspinner">
                Procesando...
            </p>
        </div>
    </div>

    <section class="content-header" style="padding-bottom:5px">
        <h1>Detalle de Proyecto de Inversión</h1>
        <div>&nbsp;</div>
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/proyectoinversion/index'">
                    <span class="fa fa-file" aria-hidden="true"></span> Crear
                </button>
                <button id="btnIconModificar" type="button" class="btn btn-default">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Modificar
                </button>
            </div>
        </div>
    </section>
        
    <section class="content">
        <div id="divPanelModificar" class="panel panel-primary" >
            <div class="panel-heading">
              <h3 class="panel-title">Proyecto de Inversión</h3>
            </div>
            
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Código proyecto</label>
                        <div class="col-sm-9">
                            <strong><%: Html.DisplayFor(m => m.IdProyecto) %></strong>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Código SNIP</label>
                        <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.CodSNIP) %>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Nombre:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.Nombre) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Descripcion:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.Descripcion) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Ubicación:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.TipoVia) %> <%: Html.DisplayFor(m => m.NomVia) %> <%: Html.DisplayFor(m => m.Ubicacion) %>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"># de beneficiarios:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.Beneficiarios) %>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Valor referencial (S/.):</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.ValorReferencial) %>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Estado:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.NomEstado) %>
                        <p></p>
                    </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                            <%if (ViewBag.MsgError!=null)
                            { %>
                                <div class="alert alert-error" style="text-align:left">
                                    <button type="button" class="close" data-dismiss="alert">×</button>
                                    <%:ViewBag.MsgError %>
                                </div>
                            <%}%>
                        </div>
                        <div class="col-sm-12 text-center">
                            <button id="btnRegresar" class="btn btn-primary" onclick="window.history.back();">Regresar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/Scripts/jquery-ui-1.8.20.js") %>
    <script>
        $(document).ready(function () {
        });

    </script>
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
