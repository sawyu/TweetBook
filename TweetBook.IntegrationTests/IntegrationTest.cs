
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DataContext));
                    services.AddDbContext<DataContext>(Options=> { Options.UseInMemoryDatabase("TestDb"); });
                });
            });
            TestClient = appFactory.CreateClient();
        }
        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }
        protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
          var response = await TestClient.PostAsJsonAsync(ApiRoutes.Posts.Create,request);
            return await response.Content.ReadAsAsync<PostResponse>();
        }
         private async Task<string> GetJwtAsync()
        {
            var response =await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest { 
                Email = "test@Inegration.com",
                Password = "SomePass1234!"
            });
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token; 
        } 
    }

}
