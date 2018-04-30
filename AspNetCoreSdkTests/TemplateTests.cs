using AspNetCoreSdkTests.Templates;
using AspNetCoreSdkTests.Util;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace AspNetCoreSdkTests
{
    [TestFixture]
    public class TemplateTests
    {
        protected static readonly TimeSpan SleepBetweenHttpRequests = TimeSpan.FromMilliseconds(100);

        private static readonly HttpClient HttpClient = new HttpClient(new HttpClientHandler()
        {
            // Allow self-signed certs
            ServerCertificateCustomValidationCallback = (m, c, ch, p) => true
        });

        [Test]
        [TestCaseSource(typeof(TemplateData), nameof(TemplateData.Current))]
        public void Restore(Template template, NuGetConfig nuGetConfig)
        {
            using (var context = new DotNetContext())
            {
                context.New(template);
                context.Restore(nuGetConfig);

                CollectionAssert.AreEquivalent(template.ExpectedObjFilesAfterRestore, context.GetObjFiles());
            }
        }

        [Test]
        [TestCaseSource(typeof(TemplateData), nameof(TemplateData.Current))]
        public void Build(Template template, NuGetConfig nuGetConfig)
        {
            using (var context = new DotNetContext())
            {
                context.New(template);
                context.Restore(nuGetConfig);
                context.Build();

                CollectionAssert.AreEquivalent(template.ExpectedObjFilesAfterBuild, context.GetObjFiles());
                CollectionAssert.AreEquivalent(template.ExpectedBinFilesAfterBuild, context.GetBinFiles());
            }
        }

        [Test]
        [TestCaseSource(typeof(TemplateData), nameof(TemplateData.CurrentWebApplications))]
        public void Run(Template template, NuGetConfig nuGetConfig)
        {
            using (var context = new DotNetContext())
            {
                context.New(template);
                context.Restore(nuGetConfig);
                var (httpUrl, httpsUrl) = context.Run();
                Console.WriteLine(httpUrl);
                Console.WriteLine(httpsUrl);
            }
        }
    }
}
