# Factory Pattern Implementation

## Features

- **Generic Factory**
  - Create and cache instances that implement an interface
- **Thread-Safe**
- **Dependency Injection Support**
- **Lots of examples as Tests**
- **Flexible Resolution**
  Creates objects based on
  - Generic type argument
  - Runtime type
  - By type name (or simply the name of the class)
    and a type in implementations assembly

## Example

### The Interface

#### IService.cs

```csharp
public interface INotificationService
{
    void Notify();
}
```

### The Implementations

#### EmailService.cs

```csharp
public class EmailService : INotificationService
{
    public void Notify() => Console.WriteLine("Sending Email...");
}
```

#### SmsService.cs

```csharp
public class SmsService : INotificationService
{
    public void Notify() => Console.WriteLine("Sending SMS...");
}
```

#### Usage

```csharp
public class DemoService(IFactory<INotificationService> factory)
{

  public void Demo(){

    var email = factory.GetOrAddInstance<EmailService>();
    email.Notify();

    var sms = factory.GetOrAddInstance(typeof(SmsService));
    sms.Notify();

    // Here EmailService is used to get the Assembly with the implemenation types
    var smsByName = factory.GetOrAddInstance<EmailService>("SmsService");
    smsByName.Notify();
  }
}
```

### A more realistic scenario

#### appsettings.json

````csharp
{
  "RealWorldOptions":{

    "CodeToServiceMap":{
      "1":"EmailNotificationService",
      "2":"PushNotificationService",
      "3":"SmsNotificationService",
    }
  }
}

#### RealWorldOptions.cs

public class RealWorldOptions {
  public Dictionary<string,string> CodeToServiceMap { get; set; }
}

#### RealWorldService.cs

```csharp
public class RealWorldService
{
  private readonly Dictionary<string,string> _codeToServiceCache = new();
  private readonly IFactory<INotificationService> _factory;
  public RealWorldService(IConfiguration configuration,IFactory<INotificationService> factory){
    _factory = factory;
    _codeToServiceCache = ConfigurationBinder.Get<RealWorldOptions>(configuration.GetRequiredSection(nameof(RealWorldOptions)))!.CodeToServiceMap;
  }

  public void DemoByCode(string code)
  {
    if(_codeToServiceCache.TryGetValue(code,out var serviceName)){
      var notificationService = _factory.GetOrAddInstance<EmailNotificationService>(serviceName);
      notificationService.Notify();
    }
  }
}
````
