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

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronDatosLocalBL : IConsultaPadronDatosLocalBL<ConsultaPadronDatosLocalDTO>
    {               
		private ConsultaPadronDatosLocalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ConsultaPadronDatosLocalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_DatosLocal, ConsultaPadronDatosLocalDTO>()
                    .ForMember(dest => dest.IdConsultaPadronDatosLocal, source => source.MapFrom(p => p.id_cpadrondatoslocal))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.SuperficieCubiertaDl, source => source.MapFrom(p => p.superficie_cubierta_dl))
                    .ForMember(dest => dest.SuperficieDescubiertaDl, source => source.MapFrom(p => p.superficie_descubierta_dl))
                    .ForMember(dest => dest.DimesionFrenteDl, source => source.MapFrom(p => p.dimesion_frente_dl))
                    .ForMember(dest => dest.LugarCargaDescargaDl, source => source.MapFrom(p => p.lugar_carga_descarga_dl))
                    .ForMember(dest => dest.EstacionamientoDl, source => source.MapFrom(p => p.estacionamiento_dl))
                    .ForMember(dest => dest.RedTransitoPesadoDl, source => source.MapFrom(p => p.red_transito_pesado_dl))
                    .ForMember(dest => dest.SobreAvenidaDl, source => source.MapFrom(p => p.sobre_avenida_dl))
                    .ForMember(dest => dest.MaterialesPisosDl, source => source.MapFrom(p => p.materiales_pisos_dl))
                    .ForMember(dest => dest.MaterialesParedesDl, source => source.MapFrom(p => p.materiales_paredes_dl))
                    .ForMember(dest => dest.MaterialesTechosDl, source => source.MapFrom(p => p.materiales_techos_dl))
                    .ForMember(dest => dest.MaterialesRevestimientosDl, source => source.MapFrom(p => p.materiales_revestimientos_dl))
                    .ForMember(dest => dest.SanitariosUbicacionDl, source => source.MapFrom(p => p.sanitarios_ubicacion_dl))
                    .ForMember(dest => dest.SanitariosDistanciaDl, source => source.MapFrom(p => p.sanitarios_distancia_dl))
                    .ForMember(dest => dest.CroquisUbicacionDl, source => source.MapFrom(p => p.croquis_ubicacion_dl))
                    .ForMember(dest => dest.CantidadSanitariosDl, source => source.MapFrom(p => p.cantidad_sanitarios_dl))
                    .ForMember(dest => dest.SuperficieSanitariosDl, source => source.MapFrom(p => p.superficie_sanitarios_dl))
                    .ForMember(dest => dest.FrenteDl, source => source.MapFrom(p => p.frente_dl))
                    .ForMember(dest => dest.FondoDl, source => source.MapFrom(p => p.fondo_dl))
                    .ForMember(dest => dest.LateralIzquierdoDl, source => source.MapFrom(p => p.lateral_izquierdo_dl))
                    .ForMember(dest => dest.LateralDerechoDl, source => source.MapFrom(p => p.lateral_derecho_dl))
                    .ForMember(dest => dest.CantidadOperariosDl, source => source.MapFrom(p => p.cantidad_operarios_dl))
                    .ForMember(dest => dest.Local_venta, source => source.MapFrom(p => p.local_venta))
                    .ForMember(dest => dest.Dj_certificado_sobrecarga, source => source.MapFrom(p => p.dj_certificado_sobrecarga));


                cfg.CreateMap<ConsultaPadronDatosLocalDTO, CPadron_DatosLocal>()
                    .ForMember(dest => dest.id_cpadrondatoslocal, source => source.MapFrom(p => p.IdConsultaPadronDatosLocal))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.superficie_cubierta_dl, source => source.MapFrom(p => p.SuperficieCubiertaDl))
                    .ForMember(dest => dest.superficie_descubierta_dl, source => source.MapFrom(p => p.SuperficieDescubiertaDl))
                    .ForMember(dest => dest.dimesion_frente_dl, source => source.MapFrom(p => p.DimesionFrenteDl))
                    .ForMember(dest => dest.lugar_carga_descarga_dl, source => source.MapFrom(p => p.LugarCargaDescargaDl))
                    .ForMember(dest => dest.estacionamiento_dl, source => source.MapFrom(p => p.EstacionamientoDl))
                    .ForMember(dest => dest.red_transito_pesado_dl, source => source.MapFrom(p => p.RedTransitoPesadoDl))
                    .ForMember(dest => dest.sobre_avenida_dl, source => source.MapFrom(p => p.SobreAvenidaDl))
                    .ForMember(dest => dest.materiales_pisos_dl, source => source.MapFrom(p => p.MaterialesPisosDl))
                    .ForMember(dest => dest.materiales_paredes_dl, source => source.MapFrom(p => p.MaterialesParedesDl))
                    .ForMember(dest => dest.materiales_techos_dl, source => source.MapFrom(p => p.MaterialesTechosDl))
                    .ForMember(dest => dest.materiales_revestimientos_dl, source => source.MapFrom(p => p.MaterialesRevestimientosDl))
                    .ForMember(dest => dest.sanitarios_ubicacion_dl, source => source.MapFrom(p => p.SanitariosUbicacionDl))
                    .ForMember(dest => dest.sanitarios_distancia_dl, source => source.MapFrom(p => p.SanitariosDistanciaDl))
                    .ForMember(dest => dest.croquis_ubicacion_dl, source => source.MapFrom(p => p.CroquisUbicacionDl))
                    .ForMember(dest => dest.cantidad_sanitarios_dl, source => source.MapFrom(p => p.CantidadSanitariosDl))
                    .ForMember(dest => dest.superficie_sanitarios_dl, source => source.MapFrom(p => p.SuperficieSanitariosDl))
                    .ForMember(dest => dest.frente_dl, source => source.MapFrom(p => p.FrenteDl))
                    .ForMember(dest => dest.fondo_dl, source => source.MapFrom(p => p.FondoDl))
                    .ForMember(dest => dest.lateral_izquierdo_dl, source => source.MapFrom(p => p.LateralIzquierdoDl))
                    .ForMember(dest => dest.lateral_derecho_dl, source => source.MapFrom(p => p.LateralDerechoDl))
                    .ForMember(dest => dest.cantidad_operarios_dl, source => source.MapFrom(p => p.CantidadOperariosDl))
                    .ForMember(dest => dest.local_venta, source => source.MapFrom(p => p.Local_venta))
                    .ForMember(dest => dest.dj_certificado_sobrecarga, source => source.MapFrom(p => p.Dj_certificado_sobrecarga));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronDatosLocalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronDatosLocalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_DatosLocal>, IEnumerable<ConsultaPadronDatosLocalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronDatosLocalDTO Single(int IdConsultaPadronDatosLocal )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronDatosLocalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronDatosLocal);
                var entityDto = mapperBase.Map<CPadron_DatosLocal, ConsultaPadronDatosLocalDTO>(entity);
     
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
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronDatosLocalDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronDatosLocalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_DatosLocal>, IEnumerable<ConsultaPadronDatosLocalDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronDatosLocalDTO> GetByFKCreateUser(Guid CreateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronDatosLocalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKCreateUser(CreateUser);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_DatosLocal>, IEnumerable<ConsultaPadronDatosLocalDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronDatosLocalDTO> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronDatosLocalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKLastUpdateUser(LastUpdateUser);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_DatosLocal>, IEnumerable<ConsultaPadronDatosLocalDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronDatosLocalDTO objectDto)
		{
		    try
		    {
                ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                var consultaPadronDTO = consultaPadronSolicitudesBL.Single(objectDto.IdConsultaPadron);

                if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDatosLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<ConsultaPadronDatosLocalDTO, CPadron_DatosLocal>(objectDto);                   
		            
                    repo.Insert(elementDto);
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
		public void Update(ConsultaPadronDatosLocalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDatosLocalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronDatosLocalDTO, CPadron_DatosLocal>(objectDTO);                   
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
		public void Delete(ConsultaPadronDatosLocalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDatosLocalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronDatosLocalDTO, CPadron_DatosLocal>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdConsultaPadron(int IdConsultaPadron)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDatosLocalRepository(unitOfWork);                    
					var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
					foreach(var element in elements)				
						repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		public void DeleteByFKCreateUser(Guid CreateUser)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDatosLocalRepository(unitOfWork);                    
					var elements = repo.GetByFKCreateUser(CreateUser);
					foreach(var element in elements)				
						repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		public void DeleteByFKLastUpdateUser(Guid LastUpdateUser)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDatosLocalRepository(unitOfWork);                    
					var elements = repo.GetByFKLastUpdateUser(LastUpdateUser);
					foreach(var element in elements)				
						repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		
		

		#endregion
    }
}

