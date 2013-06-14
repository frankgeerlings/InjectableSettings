Injectable Settings for .NET
=
This is a small library for fetching configuration settings with your favourite dependency injection container. If your class depends on values stored in App.config or Web.config, you'll want a thin abstraction layer to allow for easier testing without writing a lot of repetitive code.

InjectableSettings is a thin conventions-based wrapper for AppSettings key value pairs that allows for them to be injected into your class with NInject, AutoFac, Unity, Castle Windsor, or whatever IoC framework you like.

Usage
-
You simply derive a type from ConfigurationSetting. As long as you make sure the name of your class ends with ConfigurationSetting, things will work:

    public class ExampleConfigurationSetting : ConfigurationSetting {}

Now put an app setting in App.config as follows:

    <appSettings>
       <add key="Example" value="foo" />
    </appSettings>

Now, when you need access to the example setting, all you need to do is new up an `ExampleConfigurationSetting`, or have it handed to you by the DI container:

    public class ExampleThatNeedsAnExampleSetting
    {
      private readonly string example;
    
      // Constructor injection used here:
      public ExampleThatNeedsAnExampleSetting(ExampleConfigurationSetting example)
      {
        this.example = example.Value;
      }
    
      // Now go use your foo!
    }
 
Simple unit testing
-
Let's unit test the example used above. We can simply new up the configuration setting with the exact value we want, no need for complicated mocking or stubbing or other pre-wiring:

    [Test]
    public void ExampleWorksWithBar()
    {
      // Arrange
      var example = new ExampleConfigurationSetting { Value = "bar" };
      var sut = new ExampleThatNeedsExampleSetting(example);
      
      // Act & assert left as an exercise to the reader.
    }

Default values
-
By default, if no other default is specified, the configuration setting will have  a `null` value when nothing is specified in `App.config`. You can supply a default value as follows:

    public class ExampleConfigurationSetting : ConfigurationSetting
    {
      public ExampleConfigurationSetting : base("bar") {}
    }

FAQ
-
A **F**ew **A**nticipated **Q**uestions:

*   **Can I fetch connection strings and other stuff from App.config too?**

     No, I didn't need that. I accept pull requests though.

*   **These are all strings, I want to read integers, Guids, Urls** (*[mutatis mutandis]([http://en.wikipedia.org/wiki/Mutatis_mutandis](http://en.wikipedia.org/wiki/Mutatis_mutandis))*) **instead.**

    That would be useful. I accept pull requests.

*   **My settings are not in Web.config or App.config, they are some place else. Do you support that?**

    No, and I don't think I will. It would complicate what I think should remain a trivially thin layer.

Thanks for reading all the way up to here.