using TaskApp.Web.Controllers;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace TaskApp.Web.Tests.Controllers;

public class HomeController_Tests : TaskAppWebTestBase
{
    [Fact]
    public async Task Index_Test()
    {
        //Act
        var response = await GetResponseAsStringAsync(
            GetUrl<HomeController>(nameof(HomeController.Index))
        );

        //Assert
        response.ShouldNotBeNullOrEmpty();
    }
}
