using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCoreSdkTests.Templates
{
    public class ConsoleApplicationTemplate : ClassLibraryTemplate
    {
        public ConsoleApplicationTemplate() { }

        public override string Name => "console";

        public override string OutputPath => Path.Combine("Debug", "netcoreapp2.1", RuntimeIdentifier.Path);

        public override TemplateType Type => TemplateType.ConsoleApplication;

        public override IEnumerable<string> ExpectedObjFilesAfterBuild => Enumerable.Concat(base.ExpectedObjFilesAfterBuild, new[]
        {
            (RuntimeIdentifier == RuntimeIdentifier.None) ? string.Empty : Path.Combine("netcoreapp2.1", RuntimeIdentifier.Path, "host", $"{Name}.exe"),
        }.Where(p => !string.IsNullOrEmpty(p)));

        public override IEnumerable<string> ExpectedBinFilesAfterBuild => Enumerable.Concat(base.ExpectedBinFilesAfterBuild, new[]
        {
            $"{Name}.runtimeconfig.dev.json",
            $"{Name}.runtimeconfig.json",
            (RuntimeIdentifier == RuntimeIdentifier.None) ? string.Empty : $"{Name}.exe",
        }.Where(p => !string.IsNullOrEmpty(p)).Select(p => Path.Combine(OutputPath, p)));

        public override IEnumerable<string> ExpectedFilesAfterPublish => Enumerable.Concat(base.ExpectedFilesAfterPublish, new[]
        {
            $"{Name}.runtimeconfig.json",
        });
    }
}
