using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "P1", Category = "Apples" },
                new Product { ProductId = 2, Name = "P2", Category = "Apples" },
                new Product { ProductId = 3, Name = "P3", Category = "Plums" },
                new Product { ProductId = 4, Name = "P4", Category = "Oranges" },
            }).AsQueryable<Product>);

            NavigationMenuViewComponent target = 
                new NavigationMenuViewComponent(mock.Object);

            // Act
            // - get the set of categories
            string[] result = ((IEnumerable<string>)(target.Invoke() 
                as ViewComponentResult).ViewData.Model).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", 
                "Oranges", "Plums" }, result));
        }
    }
}
