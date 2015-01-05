using System;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using LocalNotifications.Plugin.Abstractions;

namespace LocalNotifications.Plugin
{
  /// <summary>
  /// Implementation for LocalNotifications
  /// </summary>
  public class LocalNotifier : ILocalNotifier
  {
      public void Notify(LocalNotification notification)
      {
          var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);

          var tileTitle = tileXml.GetElementsByTagName("text");
          ((XmlElement)tileTitle[0]).InnerText = notification.Title;
          ((XmlElement)tileTitle[1]).InnerText = notification.Text;

          var scheduledTileNotification = new ScheduledTileNotification(tileXml, notification.NotifyTime)
          {
              Id = notification.Id.ToString()
          };
          TileUpdateManager.CreateTileUpdaterForApplication().AddToSchedule(scheduledTileNotification);
      }

      public void Cancel(int notificationId)
      {
          var scheduledNotifications = TileUpdateManager.CreateTileUpdaterForApplication().GetScheduledTileNotifications();
          var notification =
              scheduledNotifications.FirstOrDefault(n => n.Id.Equals(notificationId.ToString(), StringComparison.OrdinalIgnoreCase));
          
          if (notification != null)
          {
              TileUpdateManager.CreateTileUpdaterForApplication().RemoveFromSchedule(notification);
          }
      }
  }
}