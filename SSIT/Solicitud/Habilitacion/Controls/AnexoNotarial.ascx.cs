using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class AnexoNotarial : System.Web.UI.UserControl
    {
        public void CargarDatos(IEnumerable<EncomiendaDTO> lstEnc, int id_solicitud)
        {
            if (lstEnc != null && lstEnc.Count() > 0)
            {
                var elements = lstEnc.SelectMany(p => p.ActasNotariales);

                if (elements != null)
                    foreach (var elem in elements)
                        elem.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(elem.id_file));

                gridAnexo_db.DataSource = elements;
                gridAnexo_db.DataBind();
            }
        }
    }
}