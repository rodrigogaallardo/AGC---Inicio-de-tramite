using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class InstructivosRepository : BaseRepository<Instructivos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructivosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        public Instructivos GetInstructivosByTipo(string tipoInstructivo)
        {
            try
            {
                var instEntity = _unitOfWork.Db.Instructivos.Where(x => x.cod_instructivo == tipoInstructivo);
            
                return instEntity.FirstOrDefault();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}

