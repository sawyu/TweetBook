using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Contracts;
using System.Net;
using TweetBook.Domain;
using System.Net.Http;
using Xunit;
using TweetBook.Contracts.V1.Requests;

namespace TweetBook.IntegrationTests
{
    public class PostControllerTests : IntegrationTest
    {
        [Fact ]
        public async Task GetAll_WithoutAnyPosts_ReturnEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }
        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            //Arrange
            await AuthenticateAsync();
           var createdPost = await CreatePostAsync(new CreatePostRequest { Name = "Test post" });
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}",createdPost.Id.ToString()));
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Name.Should().Be("Test post");
        }
        }
}
