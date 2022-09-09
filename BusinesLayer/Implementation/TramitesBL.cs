using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using IBusinessLayer;
using DataAcess.EntityCustom;
using StaticClass;
using System.Globalization;

namespace BusinesLayer.Implementation
{
    public class TramitesBL : ITramitesBL<TramitesDTO>
    {
        private IUnitOfWorkFactory uowF = null;
        private TramitesRepository repo = null;
        IMapper mapperBase;

        public TramitesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TramitesDTO, TramitesEntity>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TramitesDTO> GetTramites(Guid userid, int id_solicitud, int id_tipotramite, int id_estado, string nro_expediente, 
            int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TramitesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetTramites(userid);

            if (id_solicitud > 0)
                elements = elements.Where(idSol => idSol.IdTramite == id_solicitud);

            if (id_tipotramite > -1)
                elements = elements.Where(idTr => idTr.IdTipoTramite == id_tipotramite);

            if (id_estado > -1)
                elements = elements.Where(idEst => idEst.IdEstado == id_estado);

            if (nro_expediente.Trim() != string.Empty)
                elements = elements.Where(nroExp => (nroExp.NroExpedienteSade != null ? nroExp.NroExpedienteSade.Replace(" ","") : nroExp.NroExpedienteSade) == nro_expediente.Replace(" ",""));
                       
            
            totalRowCount = elements.Count();
            elements = elements.OrderByDescending(o => o.CreateDate).Skip(startRowIndex).Take(maximumRows);
            var elementsDto = mapperBase.Map<IEnumerable<TramitesEntity>, IEnumerable<TramitesDTO>>(elements);

            foreach (var ele in elementsDto)
            {
                string descTipoExp;
                if (ele.TipoExpediente == (int)Constantes.TipoDeExpediente.NoDefinido)
                    descTipoExp = ele.TipoExpedienteDescripcion;
                else
                {
                    TextInfo textInfo = new CultureInfo("es-AR", false).TextInfo;
                    descTipoExp = ele.TipoExpedienteDescripcion.ToLower() + (ele.SubTipoExpedienteDescripcion.Length > 0 ?  " (" + ele.SubTipoExpedienteDescripcion.ToLower() + ")" : string.Empty);
                    descTipoExp = textInfo.ToTitleCase(descTipoExp);
                }
                ele.TipoExpedienteDescripcion = descTipoExp;
            }
            return elementsDto;
            
        }
        public IEnumerable<TramitesDTO> GetTramitesNuevos(Guid userid, int id_solicitud, int id_tipotramite, int id_estado, string nro_expediente,
           int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TramitesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetTramitesNuevos(userid);

            if (id_solicitud > 0)
                elements = elements.Where(idSol => idSol.IdTramite == id_solicitud);

            if (id_tipotramite > -1)
                elements = elements.Where(idTr => idTr.IdTipoTramite == id_tipotramite);

            if (id_estado > -1)
                elements = elements.Where(idEst => idEst.IdEstado == id_estado);

            if (nro_expediente.Trim() != string.Empty)
                elements = elements.Where(nroExp => (nroExp.NroExpedienteSade != null ? nroExp.NroExpedienteSade.Replace(" ", "") : nroExp.NroExpedienteSade) == nro_expediente.Replace(" ", ""));


            totalRowCount = elements.Count();
            elements = elements.OrderByDescending(o => o.CreateDate).Skip(startRowIndex).Take(maximumRows);
            var elementsDto = mapperBase.Map<IEnumerable<TramitesEntity>, IEnumerable<TramitesDTO>>(elements);

            foreach (var ele in elementsDto)
            {
                string descTipoExp;
                if (ele.TipoExpediente == (int)Constantes.TipoDeExpediente.NoDefinido)
                    descTipoExp = ele.TipoExpedienteDescripcion;
                else
                {
                    TextInfo textInfo = new CultureInfo("es-AR", false).TextInfo;
                    descTipoExp = ele.TipoExpedienteDescripcion.ToLower() + " (" + ele.SubTipoExpedienteDescripcion.ToLower() + ")";
                    descTipoExp = textInfo.ToTitleCase(descTipoExp);
                }
                ele.TipoExpedienteDescripcion = descTipoExp;
            }
            return elementsDto;

        }
    }
}
