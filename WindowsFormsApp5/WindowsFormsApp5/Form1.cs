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
using WindowsFormsApp5;

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
            try
            {
                TrusttAdminAPI api = new TrusttAdminAPI(helper.Settings);
            var response = api.GetUserList();
            if (response.Success)
            {
                textBox1.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
            }
            else
                    textBox1.Text = JsonConvert.SerializeObject(response.Errors.ToList(), Formatting.Indented);
                                 
            }
            catch (Exception ex)
            {
                textBox1.Text = JsonConvert.SerializeObject(ex.Message, Formatting.Indented);
            }

        }

       

        private void button10_Click(object sender, EventArgs e)
        {
            GetActivitiesByUser activities = new GetActivitiesByUser(helper, textBox1);
            activities.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TrusttAdminAPI api = new TrusttAdminAPI(helper.Settings);
            var response = api.GetActivitiesList();
            if (response.Success)
            {
                textBox1.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
            }
            else
                MessageBox.Show("Error : " + response.Errors);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrusttAdminAPI api = new TrusttAdminAPI(helper.Settings);
            var response = api.GetUsersPendingApproval();
            if(response.Success)
            {
                textBox1.Text = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
            }
            else
                MessageBox.Show("Error : " + response.Errors);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            UserRejected userRejected = new UserRejected(helper, textBox1);
            userRejected.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UserApprove userApprove = new UserApprove(helper, textBox1);
            userApprove.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            GetBaseFromImage getBaseFromImage = new GetBaseFromImage(helper, textBox1);
            getBaseFromImage.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GetCountrySpecs getCountrySpecs = new GetCountrySpecs(helper, textBox1);
            getCountrySpecs.Show();
        }
    }
}
