using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyLibraryWinForms.Model.ObjectModel
{
    public abstract class Entity // abstract class to reduce code duplication of id property
    {
        public int id { get; set; }
    }

    public abstract class Item : Entity // abstract class to reduce code duplication of title and image properties
    {
        public Item() { this.tags = new List<Tag>(); }

        private string _title;
        public string title
        {
            get => this._title;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Can't have an empty title.");
                else
                    _title = value;
            }
        }

        public string image { get; set; } // conversion to and from images is handled by the presentation layer (view)
        public string notes { get; set; }

        public ICollection<Tag> tags;

        public string getCommaDelimitedTags()
        {
            var tagsBuilder = new StringBuilder();
            int tagCount = 0;
            foreach (var t in this.tags)
            {
                tagsBuilder.Append(t.name);

                if (tagCount < this.tags.Count - 1)
                    tagsBuilder.Append(", ");

                tagCount++;
            }

            return tagsBuilder.ToString();
        }
    }

    public sealed class Book : Item
    {
        private string _titleLong;
        public string titleLong
        {
            get => this._titleLong;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Can't have an empty title.");
                else
                    _titleLong = value;
            }
        }

        public string isbn { get; set; }
        public string isbn13 { get; set; }

        public string getIsbn()
        {
            if (string.IsNullOrWhiteSpace(isbn) && !(string.IsNullOrWhiteSpace(isbn13)))
            {
                return isbn13;
            }
            else if (string.IsNullOrWhiteSpace(isbn13) && !(string.IsNullOrWhiteSpace(isbn)))
            {
                return isbn;
            }
            else if (string.IsNullOrWhiteSpace(isbn13) && (string.IsNullOrWhiteSpace(isbn)))
            {
                return "";
            }
            else
            {
                return (isbn + "; " + isbn13);
            }
        }

        public double? deweyDecimal { get; set; }
        public string format { get; set; }
        public string datePublished { get; set; }
        public string edition { get; set; }
        public int pages { get; set; }
        public string dimensions { get; set; }
        public string overview { get; set; }

        public string msrp { get; set; }
        public string excerpt { get; set; }
        public string synopsys { get; set; }

        public ICollection<Author> authors { get; set; }
        public Publisher publisher { get; set; }
    }

    public enum MediaType
    {
        Dvd,
        BluRay,
        Cd,
        Vhs,
        Vinyl,
        Other
    }

    public class MediaItem : Item
    {
        public MediaType type { get; set; }
        public long number { get; set; }
        public int runningTime { get; set; }
        public int releaseYear { get; set; }
    }

    public sealed class Author : Entity
    {
        private string _firstName;
        public string firstName
        {
            get => _firstName;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Author must have first name.");
                else
                    _firstName = value;
            }
        }

        private string _lastName;
        public string lastName
        {
            get => _lastName;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Author must have last name.");
                else
                    _lastName = value;
            }
        }

        public string getFullName()
        {
            return (this.firstName + " " + this.lastName);
        }

        public string getFullNameLastNameCommaFirstName()
        {
            return (this.lastName + ", " + this.firstName);
        }

        public ICollection<Book> books { get; set; }
    }

    public sealed class Publisher : Entity
    {
        private string _name;
        public string name
        {
            get => this._name;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Publisher must have a name.");
                else
                    _name = value;
            }
        }

        public ICollection<Book> books { get; set; }
    }

    public sealed class Tag : Entity
    {
        private string _name;
        public string name
        {
            get => this._name;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Tag can't be empty.");   
                }
                else
                {
                    if (value.Contains(", ") || (value.Contains(",")))
                        throw new ArgumentException("Tag can't have commas.");
                    else
                        this._name = value;
                }
            }
        }

        public ICollection<Item> items { get; set; }
    }
}