using Microsoft.JSInterop;
using System.Security.Cryptography.X509Certificates;

namespace BlazorApp1
{
	public class AlertService : IAsyncDisposable, IAlertService
	{
		readonly Lazy<Task<IJSObjectReference>> ijsObjectReference;

		public AlertService(IJSRuntime ijsRuntime)
		{
			this.ijsObjectReference = new Lazy<Task<IJSObjectReference>>(() =>
			ijsRuntime.InvokeAsync<IJSObjectReference>
			("import", "./Home.js").AsTask());
		}

		public async ValueTask DisposeAsync()
		{
			if (ijsObjectReference.IsValueCreated)
			{
				IJSObjectReference moduleJS = await ijsObjectReference.Value;
				await moduleJS.DisposeAsync();
			}
		}

		public async Task CallJsFunction()
		{
			var jsModule = await ijsObjectReference.Value;

			await jsModule.InvokeVoidAsync("jsFuncion");
		}

	}
}

