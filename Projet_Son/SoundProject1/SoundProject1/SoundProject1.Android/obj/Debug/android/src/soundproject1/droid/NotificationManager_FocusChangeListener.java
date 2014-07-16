package soundproject1.droid;


public class NotificationManager_FocusChangeListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.media.AudioManager.OnAudioFocusChangeListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioFocusChange:(I)V:GetOnAudioFocusChange_IHandler:Android.Media.AudioManager/IOnAudioFocusChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("SoundProject1.Droid.NotificationManager/FocusChangeListener, SoundProject1.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NotificationManager_FocusChangeListener.class, __md_methods);
	}


	public NotificationManager_FocusChangeListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NotificationManager_FocusChangeListener.class)
			mono.android.TypeManager.Activate ("SoundProject1.Droid.NotificationManager/FocusChangeListener, SoundProject1.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onAudioFocusChange (int p0)
	{
		n_onAudioFocusChange (p0);
	}

	private native void n_onAudioFocusChange (int p0);

	java.util.ArrayList refList;
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
