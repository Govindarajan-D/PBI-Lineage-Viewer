using log4net;
using System;
using System.Net;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils_for_PBI.Services;

namespace Utils_for_PBI.Server
{
    /// <summary>
    ///  JSONDataServer starts a HTTP Listener that serves Calculation dependency data in JSON format
    ///  The Server runs in multi-threaded mode. 
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class UtilsPBIHTTPServer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UtilsPBIHTTPServer));

        public HttpListener dataServer;
        public bool isStarted = false;
        public CancellationTokenSource cancellationTokenSource;
        public ModelMetadata modelMetadata;
        public String serverPrefix;

        public UtilsPBIHTTPServer(string urlAddress, ModelMetadata argmodelMetadata)
        {
            modelMetadata = argmodelMetadata;
            dataServer = new HttpListener();
            serverPrefix = urlAddress;
            dataServer.Prefixes.Add(serverPrefix);

            cancellationTokenSource = new CancellationTokenSource();
        }
        public void Start()
        {
            dataServer.Start();
            isStarted = true;
            Task.Run(() => HandleRequests(cancellationTokenSource.Token));
            Logger.Info($"HTTP Server started at: {serverPrefix}");
        }
        public void Stop()
        {
            cancellationTokenSource.Cancel();
            dataServer.Stop();
            Logger.Info($"HTTP Server running at: {serverPrefix} stopped");
        }
        public async Task HandleRequests(CancellationToken token)
        {
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
                    Logger.Error(ex.Message);
                }
            }
        }

        public async Task HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            response.AddHeader("Access-Control-Allow-Origin", "*");  // Allow all origins
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");  // Allow these methods
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");  // Allow these headers

            switch (request.RawUrl)
            {
                case "/utilspbi/api/nodesinfo":
                    await ServeContent(response, modelMetadata.GetNodesInfo());
                    break;
                case "/utilspbi/api/objecttypeinfo":
                    await ServeContent(response, modelMetadata.GetObjectTypeInfo());
                    break;
                case "/utilspbi/api/nodes":
                    await ServeContent(response, modelMetadata.GetSvelteFlowNodesJson());
                    break;
                case "/utilspbi/api/edges":
                    await ServeContent(response, modelMetadata.GetSvelteFlowEdgesJson());
                    break;
                default:
                    response.StatusCode = 404;
                    await ServeContent(response, "Invalid URL. No data");
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
