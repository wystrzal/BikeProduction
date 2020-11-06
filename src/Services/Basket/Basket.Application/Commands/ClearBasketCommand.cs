using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Basket.Application.Commands
{
    public class ClearBasketCommand : BaseCommand
    {
        public ClearBasketCommand(string userId) : base(userId)
        {
        }
    }
}
