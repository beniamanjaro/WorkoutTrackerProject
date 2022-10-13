namespace WorkoutTracker.Presentation.DTOs
{
    public class PaginationDto
    {
        public PaginationDto()
        {
            PageNumber = 1;
            PageSize = 40;
        }
        public PaginationDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 40 ? 40 : pageSize;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
