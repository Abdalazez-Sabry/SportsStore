using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests {
    public class HomeControllerTests {
        [Fact]
        public void Can_Use_Repository() {
            Mock<IStoreRepository> mock = new();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new() { ProductID = 1, Name = "P1"},
                new() { ProductID = 2, Name = "P2"},
            }).AsQueryable<Product>());

            HomeController controller = new(mock.Object);

            IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Product[] prodArray = result?.ToArray() ?? [];
            Assert.True(prodArray.Length == 2);
            Assert.Equal("p1", prodArray[0].Name);
            Assert.Equal("p2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate() {
            Mock<IStoreRepository> mock = new();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new() {ProductID = 1, Name = "p1"},
                new() {ProductID = 2, Name = "p2"},
                new() {ProductID = 3, Name = "p3"},
                new() {ProductID = 4, Name = "p4"},
                new() {ProductID = 5, Name = "p5"},
            }).AsQueryable<Product>());

            HomeController controller = new(mock.Object) {
                PageSize = 3
            };

            IEnumerable<Product> result = (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Product> ?? [];

            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("p4", prodArray[0].Name);
            Assert.Equal("p5", prodArray[1].Name);
        }
    }
}