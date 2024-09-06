﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.CartItems
{
    public interface IUpdateCartItemCommand : ICommand<UpdateCartItemDto>
    {
    }
}
