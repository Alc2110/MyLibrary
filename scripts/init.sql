CREATE TABLE "Books" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "title" TEXT NOT NULL,
    "titleLong" TEXT NOT NULL,
    "isbn" INTEGER,
    "isbn13"    INTEGER,
    "deweyDecimal"  REAL,
    "publisherId"   INTEGER,
    "format"    TEXT,
    "language"  TEXT,
    "datePublished" TEXT,
    "edition"   TEXT,
    "pages" INTEGER,
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
    "lastName"  TEXT NOT NULL
);

CREATE TABLE "Publishers" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name"  TEXT NOT NULL
);

CREATE TABLE "Media" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "title" TEXT NOT NULL,
    "type"  INTEGER NOT NULL,
    "number"    INTEGER,
    "image" BLOB,
    "runningTime"   INTEGER,
    "releaseYear"   INTEGER
);

CREATE TABLE "Tags" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name"  TEXT NOT NULL
);

CREATE TABLE "Book_Tag" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "bookId"    INTEGER NOT NULL,
    "tagId" INTEGER NOT NULL,
    FOREIGN KEY("bookId") REFERENCES "Books"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("tagId") REFERENCES "Tags"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE "Book_Author" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "bookId"    INTEGER NOT NULL,
    "authorId"  INTEGER NOT NULL,
    FOREIGN KEY("bookId") REFERENCES "Books"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("authorId") REFERENCES "Authors"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE "Media_Tag" (
    "id"    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "mediaId"   INTEGER NOT NULL,
    "tagId" INTEGER NOT NULL,
    FOREIGN KEY("mediaId") REFERENCES "Media"("id") ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY("tagId") REFERENCES "Tags"("id") ON DELETE CASCADE ON UPDATE NO ACTION
);