using Lucene.Net.Index;
using Lucene.Net.Store;
using Piranha.Lucene.Search.Options;

namespace Piranha.Lucene.Search.Actors.Commands
{
    public class DeleteDocumentCommand : ILuceneActorCommand
    {
        public DeleteDocumentCommand(LuceneOptions options, Term term)
        {
            Options = options;
            Term = term;
        }

        public LuceneOptions Options { get; }
        public Term Term { get; }

        public void Run()
        {
            using var dir = FSDirectory.Open(Options.IndexPath);
            using var writer = new IndexWriter(dir, Options.IndexConfig);
            writer.DeleteDocuments(Term);
            writer.Commit();
        }
    }
}