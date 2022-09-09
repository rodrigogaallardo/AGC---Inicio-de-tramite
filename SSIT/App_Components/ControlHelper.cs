using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.App_Components
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


        public static void DisableControls(Control parent)
        {
            EstateControls(parent, false);
        }

        public static void EnableControls(Control parent)
        {
            EstateControls(parent, true);
        }

        private static void EstateControls(Control parent, bool Enabled)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            foreach (Control control in parent.Controls)
            {
                TextBox textBox = control as TextBox;
                if (textBox != null)
                {
                    textBox.Enabled = Enabled;
                    continue;
                }

                ListControl listControl = control as ListControl;
                if (listControl != null)
                {
                    listControl.Enabled = Enabled;
                    continue;
                }

                DropDownList ddlControl = control as DropDownList;
                if (ddlControl != null)
                {
                    ddlControl.Enabled = Enabled;
                    continue;
                }

                CheckBox chkControl = control as CheckBox;
                if (chkControl != null)
                {
                    chkControl.Enabled = Enabled;
                    continue;
                }

                FileUpload fupControl = control as FileUpload;
                if (fupControl != null)
                {
                    fupControl.Enabled = Enabled;
                    continue;
                }

                LinkButton lnkControl = control as LinkButton;
                if (lnkControl != null)
                {
                    lnkControl.Enabled = Enabled;
                    continue;
                }

                Button btnControl = control as Button;
                if (btnControl != null)
                {
                    btnControl.Enabled = Enabled;
                    continue;
                }


                if (control.Controls.Count > 0)
                {
                    EstateControls(control, Enabled);
                }
            }
        }

    }
}