using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace AllExtensions
{
    public class MainViewModelDesign : IMainViewModel
    {
        public MainViewModelDesign()
        {
            Hello = "Hello from IoC";
        }

        public string Hello { get; set; }
    }
}
