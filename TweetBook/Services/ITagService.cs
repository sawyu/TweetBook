using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface  ITagService
    {
        List<Tag> GetAllTagsAsync();
        Tag GetTagByNameAysnc(string tagName);
        Tag CreateTagAsync(Tag tag);
       bool DeleteTagAsync(string tagName);
      
    }
}
