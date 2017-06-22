using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Java.Lang;
using Android.Util;

namespace Wavelength.App.Droid
{
	[Activity (Label = "Wavelength.App.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, IFacebookCallback
	{

        private ICallbackManager callbackManager;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            FacebookSdk.SdkInitialize(Application.Context);
            callbackManager = CallbackManagerFactory.Create();
            LoginManager.Instance.RegisterCallback(callbackManager, this);

			SetContentView(Resource.Layout.Login);
		}

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        public void OnCancel()
        {
            Log.Info("FB", "Cancelled");
        }

        public void OnError(FacebookException error)
        {
            Log.Info("FB", "Error");
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Log.Info("FB", "Success");
        }
    }
}


