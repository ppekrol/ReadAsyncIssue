using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace ReadAsyncIssue.Controllers
{
	public class StreamResult : IActionResult
	{
		public Task ExecuteResultAsync(ActionContext context)
		{
			context.HttpContext.Response.ContentType = "text/event-stream";
			var s = new string('t', 100);


			while (true)
			{
				context.HttpContext.Response.WriteAsync(s);

				Thread.Sleep(100);
			}
		}
	}


    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		public IActionResult Get()
		{
			return new StreamResult();
		}
	}
}
