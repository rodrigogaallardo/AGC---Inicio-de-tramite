using BaseRepository;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class PagosBoletasBL
    {
        private IUnitOfWorkFactory uowF = null;

        private string Url_Interface_AGC
        {
            get
            {
                ParametrosBL blParam = new ParametrosBL();
                return blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
            }
        }
        private string User_Interface_AGC
        {
            get
            {
                ParametrosBL blParam = new ParametrosBL();
                return blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
            }
        }
        private string Password_Interface_AGC
        {
            get
            {
                ParametrosBL blParam = new ParametrosBL();
                return blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        public void GenerarBoletaUnica(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            ExternalService.BUBoletaUnica boletaGenerada = null;

            // Recupera los datos de conexión para el uso del servicio de BUI
            // --
            if (tipo_tramite == Constantes.PagosTipoTramite.CAA || tipo_tramite == Constantes.PagosTipoTramite.HAB
                || tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                SSITSolicitudesUbicacionesBL blUbi = new SSITSolicitudesUbicacionesBL();
                if (!blUbi.validarUbicacionClausuras(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_CLAUSURAS);

                if (!blUbi.validarUbicacionInhibiciones(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_INHIBIDAS);
            }

            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                servicio.Url = this.Url_Interface_AGC;

                EncomiendaBL blEnc = new EncomiendaBL();
                // Se debe utilizar esta funcionalidad y no la de SSIT_Solicitudes_Encomiendas debido a que no se inserta en esa tabla hasta que no se encuentra confirmada la solicitud
                List<int> lstEncomiendasRelacionadas = blEnc.GetByFKIdSolicitud(id_solicitud).Select(x => x.IdEncomienda).ToList();

                ParametrosBL blParam = new ParametrosBL();
                servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lstEncomiendasRelacionadas.ToArray(), ref ws_resultado_CAA);

                // se obtiene el ultimo CAA aprobado
                var solCAA = l.ToList().Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado).OrderByDescending(x => x.id_caa).FirstOrDefault();

                int id_solicitud_caa = (solCAA != null ? solCAA.id_solicitud : 0);

                int id_pago = servicio.Generar_BUI_CAA_Automatico(this.User_Interface_AGC, this.Password_Interface_AGC, id_solicitud_caa, ref ws_resultado_CAA);
                if (ws_resultado_CAA.ErrorCode != 0)
                {
                    throw new Exception("CAA - " + ws_resultado_CAA.ErrorDescription);
                }
                else
                {
                    var lstBUIsCAA = servicio.Get_BUIs_CAA(this.User_Interface_AGC, this.Password_Interface_AGC, id_solicitud_caa, ref ws_resultado_BUI);

                    if (ws_resultado_BUI.ErrorCode != 0)
                    {
                        throw new Exception("CAA - " + ws_resultado_BUI.ErrorDescription);
                    }
                    else
                    {
                        boletaGenerada = (from bui in lstBUIsCAA
                                          where bui.IdPago == id_pago
                                          select new ExternalService.BUBoletaUnica
                                          {
                                              BUI_ID = bui.BUI_ID,
                                              CodBarras = bui.CodBarras,
                                              CodigoVerificador = bui.CodigoVerificador,
                                              Contribuyente = new ExternalService.BUDatosContribuyente
                                              {
                                                  ApellidoyNombre = bui.Contribuyente.ApellidoyNombre,
                                                  CodPost = bui.Contribuyente.CodPost,
                                                  Departamento = bui.Contribuyente.Departamento,
                                                  Direccion = bui.Contribuyente.Direccion,
                                                  Documento = bui.Contribuyente.Documento,
                                                  Email = bui.Contribuyente.Email,
                                                  Localidad = bui.Contribuyente.Localidad,
                                                  Piso = bui.Contribuyente.Piso,
                                                  TipoDoc = (ExternalService.BUTipodocumento)bui.Contribuyente.TipoDoc,
                                                  TipoPersona = (ExternalService.BUTipoPersona)bui.Contribuyente.TipoPersona

                                              },
                                              Dependencia = bui.Dependencia,
                                              EstadoId = bui.EstadoId,
                                              EstadoNombre = bui.EstadoNombre,
                                              FechaAnulada = bui.FechaAnulada,
                                              FechaCancelada = bui.FechaCancelada,
                                              FechaPago = bui.FechaPago,
                                              IdPago = bui.IdPago,
                                              MontoTotal = bui.MontoTotal,
                                              NroBoletaUnica = bui.NroBoletaUnica,
                                              NroBUI = bui.NroBUI,
                                              TrazaPago = bui.TrazaPago

                                          }).FirstOrDefault();
                    }
                }
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.HAB ||
                tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {

                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
                ExternalServicePagos servicePagos = new ExternalServicePagos();


                // Carga los datos del Contribuyente  (a quien se le hará la bota) y Conceptos
                // dependiendo del tipo de tramite
                // --
                ExternalService.BUDatosContribuyente datosContribuyente = null;
                List<BUConcepto> lstConceptos = new List<BUConcepto>();

                datosContribuyente = CargarDatosContribuyente(tipo_tramite, id_solicitud, userId);
                lstConceptos = CargarConceptos(tipo_tramite, id_solicitud);

                BUDatosBoleta DatosBoleta = new BUDatosBoleta();
                DatosBoleta.datosConstribuyente = datosContribuyente;
                DatosBoleta.listaConcepto = lstConceptos;

                try
                {
                    boletaGenerada = servicePagos.GenerarBoleta(DatosBoleta);
                }
                catch (Exception ex)
                {
                    LogError.Write(ex, "Error en ws GenerarBoletaUnica de ws_pagos");
                    throw new Exception(ex.Message);
                }


                try
                {
                    SSITSolicitudesPagosBL ssitPagosBL = new SSITSolicitudesPagosBL();
                    var ssitSolPagos = ssitPagosBL.GetByFKIdSolicitud(id_solicitud);

                    foreach (var item in ssitSolPagos)
                    {
                        string boleta = servicePagos.GetEstadoPago(item.id_pago);
                        if (boleta == Enum.GetName(typeof(Constantes.BUI_EstadoPago), Constantes.BUI_EstadoPago.SinPagar))
                            return;
                    }

                    SSITSolicitudesPagosDTO dto = new SSITSolicitudesPagosDTO();
                    dto.id_solicitud = id_solicitud;
                    dto.id_pago = boletaGenerada.IdPago;
                    dto.monto_pago = boletaGenerada.MontoTotal;
                    dto.CreateUser = userId;
                    dto.CreateDate = DateTime.Now;
                    ssitPagosBL.Insert(dto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {

                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
                ExternalServicePagos servicePagos = new ExternalServicePagos();

                // Carga los datos del Contribuyente  (a quien se le hará la bota) y Conceptos
                // dependiendo del tipo de tramite
                // --
                ExternalService.BUDatosContribuyente datosContribuyente = null;
                List<BUConcepto> lstConceptos = new List<BUConcepto>();

                datosContribuyente = CargarDatosContribuyente(tipo_tramite, id_solicitud, userId);
                lstConceptos = CargarConceptos(tipo_tramite, id_solicitud);

                BUDatosBoleta DatosBoleta = new BUDatosBoleta();
                DatosBoleta.datosConstribuyente = datosContribuyente;
                DatosBoleta.listaConcepto = lstConceptos;

                try
                {
                    try
                    {
                        boletaGenerada = servicePagos.GenerarBoleta(DatosBoleta);
                    }
                    catch (Exception ex)
                    {
                        LogError.Write(ex, "Error en ws GenerarBoletaUnica de ws_pagos");
                        throw new Exception("El servicio de generación de boletas no esta disponible. Intente en otro momento.");
                    }
                    //TransferenciasSolicitudesBL tranfBL = new TransferenciasSolicitudesBL();
                    //int id_tramitetarea = tranfBL.GetIdTramiteTareaPago(id_solicitud);


                    //SGISolicitudesPagosBL blPagos = new SGISolicitudesPagosBL();
                    //SGISolicitudesPagosDTO dto = new SGISolicitudesPagosDTO();
                    //dto.IdTramiteTarea = id_tramitetarea;
                    //dto.IdPago = boletaGenerada.IdPago;
                    //dto.MontoPago = boletaGenerada.MontoTotal;
                    //dto.CreateUser = userId;
                    //dto.CreateDate = DateTime.Now;
                    //blPagos.Insert(dto);

                    try
                    {
                        TransfSolicitudesPagosBL transfPagosBL = new TransfSolicitudesPagosBL();
                        var transfSolPagos = transfPagosBL.GetByFKIdSolicitud(id_solicitud);

                        foreach (var item in transfSolPagos)
                        {
                            string boleta = servicePagos.GetEstadoPago(item.id_pago);
                            if (boleta == Enum.GetName(typeof(Constantes.BUI_EstadoPago), Constantes.BUI_EstadoPago.SinPagar))
                                return;
                        }

                        TransfSolicitudesPagosDTO dto = new TransfSolicitudesPagosDTO();
                        dto.id_solicitud = id_solicitud;
                        dto.id_pago = boletaGenerada.IdPago;
                        dto.monto_pago = boletaGenerada.MontoTotal;
                        dto.CreateUser = userId;
                        dto.CreateDate = DateTime.Now;
                        transfPagosBL.Insert(dto);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        private ExternalService.BUDatosContribuyente CargarDatosContribuyente(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud, Guid userId)
        {
            ExternalService.BUDatosContribuyente datosContribuyente = null;

            UsuarioBL usuBL = new UsuarioBL();
            var usuDTO = usuBL.Single(userId);

            TitularesBL titularesBL = new TitularesBL();
            IEnumerable<TitularesDTO> lstTitulares = null;

            switch (tipo_tramite)
            {
                case Constantes.PagosTipoTramite.HAB:
                    lstTitulares = titularesBL.GetTitularesSolicitud(id_solicitud);
                    break;
                case Constantes.PagosTipoTramite.AMP:
                    lstTitulares = titularesBL.GetTitularesSolicitud(id_solicitud);
                    break;
                case Constantes.PagosTipoTramite.TR:
                    lstTitulares = titularesBL.GetTitularesTransferencias(id_solicitud);
                    break;
            }

            if (lstTitulares.Count() == 0)
                throw new Exception(string.Format("No se encontraron titulares para la solicitud Nº {0}.", id_solicitud));

            var titular = lstTitulares.FirstOrDefault();
            string tipoPersona = titular.TipoPersona;

            datosContribuyente = new ExternalService.BUDatosContribuyente();
            string strNroDoc = titular.nro_doc;
            strNroDoc = Funciones.SoloDigitos(strNroDoc);

            string tipoDoc = Convert.ToString(titular.desc_tipo_doc);

            datosContribuyente.Documento = strNroDoc;

            switch (tipoDoc.ToUpper())
            {
                case "CI":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.CI;
                    break;
                case "CUIT":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.CUIT;
                    datosContribuyente.Documento = titular.CUIT.Replace("-", "");
                    break;
                case "DNI":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.DNI;
                    break;
                case "LC":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.LC;
                    break;
                case "LE":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.LE;
                    break;
                case "PASAPORTE":
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.CUIT;
                    datosContribuyente.Documento = titular.CUIT.Replace("-", "");
                    break;
                default:
                    datosContribuyente.TipoDoc = ExternalService.BUTipodocumento.DNI;
                    break;
            }

            datosContribuyente.TipoPersona = (tipoPersona.Equals("PF")) ? ExternalService.BUTipoPersona.Fisica : ExternalService.BUTipoPersona.Juridica;
            datosContribuyente.ApellidoyNombre = Convert.ToString(titular.ApellidoNomRazon);
            datosContribuyente.Direccion = id_solicitud.ToString() + " - " + Convert.ToString(titular.Domicilio);
            datosContribuyente.CodPost = Convert.ToString((titular.Codigo_Postal.Length == 0 ? "-" : titular.Codigo_Postal));
            datosContribuyente.Piso = Convert.ToString(titular.Piso);
            datosContribuyente.Departamento = Convert.ToString(titular.Depto);
            datosContribuyente.Email = Convert.ToString(usuDTO.Email);
            datosContribuyente.Localidad = Convert.ToString(titular.Localidad);

            if (string.IsNullOrEmpty(datosContribuyente.Email))
                datosContribuyente.Email = Membership.GetUser().Email;
            return datosContribuyente;
        }

        private List<BUConcepto> CargarConceptos(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            List<BUConcepto> wsPago_lstConcepto = new List<BUConcepto>();

            int id_subtipoexpediente = 0;
            decimal superficie_cubierta = 0;
            decimal superficie_descubierta = 0;
            decimal superficieTotal = 0;
            int idCircuito = 0;

            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            var sol = blSol.Single(id_solicitud);

            if (tipo_tramite == Constantes.PagosTipoTramite.HAB ||
                tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {

                EncomiendaBL blEnc = new EncomiendaBL();
                EngineBL blEng = new EngineBL();
                var lstEnc = blEnc.GetByFKIdSolicitud(id_solicitud);

                id_subtipoexpediente = (int)sol.IdSubTipoExpediente;
                if (lstEnc.Count() > 0)
                {
                    var enc = lstEnc.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                        .OrderByDescending(x => x.IdEncomienda).FirstOrDefault();
                    if (enc == null)
                    {
                        throw new Exception("El AT " + lstEnc.OrderByDescending(x => x.IdEncomienda).FirstOrDefault().IdEncomienda.ToString()  + " se encuentra vencido, debe rectificarlo para poder generar la boleta correspondiente.");
                    }

                    EncomiendaDatosLocalBL blDatos = new EncomiendaDatosLocalBL();
                    var datos = blDatos.GetByFKIdEncomienda(enc.IdEncomienda);
                    idCircuito = blEng.GetIdCircuitoBySolicitud(id_solicitud); //GetIdCircuitoByEncomienda(enc.IdEncomienda);

                    superficie_cubierta = datos.superficie_cubierta_dl.HasValue ? (decimal)datos.superficie_cubierta_dl : 0;
                    superficie_descubierta = datos.superficie_descubierta_dl.HasValue ? (decimal)datos.superficie_descubierta_dl : 0;
                    superficieTotal = superficie_cubierta + superficie_descubierta;

                    if (tipo_tramite == Constantes.PagosTipoTramite.AMP)
                    {
                        if (datos.ampliacion_superficie.HasValue && datos.ampliacion_superficie.Value)
                        {
                            // Cuando es una ampliación de superficie se toma la diferencia entre la superficie habilitada anteriormente 
                            // contra la superficie total declarada en la ampliacion
                            decimal superficie_cubierta_amp = datos.superficie_cubierta_amp.HasValue ? (decimal)datos.superficie_cubierta_amp : 0;
                            decimal superficie_descubierta_amp = datos.superficie_descubierta_amp.HasValue ? (decimal)datos.superficie_descubierta_amp : 0;
                            decimal superficieTotal_amp = superficie_cubierta_amp + superficie_descubierta_amp;

                            superficieTotal = superficieTotal_amp - superficieTotal;
                            if (superficieTotal <= 0)
                                superficieTotal = 0.01m; // No es posible para BUI recibir 0, el servicio devuelve null
                        }
                        else
                        {
                            // Si no es ampliacioón de superficie se envía 0 (cero)
                            superficieTotal = 0.01m; // No es posible para BUI recibir 0, el servicio devuelve null
                        }
                    }
                }
            }



            List<Concepto_a_cobrar> lst_conceptos_a_buscar = new List<Concepto_a_cobrar>();

            if (tipo_tramite == Constantes.PagosTipoTramite.HAB ||
                tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                // buscar los conceptos a cobrar

                // El uso conforme no se cobra solo en habilitaciones

                //0132660: JADHE 49886 - BUI - Quitar Concepto de uso conforme de boleta

                //if (tipo_tramite == Constantes.PagosTipoTramite.HAB)
                //{
                //    lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                //    {
                //        cantidad = 1,
                //        codigo_concepto = "SGI.Pagos.Concepto.UsoConforme"
                //    });
                //}

                if (id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos &&
                    idCircuito == (int)Constantes.ENG_Circuitos.SSP3)
                {
                    lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                    {
                        cantidad = 1,
                        codigo_concepto = "SGI.Pagos.Concepto.DRSSPA"
                    });
                }
                else if (id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                {

                    lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                    {
                        cantidad = 1,
                        codigo_concepto = "SGI.Pagos.Concepto.DRSSP"
                    });
                }
                else if (id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.ConPlanos)
                {
                    if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    {
                        lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                        {
                            cantidad = 1,
                            codigo_concepto = "SGI.Pagos.Concepto.RDU"
                        });
                    }
                    else
                    {
                        lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                        {
                            cantidad = 1,
                            codigo_concepto = "SGI.Pagos.Concepto.DRSCP"
                        });

                        if (superficieTotal > 500)
                        {
                            decimal cantidad = (superficieTotal / Convert.ToDecimal(500));
                            int cantidad_concepto = Convert.ToInt32(Math.Ceiling(cantidad - 1));
                            lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                            {
                                cantidad = cantidad_concepto,
                                codigo_concepto = "SGI.Pagos.Concepto.DRSCP.Ex500"
                            });
                        }
                    }
                }
                else if (id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.InspeccionPrevia)
                {
                    lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                    {
                        cantidad = 1,
                        codigo_concepto = "SGI.Pagos.Concepto.IP"
                    });

                    if (superficieTotal > 500)
                    {
                        decimal cantidad = (superficieTotal / Convert.ToDecimal(500));
                        int cantidad_concepto = Convert.ToInt32(Math.Ceiling(cantidad - 1));

                        lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                        {
                            cantidad = cantidad_concepto,
                            codigo_concepto = "SGI.Pagos.Concepto.IP.Exc.500"
                        });
                    }

                }
                else if (id_subtipoexpediente == (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia)
                {
                    if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    {
                        lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                        {
                            cantidad = 1,
                            codigo_concepto = "SGI.Pagos.Concepto.RDU"
                        });
                    }
                    else
                    {
                        lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                        {
                            cantidad = 1,
                            codigo_concepto = "SGI.Pagos.Concepto.HP.500m"
                        });

                        if (superficieTotal > 500)
                        {
                            decimal cantidad = (superficieTotal / Convert.ToDecimal(500));
                            int cantidad_concepto = Convert.ToInt32(Math.Ceiling(cantidad - 1));

                            lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                            {
                                cantidad = cantidad_concepto,
                                codigo_concepto = "SGI.Pagos.Concepto.HP.Ex.500m"
                            });
                        }
                    }

                }
            }

            if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {

                uowF = new TransactionScopeUnitOfWorkFactory();
                var uof = this.uowF.GetUnitOfWork();
                var repoTransf = new TransferenciasSolicitudesRepository(uof);

                var transf = repoTransf.Single(id_solicitud);

                if (transf != null && transf.idTipoTransmision != (int)Constantes.TipoTransmision.Transmision_oficio_judicial)
                {
                    lst_conceptos_a_buscar.Add(new Concepto_a_cobrar
                    {
                        cantidad = 1,
                        codigo_concepto = "SGI.Pagos.Concepto.TRANSTR"
                    });
                }

            }

            // busca en la base los conceptos a cobrar
            // ---------------------------------------
            List<ConceptosBUIIndependientesDTO> cobrar_conceptos = new List<ConceptosBUIIndependientesDTO>();

            string[] arrCodConceptosCobrar = lst_conceptos_a_buscar.Select(s => s.codigo_concepto).ToArray();

            ConceptosBUIIndependientesBL blConc = new ConceptosBUIIndependientesBL();
            cobrar_conceptos = blConc.GetList(arrCodConceptosCobrar).ToList();


            // pedir conceptos vigentes al ws para enviar la informacion correcta

            List<BUConcepto> arrayConceptos = null;
            try
            {
                ExternalServicePagos servicePagos = new ExternalServicePagos();

                arrayConceptos = servicePagos.GetConceptos();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, "Error en ws GetConceptos de ws_pagos");
                throw new Exception("El servicio de generación de boletas no esta disponible. Intente en otro momento. " + ex.Message);
            }

            foreach (var item in cobrar_conceptos)
            {

                BUConcepto ws_concepto = arrayConceptos.Where(x => x.CodConcepto1 == item.cod_concepto_1 && x.CodConcepto2 == item.cod_concepto_2 && x.CodConcepto3 == item.cod_concepto_3).FirstOrDefault();

                if (ws_concepto != null)
                {
                    ws_concepto.Cantidad = lst_conceptos_a_buscar.FirstOrDefault(x => x.codigo_concepto == item.keycode).cantidad;

                    // Los conceptos de habilitaciones simples sin plano y con plano establecen su valor dependiendo de la superficie.
                    if (item.admite_reglas)
                        ws_concepto.ValorDetalle = superficieTotal;

                    wsPago_lstConcepto.Add(ws_concepto);
                }

            }

            if (wsPago_lstConcepto == null || wsPago_lstConcepto.Count == 0)
                throw new Exception("No se encontraron los conceptos parametrizados correspondientes para la solicitud.");

            if (wsPago_lstConcepto.Count != cobrar_conceptos.Count)
                throw new Exception("No se encontraron los conceptos a cobrar.");

            return wsPago_lstConcepto;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        public IEnumerable<clsItemGrillaPagos> CargarPagos(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            List<clsItemGrillaPagos> lstPagos = new List<clsItemGrillaPagos>();

            SGISolicitudesPagosBL pagos = new SGISolicitudesPagosBL();
            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                var lst = new List<clsItemGrillaPagos>();

                List<int> lstEncomiendasRelacionadas = null;
                if (lstEncomiendas == null)
                {
                    EncomiendaBL blEnc = new EncomiendaBL();
                    // Se debe utilizar esta funcionalidad y no la de SSIT_Solicitudes_Encomiendas debido a que no se inserta en esa tabla hasta que no se encuentra confirmada la solicitud
                    lstEncomiendasRelacionadas = blEnc.GetByFKIdSolicitud(id_solicitud).Select(x => x.IdEncomienda).ToList();
                }
                else
                {
                    lstEncomiendasRelacionadas = lstEncomiendas.Select(x => x.IdEncomienda).ToList();
                }

                ws_Interface_AGC servicio = new ws_Interface_AGC();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();

                try
                {
                    if (lstEncomiendasRelacionadas.Any())
                    {
                        ParametrosBL blParam = new ParametrosBL();
                        servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                        string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                        string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                        DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lstEncomiendasRelacionadas.ToArray(), ref ws_resultado_CAA);
                        foreach (var caa in l.ToList())
                        {
                            var listPago = servicio.Get_BUIs_CAA(username_servicio, password_servicio, caa.id_solicitud, ref ws_resultado_CAA);
                            if (listPago != null)
                            {
                                foreach (var p in listPago)
                                {
                                    var pago = new clsItemGrillaPagos();
                                    var pago_estado = pagos.GetEstadoPago(p.IdPago);
                                    pago.id_sol_pago = p.IdPago;
                                    pago.id_solicitud = caa.id_caa;
                                    pago.id_pago = p.IdPago;
                                    pago.id_medio_pago = 0;
                                    pago.monto_pago = p.MontoTotal;
                                    pago.CreateDate = caa.CreateDate;//TODO aca iria la fecha creacion de la boleta que no la esta devolviendo
                                    pago.desc_medio_pago = "Boleta única";
                                    pago.desc_estado_pago = pago_estado.desc_estado_pago;
                                    pago.id_estado_pago = pago_estado.id_estado_pago;
                                    lst.Add(pago);
                                }
                            }
                            else
                                return null;
                        }
                    }
                }
                catch (Exception exc)
                {
                    LogError.Write(exc);
                }
                lstPagos = lst;
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.HAB ||
                tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                lstPagos.AddRange(pagos.GetHab(id_solicitud).ToList());

                SSITSolicitudesPagosBL blPagos = new SSITSolicitudesPagosBL();
                lstPagos.AddRange(blPagos.GetGrillaByFKIdSolicitud(id_solicitud).ToList());

            }
            if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {
                lstPagos = pagos.GetTransf(id_solicitud).ToList();
                lstPagos.AddRange(pagos.GetTransmisiones(id_solicitud).ToList());

                //TransfSolicitudesPagosBL blPagos = new TransfSolicitudesPagosBL();
                //lstPagos.AddRange(blPagos.GetGrillaByFKIdSolicitud(id_solicitud).ToList());
            }
            return lstPagos;
        }
        public bool HabilitarGeneracionPago(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            bool ret = false;

            if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {
                int[] estados_consultados = new int[] { (int)Constantes.BUI_EstadoPago.Pagado, (int)Constantes.BUI_EstadoPago.SinPagar };

                // Si la cantidad de boletas en estado Sin Pagar o Pagado es mayor a 0, la funcion retorna false.
                SGISolicitudesPagosBL blPagos = new SGISolicitudesPagosBL();
                //List<int> id_pagos = blPagos.GetTransf(id_solicitud).Select(s => s.id_pago).ToList();                
                List<int> id_pagos = blPagos.GetTransmisiones(id_solicitud).Select(s => s.id_pago).ToList();

                ExternalServicePagos servicePagos = new ExternalServicePagos();
                var lstBUIsHAB = servicePagos.ObtenerBoletas(id_pagos);

                ret = (lstBUIsHAB.Count(x => estados_consultados.Contains(x.EstadoId)) == 0);

                //0145298: JADHE 57098 - SGI - TRM 2019 piden BUI
                TransferenciasSolicitudesBL blTransferencia = new TransferenciasSolicitudesBL();
                TransferenciasSolicitudesDTO tranf = blTransferencia.Single(id_solicitud);
                DateTime fecha = new DateTime(2020, 1, 1);
                if ((tranf.TipoTransmision != null) && (tranf.TipoTransmision.id_tipoTransmision == (int)Constantes.TipoTransmision.Transmision_nominacion) && (tranf.CreateDate < fecha))
                {
                    ret = false;
                }

                if ((tranf.TipoTransmision != null) && (tranf.TipoTransmision.id_tipoTransmision == (int)Constantes.TipoTransmision.Transmision_oficio_judicial))
                {
                    ret = false;
                }
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.HAB || tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                int[] estados_consultados = new int[] { (int)Constantes.BUI_EstadoPago.Pagado, (int)Constantes.BUI_EstadoPago.SinPagar };

                // Si la cantidad de boletas en estado Sin Pagar o Pagado es mayor a 0, la funcion retorna false.
                // -------------------------------------------------------------------------------------------
                SSITSolicitudesPagosBL blPagos = new SSITSolicitudesPagosBL();
                List<int> id_pagos = blPagos.GetByFKIdSolicitud(id_solicitud).Select(s => s.id_pago).ToList();

                ExternalServicePagos servicePagos = new ExternalServicePagos();
                var lstBUIsHAB = servicePagos.ObtenerBoletas(id_pagos);



                ret = (lstBUIsHAB.Where(x => estados_consultados.Contains(x.EstadoId)).Count() == 0);
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                // Se obtiene la solicitud
                // Se debe utilizar esta funcionalidad y no la de SSIT_Solicitudes_Encomiendas debido a que no se inserta en esa tabla 
                // hasta que no se encuentra confirmada la solicitud
                if (lstEncomiendas != null && lstEncomiendas.Any())
                {
                    int[] estados_consultados = new int[] { (int)Constantes.BUI_EstadoPago.Pagado, (int)Constantes.BUI_EstadoPago.SinPagar };
                    try
                    {
                        // se obtiene el ultimo CAA aprobado
                        ws_Interface_AGC servicio = new ws_Interface_AGC();
                        ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();
                        var lstEncomiendasRelacionadas = lstEncomiendas.Select(p => p.IdEncomienda);
                        servicio.Url = this.Url_Interface_AGC;
                        DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(this.User_Interface_AGC, this.Password_Interface_AGC, lstEncomiendasRelacionadas.ToArray(), ref ws_resultado_CAA);
                        var solCAA = l.ToList().Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado).OrderByDescending(x => x.id_caa).FirstOrDefault();

                        int id_caa = (solCAA != null ? solCAA.id_solicitud : 0);

                        // Se consulta el estado del último CAA aprobado
                        ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                        var lstBUIsCAA = servicio.Get_BUIs_CAA(this.User_Interface_AGC, this.Password_Interface_AGC, id_caa, ref ws_resultado_BUI).ToList();
                        servicio.Dispose();

                        if (ws_resultado_BUI.ErrorCode != 0)
                        {
                            throw new Exception("No se ha podido recuperar las BUI/s relacionadas al CAA.");
                        }
                        else
                        {
                            ret = (lstBUIsCAA.Count(x => estados_consultados.Contains(x.EstadoId)) == 0);
                        }
                    }
                    catch (Exception exc)
                    {
                        LogError.Write(exc);
                    }
                }

            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public int GetPagosCount(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            int ret = 0;
            if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {
                SGISolicitudesPagosBL blPagos = new SGISolicitudesPagosBL();
                ret = blPagos.GetTransf(id_solicitud).Count();
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                try
                {
                    ws_Interface_AGC servicio = new ws_Interface_AGC();
                    ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                    servicio.Url = this.Url_Interface_AGC;
                    var lstBUIsCAA = servicio.Get_BUIs_CAA(this.User_Interface_AGC, this.Password_Interface_AGC, id_solicitud, ref ws_resultado_BUI).ToList();
                    servicio.Dispose();

                    if (ws_resultado_BUI.ErrorCode != 0)
                    {
                        throw new Exception("No se ha podido recuperar las BUI/s relacionadas al CAA.");
                    }
                    else
                    {
                        ret = lstBUIsCAA.Count();
                    }
                }
                catch (Exception exc)
                {
                    LogError.Write(exc);
                }

            }
            if (tipo_tramite == Constantes.PagosTipoTramite.HAB || tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                SSITSolicitudesPagosBL blPagos = new SSITSolicitudesPagosBL();
                ret = blPagos.GetByFKIdSolicitud(id_solicitud).Count();
            }

            return ret;
        }
        public Constantes.BUI_EstadoPago GetEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            Constantes.BUI_EstadoPago ret = Constantes.BUI_EstadoPago.SinPagar;

            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                ExternalService.ws_interface_AGC.wsResultado ws_resultado_BUI = new ExternalService.ws_interface_AGC.wsResultado();
                servicio.Url = this.Url_Interface_AGC;
                var lstBUIsCAA = servicio.Get_BUIs_CAA(this.User_Interface_AGC, this.Password_Interface_AGC, id_solicitud, ref ws_resultado_BUI);
                servicio.Dispose();

                if (ws_resultado_BUI.ErrorCode != 0)
                {
                    throw new Exception("No se ha podido recuperar las BUI/s relacionadas al CAA.");
                }
                else
                {
                    if (lstBUIsCAA.Count() > 0)
                    {
                        if (lstBUIsCAA.Count(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado) > 0)
                            ret = Constantes.BUI_EstadoPago.Pagado;
                        else
                            ret = (Constantes.BUI_EstadoPago)lstBUIsCAA.LastOrDefault().EstadoId;
                    }
                }
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.HAB || tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {
                ExternalServicePagos servicePagos = new ExternalServicePagos();
                List<clsItemGrillaPagos> lstPagos = new List<clsItemGrillaPagos>();

                SGISolicitudesPagosBL pagos = new SGISolicitudesPagosBL();
                lstPagos.AddRange(pagos.GetHab(id_solicitud).ToList());

                SSITSolicitudesPagosBL blPagos = new SSITSolicitudesPagosBL();
                lstPagos.AddRange(blPagos.GetGrillaByFKIdSolicitud(id_solicitud).ToList());

                List<int> id_pagos = lstPagos.Select(s => s.id_pago).ToList();

                var lstBUIsHAB = servicePagos.ObtenerBoletas(id_pagos);

                if (lstBUIsHAB.Count() > 0)
                {
                    if (lstBUIsHAB.Count(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado) > 0)
                        ret = Constantes.BUI_EstadoPago.Pagado;
                    else
                        ret = (Constantes.BUI_EstadoPago)lstBUIsHAB.LastOrDefault().EstadoId;
                }
            }
            if (tipo_tramite == Constantes.PagosTipoTramite.TR)
            {
                ExternalServicePagos servicePagos = new ExternalServicePagos();
                SGISolicitudesPagosBL blPagos = new SGISolicitudesPagosBL();
                ParametrosBL paramBL = new ParametrosBL();
                              
                int nroTrReferencia = 0;
                int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);
                List<int> id_pagos;
                if (id_solicitud < nroTrReferencia)
                {
                    id_pagos = blPagos.GetTransf(id_solicitud).Select(s => s.id_pago).ToList();
                }
                else
                {
                    id_pagos = blPagos.GetTransmisiones(id_solicitud).Select(s => s.id_pago).ToList();
                }
              
                var lstBUIsHAB = servicePagos.ObtenerBoletas(id_pagos);

                if (lstBUIsHAB.Count() > 0)
                {
                    if (lstBUIsHAB.Count(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado) > 0)
                        ret = Constantes.BUI_EstadoPago.Pagado;
                    else
                        ret = (Constantes.BUI_EstadoPago)lstBUIsHAB.LastOrDefault().EstadoId;
                }
            }
            return ret;
        }
        public string ConsultarEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_pago)
        {
            string strEstadoPago = "";

            if (id_pago <= 0)
                return strEstadoPago;

            try
            {
                ExternalServicePagos servicePagos = new ExternalServicePagos();
                strEstadoPago = servicePagos.GetEstadoPago(id_pago);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, "Error en ws GetEstadoPago");
                throw new Exception("El servicio de generación de boletas no esta disponible. Intente en otro momento.");
            }

            return strEstadoPago;

        }
    }
}
