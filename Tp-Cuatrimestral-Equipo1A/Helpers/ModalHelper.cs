using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Tp_Cuatrimestral_Equipo1A.Helpers
{
    public class ModalHelper
    {
        /// <summary>
        /// Cierra un modal de Bootstrap y limpia el fondo oscuro (backdrop)
        /// para evitar problemas con UpdatePanels.
        /// </summary>
        /// <param name="page">La instancia de la página actual (usar 'this')</param>
        /// <param name="modalId">El ID del modal en el HTML (ej: "modalNuevo")</param>
        public static void CerrarModal(Page page, string modalId)
        {
            StringBuilder script = new StringBuilder();

            // 1. Obtener la instancia del modal y ocultarlo
            script.Append("var modal = bootstrap.Modal.getInstance(document.getElementById('" + modalId + "'));");
            script.Append("if (modal) { modal.hide(); }");

            // 2. Eliminar backdrops huérfanos (La solución "Anti-Fantasma")
            script.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
            script.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");

            // 3. Restaurar el scroll del body
            script.Append("document.body.classList.remove('modal-open');");
            script.Append("document.body.style = '';");

            // 4. Registrar el script en la página
            ScriptManager.RegisterStartupScript(page, page.GetType(), "CerrarModal_" + modalId, script.ToString(), true);
        }

        /// <summary>
        /// Abre un modal de Bootstrap.
        /// </summary>
        public static void MostrarModal(Page page, string modalId)
        {
            string script = "$('#" + modalId + "').modal('show');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MostrarModal_" + modalId, script, true);
        }

        /// <summary>
        /// Limpia los backdrops huérfanos y luego muestra el modal especificado.
        /// Útil cuando ocurre un error y necesitas reabrir el modal.
        /// </summary>
        /// <param name="page">La instancia de la página actual</param>
        /// <param name="modalId">El ID del modal a mostrar</param>
        public static void LimpiarBackdropYMostrarModal(Page page, string modalId)
        {
            StringBuilder script = new StringBuilder();

            // 1. Limpiar backdrops huérfanos
            script.Append("var backdrops = document.getElementsByClassName('modal-backdrop');");
            script.Append("while(backdrops[0]) { backdrops[0].parentNode.removeChild(backdrops[0]); }");

            // 2. Restaurar el body
            script.Append("document.body.classList.remove('modal-open');");
            script.Append("document.body.style = '';");

            // 3. Mostrar el modal
            script.Append("$('#" + modalId + "').modal('show');");

            ScriptManager.RegisterStartupScript(page, page.GetType(), "LimpiarYMostrar_" + modalId, script.ToString(), true);
        }
    }
}