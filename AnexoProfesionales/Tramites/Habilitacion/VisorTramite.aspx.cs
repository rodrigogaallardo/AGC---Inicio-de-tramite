using AnexoProfesionales.App_Components;
using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace AnexoProfesionales
{
    public partial class VisorTramite : BasePage
    {

        EncomiendaBL encBL = new EncomiendaBL();

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

        private int id_estado
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_estado.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_estado.Value = value.ToString();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(pnlDatosDocumento, pnlDatosDocumento.GetType(), "init_Js_updpnlAgregarDocumentos", "init_Js_updpnlAgregarDocumentos();", true);
            }


            if (!IsPostBack)
            {
                btnBandeja.PostBackUrl = "~/" + RouteConfig.BANDEJA_DE_ENTRADA;
                ComprobarSolicitud();
            }
        }

        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            try
            {
                EncomiendaDTO enc = encBL.Single(id_encomienda);
                ActualizaEstadoCompleto(enc);
                CargarDatosTramite(enc);
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                divbtnConfirmarTramite.Visible = false; //ante cualquier error no permitir confirmar
                divbtnImprimirSolicitud.Visible = false;

                lblError.Text = Functions.GetErrorMessage(ex);
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }

        }

        private void ActualizaEstadoCompleto(EncomiendaDTO enc)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            if (enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta)
            {
                var mensaje = new List<string>();

                bool ubicacionesOk = enc.EncomiendaUbicacionesDTO.Any();
                bool datosLocalOk = enc.EncomiendaDatosLocalDTO.Any();
                bool rubrosOk = enc.EncomiendaRubrosDTO.Any() || enc.EncomiendaRubrosCNDTO.Any();
                
                bool conformacionLocalOk = true;
                bool bañoPcDOk = true;

                var encConfLocalDTO = enc.EncomiendaConformacionLocalDTO;
                var ubic = enc.EncomiendaUbicacionesDTO;
                var encDatosLocal = enc.EncomiendaDatosLocalDTO.FirstOrDefault();
                var encRubrosCN = enc.EncomiendaRubrosCNDTO;


                if (enc.IdEstado != 0)
                { 
                decimal SuperficieTotal = 0;
                if (encDatosLocal.ampliacion_superficie.HasValue && encDatosLocal.ampliacion_superficie.Value)
                    SuperficieTotal = encDatosLocal.superficie_cubierta_amp.Value + encDatosLocal.superficie_descubierta_amp.Value;
                else
                    SuperficieTotal = encDatosLocal.superficie_cubierta_dl.Value + encDatosLocal.superficie_descubierta_dl.Value;

                if (enc.IdSubTipoExpediente == (int)SubtipoDeExpediente.SinPlanos)
                {
                    if (!encConfLocalDTO.Any())
                    {
                        conformacionLocalOk = false;
                    }

                    if (SuperficieTotal > 60 && encDatosLocal.cumple_ley_962 == true && encDatosLocal.sanitarios_ubicacion_dl == 1
                        && !encConfLocalDTO.Where(x => x.id_destino == (int)TipoDestino.BañoPcD).Any())
                    {
                        bañoPcDOk = false;
                    }
                    else if (SuperficieTotal <= 60 && encDatosLocal.cumple_ley_962 == true && encDatosLocal.sanitarios_ubicacion_dl == 1
                        && !encConfLocalDTO.Where(x => x.id_destino == (int)TipoDestino.BañoPcD).Any())
                    {
                        //Verificar que Todos los rubros esten exceptuados
                        foreach (var item in encRubrosCN)
                        {
                            RubrosCNBL rubrosCNBL = new RubrosCNBL();
                            var rubroCN = rubrosCNBL.Get(item.CodigoRubro).FirstOrDefault();
                            if (!rubroCN.SinBanioPCD)
                            {
                                bañoPcDOk = false;
                            }
                        }
                    }
                }
                }

                bool titularesOk = enc.EncomiendaTitularesPersonasFisicasDTO.Any() || enc.EncomiendaTitularesPersonasJuridicasDTO.Any();

                bool planosOk = true;
                if (enc.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.ConPlanos)
                {
                    planosOk = enc.EncomiendaPlanosDTO.Any();
                }

                var condicionIncendioOk = true;
                if (enc.IdTipoTramite != (int)TipoTramite.TRANSFERENCIA && enc.EncomiendaRubrosCNDTO.Any() 
                    && enc.EncomiendaRubrosCNDTO.Where(x => x.RubrosDTO.CondicionesIncendio.idCondicionIncendio > 1).Any() 
                    && !enc.EncomiendaPlanosDTO.Where(x => x.id_tipo_plano == (int)TiposDePlanos.Contra_Incendio).Any())
                {
                    var superficie = enc.EncomiendaDatosLocalDTO.Select(x => x.superficie_cubierta_dl + x.superficie_descubierta_dl).FirstOrDefault();
                    condicionIncendioOk = !enc.EncomiendaRubrosCNDTO.Where(x => x.RubrosDTO.CondicionesIncendio.superficie < superficie).Any();
                }

                if (!ubicacionesOk)
                {
                    mensaje.Add("Ubicaciones");
                }

                if (!datosLocalOk)
                {
                    mensaje.Add("Datos del Local");
                }

                if (!rubrosOk)
                {
                    mensaje.Add("Rubros");
                }

                if (!conformacionLocalOk)
                {
                    mensaje.Add("Conformación del Local");
                }

                if (!bañoPcDOk)
                {
                    mensaje.Add("Baño PcD (Conformación del Local)");
                }

                if (!titularesOk)
                {
                    mensaje.Add("Titulares (avisar al contribuyente)");
                }

                if (!planosOk)
                {
                    mensaje.Add("Planos");
                }

                if (!condicionIncendioOk)
                {
                    #region ASOSA MENSAJE PLANO CONTRA INCENDIO
                    ScriptManager sm = ScriptManager.GetCurrent(this);
                    string cadena = "El Tramite "+ id_encomienda.ToString() + " requiere Plano Contra Incendios, el mismo puede ser Inicial o Final segun normativa vigente.";
                    string script = string.Format("alert('{0}');", cadena);
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alertScript", script, true);

                    #endregion
                    mensaje.Add("Plano contra incendios");
                }

                if (ubicacionesOk && datosLocalOk && rubrosOk && conformacionLocalOk && bañoPcDOk && titularesOk && planosOk && condicionIncendioOk)
                {
                    enc.IdEstado = (int)Constantes.Encomienda_Estados.Completa;
                    enc.LastUpdateDate = DateTime.Now;
                    enc.LastUpdateUser = userid;
                    encBL.Update(enc);
                }
                else
                {
                    MostrarMensajeAlertaFaltantes(mensaje.ToArray());
                }
            }
        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());

                var enc = encBL.Single(id_encomienda);
                this.id_estado = enc.IdEstado;
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        private void CargarDatosTramite(EncomiendaDTO enc)
        {
            if (enc.IdTipoTramite == (int)Constantes.TipoDeTramite.Transferencia)
            {
                enc.TipoTramiteDescripcion = "Transmisión";
                enc.TipoExpedienteDescripcion = "";
            }
            EncomiendaDatosLocalDTO enDl = enc.EncomiendaDatosLocalDTO.FirstOrDefault();
            //Carga estado solicitud
            lblNroEncomienda.Text = id_encomienda.ToString();

            SSITSolicitudesBL sol = new SSITSolicitudesBL();
            var Solicitud = sol.Single(enc.IdSolicitud);

            //Valido si es una ECI...
            if (Solicitud != null && Solicitud.EsECI)
            {
                switch (Solicitud.IdTipoTramite)
                {
                    case (int)TipoTramite.HabilitacionECIAdecuacion:
                        lblTipoTramite.Text = $"{TipoTramiteDescripcion.AdecuacionECI} - {enc.TipoExpedienteDescripcion}";
                        break;
                    case (int)TipoTramite.HabilitacionECIHabilitacion:
                        lblTipoTramite.Text = $"{TipoTramiteDescripcion.HabilitacionECI} - {enc.TipoExpedienteDescripcion}";
                        break;
                }
            }
            else
            {
                lblTipoTramite.Text = $"{enc.TipoTramiteDescripcion} {enc.TipoExpedienteDescripcion}";
            }

            EngineBL engBL = new EngineBL();

            string descripcionCircuito = engBL.GetDescripcionCircuito(enc.IdSolicitud);//EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()

            if (descripcionCircuito != null)
                lblTipoTramite.Text += " - " + descripcionCircuito;

            lblEstadoSolicitud.Text = enc.Estado.NomEstado;
            lblFechaEncomienda.Text = enc.FechaEncomienda.ToShortDateString();

            if (enc.EncomiendaRubrosDTO.Any())
            {
                ImpactoAmbientalDTO imp = enc.EncomiendaRubrosDTO.Where(p => p.ImpactoAmbientalDTO.id_ImpactoAmbiental
                        == enc.EncomiendaRubrosDTO.Max(a => a.IdImpactoAmbiental)).Select(p => p.ImpactoAmbientalDTO).FirstOrDefault();
                if (imp != null)
                    lblTipoImpactoAmbiental.Text = imp.nom_ImpactoAmbiental + "(" + imp.cod_ImpactoAmbiental + ")";

            }

            #region Ubicacion
            visUbicaciones.Editable = false;
            visUbicaciones.Visor = true;
            visUbicaciones.CargarDatos(enc);
            btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_UBICACION + "{0}", id_encomienda);
            #endregion
            #region DatosLocal
            visDatoslocal.CargarDatos(enc);
            btnModificarDatosLocal.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_DATOSLOCAL + "{0}", id_encomienda);
            #endregion
            #region ConformacionLocal
            hid_mostrar_conformacionLocal.Value = "false";
            if (enc.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
            {
                hid_mostrar_conformacionLocal.Value = "true";
                visConformacionLocal.CargarDatos(enc);
                btnModificarConformacionLocal.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_CONFORMACIONLOCAL + "{0}", id_encomienda);
            }
            #endregion
            #region CertificadoSobrecarga
            //hid_mostrar_certificadoSobrecarga.Value = "false";
            //if (enDl!=null && enDl.sobrecarga_corresponde_dl.Value)
            //{
            //    hid_mostrar_certificadoSobrecarga.Value = "true";
            //    visCertificadoSobrecarga.CargarDatos(enc);
            //    btnModificarCertificadoSobrecarga.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_CERTIFICADOSOBRECARGA + "{0}", id_encomienda);
            //}
            #endregion
            #region Planos
            visCargaPlanos.CargarDatos(enc);
            btnModificarCargarPlanos.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_CARGAPLANO + "{0}", id_encomienda);
            #endregion
            #region Rubros
            
            ParametrosDTO parametrosDTO = new ParametrosBL().GetParametros(ConfigurationManager.AppSettings["CodParam"]);

            if ((enc.EncomiendaSSITSolicitudesDTO?.FirstOrDefault()?.id_solicitud ?? 0) < parametrosDTO.ValornumParam && enc.EncomiendaTransfSolicitudesDTO.FirstOrDefault() == null)
            {
                visRubrosCN.Visible = false;
                visRubros.CargarDatos(enc);
                btnModificarRubros.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_RUBROS + "{0}", id_encomienda);
            }
            else
            {
                visRubros.Visible = false;
                visRubrosCN.CargarDatos(enc);
                btnModificarRubros.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_RUBROSCN + "{0}", id_encomienda);
            }

            #endregion
            #region Titulares
            visTitulares.CargarDatos(id_encomienda);
            btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA_TITULAR + "{0}", id_encomienda);
            #endregion
            #region Documentos
            CargarCombos();

            CargarDocumentos(enc);
            #endregion

            //Valido si es un ECI
            bool esEci = (enc != null && enc.EsECI) || (Solicitud != null && Solicitud.EsECI);
            if (esEci)
            {
                pnlInfoAdicional.Visible = true;
                if (enc.EsActBaile != null)
                {
                    rbActBaileSI.Checked = (bool)enc.EsActBaile;
                    rbActBaileNo.Checked = !(bool)enc.EsActBaile;
                }

                if (enc.EsLuminaria != null)
                {
                    rbLuminariaSi.Checked = (bool)enc.EsLuminaria;
                    rbLuminariaNo.Checked = !(bool)enc.EsLuminaria;
                }

            }
            else
            {
                pnlInfoAdicional.Visible = false;
            }
            #region Validaciones
            divbtnConfirmarTramite.Visible = false;
            divbtnAnularTramite.Visible = false;
            divbtnImprimirSolicitud.Visible = false;

            btnModificarDatosLocal.Visible = false;
            //btnModificarCertificadoSobrecarga.Visible = false;

            if (enc.EncomiendaTitularesPersonasJuridicasDTO.Count() == 0 && enc.EncomiendaTitularesPersonasFisicasDTO.Count() == 0)
                btnModificarTitulares.Visible = false;

            btnModificarCargarPlanos.Visible = false;
            btnModificarRubros.Visible = false;
            btnModificarUbicacion.Visible = false;
            btnModificarConformacionLocal.Visible = false;

            btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_ENCOMIENDA + "{0}", Funciones.ConvertToBase64String(id_encomienda));

            //No se puede imprimir enconmienda solo cuando está incompleta
            if (enc.IdEstado != (int)Constantes.Encomienda_Estados.Incompleta
                && enc.IdEstado != (int)Constantes.Encomienda_Estados.Completa
                && enc.IdEstado != (int)Constantes.Encomienda_Estados.Anulada)
            {
                ActualizaTipoSubtipoExpToSSIT(id_encomienda);
                RegenerarEncomienda(id_encomienda);
                divbtnImprimirSolicitud.Visible = true;
            }

            if (enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa ||
                enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta)
            {

                if (enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa)
                    divbtnConfirmarTramite.Visible = true;

                divbtnAnularTramite.Visible = true;

                btnModificarDatosLocal.Visible = true;
                btnModificarRubros.Visible = true;
                //btnModificarUbicacion.Visible = true;
                btnModificarCargarPlanos.Visible = true;

                if (hid_mostrar_conformacionLocal.Value == "true")
                    btnModificarConformacionLocal.Visible = true;

                //if (hid_mostrar_certificadoSobrecarga.Value == "true")
                //    btnModificarCertificadoSobrecarga.Visible = true;
            }

            //SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            //var sol = solBL.Single(enc.IdSolicitud);

            // Se optimiza la validación para modificar la ubicación
            btnModificarUbicacion.Visible = HabilitarModificarUbicacion(enc);

            //if (enc.EncomiendaTransfSolicitudesDTO.Select(x => x.TransferenciasSolicitudesDTO.idSolicitudRef).FirstOrDefault() > 0 ||
            //   (enc.EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.IdTipoTramite).FirstOrDefault() == (int)Constantes.TipoDeTramite.RedistribucionDeUso &&
            //    enc.EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.SSITSolicitudesOrigenDTO).FirstOrDefault() != null))
            //{
            //    btnModificarUbicacion.Visible = false;
            //    validarPlantas = false;
            //}

            #endregion
            List<int> lstEstadosNoPermitidos = new List<int>();
            lstEstadosNoPermitidos.Add((int)Constantes.Encomienda_Estados.Anulada);
            lstEstadosNoPermitidos.Add((int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo);
            lstEstadosNoPermitidos.Add((int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
            lstEstadosNoPermitidos.Add((int)Constantes.Encomienda_Estados.Ingresada_al_consejo);
            lstEstadosNoPermitidos.Add((int)Constantes.Encomienda_Estados.Confirmada);

            if (enc != null)
                if (lstEstadosNoPermitidos.Contains(enc.IdEstado))
                {
                    pnlAgregarDocumentos.Style["display"] = "none";
                    btnMostrarAgregadoDocumentos.Style["display"] = "none";
                }

            //0140723: JADHE 54604 - AT - REQ AMP RDU Carga Heredada - Poder Modificar Datos en AT

            //#region REDISTRIBUCION_USO
            //// Si es una redistribución de uso y proviene de una solicitud anterior
            //if (enc.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            //{
            //    pnlImpactoAmbiental.Visible = false;

            //    if (encBL.PoseeHabilitacionConAnexoTecnicoAnterior(enc.IdEncomienda))
            //    {
            //        btnModificarDatosLocal.Visible = false;
            //    }
            //} 
            //#endregion

            //si es transmision por tranferencia desde una transferencia(no tiene encomienda) tiene q poder modificar todo
            if (enc.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA &&
                enc.EncomiendaTransfSolicitudesDTO.FirstOrDefault() != null &&
                enc.EncomiendaTransfSolicitudesDTO.FirstOrDefault().TransferenciasSolicitudesDTO.idSolicitudRef > 0)
                btnModificarDatosLocal.Visible = true;

            updCargarDatos.Update();
            updEstadoSolicitud.Update();

        }

        private void ActualizaTipoSubtipoExpToSSIT(int id_encomienda)
        {
            EncomiendaBL encBL = new EncomiendaBL();
            encBL.ActualizaTipoSubtipoExpToSSIT(id_encomienda);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CargarDocumentos()
        {
            EncomiendaDocumentosAdjuntosBL blDoc = new EncomiendaDocumentosAdjuntosBL();
            var elements = blDoc.GetByFKIdEncomiendaTipoSis(id_encomienda, (int)Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_ENCOMIENDA);

            foreach (var doc in elements)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            gridAgregados_db.DataSource = elements.ToList();
            gridAgregados_db.DataBind();
            upPnlDocumentos.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encomienda"></param>
        public void CargarDocumentos(EncomiendaDTO encomienda)
        {
            var elements = encomienda.EncomiendaDocumentosAdjuntosDTO.Where(p => p.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_ENCOMIENDA ||
                                                                                 p.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION);

            foreach (var doc in elements)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            gridAgregados_db.DataSource = elements.ToList();
            gridAgregados_db.DataBind();
            upPnlDocumentos.Update();
        }

        #region carga documentos
        private void CargarCombos()
        {
            TiposDeDocumentosRequeridosBL blTipoDoc = new TiposDeDocumentosRequeridosBL();
            var lstTiposDocumentos = blTipoDoc.GetVisibleAnexoTecnico();
            ddlTiposDeDocumentosEscaneados.DataSource = lstTiposDocumentos;
            ddlTiposDeDocumentosEscaneados.DataTextField = "Descripcion_compuesta";
            ddlTiposDeDocumentosEscaneados.DataValueField = "id_tdocreq";
            ddlTiposDeDocumentosEscaneados.DataBind();
            ddlTiposDeDocumentosEscaneados.Items.Insert(0, "");
            updpnlAgregarDocumentos.Update();
        }

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            pnlErrorFoto.Style["display"] = "none";
            string savedFileName = "C:\\Temporal\\" + hid_filename_documento.Value;

            //Elimina las fotos de firmas con mas de 1 dÃ­a para mantener el directorio limpio.
            string[] lstArchs = Directory.GetFiles("C:\\Temporal");
            foreach (string arch in lstArchs)
            {
                DateTime fechaCreacion = File.GetCreationTime(arch);
                if (fechaCreacion < DateTime.Now.AddDays(-3))
                    File.Delete(arch);
            }
            lblError.Text = "";
            byte[] Documento = new byte[0];
            try
            {

                if (hid_filename_documento.Value.Length > 0)
                {
                    Documento = File.ReadAllBytes(savedFileName);
                    if (Documento.Length > 2097152) // 2mb
                        throw new Exception("El tamaÃ±o mÃ¡ximo permitido para los documentos es de 2 MB");

                    //using (var pdf = new PdfReader(savedFileName))
                    //{
                    //    if (!pdf.IsOpenedWithFullPermissions)
                    //        throw new Exception("El documento que intenta subir tiene un nivel de seguridad no aceptado. Por favor genere un pdf con el nivel de seguridad estandar, sin contraseÃ±as ni permisos especiales.");
                    //}

                    File.Delete(savedFileName);
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmError(); ", true);
            }

            if (lblError.Text == "")
            {
                int id_tdocreq = int.Parse(ddlTiposDeDocumentosEscaneados.SelectedValue);
                try
                {
                    //Grabar el documento en la base
                    ExternalServiceFiles service = new ExternalServiceFiles();
                    string nombre = hid_filename_documento.Value;
                    if (nombre.Length > 50)
                    {
                        var cantSob = nombre.Length - 50;
                        var ext = nombre.Substring(nombre.LastIndexOf("."));
                        nombre = nombre.Substring(0, nombre.Length - ext.Length - cantSob);
                        nombre = nombre + ext;
                    }
                    int id_file = service.addFile(nombre, Documento);

                    EncomiendaDocumentosAdjuntosBL blDoc = new EncomiendaDocumentosAdjuntosBL();
                    var doc = new EncomiendaDocumentosAdjuntosDTO();
                    doc.CreateDate = DateTime.Now;
                    doc.CreateUser = userid;
                    doc.id_file = id_file;
                    doc.generadoxSistema = false;
                    doc.id_encomienda = id_encomienda;
                    doc.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_ENCOMIENDA;
                    doc.id_tdocreq = id_tdocreq;
                    doc.nombre_archivo = nombre;

                    blDoc.Insert(doc);
                    CargarDocumentos();
                }
                catch (Exception ex)
                {
                    LogError.Write(ex);
                    lblError.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmError(); ", true);
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_docadjunto;
                int.TryParse(hid_id_docadjunto.Value, out id_docadjunto);

                EncomiendaDocumentosAdjuntosBL blDoc = new EncomiendaDocumentosAdjuntosBL();
                var doc = blDoc.Single(id_docadjunto);
                blDoc.Delete(doc);
                CargarDocumentos();
                this.EjecutarScript(upPnlDocumentos, "hideConfirmarEliminar();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmErrorDocumentos(); ", true);
            }

        }

        protected void lnkEliminar_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            try
            {
                LinkButton lnkEliminar = (LinkButton)sender;
                int id_docadjunto = Convert.ToInt32(lnkEliminar.CommandArgument);
                EncomiendaDocumentosAdjuntosBL blDoc = new EncomiendaDocumentosAdjuntosBL();
                var doc = blDoc.Single(id_docadjunto);
                blDoc.Delete(doc);
                CargarDocumentos();

            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmErrorDocumentos(); ", true);
            }

        }
        #endregion

        private void RegenerarEncomienda(int id_encomienda)
        {
            //antes de mostrar link para imprimir pdf se verifica que se haya grabado bien en base
            if (!ValidarExisteEncomienda(id_encomienda))
            {
                GuardarEncomienda(id_encomienda);
            }

        }

        private void GuardarEncomienda(int id_encomienda)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            byte[] pdfEncomienda = new byte[0];

            encBL.RegenerarPDFEncomienda(id_encomienda, userid);
        }

        private bool ValidarExisteEncomienda(int id_encomienda)
        {
            int id_tipodocsis = 0;
            EncomiendaDocumentosAdjuntosBL encDocAdjBL = new EncomiendaDocumentosAdjuntosBL();
            TiposDeDocumentosSistemaBL tipDocSis = new TiposDeDocumentosSistemaBL();
            TiposDeDocumentosSistemaDTO tipDoc = tipDocSis.GetByCodigo(Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL.ToString());
            EncomiendaDocumentosAdjuntosDTO cert = new EncomiendaDocumentosAdjuntosDTO();

            if (tipDoc != null)
            {
                id_tipodocsis = tipDoc.id_tipdocsis;
                cert = encDocAdjBL.GetByFKIdEncomiendaTipoSis(id_encomienda, id_tipodocsis).FirstOrDefault();
            }

            return (cert != null);
        }

        public bool HabilitarModificarUbicacion(EncomiendaDTO enc)
        {
            var sol = enc.EncomiendaSSITSolicitudesDTO.FirstOrDefault()?.SSITSolicitudesDTO;
            var trf = enc.EncomiendaTransfSolicitudesDTO.FirstOrDefault()?.TransferenciasSolicitudesDTO;

            var heredada = trf?.idSolicitudRef > 0;    // || (sol?.IdTipoTramite == (int)TipoDeTramite.RedistribucionDeUso && sol?.SSITSolicitudesOrigenDTO != null) ahora se pueden modificar ubicacion de una ampliación

            return !heredada && (enc.IdEstado == (int) Encomienda_Estados.Completa || enc.IdEstado == (int) Encomienda_Estados.Incompleta);
        }

        public void ValidarPlantasSeleccionadas(EncomiendaDTO enc)
        {
            if (HabilitarModificarUbicacion(enc))
            {
                EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();
                var plantas = encomiendaPlantasBL.Get(enc.IdEncomienda).Where(x => x.Seleccionado);

                if (!plantas.Any())
                    throw new Exception(Errors.ENCOMIENDA_PLANTAS_SELECCIONADAS);
            }
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();

                var enc = encBL.Single(id_encomienda);

                var encConfLocalDTO = enc.EncomiendaConformacionLocalDTO;
                var encPlanosDTO = enc.EncomiendaPlanosDTO;
                var ubic = enc.EncomiendaUbicacionesDTO;
                var encDatosLocal = enc.EncomiendaDatosLocalDTO.FirstOrDefault();
                var encRubrosCN = enc.EncomiendaRubrosCNDTO;

                string superficieRubroMayorASuperficieAHabilitar = "";

                decimal suma_superficieHabilitar = 0;
                decimal SuperficieTotal = 0;


                if (encDatosLocal.ampliacion_superficie.HasValue && encDatosLocal.ampliacion_superficie.Value)
                    SuperficieTotal = encDatosLocal.superficie_cubierta_amp.Value + encDatosLocal.superficie_descubierta_amp.Value;
                else
                    SuperficieTotal = encDatosLocal.superficie_cubierta_dl.Value + encDatosLocal.superficie_descubierta_dl.Value;

                if (encDatosLocal.cantidad_operarios_dl <= 0)
                    throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_OPERARIOS);

                #region ValidacionPlantasHabilitar
                ValidarPlantasSeleccionadas(enc);
                #endregion

                #region ValidacionSegunRubros
                if (enc.EncomiendaRubrosCNDTO.Count > 0)
                {
                    suma_superficieHabilitar = enc.EncomiendaRubrosCNDTO.Sum(p => p.SuperficieHabilitar);
                    if (suma_superficieHabilitar < SuperficieTotal)
                        throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE);

                    var rubro = enc.EncomiendaRubrosCNDTO.Where(p => p.SuperficieHabilitar > SuperficieTotal).Select(p => p).FirstOrDefault();
                    if (rubro != null)
                    {
                        superficieRubroMayorASuperficieAHabilitar = string.Format("La superficie del rubro " + rubro.DescripcionRubro + ", " + rubro.SuperficieHabilitar + " m2 es mayor a la superficie a habilitar.");
                        throw new Exception(superficieRubroMayorASuperficieAHabilitar);
                    }
                }
                else if (enc.EncomiendaRubrosDTO.Count() > 0)
                {
                    suma_superficieHabilitar = enc.EncomiendaRubrosDTO.Sum(p => p.SuperficieHabilitar);
                    if (suma_superficieHabilitar < SuperficieTotal)
                        throw new Exception(Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE);

                    var rubro = enc.EncomiendaRubrosDTO.Where(p => p.SuperficieHabilitar > SuperficieTotal).Select(p => p).FirstOrDefault();
                    if (rubro != null)
                    {
                        superficieRubroMayorASuperficieAHabilitar = string.Format("La superficie del rubro " + rubro.DescripcionRubro + ", " + rubro.SuperficieHabilitar + " m2 es mayor a la superficie a habilitar.");
                        throw new Exception(superficieRubroMayorASuperficieAHabilitar);
                    }
                }
                else
                    throw new Exception("Debe indicar al menos un rubro.");
                #endregion

                if (enc.IdSubTipoExpediente == (int)SubtipoDeExpediente.SinPlanos)
                {
                    if (!encConfLocalDTO.Any())
                    {
                        throw new Exception("Debe cargar los datos de conformación del local.");
                    }

                    if (SuperficieTotal > 60 && encDatosLocal.cumple_ley_962 == true && encDatosLocal.sanitarios_ubicacion_dl == 1 
                        && !encConfLocalDTO.Where(x => x.id_destino == (int)TipoDestino.BañoPcD).Any())
                    {
                        throw new Exception("Debe cargar en los datos de conformación del local, el destino Baño PcD.");
                    }
                    else if (SuperficieTotal <= 60 && encDatosLocal.cumple_ley_962 == true && encDatosLocal.sanitarios_ubicacion_dl == 1
                        && !encConfLocalDTO.Where(x => x.id_destino == (int)TipoDestino.BañoPcD).Any())
                    {
                        //Verificar que Todos los rubros esten exceptuados
                        foreach (var item in encRubrosCN)
                        {
                            RubrosCNBL rubrosCNBL = new RubrosCNBL();
                            var rubroCN = rubrosCNBL.Get(item.CodigoRubro).FirstOrDefault();
                            if (!rubroCN.SinBanioPCD)
                            {
                                throw new Exception("Todos los tipos de rubros declarados deben estar exceptuados para el destino Baño PCD.");
                            }
                        }
                    }
                }

                if (enc.IdSubTipoExpediente != (int)Constantes.SubtipoDeExpediente.SinPlanos && encPlanosDTO.Count() == 0 &&
                    enc.EncomiendaTransfSolicitudesDTO.Count <= 0 &&
                    !enc.EsECI)
                    throw new Exception("Debe cargar los planos correspondientes.");

                if (ubic.Count() > 1 && !enc.Servidumbre_paso)
                    throw new Exception("La opción <b>Servidumbre de paso</b> debe estar tildada, sí desea continuar");

                if (enc.IdTipoTramite != (int)TipoTramite.TRANSFERENCIA && encRubrosCN.Any()
                    && !encPlanosDTO.Where(x => x.id_tipo_plano == (int)TiposDePlanos.Contra_Incendio).Any()
                    && encRubrosCN.Where(x => x.RubrosDTO.CondicionesIncendio.idCondicionIncendio > 1).Any()
                    && encRubrosCN.Where(x => x.RubrosDTO.CondicionesIncendio.superficie < (encDatosLocal.superficie_cubierta_dl + encDatosLocal.superficie_descubierta_dl)).Any())
                {
                    throw new Exception("Debe cargar los planos contra incendios correspondientes.");
                }

                //Valido si es uns ECI
                SSITSolicitudesBL sol = new SSITSolicitudesBL();
                var Solicitud = sol.Single(enc.IdSolicitud);
                bool esEci = (enc != null && enc.EsECI) || (Solicitud != null && Solicitud.EsECI);
                if (esEci)
                {
                    //Validoo que se hallan cargado los datos correspondientes
                    if (enc.EsActBaile == null || enc.EsActBaile == null)
                        throw new Exception("Usted posee el rubro <b>2.1.1 - Espacio Cultural Independiente</b>, debe contestar las preguntas que se encuentran en la edición de rubros.");
                }


                if (encBL.ConfirmarAnexoTecnico(id_encomienda, userid))
                {

                    enc = encBL.Single(id_encomienda);

                    if (enc.EncomiendaTransfSolicitudesDTO.Count > 0)
                    {
                        copiarUbicacionToTR(enc);
                    }
                    else
                    {
                        #region Copia de Ubicacion
                        copiarUbicacionToSSIT(enc);
                        #endregion
                        #region Actualiza tipo y subtipo de expediente
                        ActualizaTipoSubtipoExpSSIT(enc);
                        #endregion
                    }
                    CargarDatosTramite(encBL.Single(id_encomienda));
                    encBL.RegenerarPDFEncomienda(id_encomienda, userid);
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        private void copiarUbicacionToSSIT(EncomiendaDTO encDTO)
        {
            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
            SSITSolicitudesUbicacionesBL ssitUbicBL = new SSITSolicitudesUbicacionesBL();
            ssitUbicBL.copiarUbicacionToSSIT(encDTO, userId);
        }

        private void copiarUbicacionToTR(EncomiendaDTO encDTO)
        {
            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
            TransferenciaUbicacionesBL trUbicBL = new TransferenciaUbicacionesBL();
            trUbicBL.copiarUbicacionToTR(encDTO, userId);
        }

        private void ActualizaTipoSubtipoExpSSIT(EncomiendaDTO enc)
        {
            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            ssitBL.ActualizaTipoSubtipoExpSSIT(enc);
        }

        protected void btnAnular_Si_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                var enc = encBL.Single(id_encomienda);

                #region validacion transiciones estado
                //IF not Exists(
                //        SELECT 1 FROM Encomienda_TransicionEstados  est
                //        INNER JOIN aspnet_UsersInRoles usr_rol ON usr_rol.UserId = @userid
                //        INNER JOIN aspnet_Roles rol ON rol.RoleId = usr_rol.RoleId
                //        WHERE est.id_estado_actual = @enc_id_estado
                //        AND est.id_estado_siguiente = @id_estado
                //        AND est.rol = rol.RoleName
                //        )
                //BEGIN
                //    SET @msg = 'Cambio de estado invalido. Su perfil no permite realizar este cambio de estado.'
                //    RAISERROR(@msg, 16, 1)
                //    RETURN
                //END
                #endregion

                #region actualizo la encomienda
                enc.LastUpdateUser = userid;
                enc.LastUpdateDate = DateTime.Now;
                enc.IdEstado = (int)Constantes.Encomienda_Estados.Anulada;
                encBL.Update(enc);
                id_estado = enc.IdEstado;
                #endregion

                CargarDatosTramite(encBL.Single(id_encomienda));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);

            }

            ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion", "hidefrmConfirmarAnulacion();", true);

        }
        private void MostrarMensajeAlertaFaltantes(params string[] mensajes)
        {
            string alerta = "- Para completar el trámite es necesario ingresar la información correspondiente a:";

            for (int iii = 0; iii < mensajes.Length; iii++)
            {
                if (iii == 0)
                {
                    alerta = $"{alerta} {mensajes[iii]}";
                }

                else if (!string.IsNullOrEmpty(mensajes[iii]))
                {
                    alerta = $"{alerta}, {mensajes[iii]}";
                }
            }

            lblTextoTramiteIncompleto.Text = alerta;
            pnlTramiteIncompleto.Visible = (alerta.Length > 0);
        }

        protected void gridAgregados_db_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int tipoDoc;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("lnkEliminar");
                lnkEliminar.Visible = false;
                tipoDoc = 0;
                int.TryParse(e.Row.Cells[2].Text, out tipoDoc);

                if (id_estado != (int)Constantes.Encomienda_Estados.Anulada &&
                    id_estado != (int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo &&
                    id_estado != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo &&
                    id_estado != (int)Constantes.Encomienda_Estados.Ingresada_al_consejo &&
                    id_estado != (int)Constantes.Encomienda_Estados.Confirmada &&
                    tipoDoc != (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION)
                    lnkEliminar.Visible = true;
                e.Row.Cells[2].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[2].Visible = false;
        }
    }
}