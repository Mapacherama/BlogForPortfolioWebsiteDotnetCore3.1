using System.Collections.Generic;

namespace BlogForPortfolioWebsite.Models.Comments
{
    public class Maincomment: Comment
    {
        public List<SubComment> Subcomments { get; set; }
    }
}