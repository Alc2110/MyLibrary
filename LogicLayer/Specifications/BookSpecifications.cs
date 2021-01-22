using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.LogicLayer.Specifications
{
    public class IsTitled : ISpecification<Book>
    {
        public string BookTitle { get; set; }

        public bool IsSatisfiedBy(Book candidate) => candidate.title == BookTitle;
    }

    public class TitleMatches : ISpecification<Book>
    {
        public Regex TitlePattern { get; set; }

        public TitleMatches(Regex pattern)
        {
            this.TitlePattern = pattern;
        }

        public bool IsSatisfiedBy(Book candidate) => TitlePattern.IsMatch(candidate.title);
    }

    public class IsPublishedBy : ISpecification<Book>
    {
        public string PublisherName { get; set; }

        public IsPublishedBy(string publisherName)
        {
            this.PublisherName = publisherName;
        }

        public bool IsSatisfiedBy(Book candidate) => candidate.publisher.name == PublisherName;
    }

    public class IsWrittenBy : ISpecification<Book>
    {
        public string AuthorName { get; set; }
        
        public IsWrittenBy(string authorName)
        {
            this.AuthorName = authorName;
        }

        public bool IsSatisfiedBy(Book candidate)
        {
            foreach (var author in candidate.authors)
                if ((author.firstName + " " + author.lastName) == AuthorName)
                    return true;

            return false;
        }
    }

    public class AuthorMatches : ISpecification<Book>
    {
        public Regex AuthorPattern { get; set; }

        public AuthorMatches(Regex authorPattern)
        {
            this.AuthorPattern = authorPattern;
        }

        public bool IsSatisfiedBy(Book candidate)
        {
            foreach (var author in candidate.authors)
                if (AuthorPattern.IsMatch(author.firstName) || AuthorPattern.IsMatch(author.lastName))
                    return true;

            return false;
        }
    }
}
