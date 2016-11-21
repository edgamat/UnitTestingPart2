// ----------------------------------------------------------------------------------------------------
// <copyright file="TestDbAsyncEnumerator.cs" company="Infinity Software Development, Inc.">
//     Copyright (c) Infinity Software Development, Inc. 2016
// </copyright>
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinity.Mobius.EntityFramework.Moq
{
    /// <summary>
    /// TestDbAsyncEnumerator Class.
    /// Based on the details of the following article: <seealso cref="https://msdn.microsoft.com/en-us/library/dn314429.aspx"/>
    /// </summary>
    /// <typeparam name="T">The type of entity to enumerate</typeparam>
    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        /// <summary>
        /// The inner enumerator
        /// </summary>
        private readonly IEnumerator<T> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbAsyncEnumerator{T}"/> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        public T Current
        {
            get { return this.inner.Current; }
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        object IDbAsyncEnumerator.Current
        {
            get { return this.Current; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.inner.Dispose();
        }

        /// <summary>
        /// Advances the enumerator to the next element in the sequence, returning the result asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the sequence.</returns>
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.inner.MoveNext());
        }
    }
}
