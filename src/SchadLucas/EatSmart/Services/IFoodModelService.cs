using SchadLucas.EatSmart.Data.Types.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Services
{
    public interface IFoodModelService : IService
    {
        /// <summary>
        ///     Adds a new <see cref="Food" /> entity to the database backend.
        /// </summary>
        /// <param name="food">The <see cref="Food" /> entity to add.</param>
        void Add(Food food);

        /// <inheritdoc cref="Add" />
        Task AddAsync(Food food);

        /// <summary>
        ///     Adds a range of <see cref="Food" /> entities to the database backend.
        /// </summary>
        /// <param name="foods">The <see cref="Food" /> entites to add.</param>
        void AddRange(params Food[] foods);

        /// <inheritdoc cref="AddRange(Food[])" />
        void AddRange(IEnumerable<Food> foods);

        /// <inheritdoc cref="AddRange(IEnumerable{Food})" />
        Task AddRangeAsync(IEnumerable<Food> foods);

        /// <inheritdoc cref="AddRange(Food[])" />
        Task AddRangeAsync(Food[] foods);

        /// <summary>
        ///     Deletes <see cref="Food" /> entity.
        /// </summary>
        /// <param name="id">The Id which will be applied to filter by <see cref="Food.Id" />.</param>
        /// <returns>
        ///     <see langword="true" /> if any <see cref="Food" /> entity could be deleted, otherwise <see langword="false" />.
        /// </returns>
        bool Delete(Guid id);

        /// <summary>
        ///     Shortcut for <see cref="Delete(Guid)" />
        /// </summary>
        bool Delete(Food food);

        /// <inheritdoc cref="Delete(Guid)" />
        Task<bool> DeleteAsync(Guid id);

        /// <inheritdoc cref="Delete(Food)" />
        Task<bool> DeleteAsync(Food food);

        /// <summary>
        ///     Shortcut for <see cref="Exists(Guid)" />.
        /// </summary>
        bool Exists(Food food);

        /// <summary>
        ///     Checks if a <see cref="Food" /> entity exists.
        /// </summary>
        /// <param name="id">The Id to look for.</param>
        /// <returns>
        ///     <see langword="true" /> if any food with the passed Id exists, otherwise <see langword="false" />.
        /// </returns>
        bool Exists(Guid id);

        /// <summary>
        ///     Checks if a <see cref="Food" /> entity exists.
        /// </summary>
        /// <param name="name">The name to look for.</param>
        /// <returns>
        ///     <see langword="true" /> if any food with the passed Id exists, otherwise <see langword="false" />.
        /// </returns>
        bool Exists(string name);

        /// <inheritdoc cref="Exists(string)" />
        Task<bool> ExistsAsync(string name);

        /// <inheritdoc cref="Exists(Food)" />
        Task<bool> ExistsAsync(Food food);

        /// <inheritdoc cref="Exists(Guid)" />
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        ///     Tries to find a <see cref="Food" /> entity.
        /// </summary>
        /// <param name="id">The filter which should be applied to filter by <see cref="Food.Id" />.</param>
        /// <returns>A <see cref="Food" /> entity if one was found, otherwise <see langword="null" />.</returns>
        Food Find(Guid id);

        /// <summary>
        ///     Tries to find a <see cref="Food" /> entity.
        /// </summary>
        /// <param name="name">The filter which should be applied to filter by <see cref="Food.Name" />.</param>
        /// <returns>A <see cref="Food" /> entity if one was found, otherwise <see langword="null" />.</returns>
        Food Find(string name);

        /// <inheritdoc cref="Find(Guid)" />
        Task<Food> FindAsync(Guid id);

        /// <inheritdoc cref="Find(string)" />
        Task<Food> FindAsync(string name);

        /// <summary>
        ///     Gets all <see cref="Food" /> entities from the database.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{T}">enumeration</see> of <see cref="Food" /> entities.
        /// </returns>
        IEnumerable<Food> GetAll();

        /// <inheritdoc cref="GetAll" />
        Task<IEnumerable<Food>> GetAllAsync();

        /// <summary>
        ///     Gets all <see cref="Food" /> entities which match the passed <paramref name="condition" />.
        /// </summary>
        /// <param name="condition">The condition which will be applied to filter the results.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}">enumeration</see> of <see cref="Food" /> entities.
        /// </returns>
        IEnumerable<Food> GetWhere(Func<Food, bool> condition);

        /// <inheritdoc cref="GetWhere" />
        Task<IEnumerable<Food>> GetWhereAsync(Func<Food, bool> condition);
    }
}