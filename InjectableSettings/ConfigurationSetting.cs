namespace InjectableSettings
{
	using System;
	using System.Configuration;

	public class ConfigurationSetting
	{
		public string Value { get; set; }

		protected ConfigurationSetting() : this(null) { }

		protected ConfigurationSetting(string defaultValue)
		{
			var key = GetAppSettingsKeyNameFromTypeName();
			this.Value = ConfigurationManager.AppSettings[key] ?? defaultValue;
		}

		private string GetAppSettingsKeyNameFromTypeName()
		{
			var key = this.GetType().Name.Replace("ConfigurationSetting", "");
			if (key == this.GetType().Name)
			{
				throw new Exception("Configuration setting type name must end with 'ConfigurationSetting'.");
			}
			return key;
		}
	}
}
