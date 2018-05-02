using AspNetCoreSdkTests.Templates;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AspNetCoreSdkTests
{
    [TestFixture]
    public class TemplateTests
    {
        [Test]
        [TestCaseSource(nameof(RestoreData))]
        public void Restore(Template template)
        {
            CollectionAssert.AreEquivalent(template.ExpectedObjFilesAfterRestore, template.ObjFilesAfterRestore);
        }

        [Test]
        [TestCaseSource(nameof(BuildData))]
        public void Build(Template template)
        {
            CollectionAssert.AreEquivalent(template.ExpectedObjFilesAfterBuild, template.ObjFilesAfterBuild);
        }

        [Test]
        [TestCaseSource(nameof(PublishData))]
        public void Publish(Template template)
        {
            CollectionAssert.AreEquivalent(template.ExpectedFilesAfterPublish, template.FilesAfterPublish);
        }

        [Test]
        [TestCaseSource(nameof(RunData))]
        public void Run(Template template)
        {
            Assert.AreEqual(HttpStatusCode.OK, template.HttpResponseAfterRun.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, template.HttpsResponseAfterRun.StatusCode);
        }

        [Test]
        [TestCaseSource(nameof(ExecData))]
        public void Exec(Template template)
        {
            Assert.AreEqual(HttpStatusCode.OK, template.HttpResponseAfterExec.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, template.HttpsResponseAfterExec.StatusCode);
        }

        public static IEnumerable<Template> RestoreData = new[]
        {
            // Framework-dependent
            Template.GetInstance<ClassLibraryTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<ConsoleApplicationTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            // Offline restore currently not supported for RazorClassLibrary template (https://github.com/aspnet/Universe/issues/1123)
            Template.GetInstance<RazorClassLibraryTemplate>(NuGetPackageSource.NuGetOrg, RuntimeIdentifier.None),
            Template.GetInstance<WebTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<RazorTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<MvcTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<AngularTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<ReactTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<ReactReduxTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),
            Template.GetInstance<WebApiTemplate>(NuGetPackageSource.None, RuntimeIdentifier.None),

            // Self-contained
            // ClassLibrary does not require a package source, even for self-contained deployments
            Template.GetInstance<ClassLibraryTemplate>(NuGetPackageSource.None, RuntimeIdentifier.Win_x64),

            Template.GetInstance<ConsoleApplicationTemplate>(NuGetPackageSource.EnvironmentVariable, RuntimeIdentifier.Win_x64),
        };

        public static IEnumerable<Template> BuildData => RestoreData;

        public static IEnumerable<Template> PublishData => RestoreData;

        public static IEnumerable<Template> RunData = RestoreData.Where(t => t.Type == TemplateType.WebApplication);

        public static IEnumerable<Template> ExecData => RunData;
    }
}
