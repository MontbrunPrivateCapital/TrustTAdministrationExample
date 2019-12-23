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

namespace AdminSDKClientSample
{
    public partial class Settings : Form
    {
        Helper _helper;
        public Settings(Helper helper)
        {
            InitializeComponent();
            _helper = helper;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _helper.Settings = new AppSettings
            {
                ApiKey = apiKeyText.Text,
                BasePath = BasePathText.Text,
                TenantId = TenantText.Text
            };
            this.Close();
        }
    }
}
