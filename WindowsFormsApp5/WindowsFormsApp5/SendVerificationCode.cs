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
    public partial class SendVerificationcode : Form
    {
        TextBox _textBox;

        public TrusttAdminAPI api;

        Helper _helper;
        public SendVerificationcode(Helper helper,TextBox textBox)
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
            EmailRequest request = new EmailRequest { Email = textBox1.Text };
            var response =api.SendVerificationCode(request);
            if (response.Success)
            {

                _textBox.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
            }
            else
            {
                _textBox.Text = JsonConvert.SerializeObject(response.Errors.ToList(), Formatting.Indented);
            }

            this.Close();
        }
    }
}
