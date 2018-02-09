using Moq;
using NUnit.Framework;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using SCMSClient.ViewModel;
using System.Security;

namespace SCMSClientTests.ViewModels
{
    [TestFixture]
    public class LoginVM
    {
        [Test]
        public void Login_LoginPageIsNull_ThrowsException()
        {
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(() => null);

            var vmToTest = new LoginViewModel(authService.Object);

            vmToTest.LoginCommand.Execute(null);

            authService.Verify(s => s.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Login_UsernameIsNull_DosentExecute()
        {
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<User>());

            var passwordHandler = new Mock<IHavePassword>();
            passwordHandler.Setup(s => s.UserPassword).Returns(() =>
            {
                var secure = new SecureString();
                foreach (char c in "pass")
                {
                    secure.AppendChar(c);
                }
                return secure;
            });

            var vmToTest = new LoginViewModel(authService.Object)
            {
                Username = ""
            };

            vmToTest.LoginCommand.Execute(passwordHandler.Object);

            authService.Verify(s => s.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Login_CorrectParameters_ReturnsAUser()
        {
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<User>());

            var passwordHandler = new Mock<IHavePassword>();
            passwordHandler.Setup(s => s.UserPassword).Returns(() =>
            {
                var secure = new SecureString();
                foreach (char c in "pass")
                {
                    secure.AppendChar(c);
                }
                return secure;
            });

            var username = "user";
            var password = passwordHandler.Object.UserPassword.Unsecure();

            var vmToTest = new LoginViewModel(authService.Object)
            {
                Username = "user"
            };

            vmToTest.LoginCommand.Execute(passwordHandler.Object);

            authService.Verify(s => s.Login(username, password), Times.Once);
        }

        [Test]
        public void Login_PasswordIsNull_DosentExecute()
        {
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<User>());

            var passwordHandler = new Mock<IHavePassword>();

            var vmToTest = new LoginViewModel(authService.Object) { Username = "user" };

            vmToTest.LoginCommand.Execute(passwordHandler.Object);

            authService.Verify(s => s.Login(It.IsAny<string>(), string.Empty), Times.Never);
        }
    }
}
