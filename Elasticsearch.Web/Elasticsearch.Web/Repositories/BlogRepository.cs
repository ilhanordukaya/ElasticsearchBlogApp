using Elastic.Clients.Elasticsearch;
using Elasticsearch.Web.Models;

namespace Elasticsearch.Web.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        private const string IndexName = "blog";
        public BlogRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<Blog> SaveAsync(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;

            var response= await _elasticsearchClient.IndexAsync(newBlog,x=>x.Index(IndexName));

            if (!response.IsValidResponse) return null;

            newBlog.Id=response.Id;
            return newBlog;

        }
    }
}
