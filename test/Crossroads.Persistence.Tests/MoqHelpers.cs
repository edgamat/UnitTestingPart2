using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Crossroads.Persistence.Tests
{
    public static class MoqHelpers
    {
        public static Mock<DbSet<TEntity>> CreateMockSet<TEntity>(this IQueryable<TEntity> source) where TEntity : class
        {
            var mockSet = new Mock<DbSet<TEntity>>();

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(source.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(source.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());

            mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);

            return mockSet;
        }

        /// <summary>
        /// Converts a generic System.Collections.Generic.IEnumerable to a mock DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence to convert.</param>
        /// <returns>A DbSet that represents the input sequence.</returns>
        public static DbSet<TEntity> AsMockDbSet<TEntity>(this IEnumerable<TEntity> source) where TEntity : class
        {
            if (source == null)
            {
                return null;
            }

            return source.AsQueryable().AsMockDbSet();
        }

        /// <summary>
        /// Converts a generic System.Collections.Generic.IQueryable to a mock DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence to convert.</param>
        /// <returns>A DbSet that represents the input sequence.</returns>
        public static DbSet<TEntity> AsMockDbSet<TEntity>(this IQueryable<TEntity> source) where TEntity : class
        {
            if (source == null)
            {
                return null;
            }

            var mockSet = source.CreateMockSet();

            return mockSet.Object;
        }

        /// <summary>
        /// Creates the mock DbSet for a a generic System.Collections.Generic.IEnumerable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence to convert.</param>
        /// <returns>A mock DbSet that represents the input sequence.</returns>
        public static Mock<DbSet<TEntity>> CreateMockSet<TEntity>(this IEnumerable<TEntity> source) where TEntity : class
        {
            return source.AsQueryable().CreateMockSet();
        }
    }
}
