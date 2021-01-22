using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.LogicLayer.Specifications
{
    public class IsCd : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.Cd;
    }

    public class IsDvd : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.Dvd;
    }

    public class IsBluray : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.BluRay;
    }

    public class IsVhs : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.Vhs;
    }

    public class IsVinyl : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.Vinyl;
    }

    public class IsOther : ISpecification<MediaItem>
    {
        public bool IsSatisfiedBy(MediaItem candidate) => candidate.type == MediaType.Other;
    }

    public class RunsForLongerThan : ISpecification<MediaItem>
    {
        public int RunningTime { get; set; }

        public RunsForLongerThan(int runningTime)
        {
            RunningTime = runningTime;
        }

        public bool IsSatisfiedBy(MediaItem candidate) => candidate.runningTime >= RunningTime;
    }
}
