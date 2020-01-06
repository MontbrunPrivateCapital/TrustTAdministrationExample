using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustt.Administration.SDK.Models;

namespace AdminSDKClientSample
{
   public  class Helper
    {

        public bool Loged { get; set; }
        public AppSettings Settings { get; set; }
        public UserFullResponse User { get; set; }
        public UserInfo UserInfo { get; set; }
        public Flag Flag { get; set; }

    }

    public enum Flag
    {
        Login = 0,
        SendVerificationCode=1,
        TwooFactorGeneration=2,
        GetProfile=3,
        ChangePassword=4,
        ForegotPassword=5,
        ResetPassword=6,
        GetUser=7,
        ActivitiesByUser=8,
        AllActivities=9
    }
}
