using System;
using Microsoft.AspNet.Mvc;

namespace SampleWebApp.Controllers
{
    [Route("[Controller]")]
    public class AboutController
    {
        [Route("[action]")]
        public string Phone()
        {
            return "6141984758";
        }
        
        [Route("")]
        public string Office()
        {
            return "CUU";
        }
    }
 }