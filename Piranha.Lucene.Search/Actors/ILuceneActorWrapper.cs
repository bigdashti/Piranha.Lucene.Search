using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Piranha.Lucene.Search.Actors
{
    public interface ILuceneActorWrapper
    {
        public void WriteDocument(Document document, bool triggerMerge = false, bool applyAllDeletes = false);
        public void DeleteDocument(Term term);
    }
}