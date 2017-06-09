using Android.App;
using Android.Content;
using Android.Runtime;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Android.Util;

namespace Wavelength.App.Droid
{
    [Activity(Label = "Wavelength.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IFacebookCallback
    {

        ICallbackManager _callbackManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set up facebook
            FacebookSdk.SdkInitialize(Application.Context);
            _callbackManager = CallbackManagerFactory.Create();
            LoginManager.Instance.RegisterCallback(_callbackManager, this);

            SetContentView(Resource.Layout.Main);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        public void OnCancel()
        {
            Log.Info("WAVE", "Login Cancelled!");
        }

        public void OnError(FacebookException error)
        {
            Log.Info("WAVE", "Login Error!");
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var loginResult = result as LoginResult;
            Log.Info("WAVE", "Login Success!");
        }
    }
}

