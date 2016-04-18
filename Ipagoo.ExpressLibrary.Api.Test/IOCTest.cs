using NUnit.Framework;
using Autofac;
using Ipagoo.ExpressLibrary.Api.Test.Config;
using Ipagoo.ExpressLibrary.Api.Test.Helper;
using Ipagoo.ExpressLibrary.Api.Controllers;

namespace Ipagoo.ExpressLibrary.Api.Test
{
    //As a software developer
    //I want a simple test that will only do one thing
    //So that when I test the application and almost all the tests fail, I can use this test as a first port of call
    [TestFixture]
    public class IOCTest
    {
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = AutoFacInitialiser.AutoFacSetup();
        }

        //Given:  I have a controller and a basic precheck
        //When:   I call it
        //Then:   It should return true
        [Test]
        public void ShouldAlwaysPassTest()
        {
            using (var lifetime = _container.BeginLifetimeScope())
            {
                //Given
                var controller = APIControllerTestHelper.ResolveController<AmIHereController>(lifetime);

                //When
                var result = controller.GetIAmHere();

                //Then
                Assert.IsTrue(result, "Could not find the controller.");
            }
        }
    }
}
