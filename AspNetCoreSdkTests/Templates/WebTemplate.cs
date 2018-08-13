﻿using AspNetCoreSdkTests.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCoreSdkTests.Templates
{
    public class WebTemplate : ConsoleApplicationTemplate
    {
        public WebTemplate() { }

        public override string Name => "web";

        public override TemplateType Type => TemplateType.WebApplication;

        public override IEnumerable<string> ExpectedObjFilesAfterBuild => 
            base.ExpectedObjFilesAfterBuild
            .Concat(new[]
            {
                $"{Name}.RazorAssemblyInfo.cache",
                $"{Name}.RazorAssemblyInfo.cs",
                $"{Name}.RazorTargetAssemblyInfo.cache",
            }.Select(p => Path.Combine(OutputPath, p)));

        private IDictionary<(string TargetFrameworkMoniker, RuntimeIdentifier), Func<IEnumerable<string>>> _additionalFilesAfterPublish =>
            new Dictionary<(string TargetFrameworkMoniker, RuntimeIdentifier), Func<IEnumerable<string>>>()
            {
                { ("netcoreapp2.1", RuntimeIdentifier.None), () => new[]
                    {
                        // Publish includes all *.config and *.json files (https://github.com/aspnet/websdk/issues/334)
                        "NuGet.config",
                        "web.config",
                    }
                },
                { ("netcoreapp2.1", RuntimeIdentifier.Linux_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.1", RuntimeIdentifier.None)]()
                    .Concat(new[]
                    {
                        "Microsoft.AspNetCore.Antiforgery.dll",
                        "Microsoft.AspNetCore.Authentication.Abstractions.dll",
                        "Microsoft.AspNetCore.Authentication.Cookies.dll",
                        "Microsoft.AspNetCore.Authentication.Core.dll",
                        "Microsoft.AspNetCore.Authentication.dll",
                        "Microsoft.AspNetCore.Authentication.Facebook.dll",
                        "Microsoft.AspNetCore.Authentication.Google.dll",
                        "Microsoft.AspNetCore.Authentication.JwtBearer.dll",
                        "Microsoft.AspNetCore.Authentication.MicrosoftAccount.dll",
                        "Microsoft.AspNetCore.Authentication.OAuth.dll",
                        "Microsoft.AspNetCore.Authentication.OpenIdConnect.dll",
                        "Microsoft.AspNetCore.Authentication.Twitter.dll",
                        "Microsoft.AspNetCore.Authentication.WsFederation.dll",
                        "Microsoft.AspNetCore.Authorization.dll",
                        "Microsoft.AspNetCore.Authorization.Policy.dll",
                        "Microsoft.AspNetCore.Connections.Abstractions.dll",
                        "Microsoft.AspNetCore.CookiePolicy.dll",
                        "Microsoft.AspNetCore.Cors.dll",
                        "Microsoft.AspNetCore.Cryptography.Internal.dll",
                        "Microsoft.AspNetCore.Cryptography.KeyDerivation.dll",
                        "Microsoft.AspNetCore.DataProtection.Abstractions.dll",
                        "Microsoft.AspNetCore.DataProtection.dll",
                        "Microsoft.AspNetCore.DataProtection.Extensions.dll",
                        "Microsoft.AspNetCore.Diagnostics.Abstractions.dll",
                        "Microsoft.AspNetCore.Diagnostics.dll",
                        "Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore.dll",
                        "Microsoft.AspNetCore.dll",
                        "Microsoft.AspNetCore.HostFiltering.dll",
                        "Microsoft.AspNetCore.Hosting.Abstractions.dll",
                        "Microsoft.AspNetCore.Hosting.dll",
                        "Microsoft.AspNetCore.Hosting.Server.Abstractions.dll",
                        "Microsoft.AspNetCore.Html.Abstractions.dll",
                        "Microsoft.AspNetCore.Http.Abstractions.dll",
                        "Microsoft.AspNetCore.Http.Connections.Common.dll",
                        "Microsoft.AspNetCore.Http.Connections.dll",
                        "Microsoft.AspNetCore.Http.dll",
                        "Microsoft.AspNetCore.Http.Extensions.dll",
                        "Microsoft.AspNetCore.Http.Features.dll",
                        "Microsoft.AspNetCore.HttpOverrides.dll",
                        "Microsoft.AspNetCore.HttpsPolicy.dll",
                        "Microsoft.AspNetCore.Identity.dll",
                        "Microsoft.AspNetCore.Identity.EntityFrameworkCore.dll",
                        "Microsoft.AspNetCore.Identity.UI.dll",
                        "Microsoft.AspNetCore.Identity.UI.Views.dll",
                        "Microsoft.AspNetCore.JsonPatch.dll",
                        "Microsoft.AspNetCore.Localization.dll",
                        "Microsoft.AspNetCore.Localization.Routing.dll",
                        "Microsoft.AspNetCore.MiddlewareAnalysis.dll",
                        "Microsoft.AspNetCore.Mvc.Abstractions.dll",
                        "Microsoft.AspNetCore.Mvc.ApiExplorer.dll",
                        "Microsoft.AspNetCore.Mvc.Core.dll",
                        "Microsoft.AspNetCore.Mvc.Cors.dll",
                        "Microsoft.AspNetCore.Mvc.DataAnnotations.dll",
                        "Microsoft.AspNetCore.Mvc.dll",
                        "Microsoft.AspNetCore.Mvc.Formatters.Json.dll",
                        "Microsoft.AspNetCore.Mvc.Formatters.Xml.dll",
                        "Microsoft.AspNetCore.Mvc.Localization.dll",
                        "Microsoft.AspNetCore.Mvc.Razor.dll",
                        "Microsoft.AspNetCore.Mvc.Razor.Extensions.dll",
                        "Microsoft.AspNetCore.Mvc.RazorPages.dll",
                        "Microsoft.AspNetCore.Mvc.TagHelpers.dll",
                        "Microsoft.AspNetCore.Mvc.ViewFeatures.dll",
                        "Microsoft.AspNetCore.NodeServices.dll",
                        "Microsoft.AspNetCore.Owin.dll",
                        "Microsoft.AspNetCore.Razor.dll",
                        "Microsoft.AspNetCore.Razor.Language.dll",
                        "Microsoft.AspNetCore.Razor.Runtime.dll",
                        "Microsoft.AspNetCore.ResponseCaching.Abstractions.dll",
                        "Microsoft.AspNetCore.ResponseCaching.dll",
                        "Microsoft.AspNetCore.ResponseCompression.dll",
                        "Microsoft.AspNetCore.Rewrite.dll",
                        "Microsoft.AspNetCore.Routing.Abstractions.dll",
                        "Microsoft.AspNetCore.Routing.dll",
                        "Microsoft.AspNetCore.Server.HttpSys.dll",
                        "Microsoft.AspNetCore.Server.IISIntegration.dll",
                        "Microsoft.AspNetCore.Server.Kestrel.Core.dll",
                        "Microsoft.AspNetCore.Server.Kestrel.dll",
                        "Microsoft.AspNetCore.Server.Kestrel.Https.dll",
                        "Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.dll",
                        "Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.dll",
                        "Microsoft.AspNetCore.Session.dll",
                        "Microsoft.AspNetCore.SignalR.Common.dll",
                        "Microsoft.AspNetCore.SignalR.Core.dll",
                        "Microsoft.AspNetCore.SignalR.dll",
                        "Microsoft.AspNetCore.SignalR.Protocols.Json.dll",
                        "Microsoft.AspNetCore.SpaServices.dll",
                        "Microsoft.AspNetCore.SpaServices.Extensions.dll",
                        "Microsoft.AspNetCore.StaticFiles.dll",
                        "Microsoft.AspNetCore.WebSockets.dll",
                        "Microsoft.AspNetCore.WebUtilities.dll",
                        "Microsoft.CodeAnalysis.CSharp.dll",
                        "Microsoft.CodeAnalysis.dll",
                        "Microsoft.CodeAnalysis.Razor.dll",
                        "Microsoft.DotNet.PlatformAbstractions.dll",
                        "Microsoft.EntityFrameworkCore.Abstractions.dll",
                        "Microsoft.EntityFrameworkCore.Design.dll",
                        "Microsoft.EntityFrameworkCore.dll",
                        "Microsoft.EntityFrameworkCore.InMemory.dll",
                        "Microsoft.EntityFrameworkCore.Relational.dll",
                        "Microsoft.EntityFrameworkCore.SqlServer.dll",
                        "Microsoft.Extensions.Caching.Abstractions.dll",
                        "Microsoft.Extensions.Caching.Memory.dll",
                        "Microsoft.Extensions.Caching.SqlServer.dll",
                        "Microsoft.Extensions.Configuration.Abstractions.dll",
                        "Microsoft.Extensions.Configuration.Binder.dll",
                        "Microsoft.Extensions.Configuration.CommandLine.dll",
                        "Microsoft.Extensions.Configuration.dll",
                        "Microsoft.Extensions.Configuration.EnvironmentVariables.dll",
                        "Microsoft.Extensions.Configuration.FileExtensions.dll",
                        "Microsoft.Extensions.Configuration.Ini.dll",
                        "Microsoft.Extensions.Configuration.Json.dll",
                        "Microsoft.Extensions.Configuration.KeyPerFile.dll",
                        "Microsoft.Extensions.Configuration.UserSecrets.dll",
                        "Microsoft.Extensions.Configuration.Xml.dll",
                        "Microsoft.Extensions.DependencyInjection.Abstractions.dll",
                        "Microsoft.Extensions.DependencyInjection.dll",
                        "Microsoft.Extensions.DependencyModel.dll",
                        "Microsoft.Extensions.DiagnosticAdapter.dll",
                        "Microsoft.Extensions.FileProviders.Abstractions.dll",
                        "Microsoft.Extensions.FileProviders.Composite.dll",
                        "Microsoft.Extensions.FileProviders.Embedded.dll",
                        "Microsoft.Extensions.FileProviders.Physical.dll",
                        "Microsoft.Extensions.FileSystemGlobbing.dll",
                        "Microsoft.Extensions.Hosting.Abstractions.dll",
                        "Microsoft.Extensions.Hosting.dll",
                        "Microsoft.Extensions.Http.dll",
                        "Microsoft.Extensions.Identity.Core.dll",
                        "Microsoft.Extensions.Identity.Stores.dll",
                        "Microsoft.Extensions.Localization.Abstractions.dll",
                        "Microsoft.Extensions.Localization.dll",
                        "Microsoft.Extensions.Logging.Abstractions.dll",
                        "Microsoft.Extensions.Logging.Configuration.dll",
                        "Microsoft.Extensions.Logging.Console.dll",
                        "Microsoft.Extensions.Logging.Debug.dll",
                        "Microsoft.Extensions.Logging.dll",
                        "Microsoft.Extensions.Logging.EventSource.dll",
                        "Microsoft.Extensions.Logging.TraceSource.dll",
                        "Microsoft.Extensions.ObjectPool.dll",
                        "Microsoft.Extensions.Options.ConfigurationExtensions.dll",
                        "Microsoft.Extensions.Options.dll",
                        "Microsoft.Extensions.Primitives.dll",
                        "Microsoft.Extensions.WebEncoders.dll",
                        "Microsoft.IdentityModel.Logging.dll",
                        "Microsoft.IdentityModel.Protocols.dll",
                        "Microsoft.IdentityModel.Protocols.OpenIdConnect.dll",
                        "Microsoft.IdentityModel.Protocols.WsFederation.dll",
                        "Microsoft.IdentityModel.Tokens.dll",
                        "Microsoft.IdentityModel.Tokens.Saml.dll",
                        "Microsoft.IdentityModel.Xml.dll",
                        "Microsoft.Net.Http.Headers.dll",
                        "Newtonsoft.Json.Bson.dll",
                        "Newtonsoft.Json.dll",
                        "Remotion.Linq.dll",
                        "System.Data.SqlClient.dll",
                        "System.IdentityModel.Tokens.Jwt.dll",
                        "System.Interactive.Async.dll",
                        "System.IO.Pipelines.dll",
                        "System.Net.Http.Formatting.dll",
                        "System.Net.WebSockets.WebSocketProtocol.dll",
                        "System.Runtime.CompilerServices.Unsafe.dll",
                        "System.Security.Cryptography.Pkcs.dll",
                        "System.Security.Cryptography.Xml.dll",
                        "System.Security.Permissions.dll",
                        "System.Text.Encoding.CodePages.dll",
                        "System.Text.Encodings.Web.dll",
                        "System.Threading.Channels.dll",
                    })
                },
                { ("netcoreapp2.1", RuntimeIdentifier.OSX_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.1", RuntimeIdentifier.Linux_x64)]()
                },
                { ("netcoreapp2.1", RuntimeIdentifier.Win_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.1", RuntimeIdentifier.Linux_x64)]()
                    .Concat(new[]
                    {
                        "sni.dll",
                    })
                },
                { ("netcoreapp2.2", RuntimeIdentifier.None), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.1", RuntimeIdentifier.None)]()
                },
                { ("netcoreapp2.2", RuntimeIdentifier.Linux_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.1", RuntimeIdentifier.Linux_x64)]()
                    .Concat(new[]
                    {
                        "Microsoft.AspNetCore.Diagnostics.HealthChecks.dll",
                        "Microsoft.AspNetCore.Server.IIS.dll",
                        "Microsoft.Extensions.Diagnostics.HealthChecks.Abstractions.dll",
                        "Microsoft.Extensions.Diagnostics.HealthChecks.dll",
                    })
                },
                { ("netcoreapp2.2", RuntimeIdentifier.OSX_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.2", RuntimeIdentifier.Linux_x64)]()
                },
                { ("netcoreapp2.2", RuntimeIdentifier.Win_x64), () =>
                    _additionalFilesAfterPublish[("netcoreapp2.2", RuntimeIdentifier.Linux_x64)]()
                    .Concat(new[]
                    {
                        "aspnetcorev2_inprocess.dll",
                        "sni.dll",
                    })
                },
            };

        public override IEnumerable<string> ExpectedFilesAfterPublish =>
            base.ExpectedFilesAfterPublish
            .Concat(_additionalFilesAfterPublish[(DotNetUtil.TargetFrameworkMoniker, RuntimeIdentifier)]());
    }
}
