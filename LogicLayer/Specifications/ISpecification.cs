using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.LogicLayer.Specifications
{
    public interface ISpecification<T> where T : Entity
    {
        bool IsSatisfiedBy(T candidate);
    }

    public class AndSpecification<T> : ISpecification<T> where T : Entity
    {
        public ISpecification<T> LeftSpec { get; set; }
        public ISpecification<T> RightSpec { get; set; }

        public AndSpecification(ISpecification<T> leftSpec, ISpecification<T> rightSpec)
        {
            this.LeftSpec = leftSpec;
            this.RightSpec = rightSpec;
        }

        public bool IsSatisfiedBy(T candidate) => LeftSpec.IsSatisfiedBy(candidate) && RightSpec.IsSatisfiedBy(candidate);
    }

    public class OrSpecification<T> : ISpecification<T> where T : Entity
    {
        public ISpecification<T> LeftSpec { get; set; }
        public ISpecification<T> RightSpec { get; set; }

        public OrSpecification(ISpecification<T> leftSpec, ISpecification<T> rightSpec)
        {
            this.LeftSpec = leftSpec;
            this.RightSpec = rightSpec;
        }

        public bool IsSatisfiedBy(T candidate) => LeftSpec.IsSatisfiedBy(candidate) || RightSpec.IsSatisfiedBy(candidate);
    }

    public class NotSpecification<T> : ISpecification<T> where T : Entity
    {
        public ISpecification<T> Spec { get; set; }

        public NotSpecification(ISpecification<T> spec)
        {
            this.Spec = spec;
        }

        public bool IsSatisfiedBy(T candidate) => Spec.IsSatisfiedBy(candidate);
    }
}