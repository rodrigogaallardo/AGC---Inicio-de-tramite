using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Implementation
{
    public class PDFBL
    {
        public bool isFirmadoPdf(byte[] archivo)
        {
            bool ret = false;
            PdfReader reader = new PdfReader(archivo);
            AcroFields af = reader.AcroFields;
            ret = af.GetSignatureNames().Count > 0;
            reader.Dispose();
            return ret;
        }
    }
}
