using System;

using Android.App;
using Android.Content;
using Android.Media;
using AndroidX.Core.App;
using WorkManagerDemo.Interfaces;

namespace WorkManagerDemo.Droid.Services
{
    public class NotificationService : INotificationService
    {
        private Context _context;
        private NotificationManager _notificationManager;
        private NotificationCompat.Builder _builder;
        public static string NOTIFICATION_CHANNEL_ID = "10023";

        public NotificationService()
        {
            _context = Application.Context;
        }
        public void CreateNotification(string title, string message)
        {
            try
            {
                //var sound = global::Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource + "://" + _context.PackageName + "/" + Resource.Raw.notification);
                //Create an Audio Attribute
                var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

                _builder = new NotificationCompat.Builder(_context, NOTIFICATION_CHANNEL_ID)
                .SetSmallIcon(Resource.Mipmap.icon)
                //.SetSound(sound)
                .SetAutoCancel(true)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetPriority((int)NotificationPriority.High)
                .SetVibrate(new long[0])
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                .SetVisibility((int)NotificationVisibility.Public);

                _notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, title, importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    //notificationChannel.SetSound(sound, alarmAttributes);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = NotificationImportance.High;
                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500 });

                    if (_notificationManager != null)
                    {
                        _builder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        _notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }
                _notificationManager.Notify(0, _builder.Build());
            }
            catch (Exception)
            {

            }
        }
    }
}