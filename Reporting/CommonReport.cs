using System;
using System.Data;
using System.IO;
using System.Web;
using DataAcess;
using Reporting.CommonClass;
using Reporting.Datasets;
using System.Drawing;
using CrystalDecisions.Web;
using BusinesLayer.Implementation;
using System.Collections.Generic;
using DataTransferObject;
using System.Linq;
using ThoughtWorks.QRCode.Codec;
using StaticClass;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Forms;
using ExternalService;
namespace Reporting
{
    public class CommonReport
    {
        public static byte[] GetPDFObleaSolicitud(int id_solicitud, string urlOblea)
        {
           
            byte[] ret = new byte[0];
            CrystalDecisions.Web.CrystalReportSource CrystalReportSource1 = new CrystalDecisions.Web.CrystalReportSource();
            Stream msArchivo = null;
            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();
            dsOblea dsOblea = new dsOblea();
            int Seccion = 0;
            string Manzana = "";
            string Parcela = "";
            int PartidaMatriz = 0;
         

            byte[] QrCode = null;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 7;      // 316 x 316 pixels 
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L; ;
            if (urlOblea != null)
            {
                System.Drawing.Image imagenQr = qrCodeEncoder.Encode(urlOblea);
                QrCode = imageToByteArray(imagenQr);

            }
            string plantas = "";

            EncomiendaBL encBL = new EncomiendaBL();
            List<EncomiendaPlantasDTO> lstEncPlantasDTO = encBL.GetByFKIdSolicitud(id_solicitud)
                                                        .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                                        .OrderByDescending(o => o.IdEncomienda)
                                                        .FirstOrDefault()
                                                        .EncomiendaPlantasDTO
                                                        .ToList();


            foreach (EncomiendaPlantasDTO encPlantasDTO in lstEncPlantasDTO)
            {
                if (string.IsNullOrEmpty(plantas))
                    plantas = encPlantasDTO.TipoSectorDTO.Nombre + " " + encPlantasDTO.detalle_encomiendatiposector.Trim();
                else
                    plantas = plantas + ", " + encPlantasDTO.TipoSectorDTO.Nombre + " " + encPlantasDTO.detalle_encomiendatiposector.Trim();
            }
            
            dsOblea.Oblea.Rows.Add(id_solicitud, QrCode, plantas);
            SSITSolicitudesUbicacionesBL blUbi = new SSITSolicitudesUbicacionesBL();
            var ubi = blUbi.GetByFKIdSolicitud(id_solicitud);
        
            foreach (var item2 in ubi)
            {
                string Direccion = "";
                string PartidaHor = "";
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                List<int> lisSol = new List<int>();
                lisSol.Add(id_solicitud);
                foreach (var item in blSol.GetDireccionesSSIT(lisSol).ToList())
                    Direccion += item.direccion + " / ";
                if (Direccion.Length > 1)
                    Direccion = Direccion.Substring(0, Direccion.Length - 3);
                Seccion = item2.UbicacionesDTO.Seccion.HasValue ?  item2.UbicacionesDTO.Seccion.Value : 0;
                Manzana = item2.UbicacionesDTO.Manzana;
                Parcela = item2.UbicacionesDTO.Parcela;
                PartidaMatriz = item2.UbicacionesDTO.NroPartidaMatriz.HasValue ? item2.UbicacionesDTO.NroPartidaMatriz.Value :  0;

                SSITSolicitudesUbicacionesPropiedadHorizontalBL blSolHor = new SSITSolicitudesUbicacionesPropiedadHorizontalBL();
                var Solhor = blSolHor.GetByFKIdSolicitudUbicacion(item2.IdSolicitudUbicacion);
                UbicacionesPropiedadhorizontalBL blHor = new UbicacionesPropiedadhorizontalBL();
                foreach (var hor in Solhor)
                {
                    var h = blHor.Single(hor.IdPropiedadHorizontal.Value);
                    PartidaHor = PartidaHor + " " + h.NroPartidaHorizontal.ToString() + " - Piso: " + h.Piso + " U.F.: " + h.UnidadFuncional + " / ";
                }
                if (PartidaHor.Length > 1)
                {
                    PartidaHor = PartidaHor.Substring(0, PartidaHor.Length - 3);
                }

                dsOblea.Ubicaciones.Rows.Add(id_solicitud, Seccion, Manzana, Parcela, Direccion, PartidaMatriz, PartidaHor);

            }
            try
            {
                CrystalReportSource1.EnableCaching = false;
                //CrystalReportSource1.EnableViewState = false;
                CrystalReportSource1.Report.FileName = "~/Reportes/Oblea.rpt";
                CrystalReportSource1.ReportDocument.SetDataSource(dsOblea);
                msArchivo = CrystalReportSource1.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                //se liberan recursos porque el crystal esta configurado para 65 instancias en registry
                CrystalReportSource1.ReportDocument.Close();
                CrystalReportSource1.ReportDocument.Dispose();
                CrystalReportSource1.Dispose();
            }
            catch (Exception ex)
            {
                CrystalReportSource1.ReportDocument.Close();
                CrystalReportSource1.ReportDocument.Dispose();
                CrystalReportSource1.Dispose();

                throw ex;
            }

            ret = Functions.StreamToArray(msArchivo);
            msArchivo.Dispose();


            return ret;
        }

        private static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public static Stream ImprimirCertificadoEncomiendaAnt(int id_encomienda)
        {
            Stream documento = null;
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var dsEncomienda = encomiendaBL.GetEncomiendaAntenas(id_encomienda);

            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            dsComprobanteAnt dsCertificado = new dsComprobanteAnt();

            var cmd = db.Database.Connection.CreateCommand();
            cmd.CommandText = string.Format("EXEC Consejos_Imprimir_Certificacion_EncomiendaAnt {0}", id_encomienda);

            db.Database.Connection.Open();

            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            var profesionalDTO = profesionalesBL.Get(dsEncomienda.CreateUser);

            string imgFile = HttpRuntime.AppDomainAppPath + "Common\\Logos\\" + profesionalDTO.ConsejoProfesionalDTO.GrupoConsejosDTO.LogoImpresion;
            byte[] Logo = Funciones.ImageToByte(System.Drawing.Image.FromFile(imgFile));

            using (var reader = cmd.ExecuteReader())
            {
                dsCertificado.Tables["DatosCabecera"].Load(reader);
                dsCertificado.Tables["ANT_Ubicaciones"].Load(reader);
                dsCertificado.Tables["ANT_Ubicaciones_PropiedadHorizontal"].Load(reader);
                dsCertificado.Tables["ANT_Ubicaciones_Puertas"].Load(reader);
                dsCertificado.Tables["ANT_Ubicaciones_Via_Publica"].Load(reader);
                dsCertificado.Tables["ANT_Ubicaciones_Coordenadas"].Load(reader);
                dsCertificado.Tables["ANT_Titulares"].Load(reader);

                int id_profesional = Convert.ToInt32(dsCertificado.Tables["DatosCabecera"].Rows[0]["id_profesional"].ToString());
                dsCertificado.Tables["DatosCabecera"].Rows[0]["logo_consejo"] = Logo;

                CrystalDecisions.Web.CrystalReportSource CrystalReportSource1 = new CrystalDecisions.Web.CrystalReportSource();

                try
                {
                    CrystalReportSource1.EnableCaching = false;
                    CrystalReportSource1.EnableViewState = false;
                    CrystalReportSource1.Report.FileName = "~/Reportes/CertificadoEncomiendaAnt.rpt";
                    CrystalReportSource1.ReportDocument.SetDataSource(dsCertificado);
                    documento = CrystalReportSource1.ReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    //se liberan recursos porque el crystal esta configurado para 65 instancias en registry
                    CrystalReportSource1.ReportDocument.Close();
                    CrystalReportSource1.ReportDocument.Dispose();
                    CrystalReportSource1.Dispose();

                }
                catch (Exception ex)
                {
                    try
                    {
                        CrystalReportSource1.ReportDocument.Close();
                        CrystalReportSource1.ReportDocument.Dispose();
                        CrystalReportSource1.Dispose();
                    }
                    catch { }

                    throw ex;
                }
            }

            if (documento == null)
                throw new Exception("No se pudo generar pdf certificado.");

            return documento;
        }

        public static Stream GenerarCertificadoExtConsejo(int tipo_tramite, int nro_tramite)
        {
            Stream documento = null;

            try
            {
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var dsEncomienda = encomiendaBL.GetEncomiendaExterna(nro_tramite, tipo_tramite);

                int id_encomienda = dsEncomienda.IdEncomienda;

                byte[] pdfCertificado = new byte[0];
                int id_fileCert = 0;
                string extension = string.Empty;
                ExternalServiceReporting reportingService = new ExternalServiceReporting();
                ExternalService.ExternalServiceFiles files = new ExternalService.ExternalServiceFiles();

                if ((tipo_tramite == (int)Constantes.TipoCertificado.EncomiendaLey257 ||
                    tipo_tramite == (int)Constantes.TipoCertificado.Formulario_inscripción_demoledores_excavadores)
                    && dsEncomienda.id_file != null)
                {                    
                    documento = new MemoryStream(files.downloadFile((int)dsEncomienda.id_file, out extension));
                    return documento;
                }
                else if (tipo_tramite == (int)(int)Constantes.TipoCertificado.Formulario_inscripción_demoledores_excavadores)
                {
                    var ReportingEntityCertificado = reportingService.GetPDFCertificadoExtConsejoEncomiendaDeEx(id_encomienda, true);
                    pdfCertificado = ReportingEntityCertificado.Reporte;
                    id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                }
                else
                {
                    var ReportingEntityCertificado = reportingService.GetPDFCertificadoExtConsejoEncomienda(id_encomienda, true);
                    pdfCertificado = ReportingEntityCertificado.Reporte;
                    id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                }

                if (pdfCertificado != null)
                    encomiendaBL.ActualizarEncomiendaEx_Estado(id_encomienda, dsEncomienda.IdEstado, dsEncomienda.CreateUser, id_fileCert);

                documento = new MemoryStream(files.downloadFile(id_fileCert, out extension));
                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static dsComprobanteExt CargarDatosImpresionCertificadoExterno(int id_encomienda, byte[] LogoConsejo)
        {
            dsComprobanteExt dsRep = new dsComprobanteExt();

            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            var cmd = db.Database.Connection.CreateCommand();
            db.Database.Connection.Open();


            StringBuilder qry = new StringBuilder();
            qry.AppendLine("SELECT");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  enc.nroTramite,");
            qry.AppendLine("  enc.FechaEncomienda,");
            qry.AppendLine("  enc.nroEncomiendaconsejo,");
            qry.AppendLine("  enc.tipoTramite,");
            qry.AppendLine("  prof.apellido as ApellidoProfesional,");
            qry.AppendLine("  prof.nombre as NombresProfesional,");
            qry.AppendLine("  prof.matricula as MatriculaProfesional,");
            qry.AppendLine("  grucon.descripcion_grupoconsejo as ConsejoProfesional,");
            qry.AppendLine("  enc.CodigoSeguridad,");
            qry.AppendLine("  tipo_doc.Nombre as tipoDocProfesional, prof.NroDocumento as nroDocProfesional");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN profesional prof ON enc.id_profesional = prof.id");
            qry.AppendLine("  INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id");
            qry.AppendLine("  INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo");
            qry.AppendLine("  INNER JOIN TipoDocumentoPersonal tipo_doc ON tipo_doc.TipoDocumentoPersonalId = prof.IdTipoDocumento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            var reader = cmd.ExecuteReader();
            dsRep.DatosCabecera.Load(reader);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  mat.Seccion,");
            qry.AppendLine("  mat.Manzana,");
            qry.AppendLine("  mat.parcela,");
            qry.AppendLine("  mat.NroPartidaMatriz as NroPartidaMatriz,");
            qry.AppendLine("  encubic.local_subtipoubicacion,");
            qry.AppendLine("  zon1.codzonapla as ZonaParcela,");
            qry.AppendLine("  dbo.EncomiendaExt_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda, encubic.id_ubicacion) as Direcciones,");
            qry.AppendLine("  encubic.deptoLocal_encomiendaubicacion as DeptoLocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN EncomiendaExt_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda");
            qry.AppendLine("  INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion");
            qry.AppendLine("  INNER JOIN Zonas_Planeamiento zon1 ON  encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();

            dsRep.Ubicaciones.Load(reader);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  phor.NroPartidaHorizontal as NroPartidaHorizontal,");
            qry.AppendLine("  phor.piso,");
            qry.AppendLine("  phor.depto");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  LEFT JOIN EncomiendaExt_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion ");
            qry.AppendLine("  LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();

            dsRep.PropiedadHorizontal.Load(reader);
            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encpuer.id_encomiendapuerta,");
            qry.AppendLine("  encubic.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  encpuer.nombre_calle as Calle,");
            qry.AppendLine("  encpuer.NroPuerta");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  LEFT JOIN EncomiendaExt_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();

            dsRep.Puertas.Load(reader);

            //--------------------------------------------------------
            qry.Clear();

            qry.AppendLine("SELECT ");
            qry.AppendLine("  pf.id_personafisica as id_persona, 'PF' as TipoPersona, pf.id_encomienda, ");
            qry.AppendLine("  '' as RazonSocial, ");
            qry.AppendLine("  pf.Apellido, pf.Nombres, tipo_doc.nombre as TipoDoc, pf.Nro_Documento as NroDoc,");
            qry.AppendLine("  pf.Calle, pf.NroPuerta, ");
            qry.AppendLine("	pf.cuit, ");
            qry.AppendLine("  pf.Email,");
            qry.AppendLine("	0 as MuestraEnTitulares, 1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Titulares_PersonasFisicas pf");
            qry.AppendLine("INNER JOIN  TipoDocumentoPersonal tipo_doc ON tipo_doc.TipoDocumentoPersonalId = pf.id_tipodoc_personal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  pf.id_encomienda = " + id_encomienda);

            qry.AppendLine("UNION ALL ");

            qry.AppendLine("SELECT pj.id_personajuridica as id_persona, 'PJ' as TipoPersona, pj.id_encomienda,");
            qry.AppendLine("	UPPER(pj.Razon_Social) as RazonSocial, ");
            qry.AppendLine("	'' as Apellido,''as Nombres, '' as TipoDoc,  '0' as NroDoc,");
            qry.AppendLine("  pj.Calle, pj.NroPuerta,  ");
            qry.AppendLine("	pj.cuit, ");
            qry.AppendLine("  pj.Email, ");
            qry.AppendLine("	0 as MuestraEnTitulares, 1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Titulares_PersonasJuridicas pj");
            qry.AppendLine("WHERE");
            qry.AppendLine("  pj.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY 1");

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();

            dsRep.Titulares.Load(reader);

            //--------------------------------------------------------
            DataTable dtLogo = new DataTable();

            dtLogo.Columns.Add("logo_agc", typeof(Byte[]));
            dtLogo.Columns.Add("logo_consejo", typeof(Byte[]));

            DataRow row = dtLogo.NewRow();
            row["logo_agc"] = LogoConsejo;
            row["logo_consejo"] = LogoConsejo;
            dtLogo.Rows.Add(row);


            dsRep.Logos.Merge(dtLogo, true, MissingSchemaAction.Ignore);

            return dsRep;
        }
        private static dsComprobanteExtDeEx CargarDatosImpresionCertificadoExternoDeEx(int id_encomienda, byte[] LogoConsejo)
        {
            dsComprobanteExtDeEx dsRep = new dsComprobanteExtDeEx();
            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            var cmd = db.Database.Connection.CreateCommand();
            db.Database.Connection.Open();

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("SELECT");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  enc.nroTramite,");
            qry.AppendLine("  enc.FechaEncomienda,");
            qry.AppendLine("  enc.nroEncomiendaconsejo,");
            qry.AppendLine("  enc.tipoTramite,");
            qry.AppendLine("  prof.apellido as ApellidoProfesional,");
            qry.AppendLine("  prof.nombre as NombresProfesional,");
            qry.AppendLine("  prof.matricula as MatriculaProfesional,");
            qry.AppendLine("  prof.Titulo as tituloProfesional,");
            qry.AppendLine("  grucon.descripcion_grupoconsejo as ConsejoProfesional,");
            qry.AppendLine("  enc.CodigoSeguridad,");
            qry.AppendLine("  tipo_doc.Nombre as tipoDocProfesional, prof.NroDocumento as nroDocProfesional");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN profesional prof ON enc.id_profesional = prof.id");
            qry.AppendLine("  INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id");
            qry.AppendLine("  INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo");
            qry.AppendLine("  INNER JOIN TipoDocumentoPersonal tipo_doc ON tipo_doc.TipoDocumentoPersonalId = prof.IdTipoDocumento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            var reader = cmd.ExecuteReader();

            dsRep.DatosCabecera.Load(reader);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  mat.Seccion,");
            qry.AppendLine("  mat.Manzana,");
            qry.AppendLine("  mat.parcela,");
            qry.AppendLine("  mat.NroPartidaMatriz as NroPartidaMatriz,");
            qry.AppendLine("  encubic.local_subtipoubicacion,");
            qry.AppendLine("  zon1.codzonapla as ZonaParcela,");
            qry.AppendLine("  dbo.EncomiendaExt_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda, encubic.id_ubicacion) as Direcciones,");
            qry.AppendLine("  encubic.deptoLocal_encomiendaubicacion as DeptoLocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN EncomiendaExt_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda");
            qry.AppendLine("  INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion");
            qry.AppendLine("  INNER JOIN Zonas_Planeamiento zon1 ON  encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();
            dsRep.Ubicaciones.Load(reader);

            //dsRep.Tables.Add(qry.Execute().Tables[0].Copy());
            //dsRep.Tables[1].TableName = "EncomiendaExt_Ubicaciones";

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  phor.NroPartidaHorizontal as NroPartidaHorizontal,");
            qry.AppendLine("  phor.piso,");
            qry.AppendLine("  phor.depto");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  LEFT JOIN EncomiendaExt_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion ");
            qry.AppendLine("  LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();
            dsRep.PropiedadHorizontal.Load(reader);


            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encpuer.id_encomiendapuerta,");
            qry.AppendLine("  encubic.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  encpuer.nombre_calle as Calle,");
            qry.AppendLine("  encpuer.NroPuerta");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  LEFT JOIN EncomiendaExt_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();
            dsRep.Puertas.Load(reader);

            qry.Clear();

            qry.AppendLine("SELECT ");
            qry.AppendLine("  e.TipoEmpresa, e.id_encomienda, ");
            qry.AppendLine("  e.RazonSocial, ");
            qry.AppendLine("  e.Calle, e.NroPuerta, ");
            qry.AppendLine("	e.cuit, ");
            qry.AppendLine("  e.Depto,");
            qry.AppendLine("  e.Piso");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Empresas e");
            qry.AppendLine("WHERE");
            qry.AppendLine("  e.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY 1");

            cmd.CommandText = qry.ToString();
            reader = cmd.ExecuteReader();

            dsRep.Empresas.Load(reader);

            DataTable dtLogo = new DataTable();

            dtLogo.Columns.Add("logo_agc", typeof(Byte[]));
            dtLogo.Columns.Add("logo_consejo", typeof(Byte[]));

            DataRow row = dtLogo.NewRow();
            row["logo_agc"] = LogoConsejo;
            row["logo_consejo"] = LogoConsejo;
            dtLogo.Rows.Add(row);

            dsRep.Logos.Merge(dtLogo, true, MissingSchemaAction.Ignore);


            return dsRep;

        }
        private static dsComprobante CargarDatosImpresionCertificado(int id_encomienda, byte[] Logo)
        {
            dsComprobante dsRep = new dsComprobante(); 

            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            var cmd = db.Database.Connection.CreateCommand();
            db.Database.Connection.Open();

            StringBuilder qry = new StringBuilder();

            string strPlantasHabilitar = "";

            qry.Clear();
            qry.AppendLine("SELECT DISTINCT ");
            qry.AppendLine("      tipsec.id as id_tiposector, ");
            qry.AppendLine("      convert(bit,CASE WHEN encplan.id_tiposector > 0 THEN 1 else 0 END) as Seleccionado,");
            qry.AppendLine("      tipsec.descripcion,");
            qry.AppendLine("      convert(bit,IsNull(tipsec.MuestraCampoAdicional,0)) as MuestraCampoAdicional,");
            qry.AppendLine("      encplan.detalle_encomiendatiposector as detalle,");
            qry.AppendLine("      IsNull(tipsec.TamanoCampoAdicional,0) as TamanoCampoAdicional");
            qry.AppendLine("FROM ");
            qry.AppendLine("  TipoSector tipsec ");
            qry.AppendLine("  INNER JOIN encomienda_plantas encplan ON tipsec.id = encplan.id_tiposector AND encplan.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY");
            qry.AppendLine("  tipsec.id");

            DataSet dsPlantas = new DataSet();

            var sConnection = ((SqlConnection)db.Database.Connection);

            DataTable dt = new DataTable();
                        
            SqlDataAdapter com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPlantas);

            foreach (DataRow dr in dsPlantas.Tables[0].Rows)
            {
                int TamanoCampoAdicional = 0;
                bool MuestraCampoAdicional = false;
                string separador = "";
                int.TryParse(dr["TamanoCampoAdicional"].ToString(), out TamanoCampoAdicional);
                bool.TryParse(dr["MuestraCampoAdicional"].ToString(), out MuestraCampoAdicional);

                if (strPlantasHabilitar.Length == 0)
                    separador = "";
                else
                    separador = ", ";

                if (MuestraCampoAdicional)
                {
                    if (TamanoCampoAdicional >= 10)
                        strPlantasHabilitar += separador + dr["detalle"].ToString();
                    else
                        strPlantasHabilitar += separador + dr["descripcion"].ToString() + " " + dr["detalle"].ToString();
                }
                else
                    strPlantasHabilitar += separador + dr["descripcion"].ToString();
            }

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  enc.FechaEncomienda,");
            qry.AppendLine("  enc.nroEncomiendaconsejo,");
            qry.AppendLine("  prof.apellido as ApellidoProfesional,");
            qry.AppendLine("  prof.nombre as NombresProfesional,");
            qry.AppendLine("  prof.matricula as MatriculaProfesional,");
            qry.AppendLine("  grucon.descripcion_grupoconsejo as ConsejoProfesional,");
            qry.AppendLine("  tipnorm.descripcion as TipoNormativa,");
            qry.AppendLine("  entnorm.descripcion as EntidadNormativa,");
            qry.AppendLine("  encnorm.nro_normativa as NroNormativa,");
            qry.AppendLine("  enc.CodigoSeguridad,");
            qry.AppendLine("  '" + strPlantasHabilitar + "' as PlantasHabilitar,");
            qry.AppendLine("  enc.Observaciones_plantas as ObservacionesPlantasHabilitar");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomienda enc");
            qry.AppendLine("  INNER JOIN profesional prof ON enc.id_profesional = prof.id");
            qry.AppendLine("  INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id");
            qry.AppendLine("  INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo");
            qry.AppendLine("  LEFT JOIN Encomienda_Normativas encnorm ON enc.id_encomienda = encnorm.id_encomienda");
            qry.AppendLine("  LEFT JOIN TipoNormativa tipnorm ON encnorm.id_tiponormativa = tipnorm.id");
            qry.AppendLine("  LEFT JOIN EntidadNormativa entnorm ON encnorm.id_entidadnormativa = entnorm.id");

            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            DataSet dsEncomienda = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsEncomienda);
            
            dsRep.Tables["DatosCabecera"].Merge(dsEncomienda.Tables[0]);

            foreach (DataRow dr in dsRep.Tables["DatosCabecera"].Rows)
            {
                dr["Logo"] = Logo;
            }

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  mat.Seccion,");
            qry.AppendLine("  mat.Manzana,");
            qry.AppendLine("  mat.parcela,");
            qry.AppendLine("  mat.NroPartidaMatriz as NroPartidaMatriz,");
            qry.AppendLine("  encubic.local_subtipoubicacion,");
            qry.AppendLine("  zon1.codzonapla as ZonaParcela,");
            qry.AppendLine("  dbo.Encomienda_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda,encubic.id_ubicacion) as Direcciones,");
            qry.AppendLine("  encubic.deptoLocal_encomiendaubicacion as DeptoLocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomienda enc");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda");
            qry.AppendLine("  INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion");
            qry.AppendLine("  INNER JOIN Zonas_Planeamiento zon1 ON  encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);


            DataSet dsUbicaciones = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsUbicaciones);

            dsRep.Tables["Ubicaciones"].Merge(dsUbicaciones.Tables[0]);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  phor.NroPartidaHorizontal as NroPartidaHorizontal,");
            qry.AppendLine("  phor.piso,");
            qry.AppendLine("  phor.depto");
            qry.AppendLine("FROM");
            qry.AppendLine("  Encomienda_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion ");
            qry.AppendLine("  INNER JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " +  id_encomienda);

            DataSet dsUbicaciones_PropiedadHorizontal = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsUbicaciones_PropiedadHorizontal);

            dsRep.Tables["PropiedadHorizontal"].Merge(dsUbicaciones_PropiedadHorizontal.Tables[0]);

            //--------------------------------------------------------


            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encpuer.id_encomiendapuerta,");
            qry.AppendLine("  encubic.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  encpuer.nombre_calle as Calle,");
            qry.AppendLine("  encpuer.NroPuerta");
            qry.AppendLine("FROM");
            qry.AppendLine("  Encomienda_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            DataSet dsPuertas = new DataSet(); 
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPuertas);

            dsRep.Tables["Puertas"].Merge(dsPuertas.Tables[0]);


            //--------------------------------------------------------
            // Firmantes
            //--------------------------------------------------------

            qry.Clear();

            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_firmante_pj as id_firmante, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PJ' as TipoPersona, ");
            qry.AppendLine("	UPPER(pj.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pj.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pj.Nro_Documento as NroDoc, ");
            qry.AppendLine("	tcl.nom_tipocaracter as CaracterLegal, ");
            qry.AppendLine("	UPPER(titpj.Razon_Social) as FirmanteDe");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Firmantes_PersonasJuridicas pj  ");
            qry.AppendLine("	INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pj.id_encomienda = " + id_encomienda);
            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pf.id_firmante_pf as id_firmante, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PF' as TipoPersona, ");
            qry.AppendLine("	UPPER(pf.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pf.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pf.Nro_Documento as NroDoc, ");
            qry.AppendLine("	tcl.nom_tipocaracter as CaracterLegal, ");
            qry.AppendLine("	UPPER(titpf.Apellido + ', ' + titpf.Nombres) as FirmanteDe");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Firmantes_PersonasFisicas pf  ");
            qry.AppendLine("	INNER JOIN Encomienda_Titulares_PersonasFisicas titpf ON pf.id_personafisica = titpf.id_personafisica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pf.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pf.id_encomienda = " + id_encomienda);


            DataSet dsTitularesTramite = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsTitularesTramite);

            dsRep.Tables["Firmantes"].Merge(dsTitularesTramite.Tables[0]);

            //--------------------------------------------------------
            // Titulares 
            //--------------------------------------------------------
            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_personajuridica as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PJ' as TipoPersona, ");
            qry.AppendLine("	UPPER(pj.Razon_Social) as RazonSocial, ");
            qry.AppendLine("	tsoc.descripcion as TipoSociedad, ");
            qry.AppendLine("	'' as Apellido, ");
            qry.AppendLine("	''as Nombres, ");
            qry.AppendLine("	'' as TipoDoc, ");
            qry.AppendLine("	'' as NroDoc, ");
            qry.AppendLine("  tipoiibb.nom_tipoiibb as TipoIIBB, ");
            qry.AppendLine("	pj.Nro_IIBB as NroIIBB, ");
            qry.AppendLine("	pj.cuit, ");
            qry.AppendLine("	1 as MuestraEnTitulares, ");
            qry.AppendLine("	CASE WHEN pj.id_tiposociedad = 2 THEN 0 ELSE 1 END as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Titulares_PersonasJuridicas pj ");
            qry.AppendLine("  INNER JOIN TipoSociedad tsoc ON pj.id_tiposociedad = tsoc.id ");
            qry.AppendLine("  INNER JOIN TiposDeIngresosBrutos tipoiibb ON pj.id_tipoiibb = tipoiibb.id_tipoiibb ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pj.id_encomienda = " +  id_encomienda);

            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_firmante_pj as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("  'PF' as TipoPersona, ");
            qry.AppendLine("	'' as RazonSocial, ");
            qry.AppendLine("  '' as TipoSociedad, ");
            qry.AppendLine("  UPPER(pj.Apellido) as Apellido, ");
            qry.AppendLine("  UPPER(pj.Nombres) as Nombres, ");
            qry.AppendLine("  tdoc.nombre as TipoDoc, ");
            qry.AppendLine("  pj.Nro_Documento as NroDoc, ");
            qry.AppendLine("  '' as TipoIIBB, ");
            qry.AppendLine("  '' as NroIIBB, ");
            qry.AppendLine("  '' as cuit,");
            qry.AppendLine("	0 as MuestraEnTitulares, ");
            qry.AppendLine("	1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("  Encomienda_Firmantes_PersonasJuridicas pj  ");
            qry.AppendLine("  INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE");
            qry.AppendLine("  titpj.id_encomienda =  " + id_encomienda);
            qry.AppendLine("  AND titpj.Id_TipoSociedad = 2");    // Sociedad de Hecho

            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pf.id_personafisica as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda, ");
            qry.AppendLine("	'PF' as TipoPersona, ");
            qry.AppendLine("  '' as RazonSocial, ");
            qry.AppendLine("  '' as TipoSociedad, ");
            qry.AppendLine("	UPPER(pf.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pf.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pf.Nro_Documento as NroDoc, ");
            qry.AppendLine("  tipoiibb.nom_tipoiibb as TipoIIBB, ");
            qry.AppendLine("	pf.Ingresos_Brutos as NroIIBB, ");
            qry.AppendLine("	pf.cuit,");
            qry.AppendLine("	1 as MuestraEnTitulares, ");
            qry.AppendLine("	1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Titulares_PersonasFisicas pf  ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("  INNER JOIN TiposDeIngresosBrutos tipoiibb ON pf.id_tipoiibb = tipoiibb.id_tipoiibb ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pf.id_encomienda = " +  id_encomienda);


            DataSet dsTitularesHabilitacion = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsTitularesHabilitacion);

            dsRep.Tables["Titulares"].Merge(dsTitularesHabilitacion.Tables[0]);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("	rub.id_encomiendarubro,");
            qry.AppendLine("	enc.id_encomienda,");
            qry.AppendLine("	rub.cod_rubro,");
            qry.AppendLine("	rub.desc_rubro,");
            qry.AppendLine("	rub.EsAnterior,");
            qry.AppendLine("	tact.nombre as TipoActividad,");
            qry.AppendLine("	docreq.nomenclatura as DocRequerida,");
            qry.AppendLine("	rub.SuperficieHabilitar");
            qry.AppendLine("FROM");
            qry.AppendLine("   encomienda enc");
            qry.AppendLine("   INNER JOIN Encomienda_rubros rub ON enc.id_encomienda = rub.id_encomienda");
            qry.AppendLine("   INNER JOIN tipoactividad tact ON rub.id_tipoactividad = tact.id");
            qry.AppendLine("   INNER JOIN Tipo_Documentacion_Req docreq ON rub.id_tipodocreq = docreq.id");

            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);

            DataSet dsRubros = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsRubros);


            dsRep.Tables["Rubros"].Merge(dsRubros.Tables[0]);

            //--------------------------------------------------------


            return dsRep;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <param name="Logo"></param>
        /// <param name="pReporte"></param>
        /// <returns></returns>
        public static dsImpresionEncomienda CargarDatosImpresionEncomienda(int id_encomienda, byte[] Logo, bool pReporte)
        {
            dsImpresionEncomienda dsRep = new dsImpresionEncomienda();

            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            var cmd = db.Database.Connection.CreateCommand();
            db.Database.Connection.Open();

            StringBuilder qry = new StringBuilder();

            string strPlantasHabilitar = "";

            qry.Clear();
            qry.AppendLine("SELECT DISTINCT ");
            qry.AppendLine("      tipsec.id as id_tiposector, ");
            qry.AppendLine("      convert(bit,CASE WHEN encplan.id_tiposector > 0 THEN 1 else 0 END) as Seleccionado,");
            qry.AppendLine("      tipsec.descripcion,");
            qry.AppendLine("      convert(bit,IsNull(tipsec.MuestraCampoAdicional,0)) as MuestraCampoAdicional,");
            qry.AppendLine("      encplan.detalle_encomiendatiposector as detalle,");
            qry.AppendLine("      IsNull(tipsec.TamanoCampoAdicional,0) as TamanoCampoAdicional");
            qry.AppendLine("FROM ");
            qry.AppendLine("  TipoSector tipsec ");
            qry.AppendLine("  INNER JOIN encomienda_plantas encplan ON tipsec.id = encplan.id_tiposector AND encplan.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY");
            qry.AppendLine("  tipsec.id");

            var sConnection = ((SqlConnection)db.Database.Connection);

            DataSet dsPlantas = new DataSet(); ;
            SqlDataAdapter com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPlantas);


            foreach (DataRow dr in dsPlantas.Tables[0].Rows)
            {
                int TamanoCampoAdicional = 0;
                bool MuestraCampoAdicional = false;
                string separador = "";
                int.TryParse(dr["TamanoCampoAdicional"].ToString(), out TamanoCampoAdicional);
                bool.TryParse(dr["MuestraCampoAdicional"].ToString(), out MuestraCampoAdicional);

                if (strPlantasHabilitar.Length == 0)
                    separador = "";
                else
                    separador = ", ";

                if (MuestraCampoAdicional)
                {
                    if (TamanoCampoAdicional >= 10)
                        strPlantasHabilitar += separador + dr["detalle"].ToString();
                    else
                        strPlantasHabilitar += separador + dr["descripcion"].ToString() + " " + dr["detalle"].ToString();
                }
                else
                    strPlantasHabilitar += separador + dr["descripcion"].ToString();
            }


            int id_encomienda_anterior = 0;
            string nroExpediente = "";

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("	rel.id_encomienda_anterior, ");
            qry.AppendLine("	IsNull(sol.NroExpediente,'') as NroExpediente ");
            qry.AppendLine("FROM  ");
            qry.AppendLine("	Rel_Encomienda_Rectificatoria rel ");
            qry.AppendLine("	LEFT JOIN SSIT_Solicitudes sol ON rel.id_solicitud_anterior = sol.id_solicitud ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	rel.id_encomienda_nueva = " +  id_encomienda);

            DataSet ds = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int.TryParse(dr["id_encomienda_anterior"].ToString(), out id_encomienda_anterior);
                nroExpediente = dr["NroExpediente"].ToString();
            }


            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  enc.FechaEncomienda,");
            qry.AppendLine("  enc.nroEncomiendaconsejo,");
            qry.AppendLine("  enc.ZonaDeclarada,");
            qry.AppendLine("  tt.cod_tipotramite as TipoDeTramite,");
            qry.AppendLine("  te.cod_tipoexpediente as TipoDeExpediente,");
            qry.AppendLine("  ste.cod_subtipoexpediente as SubTipoDeExpediente,");
            qry.AppendLine("  prof.matricula as MatriculaProfesional,");
            qry.AppendLine("  prof.apellido as ApellidoProfesional,");
            qry.AppendLine("  prof.nombre as NombresProfesional,");
            if (pReporte)
                qry.AppendLine("  grucon.id_grupoconsejo,");
            qry.AppendLine("  grucon.nombre_grupoconsejo as ConsejoProfesional,");
            qry.AppendLine("  tipnorm.descripcion as TipoNormativa,");
            qry.AppendLine("  entnorm.descripcion as EntidadNormativa,");
            qry.AppendLine("  encnorm.nro_normativa as NroNormativa,");
            qry.AppendLine("  grucon.logo_impresion_grupoconsejo as LogoUrl,");
            qry.AppendLine("  CASE WHEN enc.id_estado <= 1 THEN convert(bit,1) ELSE convert(bit,0) END as ImpresionDePrueba,");
            qry.AppendLine("  '" + strPlantasHabilitar + "' as PlantasHabilitar,");
            qry.AppendLine("  enc.Observaciones_plantas as ObservacionesPlantasHabilitar,");
            qry.AppendLine("  enc.Observaciones_rubros as ObservacionesRubros,");
            qry.AppendLine("  " + id_encomienda_anterior.ToString() + " as id_encomienda_anterior,");
            qry.AppendLine("  '" + nroExpediente + "' as NroExpediente");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomienda enc");
            qry.AppendLine("  INNER JOIN tipotramite tt ON enc.id_tipotramite = tt.id_tipotramite");
            qry.AppendLine("  INNER JOIN tipoexpediente te ON enc.id_tipoexpediente = te.id_tipoexpediente");
            qry.AppendLine("  INNER JOIN subtipoexpediente ste ON enc.id_subtipoexpediente = ste.id_subtipoexpediente");
            qry.AppendLine("  INNER JOIN profesional prof ON enc.id_profesional = prof.id");
            qry.AppendLine("  INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id");
            qry.AppendLine("  INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo");
            qry.AppendLine("  LEFT JOIN Encomienda_Normativas encnorm ON enc.id_encomienda = encnorm.id_encomienda");
            qry.AppendLine("  LEFT JOIN TipoNormativa tipnorm ON encnorm.id_tiponormativa = tipnorm.id");
            qry.AppendLine("  LEFT JOIN EntidadNormativa entnorm ON encnorm.id_entidadnormativa = entnorm.id");


            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            DataSet dsEncomienda = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsEncomienda);

            dsRep.Tables["Encomienda"].Merge(dsEncomienda.Tables[0]);

            foreach (DataRow dr in dsRep.Tables["Encomienda"].Rows)
            {
                dr["Logo"] = Logo;
            }

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  mat.Seccion,");
            qry.AppendLine("  mat.Manzana,");
            qry.AppendLine("  mat.parcela,");
            qry.AppendLine("  mat.NroPartidaMatriz as NroPartidaMatriz,");
            qry.AppendLine("  encubic.local_subtipoubicacion,");
            qry.AppendLine("  zon1.codzonapla as ZonaParcela,");
            qry.AppendLine("  dbo.Encomienda_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda,encubic.id_ubicacion) as Direcciones,");
            qry.AppendLine("  encubic.deptoLocal_encomiendaubicacion as DeptoLocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomienda enc");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda");
            qry.AppendLine("  INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion");
            qry.AppendLine("  INNER JOIN Zonas_Planeamiento zon1 ON  encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            
            DataSet dsUbicaciones = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsUbicaciones);

            dsRep.Tables["Ubicaciones"].Merge(dsUbicaciones.Tables[0]);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  phor.NroPartidaHorizontal as NroPartidaHorizontal,");
            qry.AppendLine("  phor.piso,");
            qry.AppendLine("  phor.depto");
            qry.AppendLine("FROM");
            qry.AppendLine("  Encomienda_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion ");
            qry.AppendLine("  INNER JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " +  id_encomienda);

            DataSet dsUbicaciones_PropiedadHorizontal = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsUbicaciones_PropiedadHorizontal);

            dsRep.Tables["PropiedadHorizontal"].Merge(dsUbicaciones_PropiedadHorizontal.Tables[0]);

            //--------------------------------------------------------


            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encpuer.id_encomiendapuerta,");
            qry.AppendLine("  encubic.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  encpuer.nombre_calle as Calle,");
            qry.AppendLine("  encpuer.NroPuerta");
            qry.AppendLine("FROM");
            qry.AppendLine("  Encomienda_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN Encomienda_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            DataSet dsPuertas = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPuertas);


            dsRep.Tables["Puertas"].Merge(dsPuertas.Tables[0]);
            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  conf.id_encomiendaconflocal,");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  dest.nombre as Destino,");
            qry.AppendLine("  conf.largo_conflocal,");
            qry.AppendLine("  conf.ancho_conflocal,");
            qry.AppendLine("  conf.alto_conflocal,");
            qry.AppendLine("  conf.Paredes_conflocal,");
            qry.AppendLine("  conf.Techos_conflocal,");
            qry.AppendLine("  conf.Pisos_conflocal,");
            qry.AppendLine("  conf.Frisos_conflocal,");
            qry.AppendLine("  conf.Observaciones_conflocal");
            if (pReporte)
                qry.AppendLine("  ,conf.Detalle_conflocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomienda enc");
            qry.AppendLine("  INNER JOIN Encomienda_ConformacionLocal conf ON enc.id_encomienda = conf.id_encomienda");
            qry.AppendLine("  INNER JOIN TipoDestino dest ON conf.id_destino = dest.id");

            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);

            DataSet dsConformacionLocal = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsConformacionLocal);

            dsRep.Tables["ConformacionLocal"].Merge(dsConformacionLocal.Tables[0]);

            //--------------------------------------------------------
            // Firmantes
            //--------------------------------------------------------

            qry.Clear();

            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_firmante_pj as id_firmante, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PJ' as TipoPersona, ");
            qry.AppendLine("	UPPER(pj.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pj.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pj.Nro_Documento as NroDoc, ");
            qry.AppendLine("	tcl.nom_tipocaracter as CaracterLegal, ");
            if (pReporte)
                qry.AppendLine("	pj.cargo_firmante_pj, ");
            qry.AppendLine("	UPPER(titpj.Razon_Social) as FirmanteDe");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Firmantes_PersonasJuridicas pj  ");
            qry.AppendLine("	INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pj.id_encomienda = " + id_encomienda);
            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pf.id_firmante_pf as id_firmante, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PF' as TipoPersona, ");
            qry.AppendLine("	UPPER(pf.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pf.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pf.Nro_Documento as NroDoc, ");
            qry.AppendLine("	tcl.nom_tipocaracter as CaracterLegal, ");
            if (pReporte)
                qry.AppendLine("	'' as cargo_firmante_pj, ");
            qry.AppendLine("	UPPER(titpf.Apellido + ', ' + titpf.Nombres) as FirmanteDe");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Firmantes_PersonasFisicas pf  ");
            qry.AppendLine("	INNER JOIN Encomienda_Titulares_PersonasFisicas titpf ON pf.id_personafisica = titpf.id_personafisica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pf.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pf.id_encomienda = " + id_encomienda);


            DataSet dsTitularesTramite = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsTitularesTramite);

            dsRep.Tables["Firmantes"].Merge(dsTitularesTramite.Tables[0]);

            //--------------------------------------------------------
            // Titulares 
            //--------------------------------------------------------
            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_personajuridica as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("	'PJ' as TipoPersona, ");
            qry.AppendLine("	UPPER(pj.Razon_Social) as RazonSocial, ");
            qry.AppendLine("	tsoc.descripcion as TipoSociedad, ");
            qry.AppendLine("	'' as Apellido, ");
            qry.AppendLine("	''as Nombres, ");
            qry.AppendLine("	'' as TipoDoc, ");
            qry.AppendLine("	'' as NroDoc, ");
            qry.AppendLine("  tipoiibb.nom_tipoiibb as TipoIIBB, ");
            qry.AppendLine("	pj.Nro_IIBB as NroIIBB, ");
            qry.AppendLine("	pj.cuit, ");
            qry.AppendLine("	1 as MuestraEnTitulares, ");
            qry.AppendLine("	CASE WHEN pj.id_tiposociedad = 2 THEN 0 ELSE 1 END as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Titulares_PersonasJuridicas pj ");
            qry.AppendLine("  INNER JOIN TipoSociedad tsoc ON pj.id_tiposociedad = tsoc.id ");
            qry.AppendLine("  INNER JOIN TiposDeIngresosBrutos tipoiibb ON pj.id_tipoiibb = tipoiibb.id_tipoiibb ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pj.id_encomienda = " + id_encomienda);

            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pj.id_firmante_pj as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda,");
            qry.AppendLine("  'PF' as TipoPersona, ");
            qry.AppendLine("	'' as RazonSocial, ");
            qry.AppendLine("  '' as TipoSociedad, ");
            qry.AppendLine("  UPPER(pj.Apellido) as Apellido, ");
            qry.AppendLine("  UPPER(pj.Nombres) as Nombres, ");
            qry.AppendLine("  tdoc.nombre as TipoDoc, ");
            qry.AppendLine("  pj.Nro_Documento as NroDoc, ");
            qry.AppendLine("  '' as TipoIIBB, ");
            qry.AppendLine("  '' as NroIIBB, ");
            qry.AppendLine("  '' as cuit,");
            qry.AppendLine("	0 as MuestraEnTitulares, ");
            qry.AppendLine("	1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("  Encomienda_Firmantes_PersonasJuridicas pj  ");
            qry.AppendLine("  INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica");
            qry.AppendLine("  INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("WHERE");
            qry.AppendLine("  titpj.id_encomienda =  " + id_encomienda);
            qry.AppendLine("  AND titpj.Id_TipoSociedad = 2");    // Sociedad de Hecho

            qry.AppendLine("UNION ALL ");
            qry.AppendLine("SELECT ");
            qry.AppendLine("	pf.id_personafisica as id_persona, ");
            qry.AppendLine("  " + id_encomienda.ToString() + " as id_encomienda, ");
            qry.AppendLine("	'PF' as TipoPersona, ");
            qry.AppendLine("  '' as RazonSocial, ");
            qry.AppendLine("  '' as TipoSociedad, ");
            qry.AppendLine("	UPPER(pf.Apellido) as Apellido, ");
            qry.AppendLine("	UPPER(pf.Nombres) as Nombres, ");
            qry.AppendLine("	tdoc.nombre as TipoDoc, ");
            qry.AppendLine("	pf.Nro_Documento as NroDoc, ");
            qry.AppendLine("  tipoiibb.nom_tipoiibb as TipoIIBB, ");
            qry.AppendLine("	pf.Ingresos_Brutos as NroIIBB, ");
            qry.AppendLine("	pf.cuit,");
            qry.AppendLine("	1 as MuestraEnTitulares, ");
            qry.AppendLine("	1 as MuestraEnPlancheta ");
            qry.AppendLine("FROM ");
            qry.AppendLine("	Encomienda_Titulares_PersonasFisicas pf  ");
            qry.AppendLine("  INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId ");
            qry.AppendLine("  INNER JOIN TiposDeIngresosBrutos tipoiibb ON pf.id_tipoiibb = tipoiibb.id_tipoiibb ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("	pf.id_encomienda = " + id_encomienda);

            DataSet dsTitularesHabilitacion = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsTitularesHabilitacion);

            dsRep.Tables["Titulares"].Merge(dsTitularesHabilitacion.Tables[0]);
            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("	rub.id_encomiendarubro,");
            qry.AppendLine("	enc.id_encomienda,");
            qry.AppendLine("	rub.cod_rubro,");
            qry.AppendLine("	UPPER(rub.desc_rubro) as desc_rubro,");
            qry.AppendLine("	rub.EsAnterior,");
            qry.AppendLine("	tact.nombre as TipoActividad,");
            qry.AppendLine("	docreq.nomenclatura as DocRequerida,");
            qry.AppendLine("	rub.SuperficieHabilitar");
            qry.AppendLine("FROM");
            qry.AppendLine("   encomienda enc");
            qry.AppendLine("   INNER JOIN Encomienda_rubros rub ON enc.id_encomienda = rub.id_encomienda");
            qry.AppendLine("   INNER JOIN tipoactividad tact ON rub.id_tipoactividad = tact.id");
            qry.AppendLine("   INNER JOIN Tipo_Documentacion_Req docreq ON rub.id_tipodocreq = docreq.id");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);
            qry.AppendLine("UNION ALL");
            qry.AppendLine("SELECT");
            qry.AppendLine("	rub.id_encomiendarubro,");
            qry.AppendLine("	enc.id_encomienda,");
            qry.AppendLine("	rub.codigorubro,");
            qry.AppendLine("	UPPER(rub.nombrerubro) as desc_rubro,");
            qry.AppendLine("	null,");
            qry.AppendLine("	tact.nombre as TipoActividad,");
            qry.AppendLine("	grupo.cod_grupo_circuito as DocRequerida,");
            qry.AppendLine("	rub.SuperficieHabilitar");
            qry.AppendLine("FROM");
            qry.AppendLine("   encomienda enc");
            qry.AppendLine("   INNER JOIN Encomienda_rubrosCN rub ON enc.id_encomienda = rub.id_encomienda");
            qry.AppendLine("   INNER JOIN tipoactividad tact ON rub.idtipoactividad = tact.id");
            qry.AppendLine("   INNER JOIN RubrosCN rubCN ON rub.codigorubro = rubCN.codigo");
            qry.AppendLine("   INNER JOIN ENG_Grupos_Circuitos grupo ON rubCN.idGrupoCircuito = grupo.id_grupo_circuito");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " + id_encomienda);

            DataSet dsRubros = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsRubros);

            dsRep.Tables["Rubros"].Merge(dsRubros.Tables[0]);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("	sob.id_sobrecarga,");
            qry.AppendLine("	enc.id_encomienda,");
            if (pReporte)
                qry.AppendLine("	dl.id_encomiendadatoslocal as id_datoslocal,");
            qry.AppendLine("	sob.estructura_sobrecarga as estructura,");
            qry.AppendLine("	sob.peso_sobrecarga as peso");
            qry.AppendLine("FROM");
            qry.AppendLine("   encomienda enc");
            if (pReporte)
                qry.AppendLine("   INNER JOIN Encomienda_DatosLocal dl ON enc.id_encomienda = dl.id_encomienda");
            qry.AppendLine("   INNER JOIN Encomienda_Sobrecargas sob ON enc.id_encomienda = sob.id_encomienda");


            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);

            DataSet dsSobrecargas = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsSobrecargas);

            dsRep.Tables["SobreCargas"].Merge(dsSobrecargas.Tables[0]);

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("	dl.id_encomiendadatoslocal,");
            qry.AppendLine("	enc.id_encomienda,");
            qry.AppendLine("	dl.superficie_cubierta_dl,");
            qry.AppendLine("	dl.superficie_descubierta_dl,");
            qry.AppendLine("	dl.dimesion_frente_dl,");
            qry.AppendLine("	dl.lugar_carga_descarga_dl,");
            qry.AppendLine("	dl.estacionamiento_dl,");
            qry.AppendLine("	dl.red_transito_pesado_dl,");
            qry.AppendLine("	dl.sobre_avenida_dl,");
            qry.AppendLine("	dl.materiales_pisos_dl,");
            qry.AppendLine("	dl.materiales_paredes_dl,");
            qry.AppendLine("	dl.materiales_techos_dl,");
            qry.AppendLine("	dl.materiales_revestimientos_dl,");
            qry.AppendLine("	dl.sanitarios_ubicacion_dl,");
            qry.AppendLine("	dl.sanitarios_distancia_dl,");
            qry.AppendLine("	dl.cantidad_sanitarios_dl,");
            qry.AppendLine("	dl.superficie_sanitarios_dl,");
            qry.AppendLine("	dl.frente_dl,");
            qry.AppendLine("	dl.fondo_dl,");
            qry.AppendLine("	dl.lateral_izquierdo_dl,");
            qry.AppendLine("	dl.lateral_derecho_dl,");
            qry.AppendLine("	dl.sobrecarga_corresponde_dl,");
            qry.AppendLine("	dl.sobrecarga_tipo_observacion,");
            qry.AppendLine("	dl.sobrecarga_requisitos_opcion,");
            qry.AppendLine("	dl.sobrecarga_art813_inciso,");
            qry.AppendLine("	dl.sobrecarga_art813_item,");
            qry.AppendLine("	dl.cantidad_operarios_dl");
            qry.AppendLine("FROM");
            qry.AppendLine("   encomienda enc");
            qry.AppendLine("   INNER JOIN Encomienda_DatosLocal dl ON enc.id_encomienda = dl.id_encomienda");

            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);

            DataSet dsDatosLocal = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsDatosLocal);

            dsRep.Tables["DatosLocal"].Merge(dsDatosLocal.Tables[0]);

            // Trae los mapas y completa el dataset del reporte.
            // Se hace aparte porque debe traer un solo mapa.
            // Los mapas están en otra base.
            qry.Clear();
            qry.AppendLine("SELECT TOP 1");
            qry.AppendLine("	mapas.plano_mapa as mapa_dl,");
            qry.AppendLine("	mapas.croquis_mapa as croquis_dl");
            qry.AppendLine("FROM");
            qry.AppendLine("  Encomienda_Ubicaciones encubic");
            qry.AppendLine("  INNER JOIN Mapas ON encubic.id_ubicacion = mapas.id_ubicacion ");
            qry.AppendLine("WHERE ");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY");
            qry.AppendLine("  encubic.id_encomiendaubicacion");

            DataSet dsMapas = new DataSet(); ;
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsMapas);

            if (pReporte && dsMapas.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsRep.Tables["DatosLocal"].Rows)
                {

                    dr["mapa_dl"] = dsMapas.Tables[0].Rows[0]["mapa_dl"];
                    dr["croquis_dl"] = dsMapas.Tables[0].Rows[0]["croquis_dl"];

                }

            }

            return dsRep;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public static DataSet CargarDatosImpresionEncomiendaExt(int id_encomienda)
        {
            DataSet dsResult = new DataSet();
            dsResult.DataSetName = "dsEncomiendaExt";
            
            EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();

            var cmd = db.Database.Connection.CreateCommand();
            db.Database.Connection.Open();

            var sConnection = ((SqlConnection)db.Database.Connection);

            StringBuilder qry = new StringBuilder();

            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  enc.nroTramite,");
            qry.AppendLine("  enc.FechaEncomienda,");
            qry.AppendLine("  enc.nroEncomiendaconsejo,");
            qry.AppendLine("  prof.matricula as MatriculaProfesional,");
            qry.AppendLine("  prof.apellido as ApellidoProfesional,");
            qry.AppendLine("  prof.nombre as NombresProfesional,");
            qry.AppendLine("  grucon.nombre_grupoconsejo as ConsejoProfesional,");
            qry.AppendLine("  enc.id_estado,");
            qry.AppendLine("  grucon.id_grupoconsejo");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN profesional prof ON enc.id_profesional = prof.id");
            qry.AppendLine("  INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id");
            qry.AppendLine("  INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);


            DataSet dsEncomiendaExt = new DataSet(); 
            SqlDataAdapter com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsEncomiendaExt);

            dsResult.Tables.Add(dsEncomiendaExt.Tables[0].Copy());
            dsResult.Tables[0].TableName = "EncomiendaExt";

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  enc.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  mat.Seccion,");
            qry.AppendLine("  mat.Manzana,");
            qry.AppendLine("  mat.parcela,");
            qry.AppendLine("  mat.NroPartidaMatriz as NroPartidaMatriz,");
            qry.AppendLine("  encubic.local_subtipoubicacion,");
            qry.AppendLine("  zon1.codzonapla as ZonaParcela,");
            qry.AppendLine("  dbo.EncomiendaExt_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda, encubic.id_ubicacion),");
            qry.AppendLine("  encubic.deptoLocal_encomiendaubicacion as DeptoLocal");
            qry.AppendLine("FROM");
            qry.AppendLine("  encomiendaExt enc");
            qry.AppendLine("  INNER JOIN EncomiendaExt_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda");
            qry.AppendLine("  INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion");
            qry.AppendLine("  INNER JOIN Zonas_Planeamiento zon1 ON  encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento");
            qry.AppendLine("WHERE");
            qry.AppendLine("  enc.id_encomienda = " +  id_encomienda);

            DataSet dsUbicaciones = new DataSet(); 
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsUbicaciones);


            dsResult.Tables.Add(dsUbicaciones.Tables[0].Copy());
            dsResult.Tables[1].TableName = "Ubicaciones";

            //--------------------------------------------------------

            qry.Clear();
            qry.AppendLine("SELECT ");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  phor.NroPartidaHorizontal as NroPartidaHorizontal,");
            qry.AppendLine("  phor.piso,");
            qry.AppendLine("  phor.depto");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN EncomiendaExt_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion ");
            qry.AppendLine("  INNER JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            DataSet dsPropiedadHorizontal = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPropiedadHorizontal);


            dsResult.Tables.Add(dsPropiedadHorizontal.Tables[0].Copy());
            dsResult.Tables[2].TableName = "PropiedadHorizontal";

            //--------------------------------------------------------
            qry.Clear();
            qry.AppendLine("SELECT");
            qry.AppendLine("  encubic.id_encomiendaubicacion, ");
            qry.AppendLine("  encpuer.id_encomiendapuerta,");
            qry.AppendLine("  encubic.id_encomienda,");
            qry.AppendLine("  encubic.id_ubicacion,");
            qry.AppendLine("  encpuer.nombre_calle as Calle,");
            qry.AppendLine("  encpuer.NroPuerta");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Ubicaciones encubic ");
            qry.AppendLine("  INNER JOIN EncomiendaExt_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion");
            qry.AppendLine("WHERE");
            qry.AppendLine("  encubic.id_encomienda = " + id_encomienda);

            DataSet dsPuertas = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsPuertas);

            dsResult.Tables.Add(dsPuertas.Tables[0].Copy());
            dsResult.Tables[3].TableName = "Puertas";

            qry.Clear();

            qry.AppendLine("SELECT ");
            qry.AppendLine("  pf.id_personafisica as id_persona, 'PF' as TipoPersona, pf.id_encomienda, ");
            qry.AppendLine("  '' as RazonSocial, ");
            qry.AppendLine("  pf.Apellido, pf.Nombres, tipo_doc.nombre as TipoDoc, pf.Nro_Documento as NroDoc,");
            qry.AppendLine("  pf.Calle, pf.NroPuerta, ");
            qry.AppendLine("	pf.cuit, ");
            qry.AppendLine("  pf.Email");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Titulares_PersonasFisicas pf");
            qry.AppendLine("INNER JOIN  TipoDocumentoPersonal tipo_doc ON tipo_doc.TipoDocumentoPersonalId = pf.id_tipodoc_personal");
            qry.AppendLine("WHERE");
            qry.AppendLine("  pf.id_encomienda = " + id_encomienda);

            qry.AppendLine("UNION ALL ");

            qry.AppendLine("SELECT pj.id_personajuridica as id_persona, 'PJ' as TipoPersona, pj.id_encomienda,");
            qry.AppendLine("	UPPER(pj.Razon_Social) as RazonSocial, ");
            qry.AppendLine("	'' as Apellido,''as Nombres, '' as TipoDoc,  0 as NroDoc,");
            qry.AppendLine("  pj.Calle, pj.NroPuerta,  ");
            qry.AppendLine("	pj.cuit, ");
            qry.AppendLine("  pj.Email");
            qry.AppendLine("FROM");
            qry.AppendLine("  EncomiendaExt_Titulares_PersonasJuridicas pj");
            qry.AppendLine("WHERE");
            qry.AppendLine("  pj.id_encomienda = " + id_encomienda);
            qry.AppendLine("ORDER BY 1");

            DataSet dsTitulares = new DataSet();
            com = new SqlDataAdapter(qry.ToString(), sConnection);
            com.Fill(dsTitulares);


            dsResult.Tables.Add(dsTitulares.Tables[0].Copy());
            dsResult.Tables[4].TableName = "Titulares";

            return dsResult;

        }

    }
}
