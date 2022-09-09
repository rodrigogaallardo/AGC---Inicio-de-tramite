﻿using DataTransferObject;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ISGITramitesTareasHabBL<T>
    {
        void Delete(T objectDto, SGITramitesTareasDTO sgiTTDTO);
        bool Insert(T objectDto);   
        void Update(T objectDto);
        T GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud);
    }
}