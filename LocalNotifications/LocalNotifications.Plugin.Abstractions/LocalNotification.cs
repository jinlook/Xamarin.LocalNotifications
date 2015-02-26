using System;

namespace LocalNotifications.Plugin.Abstractions
{
    /// <summary>
    /// Notification options data used for creating native notification
    /// </summary>
    public class LocalNotification
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// Notification identifier used for canceling not scheduled notification
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the notification title.
        /// </summary>
        /// <value>
        /// The notification title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the notification text.
        /// </summary>
        /// <value>
        /// The notification text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the notify time of notification.
        /// </summary>
        /// <value>
        /// The notify time of notification.
        /// </value>
        public DateTime NotifyTime { get; set; }

        /// <summary>
        /// Enable notification vibrate, default set 500ms for android and ios not configurable.
        /// </summary>
        public bool Vibrate { get; set; }

        /// <summary>
        /// Play the default notification sound
        /// </summary>
        public NoficationSoundType PlaySound { get; set; }

        public string CustomIOSSound { get; set; }
    }

    /// <summary>
    /// None and RingtoneType
    /// </summary>
    public enum NoficationSoundType
    {
        None = 0,
        Ringtone = 1,
        Notification = 2,  //Recommended, maybe some pad have no Ringtone and Alarm
        Alarm = 4
    }
}
