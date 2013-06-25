namespace InjectableSettings.Test
{
	using System.Configuration;

	using FluentAssertions;

	using NUnit.Framework;

	using Ploeh.AutoFixture;

	[TestFixture]
	public class TestSettingsWithShortNames
	{
		class Testable : ConfigurationSetting {}

		[Test]
		public void ConfigurationSettingClassNamesDoNotNeedToEndWithConfigurationSettings()
		{
			// Arrange
			var fixture = new Fixture();
			var value = fixture.Create<string>();

			ConfigurationManager.AppSettings.Set("Testable", value);

			// Act
			var sut = new Testable();

			// Assert
			sut.Value.Should().Be(value);
		}
	}
}
