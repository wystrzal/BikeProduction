using Catalog.Application.Commands.BaseCommands;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Application.Commands
{
    public class AddProductCommand : ProductCommand
    {
    }
}
