using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
 
namespace ConsejosProfesionales.ABM
{
    public partial class ABMProfesionalesMasivo : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlErrores.Visible = false;
            pnlGrillaResultados.Visible = false;
            pnlresultadoOK.Visible = false;

        }
        protected void btnComenzarValidacion_Click(object sender, EventArgs e)
        {

            string filename = Server.MapPath("../") + "Files\\" + hid_filename.Value;

            ValidarArchivo(filename);

        }

        private void ValidarArchivo(string filename)
        {

            pnlErrores.Visible = false;

            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                DataTable dtResult = new DataTable("Errores");
                dtResult.Columns.Add("id", typeof(int));
                dtResult.Columns.Add("mensaje", typeof(string));

                string CnnStringExcel = "";
                int i = 0;
                string NombreTabla = "";
                string ColumnasConnombreInvalido = "";
                string[] sCols = new string[18];

                sCols[0] = "Consejo";
                sCols[1] = "Matricula";
                sCols[2] = "Apellido";
                sCols[3] = "Nombre";
                sCols[4] = "TipoDoc";
                sCols[5] = "NroDoc";
                sCols[6] = "Calle";
                sCols[7] = "NroPuerta";
                sCols[8] = "Piso";
                sCols[9] = "Depto";
                sCols[10] = "Localidad";
                sCols[11] = "Provincia";
                sCols[12] = "Email";
                sCols[13] = "Celular";
                sCols[14] = "Telefono";
                sCols[15] = "Cuit";
                sCols[16] = "IngresosBrutos";
                sCols[17] = "Inhibido";

                //Crea una colección con los tipos de Documento
                List<string> lstTipDoc = new List<string>();
                List<int> lstTipDocId = new List<int>();
                lstTipDoc.Clear();
                lstTipDocId.Clear();
                
                //DataSet dsTipoDocPersonal = LogicEncomienda.TraerTipoDocumentoPersonal();
                TipoDocumentoPersonalBL tipoDocumentoBL = new TipoDocumentoPersonalBL();
                var dsTipoDocPersonal = tipoDocumentoBL.GetAll();
                foreach (var drDoc in dsTipoDocPersonal)
                {
                    lstTipDoc.Add(drDoc.Nombre);
                    lstTipDocId.Add(drDoc.TipoDocumentoPersonalId);

                }

                ProfesionalesBL profesionalBL = new ProfesionalesBL();
                 

                //Crea una colección con los ids de los consejos 
                List<string> lstConsejos = new List<string>();

                lstConsejos.Clear();
                //DataSet dsConsejos = LogicEncomienda.TraerConsejoProfesional(userid);
                ConsejoProfesionalBL consejoBl = new ConsejoProfesionalBL();
                var dsConsejos = consejoBl.GetGrupoConsejo(userid);
  
                foreach (var drCon in dsConsejos)
                {
                    lstConsejos.Add(drCon.Id.ToString());
                }



                //Establece la conexion de acuerdo al tipo de formato del archivo Excel 2007/2010 o 97/2003
                if (filename.Contains(".xlsx"))
                    CnnStringExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filename + "';Extended Properties='Excel 12.0;HDR=YES';";
                else
                    CnnStringExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filename + "';Extended Properties='Excel 8.0;HDR=YES';";


                OleDbConnection oCnnExcel = new OleDbConnection(CnnStringExcel);
                OleDbCommand oCmd = new OleDbCommand();
                OleDbDataAdapter oDa = new OleDbDataAdapter();
                DataSet oDs = new DataSet();

                oCnnExcel.Open();

                try
                {

                    //Averigua el Nombre de las hojas del Excel
                    DataTable dtHojas = oCnnExcel.GetSchema("TABLES");

                    if (dtHojas.Rows.Count == 0)
                        throw new Exception("El archivo Excel indicado no posee hojas o bien el mismo posee caracteres no permitidos en el nombre. <br /> (Acentos,la letra ñ, etc). Renombre el archivo con un nombre simple.");
                    else
                        NombreTabla = dtHojas.Rows[0]["TABLE_NAME"].ToString();

                    //Consulta los datos de la primera hoja del excel.
                    oCmd.CommandText = string.Format("SELECT * FROM [{0}]", NombreTabla);
                    oCmd.Connection = oCnnExcel;

                    oDa.SelectCommand = oCmd;
                    oDa.Fill(oDs);

                    DataTable dt = oDs.Tables[0];

                    //Cierra la conexion con el excel
                    oCnnExcel.Close();

                    // Verifica si el archivo contiene datos
                    if (dt.Rows.Count == 0)
                        throw new Exception("El archivo Excel indicado no posee datos en la primer hoja '" + NombreTabla + "', recuerde que los datos se deben encontrar en la primer hoja del archivo. Este archivo contiene " + dtHojas.Rows.Count + " hoja/s.");

                    //Verifica la cantidad de columnas que debe tener el archivo
                    if (dt.Columns.Count != 18)
                        throw new Exception("El archivo Excel indicado no posee la cantidad de columnas que debería.<br />Deben ser " + sCols.Length + " columnas, verifique con el archivo de Ejemplo.");


                    //Verifica que las columnas tengan el nombre igual al formato pedido
                    for (i = 0; i <= 17; i++)
                    {
                        if (dt.Columns[i].ColumnName.ToLower() != sCols[i].ToLower())
                            ColumnasConnombreInvalido += "La columna " + dt.Columns[i].ColumnName + " debería llamarse " + sCols[i] + ".<br/>";
                    }
                    if (ColumnasConnombreInvalido.Length > 0)
                        throw new Exception("Se encontraron los siguientes inconvenientes con los nombres de las columnas:<br /><br />" + ColumnasConnombreInvalido);

                    i = 1; //arranca de 1 por la cabecera

                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;
                        if (filaSinDatos(dr))
                        {   //cuando la fila del excel no tiene datos asumo que llego al final y sale del foreach
                            break;
                        }
                        int v_idconsejo = 0;
                        string v_Matricula = dr["Matricula"].ToString().Trim();
                        string v_Apellido = dr["Apellido"].ToString().Trim();
                        string v_Nombre = dr["Nombre"].ToString().Trim();
                        string v_TipoDoc = dr["TipoDoc"].ToString().Trim();
                        int v_NroDoc = 0;
                        string v_calle = dr["calle"].ToString().Trim();
                        int v_NroPuerta = 0;
                        string v_Piso = dr["Piso"].ToString().Trim();
                        string v_Depto = dr["Depto"].ToString().Trim();
                        string v_Localidad = dr["Localidad"].ToString().Trim();
                        string v_Provincia = dr["Provincia"].ToString().Trim();
                        string v_Email = dr["Email"].ToString().Trim();
                        string v_Celular = dr["Celular"].ToString().Trim();
                        string v_Telefono = dr["Telefono"].ToString().Trim();
                        Int64 v_Cuit = 0;
                        Int64 v_IngresosBrutos = 0;
                        //bool v_Inibido = false;
                        bool valTipoDocok = false;
                        bool valNroDocok = false;
                        bool valCuitok = false;

                        if (dr["Consejo"].ToString().Trim().Length == 0)
                            dtResult.Rows.Add(i, "El Consejo del registro " + i.ToString() + " no puede ser un valor vacío.");
                        else if (!int.TryParse(dr["Consejo"].ToString().Trim(), out v_idconsejo))
                            dtResult.Rows.Add(i, "El valor de la columna Consejo del registro " + i.ToString() + " no se puede convertir a un valor numérico.");
                        else if (lstConsejos.IndexOf(v_idconsejo.ToString()) < 0)
                            dtResult.Rows.Add(i, "El valor de la columna Consejo del registro " + i.ToString() + " no pertence al mismo consejo del usuario que se encuentra logueado en el sistema.");


                        if (v_Matricula.Length == 0)
                            dtResult.Rows.Add(i, "La Matrícula del registro " + i.ToString() + " no puede ser un texto vacío.");
                        else
                            if (v_Matricula.Trim().Equals("0"))
                                dtResult.Rows.Add(i, "El Número de Matrícula del registro " + i.ToString() + " no puede ser 0 (cero).");

                        if (v_Apellido.Length == 0)
                            dtResult.Rows.Add(i, "El Apellido del registro " + i.ToString() + " no puede ser un texto vacío.");

                        if (v_Nombre.Length == 0)
                            dtResult.Rows.Add(i, "El Nombre del registro " + i.ToString() + " no puede ser un texto vacío.");

                        if (v_TipoDoc.Length == 0)
                            dtResult.Rows.Add(i, "El Tipo de documento del registro " + i.ToString() + " no puede ser un texto vacío.");
                        else if (lstTipDoc.IndexOf(v_TipoDoc) < 0)
                            dtResult.Rows.Add(i, "El Tipo de documento del registro " + i.ToString() + " no pertenece a las opciones indicadas en el archivo de Ejemplo (DNI,LE,LC,CI,etc).");
                        else
                            valTipoDocok = true;

                        if (dr["NroDoc"].ToString().Trim().Length == 0)
                            dtResult.Rows.Add(i, "El Número de Documento del registro " + i.ToString() + " no puede ser un valor vacío.");
                        else if (!int.TryParse(dr["NroDoc"].ToString().Trim(), out v_NroDoc))
                            dtResult.Rows.Add(i, "El formato de la columna NroDoc del registro " + i.ToString() + " no se puede convertir a un valor numérico.");
                        else if (v_NroDoc == 0)
                            dtResult.Rows.Add(i, "El Número de Documento del registro " + i.ToString() + " no puede ser 0 (cero).");
                        else
                            valNroDocok = true;

                        if (dr["NroPuerta"].ToString().Trim().Length > 0)
                        {
                            if (!int.TryParse(dr["NroPuerta"].ToString().Trim(), out v_NroPuerta))
                                dtResult.Rows.Add(i, "El formato de la columna NroPuerta del registro " + i.ToString() + " no se puede convertir a un valor numérico.");
                        }

                        if (dr["Cuit"].ToString().Trim().Length == 0)
                            dtResult.Rows.Add(i, "El Cuit/Cuil del registro " + i.ToString() + " no puede estar vacío.");
                        else if (!Int64.TryParse(dr["Cuit"].ToString().Trim(), out v_Cuit))
                            dtResult.Rows.Add(i, "El formato de la columna Cuit del registro " + i.ToString() + " no se puede convertir a un valor numérico.");
                        else if (v_Cuit == 0)
                            dtResult.Rows.Add(i, "El Nro de Cuit/Cuil del registro " + i.ToString() + " no puede ser 0 (cero).");
                        else if (v_Cuit.ToString().Length != 11)
                            dtResult.Rows.Add(i, "El Nro de Cuit/Cuil del registro " + i.ToString() + " debe ser de 11 dígitos.");
                        else
                            valCuitok = true;


                        if (dr["IngresosBrutos"].ToString().Trim().Length > 0)
                        {
                            if (!Int64.TryParse(dr["IngresosBrutos"].ToString().Trim(), out v_IngresosBrutos))
                                dtResult.Rows.Add(i, "El formato de la columna IngresosBrutos del registro " + i.ToString() + " no se puede convertir a un valor numérico.");
                        }

                        //if (dr["Inhibido"].ToString().Trim().ToUpper() == "S")
                        //    v_Inibido = true;


                        if (valCuitok && valTipoDocok && valNroDocok)
                        {
                            if (v_TipoDoc == "DNI" && v_NroDoc < 90000000)
                            {
                                if (Convert.ToInt32(v_Cuit.ToString().Substring(2, 8)) != v_NroDoc)
                                {
                                    dtResult.Rows.Add(i, "El Nro CUIT/CUIL del registro " + i.ToString() + " no corresponde con el número de documento.");
                                }
                            }
                        }
                        
                        //var dsProf = profesionalBL.Get(v_idconsejo, string.Empty, v_Matricula, string.Empty, null);                      

                        //foreach (var row in dsProf)
                        //{
                        //    //verificar que el profesional no este dado de baja                            
                        //    if (row.BajaLogica)
                        //    {
                        //        dtResult.Rows.Add(i, "El Profesional del registro " + i.ToString() + " fue dado de baja. Ingrese a la opción de modificacìón para rehabilitarlo.");
                        //    }

                        //}

                    }

                    pnlValidacion.Style["display"] = "none";

                    if (dtResult.Rows.Count == 0)
                    {
                        // Actualiza los datos de los profesionales con los que están en el archivo.
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (filaSinDatos(dr))
                            {   //cuando la fila del excel no tiene datos asumo que llego al final y sale del foreach
                                break;
                            }

                            int v_idconsejo = int.Parse(dr["Consejo"].ToString().Trim());
                            string v_Matricula = dr["Matricula"].ToString().Trim();
                            string v_Apellido = dr["Apellido"].ToString().Trim();
                            string v_Nombre = dr["Nombre"].ToString().Trim();
                            string v_TipoDoc = dr["TipoDoc"].ToString().Trim();
                            int v_NroDoc = int.Parse(dr["NroDoc"].ToString().Trim());
                            string v_calle = dr["calle"].ToString().Trim();
                            int v_NroPuerta = 0;
                            string v_Piso = dr["Piso"].ToString().Trim();
                            string v_Depto = dr["Depto"].ToString().Trim();
                            string v_Localidad = dr["Localidad"].ToString().Trim();
                            string v_Provincia = dr["Provincia"].ToString().Trim();
                            string v_Email = dr["Email"].ToString().Trim();
                            string v_Celular = dr["Celular"].ToString().Trim();
                            string v_Telefono = dr["Telefono"].ToString().Trim();
                            Int64 v_Cuit = 0;
                            Int64 v_IngresosBrutos = 0;
                            string v_Inibido = "No";
                            int v_idTipoDoc = 0;
                            string v_Cuit_string = "";
                            string v_IngresosBrutos_string = "";

                            if (dr["NroPuerta"].ToString().Trim().Length > 0)
                                v_NroPuerta = int.Parse(dr["NroPuerta"].ToString().Trim());

                            if (dr["Cuit"].ToString().Trim().Length > 0)
                            {
                                v_Cuit = Int64.Parse(dr["Cuit"].ToString().Trim());
                                v_Cuit_string = v_Cuit.ToString();
                                v_Cuit_string = v_Cuit_string.Substring(0, 2) + "-" + v_Cuit_string.Substring(2, 8) + "-" + v_Cuit_string.Substring(v_Cuit_string.Length - 1, 1);
                            }

                            if (dr["IngresosBrutos"].ToString().Trim().Length > 0)
                            {
                                v_IngresosBrutos = Int64.Parse(dr["IngresosBrutos"].ToString().Trim());
                                v_IngresosBrutos_string = v_IngresosBrutos.ToString();
                            }

                            if (dr["Inhibido"].ToString().Trim().ToUpper() == "S")
                                v_Inibido = "Si";

                            // Busca el id del Tipo de documento
                            v_idTipoDoc = lstTipDocId[lstTipDoc.IndexOf(v_TipoDoc)];


                            //LogicEncomienda.ActualizarProfesionalMasivo(v_idconsejo, v_Matricula, v_Apellido, v_Nombre, v_idTipoDoc, v_NroDoc,
                              //                              v_calle, v_NroPuerta, v_Piso, v_Depto, v_Localidad, v_Provincia, v_Email, v_Celular,
                                //                            v_Telefono, v_Cuit_string, v_IngresosBrutos_string, v_Inibido, userid);

                        }

                        pnlresultadoOK.Visible = true;
                    }
                    else
                    {
                        grdErrores.DataSource = dtResult;
                        grdErrores.DataBind();
                        pnlGrillaResultados.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                    oCnnExcel.Close();
                    pnlValidacion.Style["display"] = "none";
                    lblErrores.Text = ex.Message;
                    pnlErrores.Visible = true;

                }

                System.IO.File.Delete(filename);
            }
            catch (Exception ex)
            {
                pnlValidacion.Style["display"] = "none";
                lblErrores.Text = "Error:" + ex.Message;
                pnlErrores.Visible = true;

            }

        }

        protected void btnComenzarValidacion2_Click(object sender, EventArgs e)
        {

            try
            {
                lblErrores.Text = "<br /> Empieza el procedimiento";
                string filename = Server.MapPath("../") + "Files\\" + hid_filename.Value;
                filename = System.IO.Path.GetFullPath(filename);

                lblErrores.Text += "<br /> Nombre de Archivo:" + filename;
                pnlErrores.Visible = true;

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                DataTable dtResult = new DataTable("Errores");
                dtResult.Columns.Add("id", typeof(int));
                dtResult.Columns.Add("mensaje", typeof(string));


                string CnnStringExcel = "";
                int i = 0;
                string NombreTabla = "";
                string ColumnasConnombreInvalido = "";
                string[] sCols = new string[18];

                sCols[0] = "Consejo";
                sCols[1] = "Matricula";
                sCols[2] = "Apellido";
                sCols[3] = "Nombre";
                sCols[4] = "TipoDoc";
                sCols[5] = "NroDoc";
                sCols[6] = "Calle";
                sCols[7] = "NroPuerta";
                sCols[8] = "Piso";
                sCols[9] = "Depto";
                sCols[10] = "Localidad";
                sCols[11] = "Provincia";
                sCols[12] = "Email";
                sCols[13] = "Celular";
                sCols[14] = "Telefono";
                sCols[15] = "Cuit";
                sCols[16] = "IngresosBrutos";
                sCols[17] = "Inhibido";

                //Crea una colección con los tipos de Documento
                List<string> lstTipDoc = new List<string>();
                List<int> lstTipDocId = new List<int>();
                lstTipDoc.Clear();
                lstTipDocId.Clear();
                //DataSet dsTipoDocPersonal = LogicEncomienda.TraerTipoDocumentoPersonal();
                TipoDocumentoPersonalBL tipoDocumentoBL = new TipoDocumentoPersonalBL();
                var dsTipoDocPersonal = tipoDocumentoBL.GetAll();


                foreach (var drDoc in dsTipoDocPersonal)
                {
                    lstTipDoc.Add(drDoc.Nombre);
                    lstTipDocId.Add(drDoc.TipoDocumentoPersonalId);

                }


                //Crea una colección con los ids de los consejos 
                List<string> lstConsejos = new List<string>();

                lstConsejos.Clear();
                //DataSet dsConsejos = LogicEncomienda.TraerConsejoProfesional(userid);
                ConsejoProfesionalBL consejoBl = new ConsejoProfesionalBL();
                var dsConsejos = consejoBl.GetGrupoConsejo(userid);

                foreach (var drCon in dsConsejos)
                {
                    lstConsejos.Add(drCon.Id.ToString());
                }

                //Establece la conexion de acuerdo al tipo de formato del archivo Excel 2007/2010 o 97/2003
                if (filename.Contains(".xlsx"))
                    CnnStringExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filename + "';Extended Properties='Excel 12.0;HDR=YES';";
                else
                    CnnStringExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filename + "';Extended Properties='Excel 8.0;HDR=YES';";

                lblErrores.Text += CnnStringExcel;

                OleDbConnection oCnnExcel = new OleDbConnection(CnnStringExcel);
                OleDbCommand oCmd = new OleDbCommand();
                OleDbDataAdapter oDa = new OleDbDataAdapter();
                DataSet oDs = new DataSet();

                try
                {
                    oCnnExcel.Open();

                    lblErrores.Text += "<br /> Conexión abierta";
                    //Averigua el Nombre de las hojas del Excel
                    DataTable dtHojas = oCnnExcel.GetSchema("TABLES");
                    lblErrores.Text += "<br /> Recupero Tablas";

                    if (dtHojas.Rows.Count == 0)
                        throw new Exception("El archivo Excel indicado no posee hojas.");
                    else
                        NombreTabla = dtHojas.Rows[0]["TABLE_NAME"].ToString();

                    lblErrores.Text += "<br /> Nombre de Tabla: " + NombreTabla;

                    //Consulta los datos de la primera hoja del excel.
                    oCmd.CommandText = string.Format("SELECT * FROM [{0}]", NombreTabla);
                    oCmd.Connection = oCnnExcel;

                    oDa.SelectCommand = oCmd;
                    oDa.Fill(oDs);

                    DataTable dt = oDs.Tables[0];

                    lblErrores.Text += "<br /> Ejecuto Comando: " + oCmd.CommandText;

                    //Cierra la conexion con el excel
                    oCnnExcel.Close();
                    lblErrores.Text += "<br /> Cerró la conexión con el Excel.";

                    // Verifica si el archivo contiene datos
                    if (dt.Rows.Count == 0)
                        throw new Exception("El archivo Excel indicado no posee datos en la primer hoja '" + NombreTabla + "', recuerde que los datos se deben encontrar en la primer hoja del archivo. Este archivo contiene " + dtHojas.Rows.Count + " hoja/s.");

                    //Verifica la cantidad de columnas que debe tener el archivo
                    if (dt.Columns.Count != 18)
                        throw new Exception("El archivo Excel indicado no posee la cantidad de columnas que debería.<br />Deben ser " + sCols.Length + " columnas, verifique con el archivo de Ejemplo.");

                    lblErrores.Text += "<br /> Verifico posee datos.";

                    //Verifica que las columnas tengan el nombre igual al formato pedido
                    for (i = 0; i <= 17; i++)
                    {
                        if (dt.Columns[i].ColumnName.ToLower() != sCols[i].ToLower())
                            ColumnasConnombreInvalido += "La columna " + dt.Columns[i].ColumnName + " debería llamarse " + sCols[i] + ".<br/>";
                    }
                    if (ColumnasConnombreInvalido.Length > 0)
                        throw new Exception("Se encontraron los siguientes inconvenientes con los nombres de las columnas:<br /><br />" + ColumnasConnombreInvalido);

                    lblErrores.Text += "<br /> Verifico igualdad de columnas.";

                    i = 1; //arranca de 1 por la cabecera
                }
                catch (Exception ex)
                {
                    pnlValidacion.Style["display"] = "none";
                    lblErrores.Text += "<br/>Error:" + ex.Message;
                    oCnnExcel.Close();

                }


                lblErrores.Text += "<br/>Eliminó el archivo:";
                System.IO.File.Delete(filename);
            }
            catch (Exception ex)
            {
                lblErrores.Text += "<br/>Error:" + ex.Message;
            }
        }

        private bool filaSinDatos(DataRow row)
        {
            bool sinDatos = true;

            if (row == null)
                return sinDatos;

            int nroCol = 0;
            for (nroCol = 0; nroCol < row.ItemArray.Length; nroCol++)
            {
                if (row.ItemArray[nroCol] != DBNull.Value)
                {
                    sinDatos = false;
                    break;
                }

            }

            return sinDatos;
        }
    }
}