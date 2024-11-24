using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils_for_PBI.Data_Structures;

namespace Utils_for_PBI.Server
{
    public class JSONDataServer
    {
        public HttpListener dataServer;
        public CancellationTokenSource cancellationTokenSource;
        public CalcDepedencyData calcDepedencyData;

        public JSONDataServer(string urlAddress, CalcDepedencyData argCalcDepedencyData)
        {
            calcDepedencyData = argCalcDepedencyData;
            dataServer = new HttpListener();
            dataServer.Prefixes.Add(urlAddress);

            cancellationTokenSource = new CancellationTokenSource();
        }
        public void Start()
        {
            dataServer.Start();
            Debug.WriteLine("Server Started");
            Task.Run(() => HandleRequests(cancellationTokenSource.Token));
        }
        public void Stop()
        {
            cancellationTokenSource.Cancel();
            dataServer.Stop();
        }
        public async Task HandleRequests(CancellationToken token)
        {
            Debug.WriteLine("Incoming Request");
            while (true)
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        HttpListenerContext context = await dataServer.GetContextAsync();
                        await HandleRequest(context);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            Debug.WriteLine(request.RawUrl);
            switch (request.RawUrl)
            {
                case "/utilspbi/api/nodesdata":
                    await ServeContent(response, calcDepedencyData.dependencyNodesJSON);
                    break;
                case "/utilspbi/api/edgesdata":
                    await ServeContent(response, calcDepedencyData.dependencyEdgesJSON);
                    break;
                default:
                    response.StatusCode = 404;
                    await ServeContent(response, "<h1>Not Found Page</h1>");
                    break;

            }
            response.OutputStream.Close();
        }

        public async Task ServeContent(HttpListenerResponse response, string jsonData)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(jsonData);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }


    }
}
