using System;
using System.Web;
using Moq;

namespace MillionaireGame.UnitTests.Helpers
{
    public static class MockHttpSession
    {
        public static HttpContextBase FakeHttpContext()
        {
            var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);

            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s.SessionID).Returns(Guid.NewGuid().ToString());

            var context = new Mock<HttpContextBase>();
            context.SetupGet(c => c.Request).Returns(request.Object);
            context.SetupGet(c => c.Response).Returns(response.Object);
            context.SetupGet(c => c.Server).Returns(server.Object);
            context.SetupGet(c => c.Session).Returns(session.Object);

            return context.Object;
        }
    }
}
