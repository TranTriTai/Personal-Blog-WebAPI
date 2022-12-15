using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Filter;
using TranTriTaiBlog.Infrastructures.Intefaces;
using TranTriTaiBlog.Infrastructures.Services;

namespace TranTriTaiBlog.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// Post api/posts/categoriess
        /// <summary>
        /// create a category
        /// </summary>
        /// <param name="request">Attributes to be created</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the create</returns>
        [SwaggerOperation(Summary = "Create a new category")]
        [HttpPost("categories",Name = "CreateCategory")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<CreateCategoryResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var result = await _postService.CreateCategory(request);
            return StatusCode(result.StatusCode, result);
        }

        /// Get api/posts/categories
        /// <summary>
        /// get list of categories
        /// </summary>
        /// <returns>CommonResponse with list of categories</returns>
        [SwaggerOperation(Summary = "Get list of categories")]
        [HttpGet("categories" ,Name = "GetListCategories")]
        [ProducesResponseType(typeof(CommonResponse<CategoryResponse[]>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListCategories()
        {
            var result = await _postService.GetListCategories();
            return StatusCode(result.StatusCode, result);
        }

        /// Put api/posts/categories/{categoryId}
        /// <summary>
        /// update a category
        /// </summary>
        /// <param name="categoryId">id of the category</param>
        /// <param name="request">Attributes to be updated</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the create</returns>
        [SwaggerOperation(Summary = "Update a category")]
        [HttpPost("categories/{categoryId}", Name = "UpdatedCategory")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<UpdateCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequest request)
        {
            var response = await _postService.UpdateCategory(categoryId, request);
            return StatusCode(response.StatusCode, response);
        }

        /// DELETE api/posts/categories/{categoryId}
        /// <summary>
        /// delete a category
        /// </summary>
        /// <param name="categoryId">Id of the category</param>
        /// <return>Response with confirmation that the category has been deleted</return>
        [SwaggerOperation(Summary = "Delete a category")]
        [HttpDelete("{categoryId}", Name = "DeleteCategory")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var response = await _postService.DeleteCategory(categoryId);
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary
        /// Add new post for a user
        /// </summary>
        /// <return> a new post for a user</return>
        [SwaggerOperation(Summary = "Create a new post")]
        [HttpPost("{userId}")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(AddPostResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost([FromRoute] Guid userId,[FromBody] AddPostRequest request)
        {
            var result = await _postService.CreatePost(userId, request);
            return StatusCode(result.StatusCode, result);
        }

        /// PUT api/posts/{postId}
        /// <summary>
        /// update Post
        /// </summary>
        /// <param name="postId">Id of the Post</param>
        /// <param name="request">Attributes to be updated</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the update </returns>
        [SwaggerOperation(Summary = "Update a post")]
        [HttpPut("{postId}", Name = "UpdatePost")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<UpdatePostResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var result = await _postService.UpdatePost(postId, request);
            return StatusCode(result.StatusCode, result);
        }

        /// DELETE api/posts/{postId}
        /// <summary>
        /// delete a post
        /// </summary>
        /// <param name="postId">Id of the post</param>
        /// <return>Response with confirmation that the post has been deleted</return>
        [SwaggerOperation(Summary = "Delete a post")]
        [HttpDelete("postDelete/{postId}", Name = "DeletePost")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePost([FromRoute] Guid postId)
        {
            var response = await _postService.DeletePost(postId);
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();
            return StatusCode(response.StatusCode, response);
        }

        /// Get api/posts
        /// <summary>
        /// get list of posts
        /// </summary>
        /// <returns>CommonResponse with list of posts</returns>
        [SwaggerOperation(Summary = "Get list of posts with pagination")]
        [HttpGet(Name = "GetListPostsWithPagination")]
        [ProducesResponseType(typeof(CommonResponse<ListPostResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListPostsWithPagination([FromQuery] PaginationRequest request)
        {
            var result = await _postService.GetListPostsWithPagination(request);

            return StatusCode(result.StatusCode, result);
        }
    }
}

