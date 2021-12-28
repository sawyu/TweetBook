using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class CosmosPostService : IPostService
    {
        public Task AddNewTag(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(Guid PostId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTagAsync(string tagName)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(Guid PostId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetTagByNameAsync(string tagName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(Post postTopUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTagAsync(Tag tagToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserOwnsPostAsync(Guid postId, string v)
        {
            throw new NotImplementedException();
        }

        Task<List<Tag>> IPostService.GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
