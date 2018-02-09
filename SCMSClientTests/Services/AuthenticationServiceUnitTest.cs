using Moq;
using NUnit.Framework;
using SCMSClient.Models;
using SCMSClient.Services.Implementation;
using SCMSClient.Services.Interfaces;
using System;
using System.Collections.Specialized;

namespace SCMSClientTests.Services
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        [Test]
        public void Login_HTTPServiceError_EscalatesError()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => testClass.Login("duba", "duba123"));
        }

        [Test]
        public void Login_UsernameIsNull_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.Login(null, "pass"));
        }

        [Test]
        public void Login_UsernameIsEmpty_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.Login("", "pass"));
        }

        [Test]
        public void Login_PasswordIsNull_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.Login("user", null));
        }

        [Test]
        public void Login_PasswordIsEmpty_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.Login("user", null));
        }

        [Test]
        public void RefreshToken_TokenIsNull_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.RefreshUserLogin(null));
        }

        [Test]
        public void RefreshToken_TokenIsEmpty_ThrowsInvalidOperationException()
        {
            var fakeHTTPService = new Mock<IHTTPService>();

            var testClass = new AuthenticationService(fakeHTTPService.Object);

            fakeHTTPService.Setup(m => m.PostForm<User>(It.IsAny<NameValueCollection>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<InvalidOperationException>(() => testClass.RefreshUserLogin(""));
        }
    }
}
