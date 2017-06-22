
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
namespace Wavelength.App.Droid
{
    [Activity (Label = "Wavelength.App.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, View.IOnClickListener
	{

        private Button button;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            // Check to see if user has logged in
            var prefs = GetSharedPreferences(GetString(Resource.String.app_prefs), FileCreationMode.Private);
            if (!prefs.Contains(GetString(Resource.String.pref_access_token)))
            {
                StartActivity(typeof(LoginActivity));
                Finish();
                return;
            }

			SetContentView(Resource.Layout.Main);

            button = FindViewById<Button>(Resource.Id.myButton);
            button.SetOnClickListener(this);
		}

        public void OnClick(View v)
        {
            var prefs = GetSharedPreferences(GetString(Resource.String.app_prefs), FileCreationMode.Private);
            var edit = prefs.Edit();

            edit.Clear();
            edit.Apply();

            StartActivity(typeof(LoginActivity));
            Finish();
        }
    }
}


