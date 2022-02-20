using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assignment3.Tests;
using Assignment3.Models;

namespace Assignment3
{
    public class DataAccessLayerTest
    {
        [Fact]
        public async Task OnGetAsync_CustomersAreReturned()
        {
            using (var db = new DeliveryCartDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedCustomers = DeliveryCartDbContext.GetSeedingCustomers();
                await db.AddRangeAsync(expectedCustomers);
                await db.SaveChangesAsync();

                // Act
                var result = await db.OnGetAsync();

                // Assert
                var actualCustomers = Assert.IsAssignableFrom<List<Customer>>(result);
                Assert.Equal(
                    expectedCustomers.OrderBy(m => m.CustomerID).Select(m => m.Email), 
                    actualCustomers.OrderBy(m => m.CustomerID).Select(m => m.Email));
            }
        }
    }
}
