using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace JsonMask.NET.AspNetCore.Mvc.Controller.Filter.Tests
{
  [TestFixture]
  public class ServiceExtensionsTests
  {
    [TestCase(ServiceLifetime.Singleton)]
    [TestCase(ServiceLifetime.Scoped)]
    [TestCase(ServiceLifetime.Transient)]
    public void AddJsonMasking_RegistersServicesWithCorrectLifetime(ServiceLifetime lifetime)
    {
      // Arrange
      var services = new ServiceCollection();

      // Act
      services.AddJsonMask(lifetime);

      // Assert
      // Assert for IMaskerService
      var maskerServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IMaskerService));
      Assert.That(maskerServiceDescriptor, Is.Not.Null, "IMaskerService is not registered.");
      Assert.That(maskerServiceDescriptor.Lifetime, Is.EqualTo(lifetime), "IMaskerService has incorrect lifetime.");

      // Assert for JsonMaskedAsyncResultFilter
      var serviceProvider = services.BuildServiceProvider();
      var options = serviceProvider.GetService<IOptions<MvcOptions>>();
      Assert.That(options, Is.Not.Null);
      var filter = options.Value.Filters.Where(f =>
      {
        return f != null && f.GetType() == typeof(TypeFilterAttribute) && ((TypeFilterAttribute)f).ImplementationType == typeof(JsonMaskedAsyncResultFilter);
      }).FirstOrDefault();
      Assert.That(filter, Is.Not.Null, "JsonMaskedAsyncResultFilter is not registered.");
    }
  }
}
