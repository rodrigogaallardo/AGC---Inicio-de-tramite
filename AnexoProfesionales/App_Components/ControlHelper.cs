using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.App_Components
{
    public class ControlHelper
    {

        public static void ClearControls(Control parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            foreach (Control control in parent.Controls)
            {
                TextBox textBox = control as TextBox;
                if (textBox != null)
                {
                    textBox.Text = string.Empty;
                    continue;
                }

                ListControl listControl = control as ListControl;
                if (listControl != null)
                {
                    if (listControl.Items.Count > 0)
                        listControl.SelectedIndex = 0;
                    continue;
                }

                if (control.Controls.Count > 0)
                {
                    ClearControls(control);
                }
            }
        }

        public static string GetValue(Control container, string controlName)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            if (string.IsNullOrEmpty(controlName))
                throw new ArgumentNullException("controlName");

            Control control = container.FindControl(controlName);

            string value = string.Empty;

            TextBox textBox = control as TextBox;
            if (textBox != null)
                value = textBox.Text;

            ListControl listControl = control as ListControl;
            if (listControl != null)
                value = listControl.SelectedValue;

            return value;
        }

        public static string GetText(Control container, string controlName)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            if (string.IsNullOrEmpty(controlName))
                throw new ArgumentNullException("controlName");

            Control control = container.FindControl(controlName);

            string value = string.Empty;

            TextBox textBox = control as TextBox;
            if (textBox != null)
                value = textBox.Text;

            ListControl listControl = control as ListControl;
            if (listControl != null)
                value = listControl.SelectedItem.Text;

            return value;
        }
    }
}