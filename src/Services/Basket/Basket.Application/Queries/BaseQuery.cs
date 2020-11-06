using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Basket.Application.Queries
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
        [Required]
        public string UserId { get; set; }

        public BaseQuery()
        {
        }

        public BaseQuery(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("UserId");
            }

            UserId = userId;
        }
    }
}
