using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Dapper;
using MyLibraryWinForms.Model.ObjectModel;

namespace MyLibraryWinForms.Model.DataAccessLayer
{
    public abstract class DataAccessObject<T> where T : BaseEntity
    {
        protected readonly string connectionString;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataAccessObject()
        {
            this.connectionString = Configuration.CONNECTION_STRING;
        }

        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public DataAccessObject(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public abstract void create(T entity);
        public abstract T readById(int id);
        public abstract IEnumerable<T> readAll();
    }

    public abstract class ItemDataAccessObject<T> : DataAccessObject<T> where T : Item
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ItemDataAccessObject() { }

        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public ItemDataAccessObject(string connectionString) : base(connectionString) { }

        public abstract void update(T entity);
        public abstract void delete(int id);
        public abstract void associateNewTag(int itemId, string tag);
        public abstract void associateExistingTag(int itemId, int tagId);
        public abstract void disassociateTag(int itemId, int tagId);
    }

    #region Authors
    public class AuthorDataAccessObject : DataAccessObject<Author>
    {
        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public AuthorDataAccessObject(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AuthorDataAccessObject() : base() { }

        /// <summary>
        /// Insert new author into database.
        /// </summary>
        /// <param name="entity"></param>
        public override void create(Author entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "INSERT INTO Authors (firstName, lastName) VALUES (@firstName, @lastName);";
                var @params = new { firstName = entity.firstName, lastName = entity.lastName };
                db.Execute(sql, @params);
            }
        }

        /// <summary>
        /// Retrieve author with specific id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Author readById(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Authors WHERE id = @id;";
                var @params = new { id = id };
                return db.Query<Author>(sql, @params).First();
            }
        }

        /// <summary>
        /// Retrieve all authors from database.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Author> readAll()
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Authors;";
                return db.Query<Author>(sql);
            }
        }
    }
    #endregion

    #region Tags
    public class TagDataAccessObject : DataAccessObject<Tag>
    {
        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public TagDataAccessObject(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagDataAccessObject() : base() { }

        /// <summary>
        /// Create new tag.
        /// </summary>
        /// <param name="entity"></param>
        public override void create(Tag entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "INSERT INTO Tags (name) VALUES (@name);";
                var @params = new { name = entity.name };
                db.Execute(sql, @params);
            }
        }

        /// <summary>
        /// Delete tag with specific id.
        /// </summary>
        /// <param name="id"></param>
        public void delete(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                // tags automatically disassociated from items (through Book_Tag and Media_Tag tables)
                var sql = "DELETE FROM Tags WHERE id = @id;";
                var @params = new { id = id };
                db.Execute(sql, @params);
            }
        }

        public override Tag readById(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Tags WHERE id = @id";
                var @params = new { id = id };
                return db.Query<Tag>(sql, @params).First();
            }
        }

        public override IEnumerable<Tag> readAll()
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Tags;";
                return db.Query<Tag>(sql);
            }
        }
    }
    #endregion

    #region Publishers
    public class PublisherDataAccessObject : DataAccessObject<Publisher>
    {
        /// <summary>
        /// Constructor with connection string dependency injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public PublisherDataAccessObject(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublisherDataAccessObject() : base() { }

        /// <summary>
        /// Create a new publisher entry in the database.
        /// </summary>
        /// <param name="entity"></param>
        public override void create(Publisher entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "INSERT INTO Publishers (name) VALUES (@name);";
                var @params = new { name = entity.name };
                db.Execute(sql, @params);
            }
        }

        /// <summary>
        /// Retreive a specific publisher by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Publisher readById(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Publishers WHERE id = @id";
                var @params = new { id = id };
                return db.Query(sql, @params).First();
            }
        }

        /// <summary>
        /// Retrieve all publishers from the database.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Publisher> readAll()
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * From Publishers;";
                return db.Query<Publisher>(sql);
            }
        }
    }
    #endregion

    #region Books
    public class BookDataAccessObject : ItemDataAccessObject<Book>
    {
        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public BookDataAccessObject(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BookDataAccessObject() : base() { }

        /// <summary>
        /// Create new book record. TODO: figure out how to deal with authors and publishers.
        /// </summary>
        /// <param name="entity"></param>
        public override void create(Book entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                // start transaction
                using (var transaction = db.BeginTransaction())
                {
                    // insert item into Books table
                    var insertItemSql = "INSERT INTO Books (title, titleLong, isbn, isbn13, deweyDecimal, publisherId, format, language, datePublished, edition, pages, dimensions, overview, image, msrp, excerpt, synopsys, notes) " +
                        "VALUES (@title, @titleLong, @isbn, @isbn13, @deweyDecimal, @publisherId, @format, @language, @datePublished, @edition, @pages, @dimensions, @overview, @image, @msrp, @excerpt, @synopsys, @notes);";
                    var @Itemparams = new
                    {
                        title = entity.title,
                        titleLong = entity.titleLong,
                        isbn = entity.isbn,
                        isbn13 = entity.isbn13,
                        deweyDecimal = entity.deweyDecimal,
                        publisherId = entity.publisher.id,
                        format = entity.format,
                        language = entity.titleLong,
                        datePublished = entity.datePublished,
                        edition = entity.edition,
                        pages = entity.pages,
                        dimensions = entity.dimensions,
                        overview = entity.overview,
                        image = entity.image,
                        msrp = entity.msrp,
                        excerpt = entity.excerpt,
                        synopsys = entity.synopsys,
                        notes = entity.notes
                    };
                    db.Execute(insertItemSql, @Itemparams);

                    // get id of inserted item record
                    int mediaId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault();

                    // tags
                    foreach (var tag in entity.tags)
                    {
                        var getTagsSql = "SELECT * FROM Tags WHERE name = @name;";
                        var @tagparams = new { name = tag.name };
                        bool exists = db.Query<Tag>(getTagsSql, @tagparams).Count() > 0;
                        if (exists)
                        {
                            // tag exists
                            // associate it
                            int tagId = db.Query<Tag>(getTagsSql, @tagparams).FirstOrDefault().id;
                            Console.WriteLine(tagId);
                            var createTagLinkSql = "INSERT INTO Book_Tag (bookId, tagId) VALUES (@bookId, @tagId);";
                            var @taglinkparams = new { mediaId = mediaId, tagId = tagId };
                            db.Execute(createTagLinkSql, @taglinkparams);
                        }
                        else
                        {
                            // tag does not exist
                            // create it, and associate it
                            var createTagSql = "INSERT INTO Tags (name) VALUES (@name);";
                            db.Execute(createTagSql, @tagparams);

                            int tagId = db.Query<Tag>(getTagsSql, @tagparams).First().id;
                            var createTagLinkSql = "INSERT INTO Book_Tag (bookId, tagId) VALUES (@bookId, @tagId);";
                            var @taglinkparams = new { mediaId = mediaId, tagId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault() };
                            db.Execute(createTagLinkSql, @taglinkparams);
                        }
                    }

                    // commit transaction
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Get book record by id. Authors, tags and publishers are dealt with by the repository, to simplify the query.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Book readById(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Books WHERE id = @id";
                var @params = new { id = id };
                return db.Query<Book>(sql).First();
            }
        }

        /// <summary>
        /// Get all book records. Authors, tags and publishers are handled by the repository, to simplify the query.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Book> readAll()
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT * FROM Books;";
                return db.Query<Book>(sql);
            }
        }

        /// <summary>
        /// Update book record in database. Dewey decimal, image and notes fields considered.
        /// </summary>
        /// <param name="entity"></param>
        public override void update(Book entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "UPDATE Books" +
                    " SET deweyDecimal = @deweyDecimal" +
                    " image = @image" +
                    " notes = @notes" +
                    " WHERE id = @id;";
                var @params = new
                {
                    id = entity.id,
                    image = entity.image,
                    notes = entity.notes
                };
                db.Execute(sql, @params);
            }
        }

        /// <summary>
        /// Delete book record from database. Table disassociations handled by database.
        /// </summary>
        /// <param name="entity"></param>
        public override void delete(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "DELETE FROM Books WHERE id = @id";
                var @params = new { id = id };
                db.Execute(sql, @params);
            }
        }

        public override void associateExistingTag(int itemId, int tagId)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "INSERT INTO Book_Tag (bookId, tagId) VALUES (@bookId, @tagId);";
                var @params = new { bookId = itemId, tagId = tagId };
                db.Execute(sql, @params);
            }
        }

        public override void associateNewTag(int itemId, string tag)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                // start transaction
                using (var transaction = db.BeginTransaction())
                {
                    // create new tag
                    var createTagSql = "INSERT INTO Tags (name) VALUES (@name);";
                    var @createTagParams = new { name = tag };
                    db.Execute(createTagSql, @createTagParams);

                    // associate tag
                    int newTagId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault();
                    var createTagLinkSql = "INSERT INTO Book_Tag (bookId, tagId) VALUES (@bookId, @tagId);";
                    var @createTagLinkParams = new { bookId = itemId, tagId = newTagId };
                    db.Execute(createTagLinkSql, createTagLinkParams);

                    // commit transaction
                    transaction.Commit();
                }
            }
        }

        public override void disassociateTag(int itemId, int tagId)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "DELETE FROM Book_Tag WHERE bookId = @bookId AND tagId = @tagId;";
                var @params = new { bookId = itemId, tagId = tagId };
                db.Execute(sql, @params);
            }
        }
    }
    #endregion

    #region Media Items
    public class MediaItemDataAccessObject : ItemDataAccessObject<MediaItem>
    {
        /// <summary>
        /// Constructor with connection string injection.
        /// </summary>
        /// <param name="connectionString"></param>
        public MediaItemDataAccessObject(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MediaItemDataAccessObject() : base() { }

        /// <summary>
        /// Single table insert, without associating tags.
        /// </summary>
        /// <param name="entity"></param>
        public override void create(MediaItem entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                // start transaction
                using (var transaction = db.BeginTransaction())
                {
                    // insert record into Media table
                    var sql = "INSERT INTO Media (title, type, number, image, runningTime, releaseYear, notes) VALUES (@title, @type, @number, @image, @runningTime, @releaseYear, @notes);";
                    var @params = new
                    {
                        title = entity.title,
                        type = entity.type,
                        number = entity.number,
                        image = entity.image,
                        runningTime = entity.runningTime,
                        releaseYear = entity.releaseYear,
                        notes = entity.notes
                    };
                    db.Execute(sql, @params);

                    // get id of inserted item record
                    int mediaId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault();

                    // tags
                    foreach (var tag in entity.tags)
                    {
                        var getTagsSql = "SELECT * FROM Tags WHERE name = @name;";
                        var @tagparams = new { name = tag.name };
                        bool exists = db.Query<Tag>(getTagsSql, @tagparams).Count() > 0;
                        if (exists)
                        {
                            // tag exists
                            // associate it
                            int tagId = db.Query<Tag>(getTagsSql, @tagparams).FirstOrDefault().id;
                            Console.WriteLine(tagId);
                            var createTagLinkSql = "INSERT INTO Media_Tag (mediaId, tagId) VALUES (@mediaId, @tagId);";
                            var @taglinkparams = new { mediaId = mediaId, tagId = tagId };
                            db.Execute(createTagLinkSql, @taglinkparams);
                        }
                        else
                        {
                            // tag does not exist
                            // create it, and associate it
                            var createTagSql = "INSERT INTO Tags (name) VALUES (@name);";
                            db.Execute(createTagSql, @tagparams);

                            int tagId = db.Query<Tag>(getTagsSql, @tagparams).First().id;
                            var createTagLinkSql = "INSERT INTO Media_Tag (mediaId, tagId) VALUES (@mediaId, @tagId);";
                            var @taglinkparams = new { mediaId = mediaId, tagId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault() };
                            db.Execute(createTagLinkSql, @taglinkparams);
                        }
                    }

                    // commit transaction
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Delete a media item record from the database. Tags are automatically disassociated through the link table.
        /// </summary>
        /// <param name="id">Record ID of the item.</param>
        public override void delete(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "DELETE FROM Media WHERE id = @id";
                var @params = new { id = id };
                db.Execute(sql, @params);
            }
        }

        /// <summary>
        /// Get media item record by ID. Includes tags.
        /// </summary>
        /// <param name="id">Record ID of the item.</param>
        /// <returns></returns>
        public override MediaItem readById(int id)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT M.Id, M.title, M.type, M.Number, M.image, M.runningTime, M.releaseYear, T.Id, T.Name FROM Media as M" +
                            " LEFT JOIN Media_Tag AS MT ON M.id = MT.mediaId" +
                            " LEFT JOIN Tags AS T ON T.id = MT.tagId" +
                            " WHERE M.Id = " + id + ";"; // TODO: parameterise!

                var mediaDict = new Dictionary<int, MediaItem>();
                var tagDict = new Dictionary<int, Tag>();

                var list = db.Query<MediaItem, Tag, MediaItem>(sql, (media, tag) =>
                {
                    MediaItem itemRecord;

                    if (!mediaDict.TryGetValue(media.id, out itemRecord))
                    {
                        // haven't seen this item before, add it to the dictionary
                        itemRecord = media;
                        mediaDict.Add(itemRecord.id, itemRecord);
                    }

                    Tag tagRecord;

                    if (tag != null)
                    {
                        if (!tagDict.TryGetValue(tag.id, out tagRecord))
                        {
                            // haven't seen this tag before, add it to the dictionary
                            tagRecord = tag;
                            tagDict.Add(tagRecord.id, tagRecord);
                        }

                        // add the tag to the collection of tags for this item
                        itemRecord.tags.Add(tagRecord);
                    }

                    return itemRecord;

                }, splitOn: "id")
                .Distinct()
                .ToList();

                return list.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all media item records. Tags included.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<MediaItem> readAll()
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "SELECT M.Id, M.title, M.type, M.Number, M.image, M.runningTime, M.releaseYear, T.Id, T.Name FROM Media as M" +
                            " LEFT JOIN Media_Tag AS MT ON M.id = MT.mediaId" + 
                            " LEFT JOIN Tags AS T ON T.id = MT.tagId;";
                
                var mediaDict = new Dictionary<int, MediaItem>();
                var tagDict = new Dictionary<int, Tag>();

                var list = db.Query<MediaItem, Tag, MediaItem>(sql, (media, tag) =>
                {
                    MediaItem itemRecord;

                    if (!mediaDict.TryGetValue(media.id, out itemRecord))
                    {
                        // haven't seen this item before, add it to the dictionary
                        itemRecord = media;
                        mediaDict.Add(itemRecord.id, itemRecord);
                    }

                    Tag tagRecord;

                    if (tag != null)
                    {
                        if (!tagDict.TryGetValue(tag.id, out tagRecord))
                        {
                            // haven't seen this tag before, add it to the dictionary
                            tagRecord = tag;
                            tagDict.Add(tagRecord.id, tagRecord);
                        }

                        // add the tag to the collection of tags for this item
                        itemRecord.tags.Add(tagRecord);
                    }

                    return itemRecord;
                    
                }, splitOn: "id")
                .Distinct()
                .ToList();

                return list;
            }
        }

        /// <summary>
        /// Update media item record in database. Number, image, running time, release year and notes fields considered.
        /// </summary>
        /// <param name="entity"></param>
        public override void update(MediaItem entity)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.AppendLine("UPDATE Media");
                sqlBuilder.AppendLine("SET title = @title,");
                sqlBuilder.AppendLine("number = @number,");
                sqlBuilder.AppendLine("image = @image,");
                sqlBuilder.AppendLine("runningTime = @runningTime,");
                sqlBuilder.AppendLine("releaseYear = @releaseYear,");
                sqlBuilder.AppendLine("notes = @notes");
                sqlBuilder.AppendLine("WHERE id = @id;");
                var @params = new { id = entity.id, 
                    title = entity.title, 
                    number = entity.number, 
                    image = entity.image, 
                    runningTime = entity.runningTime,
                    releaseYear = entity.releaseYear,
                    notes = entity.notes };
                db.Execute(sqlBuilder.ToString(), @params);
            }
        }

        public override void associateExistingTag(int itemId, int tagId)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "INSERT INTO Media_Tag (mediaId, tagId) VALUES (@mediaId, @tagId);";
                var @params = new { mediaId = itemId, tagId = tagId };
                db.Execute(sql, @params);
            }
        }

        public override void associateNewTag(int itemId, string tag)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                // start transaction
                using (var transaction = db.BeginTransaction())
                {
                    // create new tag
                    var createTagSql = "INSERT INTO Tags (name) VALUES (@name);";
                    var @createTagParams = new { name = tag };
                    db.Execute(createTagSql, @createTagParams);

                    // associate tag
                    int newTagId = db.Query<int>("SELECT last_insert_rowid();").FirstOrDefault();
                    var createTagLinkSql = "INSERT INTO Media_Tag (mediaId, tagId) VALUES (@mediaId, @tagId);";
                    var @createTagLinkParams = new { mediaId = itemId, tagId = newTagId };
                    db.Execute(createTagLinkSql, createTagLinkParams);

                    // commit transaction
                    transaction.Commit();
                }
            }
        }

        public override void disassociateTag(int itemId, int tagId)
        {
            using (var db = new SqliteConnection(this.connectionString))
            {
                db.Open();

                var sql = "DELETE FROM Media_Tag WHERE mediaId = @mediaId AND tagId = @tagId;";
                var @params = new { mediaId = itemId, tagId = tagId };
                db.Execute(sql, @params);
            }
        }
    }
    #endregion
}
