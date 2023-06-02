using System.Threading;
using System.Threading.Tasks;
using CountPad.Domain.Entities.Users;
using MediatR;
using Serilog;

namespace CountPad.Application.UseCases.Users.Notifications
{
	public class UserUpdatedNotification : INotification
	{
		public User CurrentUser { get; set; }
		public User UpdatedUser { get; set; }
	}

	public class UserUpdatedNotificationHandler : INotificationHandler<UserUpdatedNotification>
	{
		public Task Handle(UserUpdatedNotification notification, CancellationToken cancellationToken)
		{
			Log.Information($"CountPad: Update user notification\nCurrent user: {notification.CurrentUser}\n" +
				$"Updated user: {notification.UpdatedUser}");

			return Task.CompletedTask;
		}
	}
}
