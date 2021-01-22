using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryWinForms.Model.ObjectModel;
using MyLibraryWinForms.Model.DataAccessLayer;
using MyLibraryWinForms.LogicLayer.Specifications;

namespace MyLibraryWinForms.LogicLayer
{
    public sealed class ItemRepository
    {
        private BookDataAccessObject bookDataAccessObject;
        private MediaItemDataAccessObject mediaItemDataAccessObject;

        #region Constructors
        public ItemRepository()
        {
            bookDataAccessObject = new BookDataAccessObject();
            mediaItemDataAccessObject = new MediaItemDataAccessObject();
        }

        public ItemRepository(BookDataAccessObject bookDataAccessObject, 
            MediaItemDataAccessObject mediaItemDataAccessObject)
        {
            this.bookDataAccessObject = bookDataAccessObject;
            this.mediaItemDataAccessObject = mediaItemDataAccessObject;
        }
        #endregion

        #region Books
        public void AddBook(Book newBook)
        {
            bookDataAccessObject.create(newBook);
        }

        public IEnumerable<Book> GetBooks() => bookDataAccessObject.readAll();

        public IEnumerable<Book> GetBooks(ISpecification<Book> spec)
        {
            foreach (var book in GetBooks())
                if (spec.IsSatisfiedBy(book))
                    yield return book;
        }

        public Book GetBook(int id) => bookDataAccessObject.readById(id);

        public void UpdateBook(Book toUpdate)
        {
            bookDataAccessObject.update(toUpdate);
        }

        public void DeleteBook(int id)
        {
            bookDataAccessObject.delete(id);
        }
        #endregion

        #region MediaItems
        public void AddMediaItem(MediaItem newItem)
        {
            mediaItemDataAccessObject.create(newItem);
        }

        public IEnumerable<MediaItem> GetMediaItems() => mediaItemDataAccessObject.readAll();

        public IEnumerable<MediaItem> GetMediaItems(ISpecification<MediaItem> spec)
        {
            foreach (var item in mediaItemDataAccessObject.readAll())
                if (spec.IsSatisfiedBy(item))
                    yield return item;
        }

        public MediaItem GetMediaItem(int id) => mediaItemDataAccessObject.readById(id);

        public void UpdateMediaItem(MediaItem toUpdate)
        {
            mediaItemDataAccessObject.update(toUpdate);
        }

        public void DeleteMediaItem(int id)
        {
            mediaItemDataAccessObject.delete(id);
        }
        #endregion
    }
}
