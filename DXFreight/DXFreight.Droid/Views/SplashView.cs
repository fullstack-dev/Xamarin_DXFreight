using Android.App;
using Android.Content.PM;
using Android.OS;
using DXFreight.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Android.Widget;
using Android.Views;
using Android;
using System;
using Plugin.Permissions;

namespace DXFreight.Droid.Views
{
    [Activity(Label = "View for SplashViewModel"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashView : BaseView, Android.Support.V4.App.ActivityCompat.IOnRequestPermissionsResultCallback
    {
        ProgressDialog progress;
        const int RequestCameraPermissionID = 1001;
        const int RequestLocationPermissionID = 1002;
        const int RequestCallPhonePermissionID = 1003;
        const int RequestExternalStoragePermissionID = 1004;
        const int RequestReadPhoneStatePermissionID = 1005;

        protected override int LayoutResource => Resource.Layout.SplashView;

        private SplashViewModel SplashViewModel
        {
            get
            {
                return (SplashViewModel)this.ViewModel;
            }
        }
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;

                if (isLoading)
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Please wait...");
                    progress.SetCancelable(false);
                    progress.Show();
                }
                else
                {
                    progress.Hide();
                    progress.Dismiss();
                }
            }
        }

        private bool alert_error;
        public bool Alert_error
        {
            get { return alert_error; }
            set
            {
                alert_error = value;

                if (alert_error)
                {
                    View view = LayoutInflater.Inflate(Resource.Layout.NegativeToast, null);
                    var txt = view.FindViewById<TextView>(Resource.Id.txtCustomToast);
                    txt.Text = "Invalid activation barcode - please try again.";
                    var toast = new Toast(this)
                    {
                        Duration = ToastLength.Long,
                        View = view
                    };
                    toast.Show();
                }
            }
        }

        private bool rooted_dialog;
        public bool Rooted_dialog
        {
            get { return rooted_dialog; }
            set
            {
                rooted_dialog = value;

                if (rooted_dialog)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Rooted device detected");
                    alert.SetMessage("To function properly the application will only run on non-rooted devices.");
                    alert.SetPositiveButton("OK", (senderAlert, args) =>
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    });
                   
                    RunOnUiThread(() =>
                    {
                        alert.Show();
                    });
                }
            }
        }
        private bool permissionRequest;
        public bool PermissionRequest
        {
            get { return permissionRequest; }
            set
            {
                permissionRequest = value;

                if (permissionRequest)
                {
                    requestForPermission1();
                }
            }
        }

        //Allow Camera Permission
        public void requestForPermission1()
        {
            string permission1 = Manifest.Permission.Camera;
            if (CheckSelfPermission(permission1) != Android.Content.PM.Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(permission1))
                {
                    showPermissionRationalDialog1("Permission Needed!", "Rationale", permission1);
                }
                else
                {
                    requestForCameraPermission(permission1);
                }
            }
            else
            {
                requestForPermission2();
            }
        }
        private void requestForCameraPermission(string permission)
        {
            RequestPermissions(new string[] { permission }, RequestCameraPermissionID);
        }
        private void showPermissionRationalDialog1(string title, string message, string permission)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                requestForCameraPermission(permission);
            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }


        // Allow Location Permission
        public void requestForPermission2()
        {
            string permission2 = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission2) != Android.Content.PM.Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(permission2))
                {
                    showPermissionRationalDialog2("Permission Needed!", "Rationale", permission2);
                }
                else
                {
                    requestForLocationPermission(permission2);
                }
            }
            else
            {
                requestForPermission3();
            }
        }
        private void requestForLocationPermission(string permission)
        {
            RequestPermissions(new string[] { permission }, RequestLocationPermissionID);
        }
        private void showPermissionRationalDialog2(string title, string message, string permission)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                requestForLocationPermission(permission);
            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        //Allow Call Phone permission
        public void requestForPermission3()
        {
            string permission3 = Manifest.Permission.CallPhone;
            if (CheckSelfPermission(permission3) != Android.Content.PM.Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(permission3))
                {
                    showPermissionRationalDialog3("Permission Needed!", "Rationale", permission3);
                }
                else
                {
                    requestForCallPhonePermission(permission3);
                }
            }
            else
            {
                requestForPermission4();
            }
        }
        private void requestForCallPhonePermission(string permission)
        {
            RequestPermissions(new string[] { permission }, RequestCallPhonePermissionID);
        }
        private void showPermissionRationalDialog3(string title, string message, string permission)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                requestForCallPhonePermission(permission);
            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        //Allow External Storage Permission
        public void requestForPermission4()
        {
            string permission4 = Manifest.Permission.ReadExternalStorage;
            if (CheckSelfPermission(permission4) != Android.Content.PM.Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(permission4))
                {
                    showPermissionRationalDialog4("Permission Needed!", "Rationale", permission4);
                }
                else
                {
                    requestForExternalStoragePermission(permission4);
                }
            }
            else
            {
                requestForPermission5();
            }
        }
        private void requestForExternalStoragePermission(string permission)
        {
            RequestPermissions(new string[] { permission }, RequestExternalStoragePermissionID);
        }
        private void showPermissionRationalDialog4(string title, string message, string permission)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                requestForExternalStoragePermission(permission);
            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        //Allow Read Phone State Permission
        public void requestForPermission5()
        {
            string permission5 = Manifest.Permission.ReadPhoneState;
            if (CheckSelfPermission(permission5) != Android.Content.PM.Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(permission5))
                {
                    showPermissionRationalDialog5("Permission Needed!", "Rationale", permission5);
                }
                else
                {
                    requestForReadPhoneStatePermission(permission5);
                }
            }
            else
            {
                this.SplashViewModel.Permission_allow = true;
                this.SplashViewModel.Login();
                this.Finish();
            }
        }
        private void requestForReadPhoneStatePermission(string permission)
        {
            RequestPermissions(new string[] { permission }, RequestReadPhoneStatePermissionID);
        }
        private void showPermissionRationalDialog5(string title, string message, string permission)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                requestForReadPhoneStatePermission(permission);
            });

            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
                            {
                                RequestPermissions(new string[]
                                {
                                    Manifest.Permission.Camera
                                }, RequestCameraPermissionID);
                                return;
                            }
                            try
                            {
                                requestForPermission2();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }
                        }else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Permission denied!");
                            alert.SetMessage("boo! Disable the functionaity of DX ISP that depends on Camera permission.");
                            alert.SetPositiveButton("OK", (senderAlert, args) =>
                            {
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            });

                            RunOnUiThread(() =>
                            {
                                alert.Show();
                            });
                            return;
                        }
                    }
                    break;
                case RequestLocationPermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                            {
                                RequestPermissions(new string[]
                                {
                                    Manifest.Permission.AccessFineLocation
                                }, RequestLocationPermissionID);
                                return;
                            }
                            try
                            {
                                requestForPermission3();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Permission denied!");
                            alert.SetMessage("boo! Disable the functionaity of DX ISP that depends on Location permission.");
                            alert.SetPositiveButton("OK", (senderAlert, args) =>
                            {
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            });

                            RunOnUiThread(() =>
                            {
                                alert.Show();
                            });
                            return;
                        }
                    }
                    break;
                case RequestCallPhonePermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (CheckSelfPermission(Manifest.Permission.CallPhone) != Permission.Granted)
                            {
                                RequestPermissions(new string[]
                                {
                                    Manifest.Permission.CallPhone
                                }, RequestCallPhonePermissionID);
                                return;
                            }
                            try
                            {
                                requestForPermission4();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Permission denied!");
                            alert.SetMessage("boo! Disable the functionaity of DX ISP that depends on CallPhone permission.");
                            alert.SetPositiveButton("OK", (senderAlert, args) =>
                            {
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            });

                            RunOnUiThread(() =>
                            {
                                alert.Show();
                            });
                            return;
                        }
                    }
                    break;
                case RequestExternalStoragePermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) != Permission.Granted)
                            {
                                RequestPermissions(new string[]
                                {
                                    Manifest.Permission.ReadExternalStorage
                                }, RequestExternalStoragePermissionID);
                                return;
                            }
                            try
                            {
                                requestForPermission5();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Permission denied!");
                            alert.SetMessage("boo! Disable the functionaity of DX ISP that depends on External Storage permission.");
                            alert.SetPositiveButton("OK", (senderAlert, args) =>
                            {
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            });

                            RunOnUiThread(() =>
                            {
                                alert.Show();
                            });
                            return;
                        }
                    }
                    break;
                case RequestReadPhoneStatePermissionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (CheckSelfPermission(Manifest.Permission.ReadPhoneState) != Permission.Granted)
                            {
                                RequestPermissions(new string[]
                                {
                                    Manifest.Permission.ReadPhoneState
                                }, RequestReadPhoneStatePermissionID);
                                return;
                            }
                            try
                            {
                                this.SplashViewModel.Permission_allow = true;
                                this.SplashViewModel.Login();
                                this.Finish();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Permission denied!");
                            alert.SetMessage("boo! Disable the functionaity of DX ISP that depends on Read Phone State permission.");
                            alert.SetPositiveButton("OK", (senderAlert, args) =>
                            {
                                this.FinishAffinity();
                                FinishAndRemoveTask();
                                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                            });

                            RunOnUiThread(() =>
                            {
                                alert.Show();
                            });
                            return;
                        }
                    }
                    break;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var bindingSet = this.CreateBindingSet<SplashView, SplashViewModel>();
            bindingSet.Bind().For(c => c.IsLoading).To(vm => vm.IsLoading);
            bindingSet.Bind().For(c => c.Alert_error).To(vm => vm.Alert_error);
            bindingSet.Bind().For(c => c.Rooted_dialog).To(vm => vm.Rooted_dialog);
            bindingSet.Bind().For(c => c.PermissionRequest).To(vm => vm.PermissionRequest);
            bindingSet.Apply();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Finish();
        }
    }
}