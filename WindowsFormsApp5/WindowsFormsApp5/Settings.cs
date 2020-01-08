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
                ApiKey = "Bearer "+ TokenGenerator(),//apiKeyText.Text,
            BasePath = BasePathText.Text,
                TenantId = TenantText.Text
            };
            this.Close();
        }

        private string TokenGenerator()
        {
            string token = "";

            string model = JsonConvert.SerializeObject(new
            {
                grant_type= "client_credentials",
                client_id= "0NAWEal1cMyUqkSRXUt7ouhDJLlxTz7f",
                audience= "https://oikos-gold-wallet-api-test.azurewebsites.net/",
                client_secret = "kO1OcaA669l16eC4rqtEoE_WaTapNt5KiE44or9tQ76CoMjfqHuh60IpWjPSnwRv"
            });
            var client = new RestClient("https://oikos-trustt-test.auth0.com");
            var req = new RestRequest("oauth/token", Method.POST);
                req.AddHeader("content-type", "application/json");
                req.AddParameter("application/json", ParameterType.RequestBody);
            req.AddJsonBody(model);

            try
                {
                    IRestResponse response = client.Execute(req);
                token = JsonConvert.DeserializeObject<dynamic>(response.Content).access_token;
                   
                }
                catch (Exception e)
                {
                    throw e;
                }
            
            return token;
        }
    }
}
