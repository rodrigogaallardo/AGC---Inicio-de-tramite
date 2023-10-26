using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UsuarioRepository : BaseRepository<Usuario> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public UsuarioRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public int TransferirUsuario(Guid userid_nuevo, Guid userid_anterior)
        {
            try
            {
                var result = _unitOfWork.Db.SSIT_Transferir_Usuario(userid_nuevo, userid_anterior);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTokenTAD(Guid userid, string tokenJWT)
        {
            try
            {
                var result = _unitOfWork.Db.Insert_Login_Tad_Token(userid, tokenJWT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTokenTAD(Guid userid)
        {
            try
            {
                string result = _unitOfWork.Db.Get_Token_Tad(userid);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

