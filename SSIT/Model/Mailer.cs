﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSIT.Model
{
    public class MailWelcome
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string Urlactivacion { get; set; }
    }

    public class MailPassRecovery
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string UrlLogin { get; set; }
    }

    public class MailUsuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string UrlLogin { get; set; }
    }

    public class MailSolicitudNuevaPuerta
    {
        public string Username { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int? Seccion { get; set; }
        public string Manzana { get; set; }
        public string Parcela { get; set; }
        public int? NroPartidaMatriz { get; set; }
        public string Calle { get; set; }
        public string NroPuerta { get; set; }
        public string urlFoto { get; set; }
        public string UrlMapa { get; set; }
    }
    public class MailAnulacionAnexo
    {
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string Profesional { get; set; }
        public string IdEncomienda { get; set; }
    }

    
}