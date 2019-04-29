using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace AllExtensions
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel(ILogger<MainViewModel> logger, IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient();
            logger.LogCritical("Always be logging!");
            Hello = "Hello from IoC";
        }

        public string Hello { get; set; }
    }
}
