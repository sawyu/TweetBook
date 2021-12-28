using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {

        private readonly DataContext _dataContext;
        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.Posts
                .Include(x=>x.Tags).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid PostId)
        {
            return await _dataContext.Posts.Include(x => x.Tags)
                .SingleOrDefaultAsync(x =>  x.Id == PostId);
        }
        public async Task<bool> CreatePostAsync(Post post) {
            /*  await _dataContext.Posts.AddAsync(post);
              var created = await _dataContext.SaveChangesAsync();
              return created > 0;*/
            post.Tags?.ForEach(x => x.TagName = x.TagName.ToLower());
            await AddNewTag(post);
            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePostAsync(Post postTopUpdate)
        {
            /* _dataContext.Posts.Update(postTopUpdate);
             var updated = await _dataContext.SaveChangesAsync();
             return updated >0;*/
            postTopUpdate.Tags?.ForEach(x => x.TagName = x.TagName.ToLower());
             _dataContext.Posts.Update(postTopUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> DeletePostAsync(Guid postId)
        {
            /* var post = await GetPostByIdAsync(postId);
            _dataContext.Posts.Remove(post);
            var deleted =  await _dataContext.SaveChangesAsync();
             return deleted > 0;*/
            var post = await GetPostByIdAsync(postId);
            if (post == null) return false;
            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post =await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            if(post == null)  return false;
          
            if (post.UserId != userId)  return false;
            
            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.AsNoTracking().ToListAsync();
        }
        public async Task<Tag> GetTagByNameAsync(string name)
        {
            return await _dataContext.Tags.SingleOrDefaultAsync(x => x.TagName == name);
        }
        public async Task AddNewTag(Post post)
        {
            foreach(var tags in post.Tags)
            {
                var tag = new Tag
                {
                    TagName = tags.TagName,
                    CreatorId = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    CreatedBy = post.Name
                };
                await _dataContext.Tags.AddAsync(tag);
                await _dataContext.SaveChangesAsync();
            }
        }
        public async Task<bool> CreateTagAsync(Tag tag)
        {
            await _dataContext.Tags.AddAsync(tag);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<bool> UpdateTagAsync(Tag tagToUpdate)
        {
             _dataContext.Tags.Update(tagToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var tag = await GetTagByNameAsync(tagName);
            if (tag == null) return false;
            _dataContext.Tags.Remove(tag);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
        public Task<List<Tag>> GetTagsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
