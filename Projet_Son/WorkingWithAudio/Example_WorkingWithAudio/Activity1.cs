using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.Threading.Tasks;

namespace WorkingWithAudio
{
	[Activity(Label = "WorkingWithAudio", MainLauncher = true, Icon = "@drawable/icon")]
	public class WorkingWithAudioActivity : Activity
	{
		PlayAudio playAudio = new PlayAudio ();
		RecordAudio recordAudio = new RecordAudio ();
		NotificationManager nMan = new NotificationManager ();
		static public bool useNotifications = false;
		static Activity activity = null;

		static public Activity Activity {
			get { return (activity); }
		}
		// buttons.
		Button startRecording = null;
		Button stopRecording = null;
		Button startPlayback = null;
		Button stopPlayback = null;

		bool haveRecording = false;
		bool isRecording = false;
		bool isPlaying = false;

		TextView status = null;

		void disableAllButtons ()
		{
			startRecording.Enabled = false;
			stopRecording.Enabled = false;
			startPlayback.Enabled = false;
			stopPlayback.Enabled = false;

		}
		// Provides the policy for which buttons should be enabled for any particular state.
		// The rule is that any action that has been started must be ended before the user 
		// is allowed to do anything else.
		void handleButtonState ()
		{
			disableAllButtons ();
			if (isRecording) {
				stopRecording.Enabled = true;
				return;
			}
			      
			if (isPlaying) {
				stopPlayback.Enabled = true;
				return;
			} else if (haveRecording) {
				startPlayback.Enabled = true;
			}
             
			startRecording.Enabled = true;

		}

		public void setStatus (String message)
		{
			status.Text = message;
		}

		public void resetPlayback ()
		{
			isPlaying = false;
			handleButtonState ();
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			NotificationManager.audioManager = (AudioManager)GetSystemService (Context.AudioService);

			// Hook up all of our handlers.

			// High-level operations.
			startRecording = FindViewById<Button> (Resource.Id.startRecordingButton);
			startRecording.Click += async delegate {
				await startOperationAsync (recordAudio);
				disableAllButtons ();
				isRecording = true;
				haveRecording = true;
				handleButtonState ();
			};
			stopRecording = FindViewById<Button> (Resource.Id.endRecordingButton);
			stopRecording.Click += delegate {
				stopOperation (recordAudio);
				isRecording = false;
				handleButtonState ();
			};
			startPlayback = FindViewById<Button> (Resource.Id.startPlaybackButton);
			//button.Click += delegate { _playAudio.start(); };
			startPlayback.Click += async delegate {
				await startOperationAsync (playAudio);
				disableAllButtons ();
				isPlaying = true;
				handleButtonState ();
			};
			stopPlayback = FindViewById<Button> (Resource.Id.endPlaybackButton);
			stopPlayback.Click += delegate {
				stopOperation (playAudio);
				isPlaying = false;
				handleButtonState ();
			};

			handleButtonState ();

			// Notifications.
			CheckBox notifCheckBox = FindViewById<CheckBox> (Resource.Id.useNotificationsCheckBox);
			notifCheckBox.Click += delegate {
				useNotifications = notifCheckBox.Checked;
			};

			status = FindViewById<TextView> (Resource.Id.status);

			NotificationManager.MainActivity = this;
			activity = this;
		}

		async Task startOperationAsync (INotificationReceiver nRec)
		{
			if (useNotifications) {
				bool haveFocus = nMan.RequestAudioResources (nRec);
				if (haveFocus) {
					status.Text = "Granted";
					await nRec.StartAsync ();
				} else {
					status.Text = "Denied";
				}
			} else {
				await nRec.StartAsync ();
			}
		}

		void stopOperation (INotificationReceiver nRec)
		{
			nRec.Stop ();
			if (useNotifications) {
				nMan.ReleaseAudioResources ();
				status.Text = "Released";
			}

		}
	}
}

