using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyLibraryWinForms;
using MyLibraryWinForms.Model.DataAccessLayer;
using MyLibraryWinForms.Model.ObjectModel;
using NUnit;
using NUnit.Framework;

namespace MyLibraryWinForms_UnitTests.ObjectModel_Tests
{
    #region Mocks
    class MockItem : Item { }
    #endregion

    public class ObjectModel_Tests
    {
        [TestClass]
        public class Item_Tests
        {
            [TestMethod]
            public void getCommaDelimitedTags_Test()
            {
                // arrange
                MockItem testItem = new MockItem();
                testItem.tags = new System.Collections.Generic.List<Tag> { new Tag { id = 1, name = "History" },
                    new Tag { id = 2, name = "Trains and railways" },
                    new Tag { id = 3, name = "Fiction" } };
                string expectedResult = "History, Trains and railways, Fiction";

                // act
                string actualResult = testItem.getCommaDelimitedTags();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestClass]
        public class Book_Tests
        {
            [TestMethod]
            public void getIsbn_Test_isbn10()
            {
                // arrange
                Book testBook = new Book { isbn = "0123456789", isbn13 = "" };
                string expectedResult = "0123456789";

                // act
                string actualResult = testBook.getIsbn();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }

            [TestMethod]
            public void getIsbn_Test_isbn13()
            {
                // arrange
                Book testBook = new Book { isbn13 = "0123456789012", isbn = "" };
                string expectedResult = "0123456789012";

                // act
                string actualResult = testBook.getIsbn();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }

            [TestMethod]
            public void getIsbn_Test_both()
            {
                // arrange
                Book testBook = new Book { isbn13 = "0123456789012", isbn = "0123456789" };
                string expectedResult = "0123456789; 0123456789012";

                // act
                string actualResult = testBook.getIsbn();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }

            [TestMethod]
            public void getIsbn_Test_neither()
            {
                // arrange
                Book testBook = new Book();
                testBook.isbn = ""; testBook.isbn13 = "";
                string expectedResult = "";

                // act
                string actualResult = testBook.getIsbn();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestClass]
        public class Author_Tests
        {
            [TestMethod]
            public void getFullName_Test()
            {
                // arrange
                Author testAuthor = new Author { firstName = "John", lastName = "Smith" };
                string expectedResult = "John Smith";

                // act
                string actualResult = testAuthor.getFullName();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }

            [TestMethod]
            public void getFullNameLastNameCommaFirstName_Test()
            {
                // arrange
                Author testAuthor = new Author { firstName = "John", lastName = "Smith" };
                string expectedResult = "Smith, John";

                // act
                string actualResult = testAuthor.getFullNameLastNameCommaFirstName();

                // assert
                NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
            }

            [TestMethod]
            public void firstName_setter_Test_noname()
            {
                // arrange
                Author testAuthor = new Author();

                // act/assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testAuthor.firstName = "");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testAuthor.firstName = null);
            }

            [TestMethod]
            public void lastName_setter_Test_noname()
            {
                // arrange
                Author testAuthor = new Author();

                // act/assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testAuthor.firstName = "");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testAuthor.firstName = null);
            }
        }

        [TestClass]
        public class Tag_Tests
        {
            [TestMethod]
            public void name_setter_Test_noName()
            {
                // arrange
                Tag testTag = new Tag();

                // act/assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testTag.name = "");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentNullException>(() => testTag.name = null);
            }

            [TestMethod]
            public void name_setter_Test_containsComma()
            {
                // arrange
                Tag testTag = new Tag();

                // act/assert
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentException>(() => testTag.name = "Trains,railways");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<ArgumentException>(() => testTag.name = "Trains, railways");
            }

            [TestCase("Trains and railways")]
            [TestCase("Trains;railways")]
            public void name_setter_Test_validName(string name)
            {
                // arrange
                Tag testTag = new Tag();

                // act
                testTag.name = name;

                // assert
                // no exceptions should be thrown here
                NUnit.Framework.Assert.Pass();
            }
        }
    }
}