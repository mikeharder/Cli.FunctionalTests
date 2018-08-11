using AspNetCoreSdkTests.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspNetCoreSdkTests.Templates
{
    public class AngularTemplate : SpaBaseTemplate
    {
        public AngularTemplate() { }

        public override string Name => "angular";

        // Remove generated hashes since they may vary by platform
        public override IEnumerable<string> FilesAfterPublish =>
            base.FilesAfterPublish.Select(f => Regex.Replace(f, @"\.[0-9a-f]{20}\.", ".[HASH]."));

        protected override void AfterNew(string tempDir)
        {
            // Workaround until https://github.com/aspnet/Templating/pull/672 is merged
            const string before =
@"<DistFiles Include=""$(SpaRoot)dist\**; $(SpaRoot)dist-server\**; $(SpaRoot)package.json"" />";

            const string after =
@"<DistFiles Include=""$(SpaRoot)dist\**; $(SpaRoot)dist-server\**"" />";

            IOUtil.ReplaceInFile(Path.Combine(tempDir, $"{Name}.csproj"), before, after);

            base.AfterNew(tempDir);
        }

        public override IEnumerable<string> ExpectedFilesAfterPublish =>
            base.ExpectedFilesAfterPublish
            .Concat(new[]
            {
                Path.Combine("wwwroot", "favicon.ico"),
                Path.Combine("ClientApp", "dist", "3rdpartylicenses.txt"),
                Path.Combine("ClientApp", "dist", "glyphicons-halflings-regular.[HASH].woff2"),
                Path.Combine("ClientApp", "dist", "glyphicons-halflings-regular.[HASH].svg"),
                Path.Combine("ClientApp", "dist", "glyphicons-halflings-regular.[HASH].ttf"),
                Path.Combine("ClientApp", "dist", "glyphicons-halflings-regular.[HASH].eot"),
                Path.Combine("ClientApp", "dist", "glyphicons-halflings-regular.[HASH].woff"),
                Path.Combine("ClientApp", "dist", "index.html"),
            })
            .Concat(DotNetUtil.TargetFrameworkMoniker == "netcoreapp2.1" ?
                new[]
                {
                    Path.Combine("ClientApp", "dist", $"inline.[HASH].bundle.js"),
                    Path.Combine("ClientApp", "dist", $"main.[HASH].bundle.js"),
                    Path.Combine("ClientApp", "dist", $"polyfills.[HASH].bundle.js"),
                    Path.Combine("ClientApp", "dist", $"styles.[HASH].bundle.css"),
                }
                :
                new[]
                {
                    Path.Combine("ClientApp", "dist", $"runtime.[HASH].js"),
                    Path.Combine("ClientApp", "dist", $"main.[HASH].js"),
                    Path.Combine("ClientApp", "dist", $"polyfills.[HASH].js"),
                    Path.Combine("ClientApp", "dist", $"styles.[HASH].css"),
                });
    }
}
