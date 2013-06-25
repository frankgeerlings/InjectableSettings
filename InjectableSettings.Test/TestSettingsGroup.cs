namespace InjectableSettings.Test
{
	using System.Configuration;

	using FluentAssertions;

	using NUnit.Framework;

	using Ploeh.AutoFixture;

	[TestFixture]
	public class TestSettingsGroup
	{
		public class ExampleSettings
		{
			public class PasswordConfigurationSetting : ConfigurationSetting {}
		}

		public static class StaticSettings
		{
			public class PasswordConfigurationSetting : ConfigurationSetting { }
		}

		[Test]
		public void SettingsCanBeNestedInAnyClassOfWhichTheNameEndsWithSettings()
		{
			// Arrange
			var fixture = new Fixture();
			var value = fixture.Create<string>();

			ConfigurationManager.AppSettings.Set("Example.Password", value);

			// Act
			var sut = new ExampleSettings.PasswordConfigurationSetting();

			// Assert
			sut.Value.Should().Be(value);
		}

		[Test]
		public void SettingsCanBeNestedInStaticClasses()
		{
			// Arrange
			var fixture = new Fixture();
			var value = fixture.Create<string>();

			ConfigurationManager.AppSettings.Set("Static.Password", value);

			// Act
			var sut = new StaticSettings.PasswordConfigurationSetting();

			// Assert
			sut.Value.Should().Be(value);
		}

	}
}
