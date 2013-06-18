namespace InjectableSettings.Test
{
	using System.Configuration;
	using System.Globalization;

	using FluentAssertions;

	using NUnit.Framework;

	using Ploeh.AutoFixture;

	[TestFixture]
	public class TestConfigurationSetting
	{
		public class TestableConfigurationSetting : ConfigurationSetting<string> { }
		
		public class IntegerConfigurationSetting : ConfigurationSetting<int> { }

		public class StringConfigurationSetting : ConfigurationSetting { }

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

			ConfigurationManager.AppSettings.Set("Integer", integer.ToString(CultureInfo.InvariantCulture));

			// Act
			var sut = new IntegerConfigurationSetting();

			// Assert
			sut.Value.Should().Be(integer);
		}


		[Test]
		public void ConfigurationSettingsAreStringsByDefault()
		{
			// Arrange
			var fixture = new Fixture();
			var value = fixture.Create<string>();

			ConfigurationManager.AppSettings.Set("String", value);

			// Act

			//// This is a type that derives from ConfigurationSetting without
			//// a generic type specifier, so you'll get a string, which is a string by default.

			var sut = new StringConfigurationSetting();

			// Assert
			sut.Value.Should().Be(value);
		}
	}
}
