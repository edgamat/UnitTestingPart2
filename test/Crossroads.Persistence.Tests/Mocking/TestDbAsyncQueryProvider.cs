// ----------------------------------------------------------------------------------------------------
// <copyright file="TestDbAsyncQueryProvider.cs" company="Infinity Software Development, Inc.">
//     Copyright (c) Infinity Software Development, Inc. 2016
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Infinity.Mobius.EntityFramework.Moq
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// TestDbAsyncQueryProvider Class.
    /// Based on the details of the following article: <seealso cref="https://msdn.microsoft.com/en-us/library/dn314429.aspx"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        /// <summary>
        /// The _inner
        /// </summary>
        private readonly IQueryProvider inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbAsyncQueryProvider{TEntity}"/> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            this.inner = inner;
        }

        /// <summary>
        /// Constructs an <see cref="T:System.Linq.IQueryable" /> object that can evaluate the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>An <see cref="T:System.Linq.IQueryable" /> that can evaluate the query represented by the specified expression tree.</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The list of elements.</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        /// <summary>
        /// Executes the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>The value that results from executing the specified query.</returns>
        public object Execute(Expression expression)
        {
            return this.inner.Execute(expression);
        }

        /// <summary>
        /// Executes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The result.</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            return this.inner.Execute<TResult>(expression);
        }

        /// <summary>
        /// Asynchronously executes the query represented by a specified expression tree.
        /// </summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the value that results from executing the specified query.</returns>
        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute(expression));
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task containing the result.</returns>
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute<TResult>(expression));
        }
    }
}
