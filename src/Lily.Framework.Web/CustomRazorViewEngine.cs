using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lily.Framework.Web
{
    public class CustomRazorViewEngine: RazorViewEngine
    {
        public CustomRazorViewEngine(
            IRazorPageFactoryProvider pageFactory, 
            IRazorPageActivator pageActivator, 
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProjectFileSystem razorFileSystem, 
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) 
            : base(pageFactory, pageActivator, 
                htmlEncoder, optionsAccessor, 
                razorFileSystem, loggerFactory, diagnosticSource)
        {
        }
    }
}
