using AspNetCoreSdkTests.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCoreSdkTests.Templates
{
    public abstract class SpaBaseTemplate : RazorApplicationBaseTemplate
    {
        protected override string RazorPath => "Pages";

        protected override void AfterNew(string tempDir)
        {
            // Workaround until https://github.com/aspnet/Templating/pull/672 is merged
            const string before =
@"<None Include=""$(SpaRoot)**""
          Exclude=""$(SpaRoot)node_modules\**""
          CopyToPublishDirectory=""PreserveNewest""
          CopyToOutputDirectory=""PreserveNewest"" />";

            const string after =
@"<None Include=""$(SpaRoot)**"" Exclude=""$(SpaRoot)node_modules\**"" />";

            IOUtil.ReplaceInFile(Path.Combine(tempDir, $"{Name}.csproj"), before, after);

            base.AfterNew(tempDir);
        }

        public override IEnumerable<string> ExpectedObjFilesAfterBuild =>
            base.ExpectedObjFilesAfterBuild
            .Concat(new[]
            {
                Path.Combine("Razor", RazorPath, "Error.g.cshtml.cs"),
            }.Select(p => Path.Combine(OutputPath, p)));
    }
}
