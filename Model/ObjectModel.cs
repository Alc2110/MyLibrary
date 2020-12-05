using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLibraryWinForms.Model.ObjectModel
{
    public abstract class BaseEntity // abstract class to reduce code duplication
    {
        public int id { get; set; }
    }

    public abstract class Item : BaseEntity // abstract class to reduce code duplication
    {
        public string title { get; set; }

        public System.Drawing.Image image { get; set; }
    }

    public sealed class Book : Item
    {
        /// <summary>
        /// Default constructor. Initialises authors and tags lists.
        /// </summary>
        public Book()
        {
            this.authors = new List<string>();
            this.tags = new List<string>();
        }

        public string titleLong { get; set; }

        public long? isbn { get; set; }
        public long? isbn13 { get; set; }

        public double? deweyDecimal { get; set; }
        public string format { get; set; }

        public string edition { get; set; }
        public int? pages { get; set; }
        public string dimensions { get; set; }
        public string overview { get; set; }

        public string msrp { get; set; }
        public string excerpt { get; set; }
        public string synopsys { get; set; }

        public ICollection<string> authors { get; set; }
        public ICollection<string> tags { get; set; }
        public string publisher { get; set; }

        public void addAuthor(string author)
        {
            this.authors.Add(author);
        }

        public void removeAuthor(string author)
        {
            this.authors.Remove(author);
        }

        public void addTag(string tag)
        {
            this.tags.Add(tag);
        }

        public void removeTag(string tag)
        {
            this.tags.Remove(tag);
        }
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
        public long? number { get; set; } 
        public int? runningTime { get; set; }
        public int? releaseYear { get; set; }
    }

    public sealed class Author : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}