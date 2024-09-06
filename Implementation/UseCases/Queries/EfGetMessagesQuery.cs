using Application.DTO;
using Application.UseCases.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetMessagesQuery : EfUseCase, IGetMessageQuery
    {
        public EfGetMessagesQuery(Context context) : base(context)
        {
        }
        public int Id => 16;

        public string Name => "Get messages";

        public PagedResponse<MessageDto> Execute(CategorySearch search)
        {
            var query = Context.Messages.Where(x => x.IsActive == true).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.FullName.Contains(search.Keyword) || x.Email.Contains(search.Keyword));
            }


            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //16 PerPage = 5, Page = 2

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<MessageDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new MessageDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Text = x.Text,
                    Email = x.Email
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
