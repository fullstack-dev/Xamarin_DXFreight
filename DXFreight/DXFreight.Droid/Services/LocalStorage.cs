using Android.App;
using Android.Content;
using DXFreight.Core.Services;

namespace DXFreight.Droid.Services
{
    public class LocalStorage : ILocalStorage
    {
        public void SaveSet(string display, string value)
        {
            //store
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString(display, value);
            prefEditor.Commit();

        }

        public string RetrieveSet(string display)
        {
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var somePref = prefs.GetString(display, null);
            return somePref;
        }
    }
}
