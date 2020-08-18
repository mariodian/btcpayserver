using BTCPayServer.Configuration;
using BTCPayServer.Controllers;
using BTCPayServer.Models.NotificationViewModels;
using Microsoft.AspNetCore.Routing;

namespace BTCPayServer.Services.Notifications.Blobs
{
    public class ExternalPayoutTransactionNotification
    {
        internal class Handler : NotificationHandler<ExternalPayoutTransactionNotification>
        {
            private readonly LinkGenerator _linkGenerator;
            private readonly BTCPayServerOptions _options;

            public Handler(LinkGenerator linkGenerator, BTCPayServerOptions options)
            {
                _linkGenerator = linkGenerator;
                _options = options;
            }
            public override string NotificationType => "external-payout-transaction";
            protected override void FillViewModel(ExternalPayoutTransactionNotification notification, NotificationViewModel vm)
            {
                vm.Body = "A payment that was made to an approved payout by an external wallet is waiting for your confirmation.";
                vm.ActionLink = _linkGenerator.GetPathByAction(nameof(WalletsController.Payouts),
                    "Wallets",
                    new { walletId = new WalletId(notification.StoreId, notification.PaymentMethod) }, _options.RootPath);
            }
        }
        public string PayoutId { get; set; }
        public string StoreId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
