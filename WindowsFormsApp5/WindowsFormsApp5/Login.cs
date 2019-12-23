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
    public partial class Login : Form
    {
        Helper _helper;
        public TrusttAdminAPI api;
        TextBox Form;
        public Login(Helper helper,TextBox form)
        {
            InitializeComponent();
            _helper = helper;
            api = new TrusttAdminAPI(helper.Settings);
            Form = form;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserRegisterLoginRequest model = new UserRegisterLoginRequest
            {
                Email = textBox1.Text,
                Password = PasswordText.Text
            };
            var result = api.Login(model);
            if (result.Success)
            {
                _helper.UserInfo = result.Data;
            }
            else
            {
                MessageBox.Show("Error :" + result.Errors);
            }

            Form.Text = JsonConvert.SerializeObject(result.Data); 
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
