namespace InjectableSettings.Test
{
	using System.Configuration;

	using FluentAssertions;

	using NUnit.Framework;

	using Ploeh.AutoFixture;

	[TestFixture]
	public class TestConfigurationSetting
	{
		public class TestableConfigurationSetting : ConfigurationSetting<string> { }
		
		public class IntegerConfigurationSetting : ConfigurationSetting<int> { }

		public class SettingWithDefaultConfigurationSetting : ConfigurationSetting<string>
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

		[Test]
		public void ConfigurationSettingsCanBeIntegers()
		{
			// Arrange
			var fixture = new Fixture();
			var integer = fixture.Create<int>();

			ConfigurationManager.AppSettings.Set("Integer", integer.ToString());

			// Act
			var sut = new IntegerConfigurationSetting();

			// Assert
			sut.Value.Should().Be(integer);
		}
	}
}
