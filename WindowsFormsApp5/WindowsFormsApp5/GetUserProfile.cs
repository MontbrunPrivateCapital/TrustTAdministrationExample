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
                }
                else
                {
                    MessageBox.Show("Error : " + response.Errors);
                }

                //var path = "http://localhost:1185/Admin-TrustT";


                //    var client = new RestClient(path);
                //    var req = new RestRequest("api/users/getprofile/" + textBox1.Text, Method.GET);
                //    req.AddHeader("content-type", "application/json");
                //    req.AddParameter("application/json", ParameterType.RequestBody);

                //    try
                //    {
                //        IRestResponse response1 = client.Execute(req);

                //        if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                //        {
                //           // return response;
                //        }

                //    //return response;

                //    var data = JsonConvert.DeserializeObject<dynamic>(response1.Content);

                //var temp = JsonConvert.DeserializeObject<dynamic>(data.userProfile.ToString());
                //    var templist = JsonConvert.DeserializeObject<dynamic>(data.userSources.ToString());
                //    //List<object> list = JsonConvert.DeserializeObject<List<object>>(data.usreSources.ToString());
                //    UserProfileResponse a = new UserProfileResponse();
                //    a.UserProfile=JsonConvert.DeserializeObject<UserInfo>(temp.ToString());
                //    a.UserSources = JsonConvert.DeserializeObject<List<sourcetest>>(templist.ToString());

                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}


                 _textBox.Text = JsonConvert.SerializeObject(response.Data);
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
