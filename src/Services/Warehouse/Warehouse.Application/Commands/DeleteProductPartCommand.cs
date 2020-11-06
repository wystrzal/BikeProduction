using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Commands.BaseCommand;

namespace Warehouse.Application.Commands
{
    public class DeleteProductPartCommand : PartIdWithReferenceCommand
    {
        public DeleteProductPartCommand(int partId, string reference) : base(partId, reference)
        {
        }
    }
}