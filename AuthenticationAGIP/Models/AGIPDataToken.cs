using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationAGIP.Models
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class datos
    {

        private datosServicio servicioField;

        private datosAutenticado autenticadoField;

        private datosRepresentados representadosField;

        /// <remarks/>
        public datosServicio servicio
        {
            get
            {
                return this.servicioField;
            }
            set
            {
                this.servicioField = value;
            }
        }

        /// <remarks/>
        public datosAutenticado autenticado
        {
            get
            {
                return this.autenticadoField;
            }
            set
            {
                this.autenticadoField = value;
            }
        }

        /// <remarks/>
        public datosRepresentados representados
        {
            get
            {
                return this.representadosField;
            }
            set
            {
                this.representadosField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class datosServicio
    {

        private string nombreField;

        private uint exp_timeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint exp_time
        {
            get
            {
                return this.exp_timeField;
            }
            set
            {
                this.exp_timeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class datosAutenticado
    {

        private ulong cuitField;

        private string nombreField;

        private ulong isibField;

        private byte catField;

        private ushort codcalleField;

        private string calleField;

        private ushort puertaField;

        private string pisoField;

        private string dptoField;

        private string codpostalField;

        private ushort codlocalidadField;

        private string localidadField;

        private byte codprovField;

        private string provinciaField;

        private uint telefonoField;

        private string emailField;

        private byte nivelField;

        private ushort tipoDocumentoField;

        private uint documentoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong cuit
        {
            get
            {
                return this.cuitField;
            }
            set
            {
                this.cuitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong isib
        {
            get
            {
                return this.isibField;
            }
            set
            {
                this.isibField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cat
        {
            get
            {
                return this.catField;
            }
            set
            {
                this.catField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codcalle
        {
            get
            {
                return this.codcalleField;
            }
            set
            {
                this.codcalleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle
        {
            get
            {
                return this.calleField;
            }
            set
            {
                this.calleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort puerta
        {
            get
            {
                return this.puertaField;
            }
            set
            {
                this.puertaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string piso
        {
            get
            {
                return this.pisoField;
            }
            set
            {
                this.pisoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dpto
        {
            get
            {
                return this.dptoField;
            }
            set
            {
                this.dptoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codpostal
        {
            get
            {
                return this.codpostalField;
            }
            set
            {
                this.codpostalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codlocalidad
        {
            get
            {
                return this.codlocalidadField;
            }
            set
            {
                this.codlocalidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad
        {
            get
            {
                return this.localidadField;
            }
            set
            {
                this.localidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte codprov
        {
            get
            {
                return this.codprovField;
            }
            set
            {
                this.codprovField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string provincia
        {
            get
            {
                return this.provinciaField;
            }
            set
            {
                this.provinciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte nivel
        {
            get
            {
                return this.nivelField;
            }
            set
            {
                this.nivelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort tipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class datosRepresentados
    {

        private datosRepresentadosRepresentado representadoField;

        /// <remarks/>
        public datosRepresentadosRepresentado representado
        {
            get
            {
                return this.representadoField;
            }
            set
            {
                this.representadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class datosRepresentadosRepresentado
    {

        private ulong cuitField;

        private string nombreField;

        private ulong isibField;

        private byte catField;

        private ushort codcalleField;

        private string calleField;

        private ushort puertaField;

        private string pisoField;

        private string dptoField;

        private string codpostalField;

        private ushort codlocalidadField;

        private string localidadField;

        private byte codprovField;

        private string provinciaField;

        private uint telefonoField;

        private string emailField;

        private ushort tipoRepresentacionField;

        private ushort tipoDocumentoField;

        private uint documentoField;

        private bool elegidoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong cuit
        {
            get
            {
                return this.cuitField;
            }
            set
            {
                this.cuitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong isib
        {
            get
            {
                return this.isibField;
            }
            set
            {
                this.isibField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte cat
        {
            get
            {
                return this.catField;
            }
            set
            {
                this.catField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codcalle
        {
            get
            {
                return this.codcalleField;
            }
            set
            {
                this.codcalleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle
        {
            get
            {
                return this.calleField;
            }
            set
            {
                this.calleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort puerta
        {
            get
            {
                return this.puertaField;
            }
            set
            {
                this.puertaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string piso
        {
            get
            {
                return this.pisoField;
            }
            set
            {
                this.pisoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dpto
        {
            get
            {
                return this.dptoField;
            }
            set
            {
                this.dptoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codpostal
        {
            get
            {
                return this.codpostalField;
            }
            set
            {
                this.codpostalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort codlocalidad
        {
            get
            {
                return this.codlocalidadField;
            }
            set
            {
                this.codlocalidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad
        {
            get
            {
                return this.localidadField;
            }
            set
            {
                this.localidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte codprov
        {
            get
            {
                return this.codprovField;
            }
            set
            {
                this.codprovField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string provincia
        {
            get
            {
                return this.provinciaField;
            }
            set
            {
                this.provinciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort tipoRepresentacion
        {
            get
            {
                return this.tipoRepresentacionField;
            }
            set
            {
                this.tipoRepresentacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort tipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool elegido
        {
            get
            {
                return this.elegidoField;
            }
            set
            {
                this.elegidoField = value;
            }
        }
    }


}