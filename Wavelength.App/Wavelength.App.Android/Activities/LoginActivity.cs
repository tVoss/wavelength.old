using Android.App;
using Android.Content;
using Android.Runtime;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Wavelength.App.Droid.Activities
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity, IFacebookCallback
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
        }

        public void OnError(FacebookException error)
        {
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var login = result as LoginResult;

            var prefs = GetSharedPreferences(GetString(Resource.String.app_prefs), FileCreationMode.Private);
            var edit = prefs.Edit();

            edit.PutString(GetString(Resource.String.pref_access_token), login.AccessToken.Token);

            edit.Apply();

            StartActivity(typeof(MainActivity));

            Finish();
        }
    }
}