using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using IBusinessLayer;

namespace BusinesLayer.Implementation
{
    public class SSIT_Listado_ProfesionalesBL : ISSITListado_ProfesionalesBL<SSIT_Listado_ProfesionalesDTO>
    {
        private SSIT_Listado_ProfesionalesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;


        public SSIT_Listado_ProfesionalesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Listado_Profesionales_Result, SSIT_Listado_ProfesionalesDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public List<SSIT_Listado_ProfesionalesDTO> GetProfesionalesSolicitud(int BusCircuito, int startRowIndex, int maximumRows, string sortByExpression, string profesional,  out int totalRowCount)
        {
            try
            {
                int cirAnt = 0;
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSIT_Listado_ProfesionalesRepository(this.uowF.GetUnitOfWork());

                cirAnt = BusCircuito;

                if (BusCircuito == 14 || BusCircuito == 19)
                    BusCircuito = 0;

                List<SSIT_Listado_Profesionales_Result> q = repo.GetProfesionalesSolicitud(BusCircuito);

                if (cirAnt == 14)
                    q = q.Where(x => x.idcircuito == 14 || x.idcircuito == 17).ToList();
                else if(cirAnt == 19)
                    q = q.Where(x => x.idcircuito == 19 || x.idcircuito == 16).ToList();

                if (profesional != null)
                    q = q.Where(x => x.nombre.ToUpper().Contains(profesional.ToUpper()) || x.Apellido.ToUpper().Contains(profesional.ToUpper())).ToList();

                totalRowCount = q.Count();

                if ((startRowIndex + maximumRows) > totalRowCount)
                    maximumRows = totalRowCount - startRowIndex;

                if (sortByExpression != null)
                {
                    if (sortByExpression.Contains("DESC"))
                    {
                        if (sortByExpression.Contains("total"))
                            q = q.OrderByDescending(x => x.total).ToList();
                        else if (sortByExpression.Contains("Aprobadas"))
                            q = q.OrderByDescending(x => x.Aprobadas).ToList();
                        else if (sortByExpression.Contains("porcentaje_aprob"))
                            q = q.OrderByDescending(x => x.porcentaje_aprob).ToList();
                        else if (sortByExpression.Contains("Rechazadas"))
                            q = q.OrderByDescending(x => x.Rechazadas).ToList();
                        else if (sortByExpression.Contains("porcentaje_recha"))
                            q = q.OrderByDescending(x => x.porcentaje_recha).ToList();
                        else if (sortByExpression.Contains("Vencidas"))
                            q = q.OrderByDescending(x => x.Vencidas).ToList();
                        else if (sortByExpression.Contains("porcentaje_venci"))
                            q = q.OrderByDescending(x => x.porcentaje_venci).ToList();
                    }
                    else
                    {
                        if (sortByExpression.Contains("total"))
                            q = q.OrderBy(x => x.total).ToList();
                        else if (sortByExpression.Contains("Aprobadas"))
                            q = q.OrderBy(x => x.Aprobadas).ToList();
                        else if (sortByExpression.Contains("porcentaje_aprob"))
                            q = q.OrderBy(x => x.porcentaje_aprob).ToList();
                        else if (sortByExpression.Contains("Rechazadas"))
                            q = q.OrderBy(x => x.Rechazadas).ToList();
                        else if (sortByExpression.Contains("porcentaje_recha"))
                            q = q.OrderBy(x => x.porcentaje_recha).ToList();
                        else if (sortByExpression.Contains("Vencidas"))
                            q = q.OrderBy(x => x.Vencidas).ToList();
                        else if (sortByExpression.Contains("porcentaje_venci"))
                            q = q.OrderBy(x => x.porcentaje_venci).ToList();
                    }
                }

                q = q.GetRange(startRowIndex, maximumRows);
                
                var qr = mapperBase.Map<List<SSIT_Listado_Profesionales_Result>, List<SSIT_Listado_ProfesionalesDTO>>(q);

                return qr;
            }
            catch
            {
                throw;
            }
        }
    }
}
