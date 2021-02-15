using Lucene.Net.Documents;
using Lucene.Net.Index;
using Piranha.Models;

namespace Piranha.Lucene.Search.Mappers
{
    public interface ILuceneMapper
    {
        Document MapPage(PageBase page, string body);
        string MapPageBody(PageBase page);
        Term MapPageDeleteTerm(PageBase page);

        Document MapPost(PostBase post, string body);

        string MapPostBody(PostBase post);
        Term MapPostDeleteTerm(PostBase post);
    }
}