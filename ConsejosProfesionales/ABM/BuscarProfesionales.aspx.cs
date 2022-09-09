using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using System.Web.Security;
using DataTransferObject;
using System.Data;
using ConsejosProfesionales.Controls;
using DataAcess;
using System.Reflection;

namespace ConsejosProfesionales.ABM
{
    public partial class BuscarProfesionales : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdProfesionales.PageIndex = 0;
                Buscar();
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updBuscar, updBuscar.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
            }

        }

        private void Buscar()
        {
            grdProfesionales.DataBind();
        }

        protected void grdProfesionales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdProfesionales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdProfesionales.PageIndex = e.NewPageIndex;
            Buscar();

        }
        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdProfesionales.PageIndex = int.Parse(cmdPage.Text) - 1;
            Buscar();


        }
        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdProfesionales.PageIndex = grdProfesionales.PageIndex - 1;
            Buscar();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdProfesionales.PageIndex = grdProfesionales.PageIndex + 1;
            Buscar();
        }

        protected void grdProfesionales_DataBound(object sender, EventArgs e)
        {
            GridView grid = grdProfesionales;
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

        protected void grdProfesionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEliminar = (LinkButton)e.Row.FindControl("btnEliminarProfesionales");

                ProfesionalDTO oRow = (ProfesionalDTO)e.Row.DataItem;

                bool bajaLogica = oRow.BajaLogica;

                //en el caso que ya haya sido dado de baja se oculta boton
                btnEliminar.Visible = (bajaLogica) ? false : true;

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
        public IEnumerable<ProfesionalDTO> grdProfesionales_GetData(int maximumRows, int startRowIndex, out int totalRowCount, string sortByExpression)
        {
            GrupoConsejosBL grupoConsejoBl = new GrupoConsejosBL();
            var consejo = grupoConsejoBl.Get((Guid)Membership.GetUser().ProviderUserKey).FirstOrDefault();

            string strApenom = txtApeNom.Text.Trim();
            string strCUIT = "";

            string Matricula = txtNroMatricula.Text.Trim();

            int? Dni = null;
            bool? profBajaLogica = null;
            bool? bloqueado = null;
            bool? inhibido = null;
            int x = 0;

            if (!string.IsNullOrEmpty(txtNroDNI.Text.Trim()) && (Int32.TryParse(txtNroDNI.Text.Trim(), out x)))
            {                
                Dni = x;
            }

            string userName = txtUserName.Text.Trim();
            if (!string.IsNullOrEmpty(ddlProfBajaLogica.SelectedItem.Value))
            {
                profBajaLogica = bool.Parse(ddlProfBajaLogica.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlprofInhibido.SelectedItem.Value))
            {
                inhibido = bool.Parse(ddlprofInhibido.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddplBloqueado.SelectedItem.Value))
            {
                bloqueado = bool.Parse(ddplBloqueado.SelectedItem.Value);
            }
            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            totalRowCount = 0;
            var ds = profesionalesBL.Get(consejo.Id, strApenom, Dni, Matricula, strCUIT, profBajaLogica, bloqueado, inhibido, userName, startRowIndex, maximumRows, out totalRowCount).ToList();

            int cantResultados = totalRowCount;
            lblCantResultados.Text = cantResultados.ToString();
            lblCantResultados.Visible = (cantResultados > 0);

            return ds;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            try
            {
                int id_profesional = Convert.ToInt32(hfIdProfesional.Value);


                ProfesionalesBL profesionalBl = new ProfesionalesBL();
                ProfesionalDTO prof = new ProfesionalDTO();

                prof.Id = id_profesional;

                profesionalBl.darDeBajaProfesional(prof);


                Buscar();
                lblConfirmarMensaje.Text = "La baja fue dada correctamente.";
                ScriptManager.RegisterClientScriptBlock(updConfirmarMensaje, updConfirmarMensaje.GetType(), "showfrmMensajeFinal", "showfrmMensajeFinal();", true);
                //ScriptManager.RegisterClientScriptBlock(udpConfirmarEliminacion, udpConfirmarEliminacion.GetType(), "hidefrmConfirmar", "hidefrmConfirmar();", true);
                //udpConfirmarEliminacion.Update();
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlGrillaResultados, pnlGrillaResultados.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
            }

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar();
            }
            catch (Exception ex)
            {
                this.EjecutarScript(updBuscar, "showfrmError();");
            }
        }

        private void Exportar()
        {
            try
            {
                mostrarTimer("Profesionales");
                this.EjecutarScript(updExportaExcel, "showfrmExportarExcel();");

                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ExportarProfesionalesExcel));
                thread.Start();
            }
            catch (Exception ex)
            {
                this.EjecutarScript(updBuscar, "showfrmError();");
                updBuscar.Update();
            }
        }

        private void ExportarProfesionalesExcel()
        {
            decimal cant_registros_x_vez = 0m;
            int totalRowCount = 0;
            int startRowIndex = 0;
            this.EjecutarScript(updExportaExcel, "showfrmExportarExcel();");
            try
            {
                BuscarConsultaExcel(startRowIndex, 0, out totalRowCount);
                if (totalRowCount < 2000)
                    cant_registros_x_vez = 200m;
                else if (totalRowCount < 7000)
                    cant_registros_x_vez = 1000m;
                else
                    cant_registros_x_vez = 1000m;
                int cantidad_veces = (int)Math.Ceiling(totalRowCount / cant_registros_x_vez);
                List<ProfesionalesXLS> resultados = new List<ProfesionalesXLS>();
                for (int i = 1; i <= cantidad_veces; i++)
                {
                    resultados.AddRange(BuscarConsultaExcel(startRowIndex, Convert.ToInt32(cant_registros_x_vez), out totalRowCount));
                    Session["progress_data"] = string.Format("{0} / {1} registros exportados.", resultados.Count, totalRowCount);
                    startRowIndex += Convert.ToInt32(cant_registros_x_vez);
                }
                Session["progress_data"] = string.Format("{0} / {1} registros exportados.", resultados.Count, totalRowCount);

                // Convierte la lista en un dataset
                DataSet ds = new DataSet();
                DataTable dt = ToDataTable(resultados);
                dt.TableName = "Listado Profesionales";
                ds.Tables.Add(dt);
                string savedFileName = @"C:\Temporal\" + Session["filename_exportacion"].ToString();

                // Utiliza DocumentFormat.OpenXml para exportar a excel
                CreateExcelFile.CreateExcelDocument(ds, savedFileName);
                // quita la variable de session.
                Session.Remove("progress_data");
                Session.Remove("exportacion_en_proceso");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Session["progress_data"] = "Error: " + ex.InnerException.Message;
                }
                else
                {
                    Session["progress_data"] = "Error: " + ex.Message;
                }
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private IEnumerable<ProfesionalesXLS> BuscarConsultaExcel(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            totalRowCount = 0;

            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            GrupoConsejosBL grupoConsejoBl = new GrupoConsejosBL();
            var consejo = grupoConsejoBl.Get((Guid)Membership.GetUser().ProviderUserKey).FirstOrDefault();

            var q = (from p in db.Profesional
                     join usr in db.aspnet_Users on p.UserId equals usr.UserId into userLeft
                     from lu in userLeft.DefaultIfEmpty()
                     join tdoc in db.TipoDocumentoPersonal on p.IdTipoDocumento equals tdoc.TipoDocumentoPersonalId
                     where p.ConsejoProfesional.id_grupoconsejo == consejo.Id

                     select new ProfesionalesXLS
                     {
                         Consejo = p.ConsejoProfesional.Nombre,
                         Apellido = p.Apellido,
                         Nombres = p.Nombre,
                         TipoDocumento = tdoc.Descripcion,
                         NúmerodeDoc = p.NroDocumento.ToString(),
                         NroMatrícula = p.Matricula,
                         Calle = p.Calle,
                         Nro = p.NroPuerta,
                         Piso = p.Piso,
                         Depto = p.Depto,
                         Provincia = p.Provincia,
                         Localidad = p.Localidad,
                         EMail = p.Email,
                         Sms = p.Sms,
                         Teléfono = p.Telefono,
                         CUIL = p.Cuit,
                         NroIngresosBrutos = p.IngresosBrutos.ToString(),
                         MatrículaGasista = p.MatriculaMetrogas.ToString(),
                         Categoria = p.CategoriaMetrogas.ToString(),
                         Estáinihibido = p.Inhibido,
                         Usuario = (lu == null) ? "" : lu.UserName.ToString(),
                         Baja = p.BajaLogica
                     });

            bool? profBajaLogica = null;

            if (!string.IsNullOrEmpty(ddlProfBajaLogica.SelectedItem.Value))
            {
                profBajaLogica = bool.Parse(ddlProfBajaLogica.SelectedValue);
                q = q.Where(prof => prof.Baja == profBajaLogica);
            }

            totalRowCount = q.Count();
            q = q.OrderBy(o => o.Apellido).ThenBy(x => x.Nombres).Skip(startRowIndex).Take(maximumRows);
            return q;
        }

        protected void mostrarTimer(string name)
        {
            btnCerrarExportacion.Visible = false;
            // genera un nombre de archivo aleatorio
            //Random random = new Random((int)DateTime.Now.Ticks);
            //int NroAleatorio = random.Next(0, 100);
            //NroAleatorio = NroAleatorio * random.Next(0, 100);
            name = name + ".xlsx";
            string fileName = string.Format(name);

            Session["exportacion_en_proceso"] = true;
            Session["progress_data"] = "Preparando exportación.";
            Session["filename_exportacion"] = fileName;

            Timer1.Enabled = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                bool exportacion_en_proceso = (Session["exportacion_en_proceso"] != null ? (bool)Session["exportacion_en_proceso"] : false);

                if (exportacion_en_proceso)
                {
                    lblRegistrosExportados.Text = Convert.ToString(Session["progress_data"]);
                    lblExportarError.Text = Convert.ToString(Session["progress_data"]);
                    btnDescargarExcel.Visible = false;
                }
                else
                {
                    Timer1.Enabled = false;
                    btnCerrarExportacion.Visible = true;
                    pnlDescargarExcel.Style["display"] = "block";
                    pnlExportandoExcel.Style["display"] = "none";
                    string filename = Session["filename_exportacion"].ToString();
                    filename = HttpUtility.UrlEncode(filename);
                    btnDescargarExcel.Visible = true;
                    btnDescargarExcel.NavigateUrl = string.Format("~/Controls/DescargarArchivoTemporal.aspx?fname={0}", filename);
                    Session.Remove("filename_exportacion");
                }
                //Cuando falla la exportacion
                if (Session["progress_data"].ToString().StartsWith("Error:"))
                {
                    Timer1.Enabled = false;
                    btnCerrarExportacion.Visible = true;
                    pnlExportandoExcel.Style["display"] = "none";
                    pnlExportacionError.Style["display"] = "block";
                }
            }
            catch
            {
                Timer1.Enabled = false;
            }
        }

        protected void btnCerrarExportacion_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            Session.Remove("filename_exportacion");
            Session.Remove("progress_data");
            Session.Remove("exportacion_en_proceso");
            pnlExportacionError.Style["display"] = "none";
            pnlDescargarExcel.Style["display"] = "none";
            pnlExportandoExcel.Style["display"] = "block";

            this.EjecutarScript(updExportaExcel, "hidefrmExportarExcel();");

        }

        protected void btnEditarProfesionales_Click(object sender, EventArgs e)
        {

        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_profesional = Convert.ToInt32(hfIdProfesionalReactivar.Value);
                ProfesionalesBL profesionalBl = new ProfesionalesBL();
                ProfesionalDTO prof = new ProfesionalDTO();
                prof.Id = id_profesional;
                profesionalBl.reactivarProfesional(prof);
                Buscar();
                lblConfirmarMensaje.Text = "Se reactivo Correctamente";
                ScriptManager.RegisterClientScriptBlock(updConfirmarMensaje, updConfirmarMensaje.GetType(), "showfrmMensajeFinal", "showfrmMensajeFinal();", true);
                //updmpeInfo.Update();
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlGrillaResultados, pnlGrillaResultados.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
            }
        }
    }

    internal class ProfesionalesXLS
    {
        public string NroMatrícula { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Consejo { get; set; }
        public string Teléfono { get; set; }
        public string Sms { get; set; }
        public string Usuario { get; set; }
        public string TipoDocumento { get; set; }
        public string NúmerodeDoc { get; set; }
        public string CUIL { get; set; }
        public string NroIngresosBrutos { get; set; }
        public string EMail { get; set; }
        public string Estáinihibido { get; set; }
        public string MatrículaGasista { get; set; }
        public string Categoria { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string Calle { get; set; }
        public string Nro { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public bool Baja { get; set; }
    }
}