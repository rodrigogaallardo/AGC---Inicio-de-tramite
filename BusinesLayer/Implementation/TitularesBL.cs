using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using ExternalService.Class;
using ExternalService;
using StaticClass;

namespace BusinesLayer.Implementation
{
    public class TitularesBL : ITitularesBL
    {
        public static string noExistenTitulares = "";
        private TitularesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseSH;

        private SSITSolicitudesTitularesPersonasJuridicasRepository repoTitPJ = null;
        private SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository repoTitPJPF = null;
        private SSITSolicitudesFirmantesPersonasJuridicasRepository repoFirPJ = null;    

        public TitularesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TitularesDTO, TitularesEntity>().ReverseMap();
                cfg.CreateMap<PersonaTadDTO, PersonaTadEntity>().ReverseMap();
                cfg.CreateMap<DomicilioConstituidoDTO, DomicilioConstituido>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configSH = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TitularesSHDTO, TitularesSHEntity>().ReverseMap();
            });
            mapperBaseSH = configSH.CreateMapper();
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<TitularesDTO> GetTitularesEncomienda(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesEncomienda(id_encomienda);
                if (titulares != null)
                {
                    var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                    return elementsDto;
                }
                else
                {
                    noExistenTitulares = string.Format("No existen titulares para la solicitud: " + Convert.ToString(id_encomienda));
                    throw new Exception(noExistenTitulares);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHDTO> GetTitularesSHConsultaPadron(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesSHConsultaPadron(IdPersonaJuridica);
                var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                return elementsDto;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesDTO> GetTitularesTransferencias(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesTransferencias(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TitularesDTO> GetTitularesTransferenciasANT(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesTransferenciasANT(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTitPJ(int idSolicitud, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {

                    repoTitPJ = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    repoTitPJPF = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);
                    repoFirPJ = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);

                    var titPJPF = repoTitPJPF.GetByIdSolicitudIdPersonaJuridica(idSolicitud, IdPersonaJuridica);
                    foreach (var tit in titPJPF)
                        repoTitPJPF.Delete(tit);

                    var firPJ = repoFirPJ.GetByIdSolicitudIdPersonaJuridica(idSolicitud, IdPersonaJuridica);
                    foreach (var fir in firPJ)
                        repoFirPJ.Delete(fir);

                    var titPJ = repoTitPJ.GetByIdSolicitudIdPersonaJuridica(idSolicitud, IdPersonaJuridica);
                    foreach (var tit in titPJ)
                        repoTitPJ.Delete(tit);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesDTO> GetTitularesSolicitud(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesSolicitud(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TitularesDTO> GetTitularesTransmision(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesTransmision(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<TitularesSHDTO> GetTitularesSolicitudSHConsultaPadron(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesSolicitudSHConsultaPadron(IdPersonaJuridica);
                var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHDTO> GetTitularesEncomiendaSH(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesEncomiendaSH(IdPersonaJuridica);
                if (titulares != null)
                {
                    var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                    return elementsDto;
                }
                else
                {
                    noExistenTitulares = string.Format("No existen titulares para la solicitud: " + Convert.ToString(IdPersonaJuridica));
                    throw new Exception(noExistenTitulares);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TitularesSHDTO> GetTitularesSHSolicitud(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesSHSolicitud(IdPersonaJuridica);
                if (titulares != null)
                {
                    var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                    return elementsDto;
                }
                else
                {
                    noExistenTitulares = string.Format("No existen titulares para la solicitud: " + Convert.ToString(IdPersonaJuridica));
                    throw new Exception(noExistenTitulares);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHDTO> GetTitularesSHTransferencias(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesTransferenciaSH(IdPersonaJuridica);
                
                var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                return elementsDto;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TitularesSHDTO> GetTitularesSHTransferenciasANT(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesTransferenciaSHANT(IdPersonaJuridica);

                var elementsDto = mapperBaseSH.Map<IEnumerable<TitularesSHEntity>, IEnumerable<TitularesSHDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesDTO> GetTitularesConsultaPadron(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesConsultaPadron(IdSolicitud);

                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesDTO> GetTitularesConsultaPadronSolicitud(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TitularesRepository(this.uowF.GetUnitOfWork());

                var titulares = repo.GetTitularesConsultaPadronSolicitud(IdSolicitud);

                var elementsDto = mapperBase.Map<IEnumerable<TitularesEntity>, IEnumerable<TitularesDTO>>(titulares);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PersonaTadDTO GetPersonaTAD(string cuit)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            var parametros = new ParametrosRepository(this.uowF.GetUnitOfWork());

            string url = parametros.GetParametroChar("Servicios.GCBA");
            string metodo = parametros.GetParametroChar("Servicios.GCBA.Participantes");

            PersonaTadEntity persona;
            try
            {
                persona = wsTAD.GetPersonaTAD($"{url}{metodo}", cuit);
            }
            catch (Exception ex)
            {
                if (Funciones.isDesarrollo())
                {
                    persona = new PersonaTadEntity();
                    persona.Apellido1 = "Nombre Prueba";
                    persona.Nombre1 = "Apellido Prueba";
                    persona.RazonSocial = "Razon Social Prueba";
                    persona.DocumentoIdentidad = cuit.Substring(2, 8);
                    persona.Cuit = cuit;
                    persona.Telefono = "123456789";
                    persona.Email = "prueba@prueba.com";
                    DomicilioConstituido domicilio = new DomicilioConstituido();
                    domicilio.CodPostal = "1000";
                    domicilio.Direccion = "Prueba Calle";
                    domicilio.Altura = "9999";
                    persona.DomicilioConstituido = domicilio;
                }
                else
                {
                    throw ex;
                }
            }

            var personaDTO = mapperBase.Map<PersonaTadDTO>(persona);
            if (personaDTO != null && personaDTO.RazonSocial != null)
            {
                LogError.Write("Razon Social (ANTES): " + personaDTO.RazonSocial);
                personaDTO.RazonSocial = personaDTO.RazonSocial.ToUpper().Replace('#', 'Ñ');
                LogError.Write("Razon Social (DESPUES): " + personaDTO.RazonSocial);
            }
            return personaDTO;
        }
    }
}
