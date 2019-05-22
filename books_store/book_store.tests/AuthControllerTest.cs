namespace book_store.tests
{
    using books;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Xunit;

    public class AuthControllerTest
    {
        [Theory]
        [InlineData("qwerty", "55555")]
        public void Should_return_token(string username, string password)
        {
            AuthController authController = new AuthController();
            OkObjectResult response = authController.Token(new AuthModel(username, password)) as OkObjectResult;
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("qwerty", "0000")]
        public void Should_return_bad_request(string username, string password)
        {
            AuthController authController = new AuthController();
            BadRequestObjectResult response = authController.Token(new AuthModel(username, password)) as BadRequestObjectResult;
            Assert.NotNull(response);
        }
    }
}