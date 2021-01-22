using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyLibraryWinForms;
using MyLibraryWinForms.Model.DataAccessLayer;
using MyLibraryWinForms.Model.ObjectModel;
using NUnit;
using NUnit.Framework;

namespace MyLibraryWinForms_UnitTests.Model_Tests.ObjectModel_Tests
{
    public class BookBuilder_Tests
    {
        [Test]
        public void WithIsbn_Test_isbn10()
        {
            string expectedIsbn = "0123456789";
            Book book = BookBuilder.CreateBook("Awesome Python", "Awesome Python: The awesome guide to Pythonic awesomeness").WithIsbn("0123456789")
                .Get();

            NUnit.Framework.Assert.AreEqual(expectedIsbn, book.getIsbn());
        }

        [Test]
        public void WithIsbn_Test_isbn13()
        {
            string expectedIsbn = "0123456789012";
            Book book = BookBuilder.CreateBook("Awesome Python", "Awesome Python: The awesome guide to Pythonic awesomeness").WithIsbn("0123456789012")
                .Get();

            NUnit.Framework.Assert.AreEqual(expectedIsbn, book.getIsbn());
        }

        public void WithIsbn_Test_notIsbn()
        {
            NUnit.Framework.Assert.Throws<ArgumentException>(() => {
                BookBuilder.CreateBook("Awesome Python", "Awesome Python: The awesome guide to Pythonic awesomeness").WithIsbn("0123456789012234234").Get();
            });
        }
    }
}
