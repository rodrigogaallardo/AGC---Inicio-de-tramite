using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ConsejosProfesionales
{
    /// <summary>
    /// Descripción breve de WSConsejo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSConsejo : System.Web.Services.WebService
    {
        [WebMethod]
        public List<nodo> MatriculasPorCuit(/*string user, string pass, */string p_cuit)
        {
            /*if (user == null || user.Count() == 0)
                throw new Exception("El nombre de usuario no puede ser nulo.");

            MembershipUser usuario = Membership.GetUser(user);

            if (usuario == null)
                throw new Exception("El nombre de usuario no existe.");
            else if (usuario.GetPassword() != pass)
                throw new Exception("La Contraseña ingresada es incorrecta.");*/

            if (string.IsNullOrEmpty(p_cuit))
                throw new Exception("El cuit no puede ser nulo.");

            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            var profesionals = profesionalesBL.GetByCuit(p_cuit);

            if (profesionals.Count() == 0)
            {
                if (!p_cuit.Contains("-"))
                {
                    string cuit = p_cuit.Substring(0, 2) + "-" + p_cuit.Substring(2, 8) + "-" + p_cuit.Substring(10);
                    profesionals = profesionalesBL.GetByCuit(cuit);
                    if (profesionals.Count() == 0)
                        throw new Exception("Error 20: Cuit no empadronado. El profesional debe solicitar al Consejo correspondiente el alta de usuario.");
                }
                else
                    throw new Exception("Error 20: Cuit no empadronado. El profesional debe solicitar al Consejo correspondiente el alta de usuario.");
            }
            List<nodo> lista = new List<nodo>();
            foreach (var c in profesionals)
            {
                lista.Add(new nodo { Matricula = c.Matricula, NombreConsejo = c.ConsejoProfesionalDTO.Nombre });
            }
            return lista;
        }

        [WebMethod]
        public bool ProfesionalHabilitado(/*string user, string pass, */int p_idConsejo, string p_cuit)
        {
            /*if (user == null || user.Count() == 0)
                throw new Exception("El nombre de usuario no puede ser nulo.");

            MembershipUser usuario = Membership.GetUser(user);

            if (usuario == null)
                throw new Exception("El nombre de usuario no existe.");
            else if (usuario.GetPassword() != pass)
                throw new Exception("La Contraseña ingresada es incorrecta.");*/

            if (string.IsNullOrEmpty(p_cuit))
                throw new Exception("El cuit no puede ser nulo.");

            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            var profesionals = profesionalesBL.GetByCuit(p_cuit);

            if (profesionals.Count() == 0)
            {
                if (!p_cuit.Contains("-"))
                {
                    string cuit = p_cuit.Substring(0, 2) + "-" + p_cuit.Substring(2, 8) + "-" + p_cuit.Substring(10);
                    profesionals = profesionalesBL.GetByCuit(cuit);
                    if (profesionals.Count() == 0)
                        throw new Exception("Error 20: Cuit no empadronado. El profesional debe solicitar al Consejo correspondiente el alta de usuario.");
                }
                else
                    throw new Exception("Error 20: Cuit no empadronado. El profesional debe solicitar al Consejo correspondiente el alta de usuario.");
            }

            var p = profesionals.Where(x => x.ConsejoProfesionalDTO.id_grupoconsejo == p_idConsejo).FirstOrDefault();
            if (p == null)
                throw new Exception("Error 21 - Cuit no empadronado en este Consejo");

            return !p.InhibidoBit;
        }
    }
    public class nodo
    {
        public string NombreConsejo { get; set; }
        public string Matricula { get; set; }
    }
}
