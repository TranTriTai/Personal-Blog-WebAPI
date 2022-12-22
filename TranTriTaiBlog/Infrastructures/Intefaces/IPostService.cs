using System;
using System.Threading.Tasks;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;

namespace TranTriTaiBlog.Infrastructures.Intefaces
{
    public interface IPostService
    {
        Task<CommonResponse<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request);

        Task<CommonResponse<CategoryResponse[]>> GetListCategories();

        Task<CommonResponse<UpdateCategoryResponse>> UpdateCategory(Guid categoryId, UpdateCategoryRequest request);

        Task<CommonResponse<string>> DeleteCategory(Guid categoryId);

        Task<CommonResponse<AddPostResponse>> CreatePost(Guid userId, AddPostRequest request);

        Task<CommonResponse<UpdatePostResponse>> UpdatePost(Guid postId, UpdatePostRequest request);

        Task<CommonResponse<string>> DeletePost(Guid postId);

        Task<CommonResponse<ListPostResponse>> GetListPostsWithPagination(PaginationRequest request);

        Task<CommonResponse<ListPostResponse>> SearchPosts(SearchPostsRequest request);
    }
}

