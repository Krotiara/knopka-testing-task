using Knopka.Hr21.Advertising;
using NUnit.Framework;

namespace Knopka.Hr21.Tests.Advertising
{
    public class MediaLocatorTest
    {
        [Test]
        public void Basic()
        {
            var locator = BuildMediaLocator("basic.txt");
            Assert.That(locator.GetMediasForLocation("/ru/svrd/ekb"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
                "Бегущая строка в троллейбусах Екатеринбурга",
                "Быстрый курьер",
            }));
            Assert.That(locator.GetMediasForLocation("/ru/svrd/ekb/center/square1905"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
                "Бегущая строка в троллейбусах Екатеринбурга",
                "Быстрый курьер",
            }));
            Assert.That(locator.GetMediasForLocation("/ru/svrd/pervik"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
                "Ревдинский рабочий",
            }));
            Assert.That(locator.GetMediasForLocation("/ru/chelobl/chel"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
                "Газета уральских москвичей",
            }));
            Assert.That(locator.GetMediasForLocation("/ru/hmao"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
            }));
            Assert.That(locator.GetMediasForLocation("/ru/msk"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
                "Газета уральских москвичей",
            }));
            Assert.That(locator.GetMediasForLocation("/ru"), Is.EquivalentTo(new[]
            {
                "Яндекс.Директ",
            }));
            Assert.That(locator.GetMediasForLocation("/us/ohio"), Is.Empty);
        }

        [Test]
        public void Startswith()
        {
            var locator = BuildMediaLocator("startswith.txt");
            Assert.That(locator.GetMediasForLocation("/ru/svrd"), Is.EquivalentTo(new[]
            {
                "Альфа",
            }));
        }

        [Test]
        public void Contains()
        {
            var locator = BuildMediaLocator("contains.txt");
            Assert.That(locator.GetMediasForLocation("/fr/paris"), Is.EquivalentTo(new[]
            {
                "Вести Франции",
            }));
        }

        private MediaLocator BuildMediaLocator(string sourceFileName)
        {
            var type = GetType();
            var path = type.FullName!.Replace(nameof(MediaLocatorTest), $"TestData.{sourceFileName}");
            using (var sourceStream = type.Assembly.GetManifestResourceStream(path))
                return new MediaLocator(sourceStream);
        }
    }
}