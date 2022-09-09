using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace Reporting.CommonClass
{
    public class Functions
    {

        public static byte[] StreamToArray(Stream input)
        {
            byte[] buffer = new byte[20 * 1024 * 1024]; // 20 mb
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] ImageToByte(Image pImagen)
        {
            byte[] mImage;

            MemoryStream ms = new MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();

            return mImage;
        }
    }
}