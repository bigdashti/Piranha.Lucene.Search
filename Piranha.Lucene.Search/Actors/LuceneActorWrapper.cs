using Lucene.Net.Documents;
using Lucene.Net.Index;
using Microsoft.Extensions.Options;
using Piranha.Lucene.Search.Actors.Commands;
using Piranha.Lucene.Search.Options;

namespace Piranha.Lucene.Search.Actors
{
    public class LuceneActorWrapper : ILuceneActorWrapper
    {
        private readonly ILuceneActorAddress _address;
        private readonly LuceneOptions _options;

        public LuceneActorWrapper(IOptions<LuceneOptions> options, ILuceneActorAddress address)
        {
            _options = options.Value;
            _address = address;
        }

        public void WriteDocument(Document document, bool triggerMerge = false, bool applyAllDeletes = false)
        {
            var command = new WriteDocumentCommand(
                _options,
                document,
                triggerMerge,
                applyAllDeletes);

            var root = _address.ActorSystem.Root;
            root.Send(_address.Pid, command);
        }

        public void DeleteDocument(Term term)
        {
            var command = new DeleteDocumentCommand(
                _options,
                term);

            var root = _address.ActorSystem.Root;
            root.Send(_address.Pid, command);
        }
    }
}