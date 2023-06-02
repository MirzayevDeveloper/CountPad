using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace CountPad.Application.UseCases.Users.Notifications
{
	public record UserCreatedNotification(string Phone) : INotification;

	public class UserCreatedLogNotificationHandler : INotificationHandler<UserCreatedNotification>
	{
		public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
		{
			Log.Information($"CountPad: New user create with {notification.Phone} phone number.");

			return Task.CompletedTask;
		}
	}

	public class UserCreatedConsoleNotificationHandler : INotificationHandler<UserCreatedNotification>
	{
		public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
		{
			await Console.Out.WriteLineAsync($"CountPad: New user create with {notification.Phone} phone number.");
		}
	}
}
