using System.Runtime.InteropServices;

namespace AspNetCoreSdkTests
{
    // https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
    public class RuntimeIdentifier
    {
        public static RuntimeIdentifier None = new RuntimeIdentifier() {
            Name = "none",
            OSPlatform = null,
        };

        public static RuntimeIdentifier Win_x64 = new RuntimeIdentifier() {
            Name = "win-x64",
            OSPlatform = System.Runtime.InteropServices.OSPlatform.Windows,
        };

        public static RuntimeIdentifier Linux_x64 = new RuntimeIdentifier() {
            Name = "linux-x64",
            OSPlatform = System.Runtime.InteropServices.OSPlatform.Linux,
        };

        private RuntimeIdentifier() { }

        public string Name { get; private set; }
        public string RuntimeArgument => (this == None) ? string.Empty : $"--runtime {Name}";
        public string Path => (this == None) ? string.Empty : Name;
        public OSPlatform? OSPlatform { get; private set; }

        public override string ToString() => Name;
    }
}
