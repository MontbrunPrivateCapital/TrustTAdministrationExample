using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trustt.Administration.SDK.Services;
using Trustt.Administration.SDK.Models;
using AdminSDKClientSample;
using Newtonsoft.Json;
using RestSharp;

namespace WindowsFormsApp5
{
    public partial class UserRejected : Form

    {
        TextBox _textBox;

        public TrusttAdminAPI api;

        Helper _helper;
        public UserRejected(Helper helper, TextBox textBox)
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
            NoteModel model = new NoteModel {
            Data=textBox2.Text
         };
            var response = api.RejectUserVerification(textBox1.Text,model);
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
