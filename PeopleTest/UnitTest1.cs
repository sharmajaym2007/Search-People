
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People_Search.Models;
using People_Search.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;
using Xunit;
using People_Search;
using NUnit;
namespace PeopleTest
{
    [TestClass]   
    public class UnitTest1
    {
        [TestMethod]
        public void Returns_SpecificRows()
        {
            // Arrange
            PeopleRepository p = new PeopleRepository();

            // Act
            List<People_Search.UserDetail>actual = p.SearchPeople("Jay");

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, actual.Count);
        }


    }



    

    }

