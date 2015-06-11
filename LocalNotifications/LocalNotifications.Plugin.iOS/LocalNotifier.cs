using System;
using System.Linq;
using LocalNotifications.Plugin.Abstractions;
#if __UNIFIED__
using AudioToolbox;
using UIKit;
using Foundation;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AudioToolbox;
#endif


namespace LocalNotifications.Plugin
{
    /// <summary>
    /// Implementation of ILocalNotifier for iOS
    /// </summary>
    public class LocalNotifier : ILocalNotifier
    {
        private const string NotificationKey = "JinlifeNotificationKey";

        /// <summary>
        /// Notifies the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void Notify(LocalNotification notification)
        {
            var nativeNotification = createNativeNotification(notification);

            UIApplication.SharedApplication.ScheduleLocalNotification(nativeNotification);
            if (notification.Vibrate)
            {
                SystemSound.Vibrate.PlaySystemSound();
            }
        }

        /// <summary>
        /// Cancels the specified notification identifier.
        /// </summary>
        /// <param name="notificationId">The notification identifier.</param>
        public void Cancel(int notificationId)
        {
            var notifications = UIApplication.SharedApplication.ScheduledLocalNotifications;
            var notification = notifications.Where(n => n.UserInfo.ContainsKey(NSObject.FromObject(NotificationKey)))
                .FirstOrDefault(n => n.UserInfo[NotificationKey].Equals(NSObject.FromObject(notificationId)));

            if (notification != null)
            {
                UIApplication.SharedApplication.CancelLocalNotification(notification);
            }
        }

        private UILocalNotification createNativeNotification(LocalNotification notification)
        {
            var nativeNotification = new UILocalNotification
            {
                AlertAction = notification.Title,
                AlertBody = notification.Text,
                FireDate = notification.NotifyTime.DateTimeToNSDate(),
                UserInfo = NSDictionary.FromObjectAndKey(NSObject.FromObject(notification.Id), NSObject.FromObject(NotificationKey))
            };

            if (notification.PlaySound != NoficationSoundType.None)
            {
                if (notification.CustomIOSSound != null && notification.CustomIOSSound.Length > 0 && !notification.CustomIOSSound.Equals("default"))
                {
                    // for sounds different from the default one
                    nativeNotification.SoundName = notification.CustomIOSSound;
                }
                else
                {
                    nativeNotification.SoundName = UILocalNotification.DefaultSoundName;
                }
            }
            return nativeNotification;
        }
    }
    public static class DateTimeExtensions
    {
        public static DateTime NSDateToDateTime(this NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);  /* DateTimeKind.Local or DateTimeKind.Utc, this depends on each app */
            return (NSDate)date;
        }
    }
}