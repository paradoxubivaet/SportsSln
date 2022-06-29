using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Use_Repository() 
        {
            // Arrange 
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product
                {
                    ProductId = 1, Name = "P1"
                },
                new Product
                {
                    ProductId = 2, Name = "P2"
                }
            }).AsQueryable<Product>());

            HomeController homeController = new HomeController(mock.Object);

            // Act 
            IEnumerable<Product> result = 
                (homeController.Index() as ViewResult).ViewData.Model 
                as IEnumerable<Product>;

            // Assert 
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }
    }
}
