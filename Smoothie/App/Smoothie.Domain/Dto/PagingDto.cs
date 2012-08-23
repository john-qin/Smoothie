
namespace Smoothie.Domain.Dto
{
    public class PagingDto
    {
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }

        public PagingDto()
        {

        }

    }
}
