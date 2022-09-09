using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaPlanosBL : IEncomiendaPlanosBL<EncomiendaPlanosDTO>
    {
        private EncomiendaPlanosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaPlanosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {

                #region "Encomienda_Planos"
                cfg.CreateMap<Encomienda_Planos, EncomiendaPlanosDTO>()
                    .ForMember(dest => dest.TiposDePlanosDTO, source => source.MapFrom(p => p.TiposDePlanos));

                cfg.CreateMap<EncomiendaPlanosDTO, Encomienda_Planos>()
                    .ForMember(dest => dest.TiposDePlanos, source => source.MapFrom(p => p.TiposDePlanosDTO));
                #endregion

                #region "TiposDePlanos"
                cfg.CreateMap<TiposDePlanosDTO, TiposDePlanos>();

                cfg.CreateMap<TiposDePlanos, TiposDePlanosDTO>();
                #endregion
            });
            mapperBase = config.CreateMapper();


        }


        public EncomiendaPlanosDTO Single(int id_encomienda_plano)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaPlanosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_encomienda_plano);
                var entityDto = mapperBase.Map<Encomienda_Planos, EncomiendaPlanosDTO>(entity);

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
        public IEnumerable<EncomiendaPlanosDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaPlanosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Planos>, IEnumerable<EncomiendaPlanosDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de inserccion
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(EncomiendaPlanosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlanosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaPlanosDTO, Encomienda_Planos>(objectDto);
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
        #region Métodos de actualizacion
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaPlanosDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlanosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaPlanosDTO, Encomienda_Planos>(objectDTO);
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
        #region Métodos de eliminacion
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(EncomiendaPlanosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlanosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaPlanosDTO, Encomienda_Planos>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool existe(int id_tipo_plano, string nombre, int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaPlanosRepository(this.uowF.GetUnitOfWork());
                return repo.existe(id_tipo_plano, nombre, id_encomienda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SetFechaPresentado(SSIT_Solicitudes solicitudEntity)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaPlanosRepository(unitOfWork);

                    var ListencomiendasEntity = solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda);

                    foreach (var ep in ListencomiendasEntity)
                    {
                        var ListAdjPlanos = repo.GetByFKIdEncomienda(ep.id_encomienda);

                        foreach (var ap in ListAdjPlanos)
                        {
                            if (ap.fechaPresentado == null)
                            {
                                ap.fechaPresentado = DateTime.Now;
                                repo.Update(ap);
                            }
                        }
                    }
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

