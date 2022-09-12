
namespace FastFood.Domain.Dto
{
    public class ApiListResponseDto<T> : ApiResponseDto<List<T>>
    {
        public ApiListResponseDto()
        {
            Result = new List<T>();
        }

        public ApiListResponseDto(List<T> results) : base(results)
        {
        }
    }
}
