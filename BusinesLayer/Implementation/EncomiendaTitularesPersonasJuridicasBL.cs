using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using System.Data;

namespace BusinesLayer.Implementation
{
	public class EncomiendaTitularesPersonasJuridicasBL : IEncomiendaTitularesPersonasJuridicasBL<EncomiendaTitularesPersonasJuridicasDTO>
    {               
		private EncomiendaTitularesPersonasJuridicasRepository repo = null;     
        private EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository  repoTitPJPF = null;
        private EncomiendaFirmantesPersonasJuridicasRepository repoFirPJ = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperFirPJ;
        IMapper mapperTitPJPF;

        public EncomiendaTitularesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.NroIb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas, EncomiendaTitularesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NroIb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));
            });
            var configFirPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj));
                
                cfg.CreateMap<Encomienda_Firmantes_PersonasJuridicas, EncomiendaFirmantesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePj));
            });

            var configTitPJPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona));

                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>().ReverseMap()                
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona));
            });

            mapperTitPJPF = configTitPJPF.CreateMapper();
            mapperFirPJ = configFirPJ.CreateMapper();
            mapperBase = config.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
		public EncomiendaTitularesPersonasJuridicasDTO Single(int IdPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<Encomienda_Titulares_PersonasJuridicas, EncomiendaTitularesPersonasJuridicasDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSociedad"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByFKIdTipoSociedad(int IdTipoSociedad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoIb"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByFKIdTipoIb(int IdTipoIb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoIb(IdTipoIb);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}


        public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByIdEncomiendaCuitIdPersonaJuridica(int id_encomienda, string Cuit, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaCuitIdPersonaJuridica(id_encomienda, Cuit, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<EncomiendaTitularesPersonasJuridicasDTO> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, IdPersonaJuridica);
            var elementsDto = mapperBase.Map < IEnumerable<Encomienda_Titulares_PersonasJuridicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }


		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
        /// 
        public bool Insert(EncomiendaTitularesPersonasJuridicasDTO objectDto)
		{
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaBL encomiendaBL = new EncomiendaBL();
                    var encomiendaDTO = encomiendaBL.Single(objectDto.IdEncomienda);
                    if (encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Completa && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Incompleta
                        && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Confirmada)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    repo = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);
                    repoTitPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);
                    repoFirPJ = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);

                    var elementDto = mapperBase.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(objectDto);

                    var insertOk = repo.Insert(elementDto);

                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach(var itemFirPj in objectDto.firmantesSH)
                        {
                            EncomiendaFirmantesPersonasJuridicasDTO encFirPerJur = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPerJur.IdEncomienda = elementDto.id_encomienda;
                            encFirPerJur.IdPersonaJuridica = elementDto.id_personajuridica;
                            encFirPerJur.Apellido = itemFirPj.Apellidos;
                            encFirPerJur.Nombres = itemFirPj.Nombres;
                            encFirPerJur.IdTipoDocPersonal = itemFirPj.id_tipodoc_personal;
                            encFirPerJur.NroDocumento = itemFirPj.NroDoc;
                            encFirPerJur.Email = itemFirPj.email;
                            encFirPerJur.IdTipoCaracter = itemFirPj.id_tipocaracter;
                            encFirPerJur.CargoFirmantePj = itemFirPj.cargo_firmante;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(encFirPerJur);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);
                            
                            foreach(var itemTitPjSH in objectDto.titularesSH )
                            {
                                if ((Guid)itemTitPjSH.RowId == (Guid)itemFirPj.rowid_titular)
                                {
                                    EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encoTitPerJurPerFis = new EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO();
                                    encoTitPerJurPerFis.IdEncomienda = elementDto.id_encomienda;
                                    encoTitPerJurPerFis.IdPersonaJuridica = elementDto.id_personajuridica;
                                    encoTitPerJurPerFis.IdFirmantePj = encFirPerJur.IdFirmantePj;
                                    encoTitPerJurPerFis.Apellido = itemTitPjSH.Apellidos;
                                    encoTitPerJurPerFis.Nombres = itemTitPjSH.Nombres;
                                    encoTitPerJurPerFis.IdTipoDocPersonal = itemTitPjSH.IdTipoDocPersonal;
                                    encoTitPerJurPerFis.NroDocumento = itemTitPjSH.NroDoc;
                                    encoTitPerJurPerFis.Email = itemTitPjSH.Email;
                                    encoTitPerJurPerFis.FirmanteMismaPersona = itemFirPj.misma_persona.Value;
                                    var elementDtoTitPerJurInsert = mapperTitPJPF.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(encoTitPerJurPerFis);
                                    repoTitPJPF.Insert(elementDtoTitPerJurInsert);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {

                        foreach (var item in objectDto.EncomiendaFirmantesPersonasJuridicasDTO)
                        {
                            EncomiendaFirmantesPersonasJuridicasDTO encFirPerJurInsert = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPerJurInsert.IdEncomienda = elementDto.id_encomienda;
                            encFirPerJurInsert.IdPersonaJuridica = elementDto.id_personajuridica;
                            encFirPerJurInsert.Apellido = item.Apellido;
                            encFirPerJurInsert.Nombres = item.Nombres;
                            encFirPerJurInsert.IdTipoDocPersonal = item.IdTipoDocPersonal;
                            encFirPerJurInsert.NroDocumento = item.NroDocumento;
                            encFirPerJurInsert.Email = item.Email;
                            encFirPerJurInsert.IdTipoCaracter = item.IdTipoCaracter;
                            encFirPerJurInsert.CargoFirmantePj = item.CargoFirmantePj;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(encFirPerJurInsert);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);
                        }
                    }

                    unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
		

		#endregion
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Modifica la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public void Update(EncomiendaTitularesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(objectDTO);                   
		            repo.Update(elementDTO);
		            unitOfWork.Commit();           
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		

		#endregion
		#region Métodos de actualizacion e insert
		/// <summary>
		/// elimina la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>      
		public void Delete(EncomiendaTitularesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		

		#endregion

        public bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var compare = repo.CompareDataSaved(idEncomienda1, idEncomienda2);
            return compare;
        }
    }
}

