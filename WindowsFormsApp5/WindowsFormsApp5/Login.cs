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
            var a = LogIn(JsonConvert.SerializeObject(model));
            if (result.Success)
            {
                _helper.UserInfo = result.Data;
            }
            else
            {
                MessageBox.Show("Error :" + result.Errors);
            }

            Form.Text = JsonConvert.SerializeObject(result.Data,Formatting.Indented);

         
            
                var _model = model;
                var client = new RestClient(_helper.Settings.BasePath + "/" + _helper.Settings.TenantId);
                var request = new RestRequest("/api/users/login", Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", ParameterType.RequestBody);
                request.AddJsonBody(JsonConvert.SerializeObject(_model));


                try
                {
                    var response = client.Execute(request);

                    //  if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    //  {
                    //      return response;
                    //  }

                    
                }
                catch (Exception ex)
                {
                    throw;
                }


            

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public IRestResponse LogIn(string model)
        {
            var _model = JsonConvert.DeserializeObject<UserRegisterLoginRequest>(model);
            var client = new RestClient(_helper.Settings.BasePath);
            var request = new RestRequest("/api/users/login", Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", _helper.Settings.ApiKey);
            request.AddHeader("tenant", _helper.Settings.TenantId);
            request.AddParameter("application/json", ParameterType.RequestBody);
            request.AddJsonBody(JsonConvert.SerializeObject(_model));


            try
            {
                var response = client.Execute(request);

                //  if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //  {
                //      return response;
                //  }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }
}
