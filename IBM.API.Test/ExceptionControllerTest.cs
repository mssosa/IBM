using IBM.API.Controllers;
using System;
using Xunit;

namespace IBM.API.Test
{
    public class ExceptionControllerTest
    {
        private readonly TestErrorController controller;

        public ExceptionControllerTest()
        {
            controller = new TestErrorController();
        }
        [Fact]
        public void ShouldTrhowControlledExcpetion()
        {
            Assert.Throws<Exception>(() => controller.Get());
        }
    }
}
