using AnexoProfesionales.App_Components;
using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using StaticClass;
using System.Linq;
using System;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales
{
    public partial class CargarPlano : System.Web.UI.Page
    {
        EncomiendaBL encomiendaBL = new EncomiendaBL();
        EncomiendaDTO enc = null;

        private int id_encomienda 
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_encomienda.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_encomienda.Value = value.ToString();
            }

}

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                //ScriptManager.RegisterStartupScript(updPnlCargarPlano, updPnlCargarPlano.GetType(), "init_fileUpload", "init_fileUpload();", true);
            }


            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(updPnlCargarPlano, updPnlCargarPlano.GetType(), "init_fileUpload", "init_fileUpload();", true);
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarEncomienda();
                CargarTiposDePlanos();
                CargarPlanos(id_encomienda);
                Titulo.CargarDatos(id_encomienda, "Carga de Planos");
            }

            #region ASOSA MENSAJE PLANO CONTRA INCENDIO
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                var condicionIncendioOk = true;
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
                enc = encomiendaBL.Single(id_encomienda);
                var superficie = enc.EncomiendaDatosLocalDTO.Select(x => x.superficie_cubierta_dl + x.superficie_descubierta_dl).FirstOrDefault();
                condicionIncendioOk = !enc.EncomiendaRubrosCNDTO.Where(x => x.RubrosDTO.CondicionesIncendio.superficie < superficie).Any();
                if (!condicionIncendioOk)
                {
                    lblMsgPlanoContraIncendios.Text = "El Tramite " + id_encomienda.ToString() + " requiere Plano Contra Incendios, el mismo puede ser Inicial o Final segun normativa vigente.";
                    pnlMsgPlanoContraIncendios.Visible = true;
                }
            }
            #endregion
        }

        private void ComprobarEncomienda()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
                
                enc = encomiendaBL.Single(id_encomienda);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
                    }
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));
        }

        private void CargarTiposDePlanos()
        {
            TiposDePlanosBL blTipos = new TiposDePlanosBL();

            var lstTiposDePlanos = blTipos.GetAll().OrderByDescending(p => p.nombre).ToList();

            // Filtra el plano de habilitación anterior cuando ON es Redistribución de Uso.
            if (enc.IdTipoTramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                lstTiposDePlanos.Remove(lstTiposDePlanos.FirstOrDefault(x => x.id_tipo_plano == (int)Constantes.TiposDePlanos.HabilitacionAnterior));


            TipoDropDown.DataValueField = "id_tipo_plano";
            TipoDropDown.DataTextField = "nombre";
            TipoDropDown.DataSource = lstTiposDePlanos;
            TipoDropDown.DataBind();
            TipoDropDown.Focus();
            cargarDatos(Convert.ToInt32(TipoDropDown.SelectedValue));
        }

        private void CargarPlanos(int id_encomienda)
        {
            
            EncomiendaPlanosBL blPlanos = new EncomiendaPlanosBL();
            var elements = blPlanos.GetByFKIdEncomienda(id_encomienda);
            foreach (var elem in elements)
                elem.url = string.Format("~/" + RouteConfig.DESCARGA_PLANO + "{0}", Functions.ConvertToBase64String(elem.id_encomienda_plano));

            grdPlanos.DataSource = elements;
            grdPlanos.DataBind();

            enc = encomiendaBL.Single(id_encomienda);

            foreach (GridViewRow row in grdPlanos.Rows)
            {
                //EncomiendaPlanosDTO item = (EncomiendaPlanosDTO)row.DataItem;
                int id_encomienda_plano = Convert.ToInt32(grdPlanos.DataKeys[row.RowIndex].Values["id_encomienda_plano"]);
                var item = elements.FirstOrDefault(x => x.id_encomienda_plano == id_encomienda_plano);
                if(item != null)
                {
                    if( item.CreateDate < enc.CreateDate) 
                    {
                        LinkButton lnkEliminar = (LinkButton)row.FindControl("lnkEliminar");
                        lnkEliminar.Visible = false;
                    }
                }
            }

        }



        protected void lnkEliminar_Command(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkEliminar = (LinkButton)sender;
                int id_encomienda_plano = Convert.ToInt32(lnkEliminar.CommandArgument);
                EncomiendaPlanosBL blPlanos = new EncomiendaPlanosBL();
                EncomiendaPlanosDTO dto = blPlanos.Single(id_encomienda_plano);
                
                blPlanos.Delete(dto);
                CargarPlanos(id_encomienda);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                //lblError.Text = ex.Message;
                
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updPnlGrillaPlanos, updPnlGrillaPlanos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }

        }

        protected void btnCargarPlano_Click(object sender, EventArgs e)
        {
            try
            {
                EncomiendaPlanosBL blPlanos = new EncomiendaPlanosBL();
                string random = hid_filename_plano_random.Value;
                int id_encomienda = Convert.ToInt32(hid_id_encomienda.Value);
                int idTipoDePlano = Convert.ToInt32(TipoDropDown.SelectedValue);
                string nombre = hid_filename_plano.Value;
                string savedFileName = Constantes.PathTemporal + random + hid_filename_plano.Value;
                if (blPlanos.existe(idTipoDePlano, nombre, id_encomienda))
                    throw new Exception(Errors.ENCOMIENDA_PLANOS_EXISTENTE);
                if (!File.Exists(savedFileName))
                    throw new Exception(Errors.FILE_NO_TRANSFERIDO);

                //Chequeo la version de los planos .dwf, tiene que ser version 4.5 y 6.0
                if (Path.GetExtension(nombre.ToLower()).Contains(Constantes.EXTENSION_DWF))
                {
                    StreamReader file = new StreamReader(savedFileName);
                    string line = file.ReadLine();
                    int pos = line.IndexOf("(DWF V");
                    file.Close();
                    if (pos==-1)
                        throw new Exception(Errors.FILE_DWF_SIN_VERSION);
                }


                byte[] documento = File.ReadAllBytes(savedFileName);

                File.Delete(savedFileName);

                //Elimina los planos con mas de 2 días para mantener el directorio limpio.
                string[] lstArchs = Directory.GetFiles(Constantes.PathTemporal);
                foreach (string arch in lstArchs)
                {
                    DateTime fechaCreacion = File.GetCreationTime(arch);
                    if (fechaCreacion < DateTime.Now.AddDays(-2))
                        File.Delete(arch);
                }

                int id_file=0;
                ExternalServiceFiles externalServiceFiles = new ExternalServiceFiles();
                id_file = externalServiceFiles.addFile(nombre, documento);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                EncomiendaPlanosDTO dto = new EncomiendaPlanosDTO();
                dto.id_encomienda = id_encomienda;
                dto.id_file = id_file;
                dto.id_tipo_plano = idTipoDePlano;
                dto.detalle = txtDetalle.Text.Trim();
                dto.nombre_archivo = nombre;
                dto.CreateUser = userid.ToString();
                dto.CreateDate = DateTime.Now;                
                blPlanos.Insert(dto);                             
                CargarPlanos(id_encomienda);
                txtDetalle.Text = "";

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                //lblError.Text = ex.Message;

                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updPnlGrillaPlanos, updPnlGrillaPlanos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }


        }
        protected void TipoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatos(Convert.ToInt32(TipoDropDown.SelectedValue));
        }

        private void cargarDatos(int id_tipoDePlano)
        {
            TiposDePlanosBL bltipos = new TiposDePlanosBL();
            TiposDePlanosDTO dto = bltipos.Single(id_tipoDePlano);
            if (dto.requiere_detalle)
            {
                txtDetalle.Visible = true;
                lblDetalle.Visible = true;
            }
            else
            {
                lblDetalle.Visible = false;
                txtDetalle.Visible = false;
            }
            hid_requierre_detalle.Value = dto.requiere_detalle.ToString();
            hid_extension.Value = dto.extension.ToString();
            hid_tamanio.Value = dto.tamanio_max_mb.ToString();
            hid_tamanio_max.Value = Convert.ToString(dto.tamanio_max_mb * 1024 * 1024);
        }
    }
}