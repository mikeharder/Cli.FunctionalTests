using AspNetCoreSdkTests.Util;
using System.IO;

namespace AspNetCoreSdkTests.Templates
{
    public class ReactReduxTemplate : ReactTemplate
    {
        public ReactReduxTemplate() { }

        public override string Name => "reactredux";

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
    }
}
