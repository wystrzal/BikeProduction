using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Application.Commands.BaseCommand
{
    public abstract class PartIdWithReferenceCommand : PartIdCommand
    {
        [Required]
        public string Reference { get; set; }

        public PartIdWithReferenceCommand()
        {
        }

        public PartIdWithReferenceCommand(int partId, string reference) : base(partId)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                throw new ArgumentNullException("Reference");
            }

            Reference = reference;
        }
    }
}
