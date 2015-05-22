using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReadAsyncIssue.Console
{
	public class Program
	{
		private readonly byte[] buffer = new byte[8192];

		public void Main(string[] args)
		{
			DoWorkAsync().Wait();
		}

		private async Task DoWorkAsync()
		{
			using (var httpClient = new HttpClient())
			{
				var message = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/api/values");

				var response = await httpClient
					.SendAsync(message, HttpCompletionOption.ResponseHeadersRead)
					.ConfigureAwait(false);

				var stream = await response
					.Content
					.ReadAsStreamAsync()
					.ConfigureAwait(false);

				var read = await stream
					.ReadAsync(buffer, 0, buffer.Length)
					.ConfigureAwait(false);

				if (read == buffer.Length)
					throw new InvalidOperationException();
			}
		}
	}
}
