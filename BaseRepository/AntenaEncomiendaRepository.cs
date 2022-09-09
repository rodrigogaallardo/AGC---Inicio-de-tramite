using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BaseRepository
{
    public class AntenaEncomiendaRepository : BaseRepository<ANT_Encomiendas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AntenaEncomiendaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="IdEstado"></param>
        /// <param name="UserId"></param>
        public void ActualizarEncomiendaAnt_Estado(int IdEncomienda, int IdEstado, Guid UserId)
        {
            try
            {
                EncomiendadigitalEntityes db = new EncomiendadigitalEntityes();
                _unitOfWork.Db.EncomiendaAnt_ActualizarEstado(IdEncomienda, IdEstado, UserId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public ANT_Encomiendas Get(int IdEncomienda)
        {
            var antena = (from enc in _unitOfWork.Db.ANT_Encomiendas
                    where enc.id_encomienda == IdEncomienda
                    select enc).FirstOrDefault();


            antena.Estado = (from est in _unitOfWork.Db.ANT_Encomiendas_Estados
                             where est.id_estado == antena.id_estado
                             select est).FirstOrDefault();

            antena.TipoTramite = (from tram in _unitOfWork.Db.APRA_TiposDeTramite
                                  where tram.id_tipotramite == antena.id_tipotramite
                                  select tram).FirstOrDefault();

            return antena;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="estados"></param>
        /// <param name="pIdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaAntenasEntity> GetAntenas(string matricula, string Apenom, string cuit, string estados, int pIdEncomienda)
        {
            var query = (
                    from enc in _unitOfWork.Db.ANT_Encomiendas
                    join est in _unitOfWork.Db.ANT_Encomiendas_Estados on enc.id_estado equals est.id_estado 
                    join tiptra in _unitOfWork.Db.APRA_TiposDeTramite on enc.id_tipotramite equals tiptra.id_tipotramite
                    join prof in _unitOfWork.Db.Profesional on enc.CreateUser equals prof.UserId
                    join encubic in _unitOfWork.Db.ANT_Ubicaciones on enc.id_encomienda equals encubic.id_encomienda
                    into temp
                    from encubic_left in temp.DefaultIfEmpty()
                    join encubic_ubic in _unitOfWork.Db.ANT_Ubicaciones_Ubicacion on encubic_left.id_antubicacion equals encubic_ubic.id_antubicacion
                    into temp0
                    from encubic_ubic_left in temp0.DefaultIfEmpty()
                    join stubic in _unitOfWork.Db.SubTiposDeUbicacion on encubic_ubic_left.id_subtipoubicacion equals stubic.id_subtipoubicacion
                    into temp1
                    from stubic_left in temp1.DefaultIfEmpty()
                    join tubic in _unitOfWork.Db.TiposDeUbicacion on stubic_left.id_tipoubicacion equals tubic.id_tipoubicacion
                    into temp2
                    from tubic_left in temp2.DefaultIfEmpty()
                    //join encpuer in _unitOfWork.Db.ANT_Ubicaciones_Puertas on encubic_ubic_left.id_antubicacion equals encpuer.id_antubicacion
                    //into temp3
                    //from encpuer_left in temp3.DefaultIfEmpty()
                    //join encphor in _unitOfWork.Db.ANT_Ubicaciones_PropiedadHorizontal on encubic_ubic_left.id_antubicacion equals encphor.id_antubicacion
                    //into temp4
                    //from encphor_left in temp4.DefaultIfEmpty()
                    //join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on encphor_left.id_propiedadhorizontal equals phor.id_propiedadhorizontal
                    //into temp5
                    //from phor_left in temp5.DefaultIfEmpty()
                    //join viapub in _unitOfWork.Db.ANT_Ubicaciones_ViaPublica on encubic_ubic_left.id_antubicacion equals viapub.id_antubicacion
                    //into temp6
                    //from viapub_left in temp6.DefaultIfEmpty()

                    select new EncomiendaAntenasEntity
                    { 
                        cod_estado =  est.cod_estado,
		                nom_estado =  est.nom_estado,
		                id_encomienda = enc.id_encomienda,
		                FechaEncomienda = enc.CreateDate,
		                descripcion_tipotramite = tiptra.descripcion_tipotramite,
		                id_estado =  enc.id_estado,
                        Matricula = prof.Matricula,
                        Apellido =  prof.Apellido,
                        Cuit = prof.Cuit
                        //Direccion = tubic_left.id_tipoubicacion == null ? viapub_left.observaciones :
                        //       (tubic_left.id_tipoubicacion == 0 ? encpuer_left.Nombre_calle + " " 
                        //                                                                    + encpuer_left.NroPuerta 
                        //                                                                    + " " + phor_left.Piso 
                        //                                                                    + " " + phor_left.Depto : 
                        //            tubic_left.descripcion_tipoubicacion + " " +	stubic_left.descripcion_subtipoubicacion  + "Local " + encubic_ubic_left.local_subtipoubicacion)
                    }
                );

            if (!string.IsNullOrEmpty(matricula))
                query = query.Where(p => p.Matricula.Equals(matricula));
            if (!string.IsNullOrEmpty(Apenom))
                query = query.Where(p => p.Apellido.Contains(Apenom));
            if (!string.IsNullOrEmpty(cuit))
                query = query.Where(p => p.Cuit.Contains(cuit));
            if (pIdEncomienda > 0)
                query = query.Where(p => p.id_encomienda == pIdEncomienda);
            
            if (!string.IsNullOrEmpty(estados))
            {
                int[] estadosChar = Array.ConvertAll(estados.Split(','), int.Parse);
                query = query.Where(p => estadosChar.Contains(p.id_estado));
            }

            return query;
            

        }
    }
}
