DROP TABLE IF EXISTS Books;
DROP TABLE IF EXISTS Authors;
DROP TABLE IF EXISTS Tags;
DROP TABLE IF EXISTS Publishers;
DROP TABLE IF EXISTS Media;
DROP TABLE IF EXISTS Book_Tag;
DROP TABLE IF EXISTS Book_Author;
DROP TABLE IF EXISTS Media_Tag;

CREATE TABLE "Books" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "title" TEXT NOT NULL,
    "titleLong" TEXT NOT NULL,
    "isbn" TEXT,
    "isbn13"    TEXT,
    "deweyDecimal"  REAL,
    "publisherId"   INTEGER NOT NULL,
    "format"    TEXT,
    "language"  TEXT NOT NULL,
    "datePublished" TEXT,
    "edition"   TEXT,
    "pages" INTEGER NOT NULL,
    "dimensions"    TEXT,
    "overview"  TEXT,
    "image" BLOB,
    "msrp"  TEXT,
    "excerpt"   TEXT,
    "synopsys"  TEXT,
    "notes" TEXT,
    FOREIGN KEY("publisherId") REFERENCES "Publishers"("id") ON DELETE SET NULL ON UPDATE NO ACTION
);

CREATE TABLE "Authors" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "firstName"  TEXT NOT NULL,
    "lastName"  TEXT NOT NULL,
    UNIQUE("firstName", "lastName")
);

CREATE TABLE "Publishers" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name"  TEXT NOT NULL UNIQUE
);

CREATE TABLE "Media" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "title" TEXT NOT NULL,
    "type"  INTEGER NOT NULL,
    "number"    INTEGER NOT NULL,
    "image" BLOB,
    "runningTime"   INTEGER NOT NULL,
    "releaseYear"   INTEGER NOT NULL,
    "notes" TEXT
);

CREATE TABLE "Tags" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name"  TEXT NOT NULL UNIQUE
);

CREATE TABLE "Book_Tag" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "bookId"    INTEGER NOT NULL,
    "tagId" INTEGER NOT NULL,
    UNIQUE("bookId", "tagId"),
    FOREIGN KEY("bookId") REFERENCES "Books"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("tagId") REFERENCES "Tags"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE "Book_Author" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "bookId"    INTEGER NOT NULL,
    "authorId"  INTEGER NOT NULL,
    UNIQUE("bookId", "authorId"),
    FOREIGN KEY("bookId") REFERENCES "Books"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("authorId") REFERENCES "Authors"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE "Media_Tag" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "mediaId"   INTEGER NOT NULL,
    "tagId" INTEGER NOT NULL,
    UNIQUE("mediaId", "tagId"),
    FOREIGN KEY("mediaId") REFERENCES "Media"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("tagId") REFERENCES "Tags"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);