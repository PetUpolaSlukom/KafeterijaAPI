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
    public class EfGetCartItemsQuery : EfUseCase, IGetCartItemQuery
    {
        public EfGetCartItemsQuery(Context context) : base(context)
        {
        }

        public int Id => 18;

        public string Name => "Get items in cart";

        public PagedResponse<CartItemDto> Execute(SearchKeyword search)
        {
            throw new NotImplementedException();
        }
    }
}
