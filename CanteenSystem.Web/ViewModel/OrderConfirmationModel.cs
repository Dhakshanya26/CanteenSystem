using System;
namespace CanteenSystem.Web.ViewModel
{
    public class OrderConfirmationModel
    {
        public OrderConfirmationModel(string message)
        {
            NotificationMessage = message;
        }

        public string NotificationMessage { get; set; }
        public string Status { get; set; }

    }
}