using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.Helper
{

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = String.Empty;

        public T? Data { get; set; }

        public ApiError? Error { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,

            };
        }

        public static ApiResponse<T> FailureResponse(string message, string details, string code)
        {

            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Error = new ApiError { Code = code, Details = details }
            };
        }
    }

    public class ApiError
    {
        public string Code { get; set; } = String.Empty;

        public string Details { get; set; } = String.Empty;
    }

    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public PagedResponse(IEnumerable<T> Data, int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            this.Data = Data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;

            this.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            this.Success = true;
            this.Message = "Success";


        }
    }
}
