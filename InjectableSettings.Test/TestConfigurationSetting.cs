namespace InjectableSettings.Test
{
	using System.Configuration;

	using FluentAssertions;

	using NUnit.Framework;

	[TestFixture]
	public class TestConfigurationSetting
	{
		public class TestableConfigurationSetting : ConfigurationSetting { }

		public class SettingWithDefaultConfigurationSetting : ConfigurationSetting
		{
			public SettingWithDefaultConfigurationSetting() : base("Default value") {}
		}

		[Test]
		public void ConfigurationSettingsAreReadFromAppConfig()
		{
			// Arrange
			ConfigurationManager.AppSettings.Set("Testable", "Value");

			// Act
			var sut = new TestableConfigurationSetting();

			// Assert
			sut.Value.Should().Be("Value");
		}

		[Test]
		public void ConfigurationSettingsSupportDefaultValues()
		{
			// Arrange
			ConfigurationManager.RefreshSection("appSettings");

			// Act
			var sut = new SettingWithDefaultConfigurationSetting();

			// Assert
			sut.Value.Should().Be("Default value");
		}

		[Test]
		public void ConfigurationSettingsWithoutDefaultValuesHaveNullValues()
		{
			// Arrange
			ConfigurationManager.RefreshSection("appSettings");

			// Act
			var sut = new TestableConfigurationSetting();

			// Assert
			sut.Value.Should().BeNull();
		}
	}
}
