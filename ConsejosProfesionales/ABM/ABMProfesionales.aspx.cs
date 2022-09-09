using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;

namespace ConsejosProfesionales.ABM
{
    public partial class ABMProfesionales : BasePage
    {
        public static bool tieneUserName = false;
        IList<RolesDTO> _rolesSeleccionados = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipUser Usuario = Membership.GetUser();
            if (Usuario == null)
                Response.Redirect("~/Usuarios/Login.aspx?returnurl=" + Request.Url);

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updpnlBuscar, updpnlBuscar.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);

            }
            if (!IsPostBack)
            {
                // Obtiene el id_ de profesional de los parámetros pasados a la página
                try
                {
                    id_profesional.Value = Request.QueryString["id"].ToString();
                    HabilitarBtnCargaUsuario();
                }
                catch
                { id_profesional.Value = "0"; }
                hid_id_profesional.Value = id_profesional.Value;
                try
                {
                    int idProfesional;
                    int.TryParse(id_profesional.Value, out idProfesional);
                    ProfesionalesBL profesionalBL = new ProfesionalesBL();
                    var drProfesional = profesionalBL.Single(idProfesional);
                    if (drProfesional.UserId != null)
                    {
                        tieneUserName = true;
                        hid_userid.Value = drProfesional.UserId.ToString();
                        txtusername_datos.Enabled = false;
                    }
                }
                catch
                { }
                CargarConsejos();
                CargarProvincias();
                CargarTiposDeDocumentos();

                if (int.Parse(id_profesional.Value) > 0)
                    CargarDatosProfesional(int.Parse(id_profesional.Value));

                HabiliarComboBajaLogica();
            }
        }

        private void HabilitarBtnCargaUsuario()
        {
            cmdDatosUsuario.CssClass = "btn btn-success";
            cmdDatosUsuario.Enabled = true;
        }

        private void CargarConsejos()
        {

            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            GrupoConsejosBL grupoConsejo = new GrupoConsejosBL();
            var dto = grupoConsejo.Get(userid).FirstOrDefault();

            if (dto != null)
            {
                ConsejoProfesionalBL consejoProfesionalBl = new ConsejoProfesionalBL();

                var qry = consejoProfesionalBl.GetConsejosxGrupo(dto.Id);

                ddlConsejo.DataSource = qry;
                ddlConsejo.DataTextField = "Nombre";
                ddlConsejo.DataValueField = "id";
                ddlConsejo.DataBind();

                if (id_profesional.Value == "0")
                {
                    ddlConsejo.Items.Insert(0, "");

                    if (ddlConsejo.Items.Count == 2)
                        ddlConsejo.SelectedIndex = 1;
                }
            }
        }

        private void CargarProvincias()
        {
            ProvinciaBL provinciasBL = new ProvinciaBL();
            var dsProv = provinciasBL.GetProvincias();

            foreach (var dr in dsProv)
            {
                ListItem itm = new ListItem(dr.Nombre, Convert.ToString(ddlProvincia.Items.Count + 1));
                ddlProvincia.Items.Insert(ddlProvincia.Items.Count, itm);
            }

            if (id_profesional.Value == "0")
            {
                ListItem itm = new ListItem("(Seleccione la Provincia)", "0");
                ddlProvincia.Items.Insert(0, itm);

            }
        }

        private void CargarTiposDeDocumentos()
        {
            TipoDocumentoPersonalBL tipoBL = new TipoDocumentoPersonalBL();

            var qry = tipoBL.GetAll();

            ddlTipoDoc.DataSource = qry;
            ddlTipoDoc.DataTextField = "Nombre";
            ddlTipoDoc.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDoc.DataBind();

            if (id_profesional.Value == "0")
            {
                ListItem itm = new ListItem("(Selec. Tipo de Doc)", "0");
                ddlTipoDoc.Items.Insert(0, itm);
            }
        }

        private void HabiliarComboBajaLogica()
        {
            if (ddlBajaLogica.SelectedIndex == 1)
            {   //cuando esta dado de baja se le permite al usuario rehabilitarlos
                pnlBajaLogicaLabel.Visible = true;
                pnlBajaLogicaDato.Visible = true;
            }
            else
            {
                pnlBajaLogicaLabel.Visible = false;
                pnlBajaLogicaDato.Visible = false;
            }
        }

        private void CargarDatosProfesional(int id_profesional)
        {
            ProfesionalesBL profesionalBL = new ProfesionalesBL();
            var ds = profesionalBL.Single(id_profesional);

            if (ds != null)
            {
                ddlConsejo.SelectedValue = ds.IdConsejo.ToString();
                txtMatricula.Text = ds.Matricula;
                txtApellido.Text = ds.Apellido;
                txtNombre.Text = ds.Nombre;
                ddlTipoDoc.SelectedValue = ds.IdTipoDocumento.ToString();
                txtNroDoc.Text = ds.NroDocumento.ToString();
                txtCalle.Text = ds.Calle;
                txtNroPuerta.Text = ds.NroPuerta;
                txtPiso.Text = ds.Piso;
                txtDepto.Text = ds.Depto;

                txtMatriculaMetrogas.Text = ds.MatriculaMetrogas.HasValue ? ds.MatriculaMetrogas.Value.ToString() : "";
                if (ds.CategoriaMetrogas.HasValue)
                    ddlCategoria.SelectedValue = ds.CategoriaMetrogas.Value.ToString();

                ddlProvincia.Text = ds.Provincia;
                txtLocalidad.Text = ds.Localidad;

                txtEmail.Text = ds.Email;
                txtSms.Text = ds.Sms;
                txtTelefono.Text = ds.Telefono;
                txtCuit.Text = ds.Cuit;
                txtIngresosBrutos.Text = ds.IngresosBrutos.HasValue ? ds.IngresosBrutos.ToString() : string.Empty;
                txtObservaciones.Text = ds.observaciones;

                if (ds.InhibidoBit)
                    ddlInhibido.SelectedValue = "1";
                else
                    ddlInhibido.SelectedValue = "2";

                bool bajaLogica = Convert.ToBoolean(ds.BajaLogica);
                if (bajaLogica)
                {
                    ddlBajaLogica.SelectedIndex = 1;
                }
                else
                {
                    ddlBajaLogica.SelectedIndex = 0;
                }
            }
        }

        protected void cmdVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ABM/BuscarProfesionales.aspx");
        }

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                pnlError.Visible = false;
                lblError.Text = "";
                MembershipUser user = Membership.GetUser();

                try
                {
                    Guid userid = (Guid)user.ProviderUserKey;
                    ProfesionalesBL profesionalBL = new ProfesionalesBL();
                    ProfesionalDTO dto = new ProfesionalDTO();
                    string strInhibido = ddlInhibido.SelectedValue.Equals("1") ? "Si" : "No";

                    // Si es un update
                    if (id_profesional.Value != "0")
                    {
                        dto.Id = int.Parse(id_profesional.Value);
                        var profAnterior = profesionalBL.Single(dto.Id);
                        dto.UserId = profAnterior.UserId;
                    }
                    dto.IdConsejo = int.Parse(ddlConsejo.SelectedValue);

                    dto.Matricula = txtMatricula.Text.Trim();
                    dto.Apellido = txtApellido.Text.Trim().ToUpper();
                    dto.Nombre = txtNombre.Text.Trim().ToUpper();
                    dto.IdTipoDocumento = int.Parse(ddlTipoDoc.SelectedValue);
                    dto.NroDocumento = int.Parse(txtNroDoc.Text);
                    dto.Calle = txtCalle.Text.Trim();
                    dto.NroPuerta = txtNroPuerta.Text.Trim();
                    dto.Piso = txtPiso.Text.Trim();
                    dto.Depto = txtDepto.Text.Trim();
                    dto.Localidad = txtLocalidad.Text;
                    dto.Provincia = ddlProvincia.SelectedItem.Text;
                    dto.Email = txtEmail.Text.Trim();
                    dto.Sms = txtSms.Text.Trim();
                    dto.Telefono = txtTelefono.Text.Trim();
                    dto.Cuit = txtCuit.Text.Trim();
                    dto.observaciones = txtObservaciones.Text.Trim();
                    if (ddlCategoria.SelectedIndex != 0)
                    {
                        dto.CategoriaMetrogas = int.Parse(ddlCategoria.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(txtIngresosBrutos.Text))
                        dto.IngresosBrutos = Convert.ToInt64(txtIngresosBrutos.Text.Trim());

                    dto.Inhibido = strInhibido;
                    dto.InhibidoBit = ddlInhibido.SelectedValue.Equals("1") ? true : false;
                    dto.CreateUser = userid;
                    dto.CreateDate = DateTime.Now.ToString();
                    dto.LastUpdateUser = userid;
                    dto.LastUpdateDate = DateTime.Now;

                    if (id_profesional.Value != "0")
                    {
                        bool bajaLogica = Convert.ToBoolean(ddlBajaLogica.SelectedItem.Value);
                        dto.BajaLogica = bajaLogica;
                    }
                    if (!string.IsNullOrEmpty(txtMatriculaMetrogas.Text) && (ddlCategoria.SelectedIndex != 0))
                    {
                        dto.MatriculaMetrogas = Convert.ToInt32(txtMatriculaMetrogas.Text);
                    }

                    // Si es un insert
                    if (id_profesional.Value == "0")
                        profesionalBL.Insert(dto);
                    else
                        profesionalBL.Update(dto);

                    //pnlBotones.Visible = false;

                    // Si es un alta verifica si el usuario logueado tiene permisos para generar el usuario del profesional
                    // Si posee los permisos muestra el botón para generar el usuario
                    if (id_profesional.Value == "0")
                    {
                        var nuevoProfesional = profesionalBL.GetByCuit(txtCuit.Text.Trim()).ToList().FirstOrDefault();

                        hid_id_profesional.Value = nuevoProfesional.Id.ToString();
                        hid_userid.Value = nuevoProfesional.UserId.ToString();
                        string[] strRoles = Roles.GetRolesForUser(user.UserName);
                        if (ExisteRol(strRoles, "adm_usuarios") || ExisteRol(strRoles, "administrador"))
                        {
                            pnlGenerarUsuario.Visible = true;
                            btnGenerarUsuario1.NavigateUrl = "~/ABM/ABMProfesionales.aspx?id=" + dto.Id;
                            btnCerrarAgregarProfesional.NavigateUrl = "~/ABM/ABMProfesionales.aspx?id=" + dto.Id;
                            lblMensajeActualizacion.Text = "Se Creo el Profesional Correctamente.";
                            ScriptManager.RegisterClientScriptBlock(UpdPnlSuccess_ok, UpdPnlSuccess_ok.GetType(), "ShowVisualizadorActualizacion", "ShowVisualizadorActualizacion()", true);
                        }
                    }
                    else
                    {
                        lblMensajeActualizacion.Text = "Los datos del profesional fueron actualizados correctamente.";
                        ScriptManager.RegisterClientScriptBlock(UpdPnlSuccess_ok, UpdPnlSuccess_ok.GetType(), "ShowVisualizadorActualizacion", "ShowVisualizadorActualizacion()", true);
                    }
                }
                catch (Exception ex)
                {
                    pnlError.Visible = true;
                    lblError.Text = ex.Message;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaRoles"></param>
        /// <param name="RolBuscado"></param>
        /// <returns></returns>
        private bool ExisteRol(string[] ListaRoles, string RolBuscado)
        {
            bool ret = false;

            foreach (string strRol in ListaRoles)
            {

                if (strRol.ToLower().Equals(RolBuscado.ToLower()))
                    ret = true;
            }

            return ret;
        }

        protected void CusValMatriculaUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            ProfesionalesBL profesionalBL = new ProfesionalesBL();

            int idConsejo = 0;

            Int32.TryParse(ddlConsejo.Text, out idConsejo);
            //si es insert
            if (id_profesional.Value == "0")
            {
                if (!profesionalBL.ExisteMatricula(idConsejo, txtMatricula.Text))
                {
                    pnlError.Visible = true;
                    lblError.Text = " Ya existe un profesional con esa matricula, por favor verifique.";
                    args.IsValid = false;
                }
            }
            else
            {
                var p = profesionalBL.Single(int.Parse(id_profesional.Value));

                if (p.Matricula != txtMatricula.Text)
                    if (!profesionalBL.ExisteMatricula(idConsejo, txtMatricula.Text))
                    {
                        pnlError.Visible = true;
                        lblError.Text = " Ya existe un profesional con esa matricula, por favor verifique.";
                        args.IsValid = false;
                    }
            }
        }
        private int getGrupoConsejoLogeado()
        {
            GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();
            // Obtiene el grupo de consejos al que pertenece el usuario logueado
            var dsGruCon = grupoConsejoBL.Get((Guid)Membership.GetUser().ProviderUserKey).FirstOrDefault();
            int id_grupoconsejo = 0;
            if (dsGruCon != null)
            {
                int.TryParse(dsGruCon.Id.ToString(), out id_grupoconsejo);
            }
            return id_grupoconsejo;
        }
        private void cargarPerfiles(Guid strUserId)
        {
            ConsejoProfesionalBL consejoBL = new ConsejoProfesionalBL();
            //cargar checkBox con perfiles 
            int id_grupoconsejo = getGrupoConsejoLogeado();
            IEnumerable<RolesDTO> ds = consejoBL.TraerPerfilesProfesionalXGrupo(id_grupoconsejo);

            _rolesSeleccionados = consejoBL.TraerPerfilesProfesionalXGrupo(id_grupoconsejo, strUserId);

            grdPerfiles.DataSource = ds;
            grdPerfiles.DataBind();
        }
        protected void grdPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField h = (HiddenField)e.Row.FindControl("hid_id_rol");
                CheckBox chkPerfil = (CheckBox)e.Row.FindControl("chkPerfil");
                var role = (RolesDTO)e.Row.DataItem;

                DropDownList ddlCalificacion = (DropDownList)e.Row.FindControl("ddlCalificacion");
                ddlCalificacion.DataValueField = "IdClasificacion";
                ddlCalificacion.DataTextField = "Descripcion";
                ddlCalificacion.DataSource = role.RolesGruposClasificacion;

                ddlCalificacion.DataBind();

                MembershipProvider securityProfesionales = Membership.Providers["SqlMembershipProviderProfesionales"];
                RoleProvider roleProvider = System.Web.Security.Roles.Providers["SqlRoleProviderProfesionales"];
                string strUsername = txtusername_datos.Text.Trim();
                MembershipUser user = securityProfesionales.GetUser(strUsername, false);
                if (user != null)
                {
                    string[] Roles = roleProvider.GetRolesForUser(user.UserName);

                    if (Roles.Contains(role.RoleName))
                        chkPerfil.Checked = true;
                }

                ddlCalificacion.Visible = role.RolesGruposClasificacion.Any();

                if (_rolesSeleccionados != null)
                {
                    var rolSeleccionado = _rolesSeleccionados.FirstOrDefault(p => p.RoleId == role.RoleId);

                    if (rolSeleccionado != null && rolSeleccionado.GruposUsuariosClasificacion != null)
                    {
                        ddlCalificacion.Visible = true;
                        if (!txtusername_datos.Enabled)
                        {
                            ddlCalificacion.SelectedValue = rolSeleccionado.GruposUsuariosClasificacion.IdClasificacion.ToString();
                        }
                    }
                }
            }
        }
        protected void reenvioclave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strUsername = txtusername_datos.Text.Trim();
                try
                {
                    MembershipProvider securityProfesionales = Membership.Providers["SqlMembershipProviderProfesionales"];
                    MembershipUser user = securityProfesionales.GetUser(strUsername, false);

                    ExternalService.MailMessages mailer = new MailMessages();

                    string htmlBody;
                    string Asunto = "Recupero de contraseña - Consejo de Profesionales";

                    htmlBody = mailer.MailPassRecoveryProfesional(user.UserName, user.GetPassword());
                    EmailServiceBL serviceMail = new EmailServiceBL();

                    EmailEntity emailEntity = new EmailEntity();
                    emailEntity.Email = user.Email;
                    emailEntity.Html = htmlBody;
                    emailEntity.Asunto = Asunto;
                    emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                    emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.RecuperoContrasena;
                    emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                    emailEntity.CantIntentos = 3;
                    emailEntity.CantMaxIntentos = 3;
                    emailEntity.FechaAlta = DateTime.Now;
                    emailEntity.Prioridad = 1;

                    serviceMail.SendMail(emailEntity);

                    lblSuccess.Text = "Su contraseña ha sido enviada correctamente.";
                    ScriptManager.RegisterClientScriptBlock(udpMensajesSucess, udpMensajesSucess.GetType(), "ShowSucess", "ShowSucess()", true);
                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                }
            }
        }
        protected void lnkBlanqueo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strUsername = txtusername_datos.Text.Trim();
                string strPassword = txtPass1.Text;
                try
                {
                    MembershipProvider securityProfesionales = Membership.Providers["SqlMembershipProviderProfesionales"];
                    MembershipUser user = securityProfesionales.GetUser(strUsername, false);

                    MailMessages mailer = new MailMessages();

                    string htmlBody;
                    string url;
                    string Asunto = "Recupero de contraseña - Consejo de Profesionales";
                    url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/Login");
                    url = IPtoDomain(url);

                    if (user.ChangePassword(user.GetPassword(), strPassword))
                    {
                        htmlBody = mailer.MailPassRecovery(user.UserName, strPassword, url);
                        EmailServiceBL serviceMail = new EmailServiceBL();

                        EmailEntity emailEntity = new EmailEntity();
                        emailEntity.Email = user.Email;
                        emailEntity.Html = htmlBody;
                        emailEntity.Asunto = Asunto;
                        emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                        emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.RecuperoContrasena;
                        emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                        emailEntity.CantIntentos = 3;
                        emailEntity.CantMaxIntentos = 3;
                        emailEntity.FechaAlta = DateTime.Now;
                        emailEntity.Prioridad = 1;

                        serviceMail.SendMail(emailEntity);

                        lblSuccess.Text = "Su contraseña ha sido blanqueada correctamente.";
                        ScriptManager.RegisterClientScriptBlock(udpMensajesSucess, udpMensajesSucess.GetType(), "ShowSucess", "ShowSucess()", true);
                    }
                    else
                    {
                        throw new Exception("No se pudo cambiar el password");
                    }
                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                }
            }
        }
        protected void btnAceptarDatosUsuario_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UsuarioDTO usuario = new UsuarioDTO();
                ProfesionalesBL profesionalBL = new ProfesionalesBL();
                usuario.UserName = txtusername_datos.Text.Trim();
                usuario.Email = txtEmail_datos.Text.Trim();

                usuario.Bloqueado = (RadioUsuarioBloqueado_datos.SelectedItem.ToString() == "Si") ? true : false;

                try
                {
                    usuario.Roles = new List<RolesDTO>();


                    foreach (GridViewRow row in grdPerfiles.Rows)
                    {
                        CheckBox chkPerfil = (CheckBox)row.Cells[0].FindControl("chkPerfil");
                        if (chkPerfil.Checked)
                        {
                            HiddenField hid_id_rol = (HiddenField)row.Cells[0].FindControl("hid_id_rol");
                            HiddenField hid_rolname = (HiddenField)row.Cells[0].FindControl("hid_rolname");
                            DropDownList ddlCalificacion = (DropDownList)row.Cells[0].FindControl("ddlCalificacion");
                            RolesDTO rol = new RolesDTO();
                            rol.RoleName = hid_rolname.Value;
                            rol.RoleId = Guid.Parse(hid_id_rol.Value);

                            if (ddlCalificacion.Visible)
                            {
                                rol.GruposUsuariosClasificacion = new UsuariosProfesionalesRolesClasificacionDTO();
                                rol.GruposUsuariosClasificacion.IdClasificacion = Convert.ToInt32(ddlCalificacion.SelectedValue);
                            }
                            else
                            {
                            }
                            usuario.Roles.Add(rol);
                        }
                    }

                    

                    int id_profesional = 0;
                    int.TryParse(hid_id_profesional.Value, out id_profesional);
                    usuario.IdProfesional = id_profesional;
                    usuario.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

                    var drProfesional = profesionalBL.Single(id_profesional);

                    MailMessages mailer = new MailMessages();

                    string url;
                    url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/Login");
                    url = IPtoDomain(url);
                    string nombreApellido = drProfesional.Nombre + " " + drProfesional.Apellido;
                    string htmlBody = mailer.MailWelcomeProfesional(usuario.UserName, usuario.UserName, url, nombreApellido);

                    ExternalService.EmailEntity emailEntity = new ExternalService.EmailEntity();

                    emailEntity.Asunto = "Información de acceso al sistema - Consejo de Profesionales";
                    emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                    emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.CreacionUsuario;
                    emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                    emailEntity.CantIntentos = 3;
                    emailEntity.CantMaxIntentos = 3;
                    emailEntity.Email = usuario.Email;
                    emailEntity.FechaAlta = DateTime.Now;
                    emailEntity.Prioridad = 1;
                    emailEntity.Html = htmlBody;
                    bool userNuevo = true;
                    userNuevo = (txtusername_datos.Enabled && hid_userid.Value == "") ? true : false;
                    profesionalBL.Insert(usuario, txtusername_datos.Enabled, emailEntity);
                    hid_userid.Value = usuario.UserId.ToString();
                    if (userNuevo)
                    {
                        lblMensajeActualizacion.Text = "El usuario fue creado correctamente.";
                        ScriptManager.RegisterClientScriptBlock(UpdPnlSuccess_ok, UpdPnlSuccess_ok.GetType(), "ShowVisualizadorActualizacion", "ShowVisualizadorActualizacion()", true);
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarFondoModal", "ocultarFondoModal()", true);

                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;

                    ScriptManager.RegisterClientScriptBlock(updMsjError, updMsjError.GetType(), "ShowError", "ShowError()", true);
                }
            }
        }

        protected void cmdDatosUsuario_Click(object sender, EventArgs e)
        {
            //txtusername_datos.Enabled = true;
            if (hid_userid.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(updMSJGenerarUsuario, updMSJGenerarUsuario.GetType(), "ShowMSJGenerarUsuario", "ShowMSJGenerarUsuario()", true);
            }
            else
                btnGenerarUserNuevo_Click(sender, e);



        }
        private void cargarPerfilesInhibidos(int idProf)
        {
            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            var ds = profesionalesBL.TraerProfesional_Perfiles_Inhibiciones(idProf);

            if (ds.Any())
            {
                pnlPerfilInhibido.Visible = true;
                gvPerfilInibido.DataSource = ds;
            }
            else
            {
                pnlPerfilInhibido.Visible = false;
                gvPerfilInibido.DataSource = null;
            }

            gvPerfilInibido.DataBind();

        }
        protected void btnGenerarUserNuevo_Click(object sender, EventArgs e)
        {
            txtusername_datos.Text = "";
            txtEmail_datos.Text = "";
            ProfesionalesBL profesionalBL = new ProfesionalesBL();
            int id_profesional = Convert.ToInt32(hid_id_profesional.Value);
            var ds = profesionalBL.Single(id_profesional);

            lblProfesional_datos.Text = ds.Apellido + " " + ds.Nombre;
            txtEmail_datos.Text = ds.Email;

            cargarPerfiles(Guid.Empty);
            cargarPerfilesInhibidos(id_profesional);
            if (hid_userid.Value == "")
            {
                txtusername_datos.Text = ds.NroDocumento.ToString();
                txtusername_datos.Enabled = true;
                pnlReenvioClave.Visible = false;
            }
            else
            {
                txtusername_datos.Text = ds.UserAspNet.UserName;
                txtusername_datos.Enabled = false;
                pnlReenvioClave.Visible = true;

            }
            lblUsuario.Text = "Generar Usuario";
            if (string.IsNullOrEmpty(hid_userid.Value))
                cargarPerfiles(Guid.Empty);
            else
            {
                Guid tmbUser = new Guid(hid_userid.Value);
                cargarPerfiles(tmbUser);
            }
            ScriptManager.RegisterClientScriptBlock(pnlDatosUsuario, pnlDatosUsuario.GetType(), "ShowModalCargadeUsuario", "ShowModalCargadeUsuario()", true);
        }
    }
}