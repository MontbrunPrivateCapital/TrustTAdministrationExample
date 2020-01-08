using AdminSDKClientSample;
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

namespace WindowsFormsApp5
{
    public partial class GetActivitiesByUser : Form
    {
        Helper _helper;
        TextBox _textBox;
        public TrusttAdminAPI api;

         int walletType;
        DateTime InitDate;
        DateTime FinishDate;

        public GetActivitiesByUser(Helper helper, TextBox textBox)
        {
            InitializeComponent();
            _helper = helper;
            _textBox = textBox;
            api = new TrusttAdminAPI(helper.Settings);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                ActivityQueryRequest querry = new ActivityQueryRequest();
                querry.InitDate = null;
                querry.FinishTime = null;

                if (walletType ==1)
                {
                    querry.WalletAccountType=WalletAccountTypes.Cash;
                }
                 else if (walletType == 2)
                {
                    querry.WalletAccountType = WalletAccountTypes.Coin;
                }
                else if (walletType == 3)
                {
                    querry.WalletAccountType = WalletAccountTypes.Card;
                }

                if(InitDate!=default(DateTime)&&FinishDate!= default(DateTime))
                {
                    querry.InitDate = InitDate.ToString();
                    querry.FinishTime = InitDate.ToString();
                }

                var response = api.GetActivitiesList(textBox1.Text,querry);
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

                MessageBox.Show(ex.Message) ;
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            walletType = decimal.ToInt32(numericUpDown1.Value);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            InitDate = dateTimePicker1.Value;
        }

        private void GetActivitiesByUser_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            FinishDate = dateTimePicker2.Value;
        }
    }
}
