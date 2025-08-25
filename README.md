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
And can use it like this
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
