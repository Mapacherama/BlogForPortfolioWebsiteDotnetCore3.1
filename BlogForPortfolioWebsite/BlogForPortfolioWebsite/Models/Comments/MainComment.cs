using System.Collections.Generic;

namespace BlogForPortfolioWebsite.Models.Comments
{
    public class MainComment: Comment
    {
        public List<SubComment> SubComments { get; set; }
    }
}