using System.Collections.Generic;

namespace BlogForPortfolioWebsite.Models.Comments
{
    public class MainComment: Comment
    {
        public List<SubComment> Subcomments { get; set; }
    }
}