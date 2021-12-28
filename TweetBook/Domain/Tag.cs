using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Domain
{
    public class Tag
    {
        [Key]
        public string TagName { get; set; }
        public string CreatorId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual  List<PostTag> Tags{ get; set; }
    }
}
