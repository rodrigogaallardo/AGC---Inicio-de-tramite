using BusinesLayer.Implementation;
using BusinesLayer.MappingConfig;
using DataTransferObject;
using ExternalService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTest
{

    [TestClass]
    public class SSITUnitTest
    {
        Guid CreateUser = Guid.Parse("81D7CA25-757E-4D2E-B77D-1DBC54900F3C");
        Guid CreateUserAnexo = Guid.Parse("5674BEEF-AFEA-4ADE-AEC3-6774E251BAF2");

        [TestInitialize]
        public void Iniciar()
        {
            AutoMapperConfig.RegisterMappingEncomienda();
        }

        [TestMethod]
        public void llamarbl()
        {
            EncomiendaBL encbl1 = new EncomiendaBL();
            EncomiendaBL encbl2 = new EncomiendaBL();
            var encdto1 = encbl1.Single(113026);
            var encdto2 = encbl2.Single(112996);

        }

        [TestMethod]
        public void logica()
        {
            int CantOperarios = 10;
            int CantBanos = 2;
            bool salubridad_especial = false;
            bool eximido_ley_962 = true;

            if (CantOperarios >= 10 && (CantBanos < 2 || !(salubridad_especial || eximido_ley_962)))
            {

            }
        }

        [TestMethod]
        public void email()
        {
            int id_solicitud = 343399;
            MailMessages mailer = new MailMessages();
            string htmlBody = mailer.MailDisponibilzarQR(id_solicitud);
            
            string asunto = "Solicitud de habilitación N°: " + id_solicitud + " - " + "Direccion";

            EmailServiceBL mailService = new EmailServiceBL();
            EmailEntity emailEntity = new EmailEntity();
            emailEntity.Email = "email@email.com";
            emailEntity.Html = htmlBody;
            emailEntity.Asunto = asunto;
            emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
            emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.WebSGI_AprobacionDG;
            emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
            emailEntity.CantIntentos = 3;
            emailEntity.CantMaxIntentos = 3;
            emailEntity.FechaAlta = DateTime.Now;
            emailEntity.Prioridad = 1;

            mailService.SendMail(emailEntity);
        }

        [TestMethod]
        public void CopiarTitularesToEncomienda()
        {
            EncomiendaBL encBL = new EncomiendaBL();
            encBL.copyTitularesFromEncomienda(344007, 105754, CreateUser);
        }


        [TestMethod]
        public void EliminarSSIT() 
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            SSITSolicitudesBL solicitudesBL = new SSITSolicitudesBL();
            SSITSolicitudesDTO solicitud = new SSITSolicitudesDTO();
            var solicitudDTO = solicitudesBL.Single(337600);
            IEnumerable<EncomiendaDTO> encomiendas = encomiendaBL.GetByFKIdSolicitud(solicitudDTO.IdSolicitud);
            //Anexo
            foreach (var encomienda in encomiendas)
            {
                encomienda.IdEstado = (int)Constantes.Encomienda_Estados.Incompleta;
                //Se debe anular la encomienda para poder borrar los datos
                encomiendaBL.Update(encomienda);
                //ubicaciones
                BorrarUbicaciones(encomienda.EncomiendaUbicacionesDTO);                
                //Rubros
                BorrarRubros(encomienda.EncomiendaRubrosDTO);
                //Datos del loca
                BorrarDatosLocal(encomienda.EncomiendaDatosLocalDTO.FirstOrDefault());
                //Conformacion local
                BorrarConformacionLocal(encomienda.EncomiendaConformacionLocalDTO);
                //plantas
                BorrarPlantas(encomienda.EncomiendaPlantasDTO);
                //Titulares
                BorrarTitularesAnexo(encomienda.IdEncomienda);
                //Anexo
                encomiendaBL.Delete(encomiendaBL.Single(encomienda.IdEncomienda));
            }
            //Ubicaciones
            BorrarUbicaciones(solicitudDTO.SSITSolicitudesUbicacionesDTO);
            //Titulares
            BorrarTitularesSolicitud(solicitudDTO.IdSolicitud);
            //Solicitud
            BorraSolicitud(solicitudDTO);
        }

        [TestMethod]
        public void CreateSSIT()
        {
            SSITSolicitudesBL solicitudesBL = new SSITSolicitudesBL();
            SSITSolicitudesDTO solicitud = new SSITSolicitudesDTO();
            solicitud.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
            solicitud.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
            solicitud.IdTipoTramite = 1;
            solicitud.IdTipoExpediente = 0;
            solicitud.IdSubTipoExpediente = 0;
            solicitud.CreateDate = DateTime.Now;
            solicitud.CreateUser = CreateUser;
            solicitud.Servidumbre_paso = false;
            int id_solicitud = solicitudesBL.Insert(solicitud);

            Assert.IsTrue(id_solicitud > 0);

            var solicitudDTO = solicitudesBL.Single(id_solicitud);

            Assert.AreEqual(solicitudDTO.IdSolicitud, id_solicitud);
            
            InsertarTitulares(id_solicitud);

            InsertarUbicacion(id_solicitud);

            solicitudesBL.ActualizarEstado(id_solicitud, CreateUser);

            solicitudDTO = solicitudesBL.Single(id_solicitud); 

            solicitudesBL.confirmarSolicitud(id_solicitud, CreateUser);

            var encomiendaId = CrearAnexoTecnico(solicitudDTO.IdSolicitud,solicitud.CodigoSeguridad,CreateUserAnexo);

            CrearDatosLocal(encomiendaId);

            CrearRubros(encomiendaId);

            CargarPlantas(encomiendaId);

            CargarConformacionLocal(encomiendaId);

        }

        [TestMethod]
        public void BorrarConformacionLocal(IEnumerable<EncomiendaConformacionLocalDTO> conformacionLocal) 
        {
            EncomiendaConformacionLocalBL blECL = new EncomiendaConformacionLocalBL();

            var conformacion = conformacionLocal.FirstOrDefault();
            if (conformacion != null)
                blECL.Delete(conformacion);
          
        }

        [TestMethod]
        public void CargarConformacionLocal(int encomiendaId) 
        {
            EncomiendaConformacionLocalBL blECL = new EncomiendaConformacionLocalBL();
            EncomiendaConformacionLocalDTO dto = new EncomiendaConformacionLocalDTO();
            EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();
            var plantas = encomiendaPlantasBL.GetByFKIdEncomienda(encomiendaId); 

            dto.CreateDate = DateTime.Now;
            dto.CreateUser = CreateUserAnexo;
            
            dto.id_encomienda = encomiendaId;
            dto.id_destino = 10;//Local
            dto.largo_conflocal = 5;
            dto.ancho_conflocal = 10;
            dto.alto_conflocal = 10;
            dto.superficie_conflocal = 50;
            dto.Paredes_conflocal = "1";
            dto.Techos_conflocal = "1";
            dto.Pisos_conflocal = "1";
            dto.Frisos_conflocal = "1";
            dto.Observaciones_conflocal = "Observaciones";
            dto.id_encomiendatiposector = plantas.FirstOrDefault().id_encomiendatiposector;
            dto.id_ventilacion = 0;
            dto.id_iluminacion = 1;
            dto.id_tiposuperficie = 1;
            
            dto.Detalle_conflocal = "";

            //TipoIluminacionBL tipoIluminacion = new TipoIluminacionBL();
            //var tipoIluminacionDTO = tipoIluminacion.Single(1);
            //dto.TipoIluminacionDTO = tipoIluminacionDTO;

            //TipoDestinoBL tipoDestinoBL = new TipoDestinoBL();
            //var tipoDestinoDTO = tipoDestinoBL.Single(10);
            //dto.TipoDestinoDTO = tipoDestinoDTO;

            //TipoSuperficieBL tipoSuperficieBL = new TipoSuperficieBL();
            //var tipoSuperficieDTO = tipoSuperficieBL.Single(1);
            //dto.TipoSuperficieDTO = tipoSuperficieDTO;

            //TipoVentilacionBL tipoVentilacionBL = new TipoVentilacionBL();
            //var tipoVentilacionDTO = tipoVentilacionBL.Single(0);
            //dto.TipoVentilacionDTO = tipoVentilacionDTO;

            blECL.Insert(dto);
        }
        public virtual TipoVentilacionDTO TipoVentilacionDTO { get; set; }
        [TestMethod]
        public void BorrarPlantas(IEnumerable<EncomiendaPlantasDTO> plantas)
        {
            EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();

            foreach(var planta in plantas)
                encomiendaPlantasBL.Delete(planta);
        }

        [TestMethod]
        public void CargarPlantas(int encomiendaId) 
        {             
            EncomiendaPlantasDTO planta = new EncomiendaPlantasDTO();
            planta.id_encomienda = encomiendaId;
            planta.IdTipoSector = 1;

            EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();             
            encomiendaPlantasBL.Insert(planta);

        }

        [TestMethod]
        public void CrearRubros(int encomiendaId)
        {
            EncomiendaRubrosBL encomiendaRubroBL = new EncomiendaRubrosBL();
            EncomiendaRubrosDTO encomiendaRubroDTO = new EncomiendaRubrosDTO();

            encomiendaRubroDTO.IdEncomienda = encomiendaId;
            encomiendaRubroDTO.SuperficieHabilitar = 15;
            encomiendaRubroDTO.CodigoRubro = "601040";

            encomiendaRubroBL.Insert(encomiendaRubroDTO); 
        }

        [TestMethod]
        public void BorrarRubros(IEnumerable<EncomiendaRubrosDTO> rubros)
        {
            EncomiendaRubrosBL encomiendaRubroBL = new EncomiendaRubrosBL();
            foreach (var rubro in rubros)
                encomiendaRubroBL.Delete(rubro);
        }
        public void BorrarDatosLocal(EncomiendaDatosLocalDTO datosLocalDTO)
        {            
            if (datosLocalDTO != null)
            {
                EncomiendaDatosLocalBL datosLocalBl = new EncomiendaDatosLocalBL();
                datosLocalBl.Delete(datosLocalDTO);
            }
        }
        [TestMethod]
        public void CrearDatosLocal(int encomiendaId)
        {
            EncomiendaDatosLocalBL datosLocalBl = new EncomiendaDatosLocalBL();
            EncomiendaDatosLocalDTO datosLocalDTO = new EncomiendaDatosLocalDTO();
            datosLocalDTO.id_encomienda = encomiendaId;
            datosLocalDTO.cantidad_operarios_dl = 1;
            datosLocalDTO.cantidad_sanitarios_dl = 1;
            datosLocalDTO.CreateDate = DateTime.Now;
            datosLocalDTO.CreateUser = CreateUserAnexo;
            datosLocalDTO.croquis_ubicacion_dl = "1";
            datosLocalDTO.dimesion_frente_dl = 10;
            datosLocalDTO.estacionamiento_dl = false;
            datosLocalDTO.fondo_dl  = 1;
            datosLocalDTO.frente_dl = 10;
            datosLocalDTO.LastUpdateDate = null;
            datosLocalDTO.LastUpdateUser = CreateUserAnexo;
            datosLocalDTO.lateral_derecho_dl = 1;
            datosLocalDTO.lateral_izquierdo_dl = 1;
            datosLocalDTO.local_venta = 1;
            datosLocalDTO.lugar_carga_descarga_dl = false;
            datosLocalDTO.materiales_paredes_dl = "12"; 
            datosLocalDTO.materiales_pisos_dl = "123";
            datosLocalDTO.materiales_revestimientos_dl = "1eer";
            datosLocalDTO.materiales_techos_dl = "333";
            datosLocalDTO.red_transito_pesado_dl = false;
            datosLocalDTO.sanitarios_distancia_dl = 1;
            datosLocalDTO.sanitarios_ubicacion_dl = 1;
            datosLocalDTO.sobre_avenida_dl = false;
            datosLocalDTO.sobrecarga_art813_inciso = "1";
            datosLocalDTO.sobrecarga_art813_item = "123232323";
            datosLocalDTO.sobrecarga_corresponde_dl = false;
            datosLocalDTO.sobrecarga_requisitos_opcion = 1;
            datosLocalDTO.sobrecarga_tipo_observacion = 1;
            datosLocalDTO.superficie_cubierta_dl = 5;
            datosLocalDTO.superficie_descubierta_dl = 10;
            datosLocalDTO.superficie_sanitarios_dl = 1;

            datosLocalBl.Insert(datosLocalDTO);
        }
        [TestMethod]
        public int CrearAnexoTecnico(int id_solicitud, string CodigoSeguridad, Guid userId)
        { 
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var encomiendaId = encomiendaBL.CrearEncomienda(id_solicitud, CodigoSeguridad, userId);

            Assert.IsTrue(encomiendaId > 0);

            return encomiendaId;
        }
        [TestMethod]
        public void InsertarTitulares(int id_solicitud)
        {
            Guid userid = Guid.Parse("81D7CA25-757E-4D2E-B77D-1DBC54900F3C");

            string Apellido = "Melgarejo";
            string Nombres = "Daniel";
            int id_tipodoc_personal = 0;
            string Nro_Documento = "29511396";
            string Cuit = "20295113961";
            int id_tipoiibb = 0;
            string Ingresos_Brutos = "";
            string Calle = "Int. Alvear";
            int Nro_Puerta = 2930;
            string Piso = "";
            string Depto = "";
            string Torre = "";
            int id_Localidad = 0;
            string Codigo_Postal = "1651";
            string Telefono = "";
            string TelefonoMovil = "";

            string Email = "daniel.melgarejo@grupomost.com";
            bool MismoFirmante = true;
            int TipoCaracterLegalTitular = 1;

            SSITSolicitudesFirmantesPersonasFisicasBL solFirPFBL = new SSITSolicitudesFirmantesPersonasFisicasBL();
            SSITSolicitudesTitularesPersonasFisicasBL solTitPFBL = new SSITSolicitudesTitularesPersonasFisicasBL();

            SSITSolicitudesFirmantesPersonasFisicasDTO solFirPFDTO = new SSITSolicitudesFirmantesPersonasFisicasDTO();
            SSITSolicitudesTitularesPersonasFisicasDTO solTitPFDTO = new SSITSolicitudesTitularesPersonasFisicasDTO();

            solTitPFDTO.IdSolicitud = id_solicitud;
            solTitPFDTO.Apellido = Apellido;
            solTitPFDTO.Nombres = Nombres;
            solTitPFDTO.IdTipodocPersonal = id_tipodoc_personal;
            solTitPFDTO.NroDocumento = Nro_Documento;
            solTitPFDTO.Cuit = Cuit;
            solTitPFDTO.IdTipoiibb = id_tipoiibb;
            solTitPFDTO.IngresosBrutos = Ingresos_Brutos;
            solTitPFDTO.Calle = Calle;
            solTitPFDTO.NroPuerta = Nro_Puerta;
            solTitPFDTO.Piso = Piso;
            solTitPFDTO.Depto = Depto;
            solTitPFDTO.Torre = Torre;
            solTitPFDTO.IdLocalidad = id_Localidad;
            solTitPFDTO.CodigoPostal = Codigo_Postal;
            solTitPFDTO.Telefono = Telefono;
            solTitPFDTO.TelefonoMovil = TelefonoMovil;

            solTitPFDTO.Email = Email.ToLower();
            solTitPFDTO.MismoFirmante = MismoFirmante;
            solTitPFDTO.CreateUser = userid;
            solTitPFDTO.CreateDate = DateTime.Now;

            solTitPFDTO.DtoFirmantes = new SSITSolicitudesFirmantesPersonasFisicasDTO();
            solTitPFDTO.DtoFirmantes.IdSolicitud = id_solicitud;

            solTitPFDTO.DtoFirmantes.Apellido = Apellido;
            solTitPFDTO.DtoFirmantes.Nombres = Nombres;
            solTitPFDTO.DtoFirmantes.IdTipoDocPersonal = id_tipodoc_personal;
            solTitPFDTO.DtoFirmantes.NroDocumento = Nro_Documento;
            solTitPFDTO.DtoFirmantes.IdTipoCaracter = TipoCaracterLegalTitular;

            Assert.IsTrue(solTitPFBL.Insert(solTitPFDTO));

        }

        public void InsertarUbicacion(int id_solicitud)
        {
            //Alta de la ubicación   
            SSITSolicitudesUbicacionesBL ssitBL = new SSITSolicitudesUbicacionesBL();
            SSITSolicitudesUbicacionesDTO ubicacion = new SSITSolicitudesUbicacionesDTO();
            ubicacion.IdSolicitud = id_solicitud;
            ubicacion.IdUbicacion = 283844;
            ubicacion.IdSubtipoUbicacion = 0;
            ubicacion.LocalSubtipoUbicacion = "";
            ubicacion.DeptoLocalUbicacion = "";
            ubicacion.Depto = "";
            ubicacion.Local = "";
            ubicacion.Torre = "";
            ubicacion.CreateDate = DateTime.Now;
            ubicacion.CreateUser = CreateUser;

            List<UbicacionesPropiedadhorizontalDTO> propiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
            ////Alta de las propiedades horizontales

            //propiedadesHorizontales.Add(new UbicacionesPropiedadhorizontalDTO()
            //{
            //    IdPropiedadHorizontal = id_propiedad_horizontal
            //});

            List<UbicacionesPuertasDTO> puertas = new List<UbicacionesPuertasDTO>();

            puertas.Add(new UbicacionesPuertasDTO()
            {
                CodigoCalle = 7018,
                NroPuertaUbic = 150,
                IdUbicacion = 283844
            });

            ubicacion.PropiedadesHorizontales = propiedadesHorizontales;
            ubicacion.Puertas = puertas;

            Assert.IsTrue(ssitBL.Insert(ubicacion));
        }

        public void BorrarUbicaciones(IEnumerable<EncomiendaUbicacionesDTO> ubicaciones)
        {
            foreach (var ubicacion in ubicaciones)
                BorrarUbicaciones(ubicacion);

        }

        public void BorrarUbicaciones(IEnumerable<SSITSolicitudesUbicacionesDTO> ubicaciones)
        {
            foreach (var ubicacion in ubicaciones)
                BorrarUbicaciones(ubicacion);
            
        }

        public void BorrarUbicaciones(EncomiendaUbicacionesDTO ubicacion)
        {
            EncomiendaUbicacionesBL encomiendaUbicacionesBL = new EncomiendaUbicacionesBL();
            encomiendaUbicacionesBL.Delete(ubicacion);
        }

        public void BorrarUbicaciones(SSITSolicitudesUbicacionesDTO ubicacion)
        {
            SSITSolicitudesUbicacionesBL ssitBL = new SSITSolicitudesUbicacionesBL();
            ssitBL.Delete(ubicacion);
        }

        public void BorrarTitularesSolicitud(int IdSolicitud)
        {
            SSITSolicitudesFirmantesPersonasFisicasBL firmantesPersonasFisicasBL = new SSITSolicitudesFirmantesPersonasFisicasBL();
            IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO> personasFisicasT = firmantesPersonasFisicasBL.GetByFKIdSolicitud(IdSolicitud);

            foreach (var personaFisica in personasFisicasT)
                firmantesPersonasFisicasBL.Delete(personaFisica);

            SSITSolicitudesTitularesPersonasFisicasBL titularesPersonasFisicasBL = new SSITSolicitudesTitularesPersonasFisicasBL();
            IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO> personasFisicas = titularesPersonasFisicasBL.GetByFKIdSolicitud(IdSolicitud);

            foreach (var personaFisica in personasFisicas)
                titularesPersonasFisicasBL.Delete(personaFisica);
        }
        
        [TestMethod]
        public void BorrarTitularesAnexo(int id_encomienda)
        {
            EncomiendaTitularesPersonasFisicasBL TitularesPersonaFisicaBL = new EncomiendaTitularesPersonasFisicasBL();
 
            EncomiendaTitularesPersonasJuridicasBL TitularesPersonaJuridicaBL = new EncomiendaTitularesPersonasJuridicasBL();

            var TitularesPersonaFisica = TitularesPersonaFisicaBL.GetByFKIdEncomienda(id_encomienda);
            var TitularesPersonaJuridica = TitularesPersonaJuridicaBL.GetByFKIdEncomienda(id_encomienda);


            foreach (var personaFisica in TitularesPersonaFisica)
                TitularesPersonaFisicaBL.Delete(personaFisica);

            foreach (var personaJuridica in TitularesPersonaJuridica)
                TitularesPersonaJuridicaBL.Delete(personaJuridica);
        }
        [TestMethod]
        public void BorraSolicitud(SSITSolicitudesDTO Solicitud)
        {
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            SSITSolicitudesHistorialEstadosBL historialEstadosBL = new SSITSolicitudesHistorialEstadosBL();
            var historial = historialEstadosBL.GetByFKIdSolicitud(Solicitud.IdSolicitud);
            
            historialEstadosBL.Delete(historial); 

            blSol.Delete(Solicitud);
        }

        [TestMethod]
        public void buscarBandejaEntradaSSIT()
        {
            SSITSolicitudesBL ssitBl = new SSITSolicitudesBL();
            ssitBl.GetDireccionesSSIT(new List<int>() { 335728, 335717, 307302 });
            
        }

        [TestMethod]
        public void InsertAmpliacion()
        {
            AmpliacionesBL blSol = new AmpliacionesBL();

            DataTransferObject.SSITSolicitudesDTO sol = new DataTransferObject.SSITSolicitudesDTO();
            DataTransferObject.SSITSolicitudesOrigenDTO oSSITSolicitudesOrigenDTO = null;

            oSSITSolicitudesOrigenDTO = new DataTransferObject.SSITSolicitudesOrigenDTO();

            oSSITSolicitudesOrigenDTO.id_solicitud_origen = 219928;

            oSSITSolicitudesOrigenDTO.CreateDate = DateTime.Now;



            sol.CodigoSeguridad = "8U8L";
            sol.IdEstado = 0;

            sol.IdTipoTramite = 3;
            sol.IdTipoExpediente = 0;
            sol.IdSubTipoExpediente = 0;
            sol.CreateDate = DateTime.Now;
            sol.CreateUser = Guid.Parse("81d7ca25-757e-4d2e-b77d-1dbc54900f3c");
            sol.SSITSolicitudesOrigenDTO = oSSITSolicitudesOrigenDTO;
            sol.Servidumbre_paso = false;

            int id_solicitud = blSol.Insert(sol);

        }

    }
}

