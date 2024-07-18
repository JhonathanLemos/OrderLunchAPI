using Lanches.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Lanches.Application
{
    public static class Extensions
    {
        public static string ToDashCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('-');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            Console.WriteLine($"ToDashCase: " + sb.ToString());

            return sb.ToString();
        }

        public static async Task<PaginationResult<T>> GetPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize) where T : class
        {
            var result = new PaginationResult<T>();

            result.Page = page;
            result.PageSize = pageSize;
            result.ItemsCount = await query.CountAsync();


            var pageCount = (double)result.ItemsCount / pageSize;
            result.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;


            result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static PaginationResult<TNew> ReplaceData<T, TNew>(this PaginationResult<T> paginationResult, List<TNew> newData)
        {
            return new PaginationResult<TNew>(
                paginationResult.Page,
                paginationResult.TotalPages,
                paginationResult.PageSize,
                paginationResult.ItemsCount,
                newData
            );
        }
    }
}
