using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
       Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid PostId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post postTopUpdate);
        Task<bool> DeletePostAsync(Guid PostId);
        Task<bool> UserOwnsPostAsync(Guid postId, string v);
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByNameAsync(string tagName);
        Task AddNewTag(Post post);
        Task<bool> CreateTagAsync(Tag tag);

        Task<bool> UpdateTagAsync(Tag tagToUpdate);
        Task<bool> DeleteTagAsync(string tagName);
    }
}
