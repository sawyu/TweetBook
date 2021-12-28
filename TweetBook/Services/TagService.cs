using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _dataContext;
        private readonly List<Tag> tags;
        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
            tags = new List<Tag>();
            tags.Add(new Tag { CreatorId = Guid.NewGuid().ToString(), TagName = "Tag 1", CreatedOn = DateTime.Now });
            tags.Add(new Tag { CreatorId = Guid.NewGuid().ToString(), TagName = "Tag 2", CreatedOn = DateTime.Now });

        }
        public List<Tag> GetAllTagsAsync()
        
        {
            return  tags;
        }
        public Tag GetTagByNameAysnc(string tagName)
        {
            return  tags.SingleOrDefault(x => x.TagName == tagName);
        }
        public Tag CreateTagAsync(Tag tag)
        {
             tags.Add(tag);
            return tag;
        }
        public bool DeleteTagAsync(string tagName)
        {
            var tag = GetTagByNameAysnc(tagName);
            if (tag == null) return false;
            tags.Remove(tag);
         
            return true;
        }

       

    
    }
}
