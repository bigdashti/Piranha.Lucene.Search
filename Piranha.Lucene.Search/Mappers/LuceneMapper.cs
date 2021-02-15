using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Piranha.Extend;
using Piranha.Models;
using Field = Lucene.Net.Documents.Field;

namespace Piranha.Lucene.Search.Mappers
{
    public class LuceneMapper : ILuceneMapper
    {
        public Document MapPage(PageBase page, string body)
        {
            var document = new Document
            {
                new StringField(
                    "contentId",
                    page.Id.ToString(),
                    Field.Store.YES),
                new TextField(
                    "slug",
                    page.Slug,
                    Field.Store.YES),
                new StringField(
                    "contentType",
                    "page",
                    Field.Store.YES),
                new SortedDocValuesField(
                    "title",
                    new BytesRef(page.Title)),
                new StringField(
                    "title",
                    page.Title,
                    Field.Store.YES),
                new TextField(
                    "body",
                    body,
                    Field.Store.YES)
            };

            return document;
        }

        public string MapPageBody(PageBase page)
        {
            var bodyBuilder = new StringBuilder();
            foreach (var block in page.Blocks)
                if (block is ISearchable searchableBlock)
                    bodyBuilder.AppendLine(searchableBlock.GetIndexedContent());

            var body = bodyBuilder.ToString();

            var cleanHtml = new Regex("<[^>]*(>|$)");
            var cleanSpaces = new Regex("[\\s\\r\\n]+");

            var cleanedHtml = cleanHtml.Replace(body, " ");
            var cleanedBody = cleanSpaces.Replace(cleanedHtml, " ")
                .Trim();

            return cleanedBody;
        }

        public Term MapPageDeleteTerm(PageBase page)
        {
            return new("contentId", page.Id.ToString());
        }

        public Document MapPost(PostBase post, string body)
        {
            var document = new Document
            {
                new StringField(
                    "contentId",
                    post.Id.ToString(),
                    Field.Store.YES),
                new TextField(
                    "slug",
                    post.Slug,
                    Field.Store.YES),
                new StringField(
                    "contentType",
                    "page",
                    Field.Store.YES),
                new SortedDocValuesField(
                    "title",
                    new BytesRef(post.Title)),
                new StringField(
                    "title",
                    post.Title,
                    Field.Store.YES),
                new SortedDocValuesField(
                    "category",
                    new BytesRef(post.Category.Title)),
                new StringField(
                    "category",
                    post.Category.Title,
                    Field.Store.YES),
                new TextField(
                    "body",
                    body,
                    Field.Store.YES)
            };

            foreach (var tag in post.Tags.Select(t => t.Title))
                document.Add(new TextField("tag", tag, Field.Store.YES));

            // TODO: Add filterability to category
            // TODO: Add filterability to tags

            return document;
        }

        public string MapPostBody(PostBase post)
        {
            var bodyBuilder = new StringBuilder();
            foreach (var block in post.Blocks)
                if (block is ISearchable searchableBlock)
                    bodyBuilder.AppendLine(searchableBlock.GetIndexedContent());

            var body = bodyBuilder.ToString();


            var cleanHtml = new Regex("<[^>]*(>|$)");
            var cleanSpaces = new Regex("[\\s\\r\\n]+");

            var cleanedHtml = cleanHtml.Replace(body, " ");
            var cleanedBody = cleanSpaces.Replace(cleanedHtml, " ")
                .Trim();

            return cleanedBody;
        }

        public Term MapPostDeleteTerm(PostBase post)
        {
            return new("contentId", post.Id.ToString());
        }
    }
}