using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using StaticClass;
using System;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
	public class EncomiendaDatosLocalBL : IEncomiendaDatosLocalBL<EncomiendaDatosLocalDTO>
    {               
		private EncomiendaDatosLocalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaDatosLocalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
		public EncomiendaDatosLocalDTO Single(int IdEncomiendaDatosLocal )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDatosLocalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaDatosLocal);
                var entityDto = mapperBase.Map<Encomienda_DatosLocal, EncomiendaDatosLocalDTO>(entity);
     
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
        public EncomiendaDatosLocalDTO GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaDatosLocalRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            if (elements == null)
                return null;
            var elementsDto = mapperBase.Map<Encomienda_DatosLocal, EncomiendaDatosLocalDTO>(elements);
            return elementsDto;				
		}

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public int Insert(EncomiendaDatosLocalDTO objectDto)
		{
		    try
		    {
                EncomiendaBL encomiendaBL = new EncomiendaBL();  
                EncomiendaDTO encomienda = encomiendaBL.Single(objectDto.id_encomienda);
                if (encomienda.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomienda.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaDatosLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>(objectDto);
                    var insertOk = repo.Insert(elementDto);
                    unitOfWork.Commit();
                    return elementDto.id_encomiendadatoslocal;
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
		public void Update(EncomiendaDatosLocalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaDatosLocalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>(objectDTO);                   
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
		public void Delete(EncomiendaDatosLocalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaDatosLocalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>(objectDto);                   
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
    }
}

