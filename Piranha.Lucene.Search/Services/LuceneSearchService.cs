using System.Threading.Tasks;
using Piranha.Lucene.Search.Actors;
using Piranha.Lucene.Search.Mappers;
using Piranha.Models;

namespace Piranha.Lucene.Search.Services
{
    /// <summary>
    ///     The identity module.
    /// </summary>
    public class LuceneSearchService : ISearch
    {
        private readonly ILuceneActorWrapper _actor;
        private readonly ILuceneMapper _mapper;

        public LuceneSearchService(ILuceneMapper mapper, ILuceneActorWrapper actor)
        {
            _mapper = mapper;
            _actor = actor;
        }


        /// <summary>
        ///     Creates or updates the searchable content for the
        ///     given page.
        /// </summary>
        /// <param name="page">The page</param>
        public async Task SavePageAsync(PageBase page)
        {
            var body = _mapper.MapPageBody(page);
            var doc = _mapper.MapPage(page, body);
            _actor.WriteDocument(doc);
        }

        /// <summary>
        ///     Deletes the given page from the search index.
        /// </summary>
        /// <param name="page">The page to delete</param>
        public async Task DeletePageAsync(PageBase page)
        {
            var term = _mapper.MapPageDeleteTerm(page);
            _actor.DeleteDocument(term);
        }

        /// <summary>
        ///     Creates or updates the searchable content for the
        ///     given post.
        /// </summary>
        /// <param name="post">The post</param>
        public async Task SavePostAsync(PostBase post)
        {
            var body = _mapper.MapPostBody(post);
            var doc = _mapper.MapPost(post, body);
            _actor.WriteDocument(doc);
        }

        /// <summary>
        ///     Deletes the given post from the search index.
        /// </summary>
        /// <param name="post">The post to delete</param>
        public async Task DeletePostAsync(PostBase post)
        {
            var term = _mapper.MapPostDeleteTerm(post);
            _actor.DeleteDocument(term);
        }
    }
}