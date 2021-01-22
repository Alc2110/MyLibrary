using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.Model.ObjectModel
{
    public sealed class MediaItemBuilder
    {
        private MediaItem item;

        private MediaItemBuilder(string title, MediaType type)
        {
            this.item = new MediaItem { title = title, type = type };
        }

        private MediaItemBuilder(int id, string title, MediaType type)
            : this(title, type)
        {
            this.item.id = id;
        }

        public static MediaItemBuilder CreateCd(string title)
        {
            return new MediaItemBuilder(title, MediaType.Cd);
        }

        public static MediaItemBuilder CreateDvd(string title)
        {
            return new MediaItemBuilder(title, MediaType.Dvd);
        }

        public static MediaItemBuilder CreateBluray(string title)
        {
            return new MediaItemBuilder(title, MediaType.BluRay);
        }

        public static MediaItemBuilder CreateVhs(string title)
        {
            return new MediaItemBuilder(title, MediaType.Vhs);
        }

        public static MediaItemBuilder CreateVinyl(string title)
        {
            return new MediaItemBuilder(title, MediaType.Vinyl);
        }

        public static MediaItemBuilder CreateMiscMediaItem(string title)
        {
            return new MediaItemBuilder(title, MediaType.Other);
        }

        public MediaItemBuilder Numbered(long number)
        {
            this.item.number = number;
            return this;
        }

        public MediaItemBuilder RunningForMins(int runningTime)
        {
            this.item.runningTime = runningTime;
            return this;
        }

        public MediaItemBuilder ReleasedInYear(int year)
        {
            this.item.releaseYear = year;
            return this;
        }

        public MediaItemBuilder WithTags(IEnumerable<Tag> tags)
        {
            foreach (var t in tags)
                this.item.tags.Add(t);

            return this;
        }

        public MediaItem Get() => this.item;
    }

    public sealed class BookBuilder
    {
        private Book book;

        private BookBuilder(string title, string titleLong)
        {
            this.book = new Book { title = title, titleLong = titleLong };
        }

        private BookBuilder(int id, string title, string titleLong)
            : this(title, titleLong)
        {
            this.book.id = id;
        }

        public static BookBuilder CreateBook(string title, string titleLong)
        {
            return new BookBuilder(title, titleLong);
        }

        public BookBuilder WithIsbn(string isbn)
        {
            switch (isbn.Length)
            {
                case 10:
                    this.book.isbn = isbn;
                    break;
                case 13:
                    this.book.isbn13 = isbn;
                    break;
                default:
                    throw new ArgumentException("ISBN must have 10 or 13 digits.");
            }

            return this;
        }

        public BookBuilder WithDeweyDecimal(double deweyDecimal)
        {
            this.book.deweyDecimal = deweyDecimal;
            return this;
        }

        public BookBuilder InFormat(string format)
        {
            this.book.format = format;
            return this;
        }

        public BookBuilder PublishedIn(string datePublished)
        {
            this.book.datePublished = datePublished;
            return this;
        }

        public BookBuilder Edition(string edition)
        {
            this.book.edition = edition;
            return this;
        }

        public BookBuilder Pages(int pages)
        {
            this.book.pages = pages;
            return this;
        }

        public BookBuilder Sized(string dimensions)
        {
            this.book.dimensions = dimensions;
            return this;
        }

        public BookBuilder WithOverview(string overview)
        {
            this.book.overview = overview;
            return this;
        }

        public BookBuilder WithMsrp(string msrp)
        {
            this.book.msrp = msrp;
            return this;
        }

        public BookBuilder WithExcerpt(string excerpt)
        {
            this.book.excerpt = excerpt;
            return this;
        }

        public BookBuilder WithSynopsys(string synopsys)
        {
            this.book.synopsys = synopsys;
            return this;
        }

        public BookBuilder WrittenBy(Author author)
        {
            this.book.authors.Add(author);
            return this;
        }

        public BookBuilder PublishedBy(Publisher publisher)
        {
            this.book.publisher = publisher;
            return this;
        }

        public BookBuilder WithTags(IEnumerable<Tag> tags)
        {
            foreach (var t in tags)
                this.book.tags.Add(t);

            return this;
        }

        public Book Get() => this.book;
    }
}
