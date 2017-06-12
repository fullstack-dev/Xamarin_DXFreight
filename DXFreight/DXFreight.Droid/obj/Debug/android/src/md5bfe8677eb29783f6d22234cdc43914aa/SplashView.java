package md5bfe8677eb29783f6d22234cdc43914aa;


public class SplashView
	extends md5bfe8677eb29783f6d22234cdc43914aa.BaseView
	implements
		mono.android.IGCUserPeer,
		android.support.v4.app.ActivityCompat.OnRequestPermissionsResultCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"";
		mono.android.Runtime.register ("DXFreight.Droid.Views.SplashView, DXFreight.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SplashView.class, __md_methods);
	}


	public SplashView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SplashView.class)
			mono.android.TypeManager.Activate ("DXFreight.Droid.Views.SplashView, DXFreight.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
