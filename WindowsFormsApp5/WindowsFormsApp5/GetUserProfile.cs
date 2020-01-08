using Newtonsoft.Json;
using RestSharp;
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
    public partial class GetUserProfile : Form
    {
        TextBox _textBox;

        public TrusttAdminAPI api;

        Helper _helper;
        public GetUserProfile(Helper helper,TextBox textBox)
        {
            InitializeComponent();
            _textBox = textBox;
            _helper = helper;
            api = new TrusttAdminAPI(helper.Settings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                var response = api.GetProfile(textBox1.Text);
                if (response.Success)
                {

                    _textBox.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
                }
                else
                {
                    _textBox.Text = JsonConvert.SerializeObject(response.Errors.ToList(), Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
