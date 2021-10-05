﻿using System.Collections.Generic;

namespace Mango.Services.ShoppingCartApi.DTO
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { set; get; }

        public IEnumerable<CardDetailDto> CardDetails { set; get; }
    }
}