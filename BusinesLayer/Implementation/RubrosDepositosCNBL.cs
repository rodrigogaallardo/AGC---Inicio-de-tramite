using IBusinessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using DataTransferObject;
using AutoMapper;
using BaseRepository;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;
using System.Data;

namespace BusinesLayer.Implementation
{
    public class RubrosDepositosCNBL : IRubrosDepositosCNBL<RubrosDepositosCNDTO>
    {
        private RubrosDepositosCNRepository repo = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RubrosDepositosCNBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosDepositosCNDTO, RubrosDepositosCN>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN_RangosSuperficie, RubrosDepositosCN_RangosSuperficieDTO>();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
        public IEnumerable<RubrosDepositosCNDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosDepositosCNRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetAll();
                var elementsDTO = mapperBase.Map<IEnumerable<RubrosDepositosCN>, IEnumerable<RubrosDepositosCNDTO>>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RubrosDepositosCNDTO Single(int idDeposito)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosDepositosCNRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Single(idDeposito);
                var elementsDTO = mapperBase.Map<RubrosDepositosCN, RubrosDepositosCNDTO>(elements);

                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(RubrosDepositosCNDTO rubrosDepositosCN, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new RubrosDepositosCNRepository(unitOfWork);
                    var entities = mapperBase.Map<RubrosDepositosCNDTO, RubrosDepositosCN>(rubrosDepositosCN);
                    var insertOk = repo.Insert(entities);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistDepositos(List<ZonasDistritosDTO> ZonasDistritos)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosDepositosCNRepository(this.uowF.GetUnitOfWork());

            var elements = repo.GetAll();
            elements =
                (
                    from e in elements
                    where
                        ZonasDistritos.Exists(z => z.Codigo == e.ZonaMixtura1) ||
                        ZonasDistritos.Exists(z => z.Codigo == e.ZonaMixtura2) ||
                        ZonasDistritos.Exists(z => z.Codigo == e.ZonaMixtura3) ||
                        ZonasDistritos.Exists(z => z.Codigo == e.ZonaMixtura4)
                    select
                        e
                );

            return elements.Any();
        }


        public List<clsItemGrillaSeleccionarDepositos> CargarCuadroDepositos(EncomiendaDTO encomienda, decimal superficie)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(); 
            repo = new RubrosDepositosCNRepository(this.uowF.GetUnitOfWork());
            var repoEnc = new EncomiendaRepository(this.uowF.GetUnitOfWork());

            var repoNormativa = new EncomiendaNormativasRepository(this.uowF.GetUnitOfWork());
            var normativa = repoNormativa.GetByFKIdEncomienda(encomienda.IdEncomienda).FirstOrDefault();

            IEnumerable<clsItemGrillaSeleccionarDepositos> lstDepositos = repo.GetListaDepositos(encomienda.IdEncomienda);

            var lstZonasmixtura = encomienda.EncomiendaUbicacionesDTO.FirstOrDefault().EncomiendaUbicacionesMixturasDTO.Select(x => x.IdZonaMixtura);
                       
            List <clsItemGrillaSeleccionarDepositos> depositos = new List<clsItemGrillaSeleccionarDepositos>();
            foreach (var deposito in lstDepositos.ToList())
            {
                List<bool> lstResultadosRubros = new List<bool>();

                deposito.ZonasMixtura = $" / {lstZonasmixtura}";

                foreach (var ZonaMixtura in lstZonasmixtura)
                {
                    switch (ZonaMixtura)
                    {
                        case 1:
                            deposito.CondicionZonaMixtura = $"{deposito.CondicionZonaMixtura} | {deposito.CondicionZonaMixtura1}";
                            break;
                        case 2:
                            deposito.CondicionZonaMixtura = $"{deposito.CondicionZonaMixtura} | {deposito.CondicionZonaMixtura2}";
                            break;
                        case 3:
                            deposito.CondicionZonaMixtura = $"{deposito.CondicionZonaMixtura} | {deposito.CondicionZonaMixtura3}";
                            break;
                        case 4:
                            deposito.CondicionZonaMixtura = $"{deposito.CondicionZonaMixtura} | {deposito.CondicionZonaMixtura4}";
                            break;
                    }

                    List <RubrosDepositosCN_Evaluar_Result> depEvaluar = new List<RubrosDepositosCN_Evaluar_Result>();
                    try
                    {
                        depEvaluar = repo.RubrosDepositosCN_Evaluar(encomienda.IdEncomienda, deposito.IdDeposito, superficie, ZonaMixtura, "Encomienda");
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }

                    // Si tiene normativa o es Transferencia, siempre se permite elegir el depósito
                    if (depEvaluar.FirstOrDefault().Resultado == true || normativa != null || encomienda.TipoTramite.IdTipoTramite.Equals(2))
                    {
                        deposito.Icono = "tilde.png";
                        deposito.Resultado = true;
                    }
                    else
                    {
                        deposito.Icono = "Prohibido.png";
                        deposito.Resultado = false;
                    }                    

                    depositos.Add(deposito);
                }
            }

            return depositos;
        }
    }
}
