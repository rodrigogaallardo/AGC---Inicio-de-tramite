using StaticClass;
using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IUsuarioBL<T>
	{
        bool Insert(T objectDto, ExternalService.EmailEntity emailEntity);        
		void Update(T objectDto); 
		void Delete(T objectDto);
        T Single(Guid UserId);  
    }
}


