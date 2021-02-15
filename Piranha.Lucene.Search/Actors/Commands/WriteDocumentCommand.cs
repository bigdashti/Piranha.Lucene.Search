using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Piranha.Lucene.Search.Options;

namespace Piranha.Lucene.Search.Actors.Commands
{
    public class WriteDocumentCommand : ILuceneActorCommand
    {
        public WriteDocumentCommand(
            LuceneOptions options,
            Document document,
            bool triggerMerge = false,
            bool applyAllDeletes = false)
        {
            Options = options;
            Document = document;
            TriggerMerge = triggerMerge;
            ApplyAllDeletes = applyAllDeletes;
        }

        public LuceneOptions Options { get; }
        public Document Document { get; }
        public bool TriggerMerge { get; }
        public bool ApplyAllDeletes { get; }

        public void Run()
        {
            using var dir = FSDirectory.Open(Options.IndexPath);
            using var writer = new IndexWriter(dir, Options.IndexConfig);
            writer.AddDocument(Document);
            writer.Flush(TriggerMerge, ApplyAllDeletes);
        }
    }
}