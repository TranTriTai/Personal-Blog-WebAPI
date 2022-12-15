using System;
using System.ComponentModel.DataAnnotations;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class PaginationRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int Size { get; set; } = 5;

        public int GetSkip() => (Page - 1) * Size;
    }
}

