using AspNetCoreSdkTests.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCoreSdkTests.Templates
{
    public class MvcTemplate : RazorBootstrapJQueryTemplate
    {
        public MvcTemplate() { }

        public override string Name => "mvc";

        protected override string RazorPath => "Views";

        private IDictionary<string, Func<IEnumerable<string>>> _additionalFilesAfterPublish =>
            new Dictionary<string, Func<IEnumerable<string>>>()
            {
                { "netcoreapp2.1", () =>
                    _additionalFilesAfterPublish["netcoreapp2.2"]()
                    .Concat(new[]
                    {
                        Path.Combine("Razor", RazorPath, "Home", "About.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Home", "Contact.g.cshtml.cs"),
                    })
                },
                { "netcoreapp2.2", () => new[]
                    {
                        Path.Combine("Razor", RazorPath, "_ViewStart.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Home", "Index.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Home", "Privacy.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Shared", "_CookieConsentPartial.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Shared", "_Layout.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Shared", "_ValidationScriptsPartial.g.cshtml.cs"),
                        Path.Combine("Razor", RazorPath, "Shared", "Error.g.cshtml.cs"),
                    }
                },
            };

        public override IEnumerable<string> ExpectedObjFilesAfterBuild =>
            base.ExpectedObjFilesAfterBuild
            .Concat(_additionalFilesAfterPublish[DotNetUtil.TargetFrameworkMoniker]().Select(p => Path.Combine(OutputPath, p)));
    }
}
