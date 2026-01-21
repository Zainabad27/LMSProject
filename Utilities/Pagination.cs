using Microsoft.EntityFrameworkCore;

namespace LmsApp2.Api.Utilities
{
    public class Pagination<T>
    {
        public ICollection<T> Items { get; }
        public int PageNumber { get; } = 1;

        public int PageSize { get; } = 10;

        public int TotalItems { get; } = 0;

        public bool HasNextPage => PageNumber * PageSize < TotalItems;

        public bool HasPreviousPage => PageNumber > 1;


        private Pagination(ICollection<T> items, int pageNumber, int pageSize, int totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> items, int pageNumber, int pageSize)
        {
            int totalItems = await items.CountAsync();
            ICollection<T> pagedItems = await items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Pagination<T>(pagedItems, pageNumber, pageSize, totalItems);
        }
    }
}
