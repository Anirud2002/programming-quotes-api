using ProgrammingQuotesApi.Models;
using ProgrammingQuotesApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingQuotesApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IQuoteService _quoteService;
        readonly Dictionary<string, Author> Authors = new();

        public AuthorService(IQuoteService quoteService)
        {
            _quoteService = quoteService;
            foreach (Quote q in _quoteService.GetAll())
            {
                if (Authors.ContainsKey(q.Author)) {
                    Authors[q.Author].QuoteCount++;  
                }
                else {  
                    Authors.Add(q.Author, new Author()
                    {
                        Name = q.Author,
                        WikiUrl = $"https://en.wikipedia.org/wiki/{q.Author}",
                        QuoteCount = 1
                    });  
                }
            }
        }

        public List<Author> GetAuthors() => Authors.Values.OrderByDescending(author => author.QuoteCount).ToList();

        public Author GetAuthorDetails(string authorName)
        {
            IEnumerable<Quote> authorQuotes = _quoteService.GetByAuthor(authorName);
            if (!authorQuotes.Any()) {
              return null;
            }

            Author authorDetails = new Author()
            {
                Name = authorName,
                WikiUrl = $"https://en.wikipedia.org/wiki/{authorName}",
                QuoteCount = authorQuotes.Count(),
                Quotes = authorQuotes
            };
            return authorDetails;
        }

    }
}