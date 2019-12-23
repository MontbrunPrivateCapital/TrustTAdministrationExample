using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trustt.Administration.SDK.Models;
using Trustt.Administration.SDK.Services;

namespace AdminSDKClientSample
{
    public partial class ResetPassword : Form
    {
        TextBox _textBox;

        public TrusttAdminAPI api;

        Helper _helper;
        public ResetPassword(Helper helper, TextBox textBox)
        {

            InitializeComponent();
            _helper = helper;
            _textBox = textBox;
            api = new TrusttAdminAPI(helper.Settings);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trustt.Administration.SDK.Models.ResetPassword request = new Trustt.Administration.SDK.Models.ResetPassword { Code=CodeText.Text,Email=emailText.Text,Password=PasswordText.Text };
            var response = api.ResetPassword(request);
            if (response.Success)
            {
            }
            else
            {
                MessageBox.Show("Error :" + response.Errors);
            }
            _textBox.Text = JsonConvert.SerializeObject(response.Data);

            this.Close();
        }
    }
}
