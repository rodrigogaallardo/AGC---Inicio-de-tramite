using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.ABM
{
    public partial class UsuariosProf : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscar, updBuscar.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);
       
            }
            if (!IsPostBack)
            {
                cargarDatos();
                VerificarLlamadaABMProf();
            }
        }

        private void cargarDatos()
        {
            //cargar para filtrar las busquedas
            ConsejoProfesionalBL consejoBL = new ConsejoProfesionalBL();
            var ds = consejoBL.TraerPerfilesProfesionalXGrupo(getGrupoConsejoLogeado());

            foreach (var dr in ds)
            {
                ddlPerfil.Items.Add(new ListItem(dr.RoleName + " - " + dr.Description, dr.RoleId.ToString()));
            }

            ddlPerfil.DataBind();

            ddlPerfil.Items.Insert(0, "");
            ddlPerfil.SelectedIndex = 0;
        }
   
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdUsuarios.PageIndex = 0;
                BuscarUsuarios();
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updBuscar, updBuscar.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
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
                int.TryParse(dsGruCon.Id.ToString() , out id_grupoconsejo);
            }
            return id_grupoconsejo;
        }

        private void BuscarUsuarios()
        {
          
            grdUsuarios.DataBind();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            LinkButton btnEliminarUsuario = (LinkButton)sender;

            try
            {                
                Guid userid_profesional = Guid.Parse(btnEliminarUsuario.CommandArgument);
                ProfesionalesBL profesionalBL = new ProfesionalesBL();
                
                profesionalBL.BloquearUsuarioProfesional(userid_profesional, true); 

                txtNroMatricula.Text = "";
                txtUsername.Text = "";
                txtApeNom.Text = "";
                BuscarUsuarios();
                
            }                
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
            }
        }

        #region paginar grilla usuario

        protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsuarios.PageIndex = e.NewPageIndex;
            BuscarUsuarios();
        }

        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdUsuarios.PageIndex = int.Parse(cmdPage.Text) - 1;
            BuscarUsuarios();
        }

        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdUsuarios.PageIndex = grdUsuarios.PageIndex - 1;
            BuscarUsuarios();
        }

        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdUsuarios.PageIndex = grdUsuarios.PageIndex + 1;
            BuscarUsuarios();
        }

        protected void grdUsuarios_DataBound(object sender, EventArgs e)
        {
            GridView grid = grdUsuarios;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;


            if (fila != null)
            {
                LinkButton btnAnterior = (LinkButton)fila.Cells[0].FindControl("cmdAnterior");
                LinkButton btnSiguiente = (LinkButton)fila.Cells[0].FindControl("cmdSiguiente");

                if (grid.PageIndex == 0)
                    btnAnterior.Visible = false;
                else
                    btnAnterior.Visible = true;

                if (grid.PageIndex == grid.PageCount - 1)
                    btnSiguiente.Visible = false;
                else
                    btnSiguiente.Visible = true;


                // Ocultar todos los botones con Números de Página
                for (int i = 1; i <= 19; i++)
                {
                    LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                            btn.Text = i.ToString();
                            btn.Visible = true;
                        }
                    }
                }
                else
                {
                    // Mostrar 9 botones hacia la izquierda y 9 hacia la derecha
                    // o bien los que sea posible en caso de no llegar a 9

                    int CantBucles = 0;

                    LinkButton btnPage10 = (LinkButton)fila.Cells[0].FindControl("cmdPage10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 - CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }

                    }

                    CantBucles = 0;
                    // Ubica los 9 botones hacia la derecha
                    for (int i = grid.PageIndex + 1; i <= grid.PageIndex + 9; i++)
                    {
                        CantBucles++;
                        if (i <= grid.PageCount - 1)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }
                LinkButton cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (LinkButton)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn btn-default";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpager").Controls)
                {
                    if (ctl is LinkButton)
                    {
                        LinkButton btn = (LinkButton)ctl;
                        if (btn.Text.Equals(btnText))
                        {
                            btn.CssClass = "btn btn-info";
                        }
                    }
                }
            }
        }

        #endregion

        protected void grdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProfesionalDTO row = (ProfesionalDTO)e.Row.DataItem;

                LinkButton btnGenerarUsuario = (LinkButton)e.Row.FindControl("btnGenerarUsuario");
                LinkButton btnEditarUsuario = (LinkButton)e.Row.FindControl("btnEditarUsuario");

                if (btnEditarUsuario.CommandArgument.Length == 0)
                {
                    btnGenerarUsuario.Visible = true;
                    btnEditarUsuario.Visible = false;
                }
                else
                {
                    btnGenerarUsuario.Visible = false;
                    btnEditarUsuario.Visible = true;
                }

                Label lblPerfil = (Label)e.Row.FindControl("lblPerfil");
                string strPerfiles = "";

                //buscar perfiles del usuario                 
                if (row.UserId != null)
                {
                    //RoleProvider roleProvider = System.Web.Security.Roles.Providers["SqlRoleProviderProfesionales"];
                    //ProfesionalesBL profesionalesBL = new ProfesionalesBL();
                    //var ds = profesionalesBL.TraerPerfilesProfesional(roleProvider.ApplicationName, row.UserId.Value);

                    foreach (var dr in row.UserAspNet.AspNetRoles)
                    {
                        strPerfiles = string.IsNullOrEmpty(strPerfiles) ? dr.RoleName : strPerfiles + "<br>" + dr.RoleName;
                    }
                }

                lblPerfil.Text = strPerfiles;
            }
        }

        protected void btnGenerarUsuario_Click(object sender, EventArgs e)
        {
            pnlNotaEmail.Visible = true;
            pnlReenvioClave.Visible = false;
            LinkButton btnGenerarUsuario = (LinkButton)sender;
            int id_profesional = int.Parse(btnGenerarUsuario.CommandArgument);
            hid_userid.Value = "";
            hid_id_profesional.Value = id_profesional.ToString();

      
            txtusername_datos.Enabled = true;

            txtusername_datos.Text = "";
            txtEmail_datos.Text = "";
            lblUsuarioBloqueado.Visible = false;
            chkUsuarioBloqueado_datos.Visible = false;
            chkUsuarioBloqueado_datos.Checked = false;
            ProfesionalesBL profesionalBL = new ProfesionalesBL();
            var ds = profesionalBL.Single(id_profesional);

            txtusername_datos.Text = ds.NroDocumento.ToString();          
            lblProfesional_datos.Text = ds.Apellido + " " + ds.Nombre;
            txtEmail_datos.Text = ds.Email;
            
            cargarPerfiles(Guid.Empty);
            cargarPerfilesInhibidos(id_profesional);

            lblUsuario.Text = "Generar Usuario";

            ScriptManager.RegisterClientScriptBlock(
                updGrillaResultados, updGrillaResultados.GetType(),"mostrarPopup", "mostrarPopup('pnlDatosUsuarioClient');", true);

        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {            
            try
            {
                pnlNotaEmail.Visible = false;
                pnlReenvioClave.Visible = true;
                MembershipProvider securityProfesionales = Membership.Providers["SqlMembershipProviderProfesionales"];
                RoleProvider roleProvider = System.Web.Security.Roles.Providers["SqlRoleProviderProfesionales"];

                txtusername_datos.Enabled = false;

                LinkButton btnEditarUsuario = (LinkButton)sender;
                hid_userid.Value = btnEditarUsuario.CommandArgument;
                Guid userid_profesional = Guid.Parse(btnEditarUsuario.CommandArgument);
               
                MembershipUser user = securityProfesionales.GetUser(userid_profesional, false);
                
                ProfesionalesBL profesionalBL = new ProfesionalesBL();
                var ds = profesionalBL.Get(userid_profesional);

                txtusername_datos.Text = user.UserName;
                txtEmail_datos.Text = ds.Email;
                lblUsuarioBloqueado.Visible = true;
                chkUsuarioBloqueado_datos.Visible = true;
                chkUsuarioBloqueado_datos.Checked = user.IsLockedOut;


                hid_id_profesional.Value = ds.Id.ToString();

                lblProfesional_datos.Text = ds.Apellido + " " + ds.Nombre;                                

                cargarPerfiles(userid_profesional);
                cargarPerfilesInhibidos(ds.Id);

                txtPass1.Text = string.Empty;
                txtPass2.Text = string.Empty;

                lblUsuario.Text = "Editar Usuario";
                btnCancelarDatosUsuario.OnClientClick = "ocultarPopup('pnlDatosUsuarioClient')";

                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(),"mostrarPopup","mostrarPopup('pnlDatosUsuarioClient');", true);
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", "mostrarPopup('pnlInformacion');", true);
            }
        }
        IList<RolesDTO> _rolesSeleccionados = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserId"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProf"></param>
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

        #region Confirmar Edicion usuario

        protected void btnAceptarDatosUsuario_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UsuarioDTO usuario = new UsuarioDTO();
                ProfesionalesBL profesionalBL = new ProfesionalesBL(); 
                usuario.UserName = txtusername_datos.Text.Trim();
                usuario.Email = txtEmail_datos.Text.Trim();
                usuario.Bloqueado = chkUsuarioBloqueado_datos.Checked;

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

                    profesionalBL.Insert(usuario, txtusername_datos.Enabled, emailEntity);
                  
                    //Una vez que se dio de alta o modifico al usuario, se buscan los datos para mostralo en la grilla
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", "ocultarPopup('pnlDatosUsuarioClient')", true);

                    txtUsername.Text = usuario.UserName;
                    txtNroMatricula.Text = "";
                    txtApeNom.Text = "";
                    ddlPerfil.SelectedIndex = 0;
                    BuscarUsuarios();

                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;
                    //Oculto panel
                    ScriptManager.RegisterClientScriptBlock(updBotonesDatosUsuario, updBotonesDatosUsuario.GetType(), "ocultarPopup", "ocultarPopup('pnlDatosUsuarioClient')", true);

                    ScriptManager.RegisterClientScriptBlock(updBotonesDatosUsuario, updBotonesDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');",pnlInformacion.ClientID), true);
                }
            }
        }

        protected void CusValusername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;            
        }        

        private void VerificarLlamadaABMProf()
        {
            int id_prof = 0;

            if (Request.QueryString["id"] != null)
            {
                id_prof = Convert.ToInt32(Request.QueryString["id"].ToString());
                LinkButton btnGenUsu = new LinkButton();
                btnGenUsu.CommandArgument = id_prof.ToString();

                btnGenerarUsuario_Click(btnGenUsu, new EventArgs());
            }
        }

        #endregion

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

                    MailMessages mailer = new MailMessages();

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
                    
                    lblSuccess.Text = "Se le ha enviado la contraseña.";
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID), true);
                    //Oculto
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", "ocultarPopup('pnlDatosUsuarioClient')", true);
                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="sortByExpression"></param>
        /// <returns></returns>
        public IEnumerable<ProfesionalDTO> grdUsuarios_GetData(int maximumRows, int startRowIndex, out int totalRowCount, string sortByExpression)
        {

            int id_grupoconsejo = getGrupoConsejoLogeado();
            string strApenom = txtApeNom.Text.Trim();
            string strUsername = txtUsername.Text.Trim();
            string Matricula = txtNroMatricula.Text.Trim();
            bool profBajaLogica = false;
            bool? Baja = null;

            if (bool.TryParse(ddlProfBajaLogica.SelectedValue, out profBajaLogica))
                Baja = profBajaLogica;

            string perfil = ddlPerfil.SelectedValue;

            ProfesionalesBL profesionalBL = new ProfesionalesBL();
            Guid guidPerfil = Guid.Empty;
            Guid.TryParse(perfil, out guidPerfil);

            var ds = profesionalBL.Get(id_grupoconsejo, guidPerfil, strApenom, strUsername, Matricula, Baja, maximumRows, startRowIndex , out totalRowCount);

            lblCantResultados.Text = "( Cantidad de resultados: " + totalRowCount.ToString() + " )";
            lblCantResultados.Visible = (totalRowCount > 0);

            return ds;
        }
        protected void btnHabilitarUsuario_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            LinkButton btnHabilitarUsuario = (LinkButton)sender;

            try
            {

                Guid userid_profesional = Guid.Parse(btnHabilitarUsuario.CommandArgument);

                ProfesionalesBL profesionalBL = new ProfesionalesBL();
                profesionalBL.BloquearUsuarioProfesional(userid_profesional, false);
                
                BuscarUsuarios();

            }
            catch
            {
                lblmpeInfo.Text = "No se pudo habilitar al Profesional";
                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(), "mostrarError", "mostrarPopup('pnlInformacion');", true);
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

                        lblSuccess.Text = "Se le ha enviado la contraseña nueva.";
                        ScriptManager.RegisterClientScriptBlock(UpdPnlSuccess, UpdPnlSuccess.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID), true);
                        //Oculto panel
                        ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", "ocultarPopup('pnlDatosUsuarioClient')", true);
                    }
                    else {
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
    }
}