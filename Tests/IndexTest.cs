using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Classes;
using Xunit;

namespace Tests
{
    public class IndexTest
    {
        [Fact]
        public void EnsureImplicitNonUniqueIndexIsCreated()
        {
            var testDatabaseName = "EnsureImplicitNonUniqueIndexIsCreated";

            var expectedValues = new Dictionary<int, string>
            {
                {
                    1, "Foo"
                },
                {
                    2, "Bar"
                },
                {
                    3, "Xoo"
                }
            };

            using (var context = new TestDbContext(testDatabaseName))
            {
                foreach (var item in expectedValues)
                {
                    context.EntitiesWithImplicitNonUniqueIndexes.Add(new EntityWithImplicitNonUniqueIndex { Id = item.Key, Name = item.Value });
                }

                context.SaveChanges();
            }

            using (var context = new TestDbContext(testDatabaseName))
            {
                var entities = context.EntitiesWithImplicitNonUniqueIndexes.AsNoTracking().ToList();

                // Compare count.
                Assert.Equal(expectedValues.Count, entities.Count());

                for (int i = 0; i < expectedValues.Count; i++)
                {
                    var expected = expectedValues.ToList()[i];
                    var entity = entities[i];

                    // Compare values.
                    Assert.Equal(expected.Key, entity.Id);
                    Assert.Equal(expected.Value, entity.Name);
                }
            }
        }

        //[Fact]
        //public void EnsureIndexesAreBeingCreatedOnModelBuilding()
        //{
        //    var testDatabaseName = "EnsureIndexesAreBeingCreatedOnModelBuilding";

        //    using (var context = new TestDbContext(testDatabaseName))
        //    {
        //        var existentIndexes = context.Database.ExecuteSqlCommand("SELECT * FROM sys.indexes WHERE name='IX_Name' AND object_id = OBJECT_ID('EntityWithUniqueIndex')");

        //        Assert.Equal(1, existentIndexes);
        //    }
        //}

        [Fact]
        public void EnsureNonUniqueIndexIsCreated()
        {
            var testDatabaseName = "EnsureNonUniqueIndexIsCreated";

            var expectedValues = new Dictionary<int, string>
            {
                {
                    1, "Foo"
                },
                {
                    2, "Bar"
                },
                {
                    3, "Xoo"
                }
            };

            using (var context = new TestDbContext(testDatabaseName))
            {
                foreach (var item in expectedValues)
                {
                    context.EntitiesWithNonUniqueIndexes.Add(new EntityWithNonUniqueIndex { Id = item.Key, Name = item.Value });
                }

                context.SaveChanges();
            }

            using (var context = new TestDbContext(testDatabaseName))
            {
                var entities = context.EntitiesWithNonUniqueIndexes.AsNoTracking().ToList();

                // Compare count.
                Assert.Equal(expectedValues.Count, entities.Count());

                for (int i = 0; i < expectedValues.Count; i++)
                {
                    var expected = expectedValues.ToList()[i];
                    var entity = entities[i];

                    // Compare values.
                    Assert.Equal(expected.Key, entity.Id);
                    Assert.Equal(expected.Value, entity.Name);
                }
            }
        }

        [Fact]
        public void EnsureUniqueIndexIsNotViolated()
        {
            var testDatabaseName = "EnsureUniqueIndexIsNotViolated";

            var expectedValues = new Dictionary<int, string>
            {
                {
                    1, "Foo"
                },
                {
                    2, "Foo"
                },
                {
                    3, "Foo"
                }
            };

            using (var context = new TestDbContext(testDatabaseName))
            {
                foreach (var item in expectedValues)
                {
                    context.EntitiesWithUniqueIndexes.Add(new EntityWithUniqueIndex { Id = item.Key, Name = item.Value });
                }

                context.SaveChanges();
                //Assert.Throws<Exception>(() =>
                //{
                //});
            }

            using (var context = new TestDbContext(testDatabaseName))
            {
                var entities = context.EntitiesWithUniqueIndexes.AsNoTracking().ToList();

                // Compare count.
                Assert.Single(entities);

                // Compare values.
                var expected = expectedValues.ToList().FirstOrDefault();
                var entity = entities.FirstOrDefault();

                Assert.Equal(expected.Key, entity.Id);
                Assert.Equal(expected.Value, entity.Name);
            }
        }

        [Fact]
        public void EnsureNormalSavingIsNotBroken()
        {
            var testDatabaseName = "EnsureNormalSavingIsNotBroken";

            var expectedValues = new Dictionary<int, string>
            {
                {
                    1, "Foo"
                },
                {
                    2, "Bar"
                },
                {
                    3, "Xoo"
                }
            };

            using (var context = new TestDbContext(testDatabaseName))
            {
                foreach (var item in expectedValues)
                {
                    context.EntitiesWithoutIndexes.Add(new EntityWithoutIndex { Id = item.Key, Name = item.Value });
                }

                context.SaveChanges();
            }

            using (var context = new TestDbContext(testDatabaseName))
            {
                var entities = context.EntitiesWithoutIndexes.AsNoTracking().ToList();

                // Compare count.
                Assert.Equal(expectedValues.Count, entities.Count());

                for (int i = 0; i < expectedValues.Count; i++)
                {
                    var expected = expectedValues.ToList()[i];
                    var entity = entities[i];

                    // Compare values.
                    Assert.Equal(expected.Key, entity.Id);
                    Assert.Equal(expected.Value, entity.Name);
                }
            }
        }
    }
}
