using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Util;

namespace Piranha.Lucene.Search.Options
{
    public class LuceneOptions
    {
        public LuceneVersion Version { get; set; }
        public string IndexPath { get; set; }
        public Analyzer Analyzer { get; set; }
        public IndexWriterConfig IndexConfig { get; set; }
    }
}