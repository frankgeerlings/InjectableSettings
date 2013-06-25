namespace InjectableSettings
{
	using System;
	using System.Configuration;

	// No generic type parameter means we'll default to string
	public abstract class ConfigurationSetting : ConfigurationSetting<string> { }

	public abstract class ConfigurationSetting<T>
	{
		public T Value { get; set; }

		protected ConfigurationSetting() : this(default(T)) { }

		protected ConfigurationSetting(T defaultValue)
		{
			var key = GetAppSettingsKeyNameFromTypeName();
			var value = ConfigurationManager.AppSettings[key];

			this.Value = value == null ? defaultValue : (T)Convert.ChangeType(value, typeof(T));
		}

		private string GetAppSettingsKeyNameFromTypeName()
		{
			var key = this.GetType().Name.Replace("ConfigurationSetting", "");
			if (key == this.GetType().Name)
			{
				throw new Exception("Configuration setting type name must end with 'ConfigurationSetting'.");
			}

			var prefix = this.GetAppSettingsPrefix();

			return prefix + key;
		}

		private object GetAppSettingsPrefix()
		{
			var declaringType = this.GetType().DeclaringType;

			if (declaringType.Name.EndsWith("Settings"))
			{
				return declaringType.Name.Replace("Settings", ".");
			}

			return string.Empty;
		}
	}
}
