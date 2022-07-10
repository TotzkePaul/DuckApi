using System.ComponentModel.DataAnnotations;
namespace DuckApi
{
    public class Pagination
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Count of items at the page
        /// </summary>
        [Range(1,100)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Sorted by field name
        /// </summary>
        public string? SortBy { get; set; } = "date_desc";

        internal int StartAt { 
            get
            {
                return (Page - 1) * PageSize;
            } 
        }
    }
}
