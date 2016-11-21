// ----------------------------------------------------------------------------------------------------
// <copyright file="TestDbAsyncEnumerable.cs" company="Infinity Software Development, Inc.">
//     Copyright (c) Infinity Software Development, Inc. 2016
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Infinity.Mobius.EntityFramework.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// TestDbAsyncEnumerable Class.
    /// </summary>
    /// <typeparam name="T">The type of entity</typeparam>
    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbAsyncEnumerable{T}"/> class.
        /// Based on the details of the following article: <seealso cref="https://msdn.microsoft.com/en-us/library/dn314429.aspx"/>
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbAsyncEnumerable{T}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }

        /// <summary>
        /// Gets the asynchronous enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        /// <summary>
        /// Gets an enumerator that can be used to asynchronously enumerate the sequence.
        /// </summary>
        /// <returns>Enumerator for asynchronous enumeration over the sequence.</returns>
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return this.GetAsyncEnumerator();
        }
    }
}
