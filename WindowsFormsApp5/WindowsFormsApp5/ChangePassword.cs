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
    public partial class ChangePassword : Form
    {
        TextBox _textBox;

        public TrusttAdminAPI api;

        Helper _helper;
        public ChangePassword(Helper helper,TextBox textBox)
        {
            InitializeComponent();
            _textBox = textBox;
            _helper = helper;
            api = new TrusttAdminAPI(helper.Settings);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePaswordRequest changePassword = new ChangePaswordRequest
            {
                NewPassword = newPassWordText.Text,
                OldPassword = oldPassWordText.Text
            };
          var response=  api.ChangePassword(changePassword);
            if (!response.Success)
                MessageBox.Show("Error : "+response.Errors);
            _textBox.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
            this.Close();
        }
    }
}
