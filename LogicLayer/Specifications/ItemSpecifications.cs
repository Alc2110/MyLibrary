using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.LogicLayer.Specifications
{
    public class HasTags : ISpecification<Item>
    {
        public IEnumerable<string> Tags { get; set; }

        public HasTags(IEnumerable<string> tags)
        {
            this.Tags = tags;
        }

        public bool IsSatisfiedBy(Item candidate)
        {
            foreach (var xTag in candidate.tags)
            {
                foreach (var yTag in this.Tags)
                    if (xTag.name == yTag)
                        return true;
            }

            return false;
        }
    }
}
