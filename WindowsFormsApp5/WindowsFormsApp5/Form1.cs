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
using Trustt.Administration.SDK.Services;

namespace AdminSDKClientSample
{
    public partial class Form1 : Form
    {
        bool Loged = false;
        Helper helper;
        public Form1()
        {
            InitializeComponent();
            helper = new Helper();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login(helper,textBox1);
            login.Show();
           
            
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(helper);
            settings.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendVerificationcode sendVC = new SendVerificationcode(helper, textBox1);
            sendVC.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TwooFactorGeneration twooFactorGeneration = new TwooFactorGeneration(helper, textBox1);
            twooFactorGeneration.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetUserProfile getUserProfile = new GetUserProfile(helper, textBox1);
            getUserProfile.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(helper, textBox1);
            changePassword.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ForegotPassword foregotPassword = new ForegotPassword(helper, textBox1);
            foregotPassword.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ResetPassword reset = new ResetPassword(helper, textBox1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrusttAdminAPI api = new TrusttAdminAPI(helper.Settings);
            var response = api.GetUserList();
            if (response.Success)
            {
                textBox1.Text = JsonConvert.SerializeObject(response.Data);
            }
            else
                MessageBox.Show("Error : " + response.Errors);

        }
    }
}
