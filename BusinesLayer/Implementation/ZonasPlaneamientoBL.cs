using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
	public class ZonasPlaneamientoBL : IZonasPlaneamientoBL<ZonasPlaneamientoDTO>
    {               
		private ZonasPlaneamientoRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ZonasPlaneamientoBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>().ReverseMap()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>().ReverseMap()
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ZonasPlaneamientoDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ZonasPlaneamientoDTO> GetZonaPlaneamientoByIdEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetZonaPlaneamientoByIdEncomienda(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ZonasPlaneamientoDTO> GetByCodZonaPlaneamiento(string CodZonaPla)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByCodZonaPlaneamiento(CodZonaPla);
                var entityDto = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public ZonasPlaneamientoDTO Single(int IdZonaPlaneamiento )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdZonaPlaneamiento);
                var entityDto = mapperBase.Map<Zonas_Planeamiento, ZonasPlaneamientoDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ZonasPlaneamientoDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ZonasPlaneamientoRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ZonasPlaneamientoDTO, Zonas_Planeamiento>(objectDto);                   
		            var insertOk = repo.Insert(elementDto);
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
		public void Update(ZonasPlaneamientoDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ZonasPlaneamientoRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ZonasPlaneamientoDTO, Zonas_Planeamiento>(objectDTO);                   
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
		public void Delete(ZonasPlaneamientoDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ZonasPlaneamientoRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ZonasPlaneamientoDTO, Zonas_Planeamiento>(objectDto);                   
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IList<ZonasPlaneamientoDTO> GetZonasEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetZonasEncomienda(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elements);

                var elementsComp = repo.GetZonaComplementariaEncomienda(IdEncomienda);
                var elementsDtoComp = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elementsComp);

                var totalElements = elementsDto.Concat(elementsDtoComp);
                var lstZonasCombo = new List<ZonasPlaneamientoDTO>();  
                if (totalElements.Any()) 
                {
                    string ZonaParcela = totalElements.FirstOrDefault().CodZonaPla;
                    foreach (var zona in totalElements)
                    {
                        /* Lógica propuesta por Juan Barrionuevo (AGC)

                             R1 - R2a - R2b - C1 - C2 - C3 - E1 - E3
                            PUEDEN TOMARSE LOS USOS FRENTISTAS ENTRE SI.

                            R1
                            UNICAMENTE SI ESTA EN PARCELA DE ESQUINA.

                            E2 - I
                            SOLAMENTE ENTRE SI MISMOS.

                        */

                        string ZonaFila = zona.CodZonaPla;
                        if (ZonaParcela.Equals("E2") || ZonaParcela.Equals("I"))
                        {
                            if (ZonaFila.Equals("E2") || ZonaFila.Equals("I"))
                            {
                                if (!lstZonasCombo.Any(p => p.CodZonaPla.Equals(zona.CodZonaPla)))
                                    lstZonasCombo.Add(zona);
                            }
                        }
                        else
                        {
                            if (!ZonaFila.Equals("E2") && !ZonaFila.Equals("I"))
                            {
                                if (!lstZonasCombo.Any(p => p.CodZonaPla.Equals(zona.CodZonaPla)))
                                    lstZonasCombo.Add(zona);
 
                            }

                        }
                    }
                }

                return lstZonasCombo;
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
        public IList<ZonasPlaneamientoDTO> GetZonasConsultaPadron(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ZonasPlaneamientoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetZonasConsultaPadron(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elements);

                var elementsComp = repo.GetZonaComplementariaConsultaPadron(IdSolicitud);
                var elementsDtoComp = mapperBase.Map<IEnumerable<Zonas_Planeamiento>, IEnumerable<ZonasPlaneamientoDTO>>(elements);

                var totalElements = elementsDto.Concat(elementsDtoComp);
                var lstZonasCombo = new List<ZonasPlaneamientoDTO>();
                if (totalElements.Any())
                {
                    string ZonaParcela = totalElements.FirstOrDefault().CodZonaPla;
                    foreach (var zona in totalElements)
                    {
                        /* Lógica propuesta por Juan Barrionuevo (AGC)

                             R1 - R2a - R2b - C1 - C2 - C3 - E1 - E3
                            PUEDEN TOMARSE LOS USOS FRENTISTAS ENTRE SI.

                            R1
                            UNICAMENTE SI ESTA EN PARCELA DE ESQUINA.

                            E2 - I
                            SOLAMENTE ENTRE SI MISMOS.

                        */

                        string ZonaFila = zona.CodZonaPla;
                        if (ZonaParcela.Equals("E2") || ZonaParcela.Equals("I"))
                        {
                            if (ZonaFila.Equals("E2") || ZonaFila.Equals("I"))
                            {
                                if (!lstZonasCombo.Any(p => p.CodZonaPla.Equals(zona.CodZonaPla)))
                                    lstZonasCombo.Add(zona);
                            }
                        }
                        else
                        {
                            if (!ZonaFila.Equals("E2") && !ZonaFila.Equals("I"))
                            {
                                if (!lstZonasCombo.Any(p => p.CodZonaPla.Equals(zona.CodZonaPla)))
                                    lstZonasCombo.Add(zona);
                            }

                        }
                    }
                }

                return lstZonasCombo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

