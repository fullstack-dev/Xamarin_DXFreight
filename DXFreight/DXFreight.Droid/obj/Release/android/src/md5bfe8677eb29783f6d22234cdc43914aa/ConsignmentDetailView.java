package md5bfe8677eb29783f6d22234cdc43914aa;


public class ConsignmentDetailView
	extends md5bfe8677eb29783f6d22234cdc43914aa.BaseView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("DXFreight.Droid.Views.ConsignmentDetailView, DXFreight.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ConsignmentDetailView.class, __md_methods);
	}


	public ConsignmentDetailView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ConsignmentDetailView.class)
			mono.android.TypeManager.Activate ("DXFreight.Droid.Views.ConsignmentDetailView, DXFreight.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
