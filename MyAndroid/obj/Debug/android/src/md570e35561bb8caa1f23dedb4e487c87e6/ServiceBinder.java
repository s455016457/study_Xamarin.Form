package md570e35561bb8caa1f23dedb4e487c87e6;


public class ServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MyAndroid.MyServices.ServiceBinder, MyAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ServiceBinder.class, __md_methods);
	}


	public ServiceBinder ()
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("MyAndroid.MyServices.ServiceBinder, MyAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ServiceBinder (com.xamarin.DemoService p0)
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("MyAndroid.MyServices.ServiceBinder, MyAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "MyAndroid.MyServices.DemoService, MyAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

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
