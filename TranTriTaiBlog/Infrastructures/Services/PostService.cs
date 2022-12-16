using System;
using System.Linq;
using AutoMapper;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Filter;
using TranTriTaiBlog.Infrastructures.Constants;
using TranTriTaiBlog.Infrastructures.Helper.Constants;
using TranTriTaiBlog.Infrastructures.Helper.MessageUtil;
using TranTriTaiBlog.Infrastructures.Intefaces;
using TranTriTaiBlog.Infrastructures.Intefaces.UserServices;
using TranTriTaiBlog.Infrastructures.Services.UserServices;

namespace TranTriTaiBlog.Infrastructures.Services
{
    public partial class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IMapper mapper, ILogger<UserService> logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResponse<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request)
        {
            if (request == null || request.Category == null)
            {
                return new CommonResponse<CreateCategoryResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Category)), null);
            }

            try
            {
                var inValids = ValidateCreateCategoryRequest(request);
                if (inValids.ContainsKey(JsonPropertyNames.Category))
                {
                    return new CommonResponse<CreateCategoryResponse>(StatusCodes.Status400BadRequest,
                ErrorMsgUtil.GetBadRequestMsg(nameof(Category)), null);
                }

                IRepository<Category> repo = _unitOfWork.GetRepository<Category>();
                IQueryable<Category> category = repo.GetQueryable()
                    .OrderByDescending(x => x.CreatedAt);
                
                if (category.Any(x => x.Name == request.Category))
                {
                    return new CommonResponse<CreateCategoryResponse>(StatusCodes.Status422UnprocessableEntity,
                        ErrorMsgUtil.GetUnprocessibleMsg(nameof(Category)), null);
                }

                var categoryEntity = _mapper.Map<Category>(request);

                await repo.AddAsync(categoryEntity);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<CreateCategoryResponse>(StatusCodes.Status201Created,
                    _mapper.Map<CreateCategoryResponse>(categoryEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(Post)));
            }
        }

        public async Task<CommonResponse<AddPostResponse>> CreatePost(Guid userId, AddPostRequest request)
        {
            if (userId == Guid.Empty || request == null)
            {
                return new CommonResponse<AddPostResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Post)), null);
            }

            try
            {
                var inValids = ValidateAddPostRequest(request);
                if (inValids.Count > 0)
                {
                    return new CommonResponse<AddPostResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Post)), null);
                }

                IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
                User user = await userRepo.GetByIdAsync(userId);
                if (user.Id != userId)
                {
                    return new CommonResponse<AddPostResponse>(StatusCodes.Status422UnprocessableEntity,
                        ErrorMsgUtil.GetUnprocessibleMsg(nameof(User)), null);
                }

                var postEntity = _mapper.Map<Post>(request);
                postEntity.OwnerId = userId;

                IRepository<Post> repo = _unitOfWork.GetRepository<Post>();
                await repo.AddAsync(postEntity);
                await _unitOfWork.CompleteAsync();

                var response = _mapper.Map<AddPostResponse>(postEntity);

                return new CommonResponse<AddPostResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(Post)));
            }
        }

        public async Task<CommonResponse<string>> DeleteCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Category)), null);
            }

            try
            {
                IRepository<Category> repo = _unitOfWork.GetRepository<Category>();
                Category category = await repo.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(Category)), null);
                }

                repo.HardDelete(category);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<string>(StatusCodes.Status204NoContent, "Delete a category Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(DeleteCategory)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Category)));
            }
        }

        public async Task<CommonResponse<CategoryResponse[]>> GetListCategories()
        {
            try
            {
                IRepository<Category> repo = _unitOfWork.GetRepository<Category>();
                IQueryable<Category> categoryList = repo.GetQueryable()
                    .OrderByDescending(x => x.CreatedAt);

                int count = await categoryList.CountAsync();
                if(count == 0)
                {
                    return new CommonResponse<CategoryResponse[]>(StatusCodes.Status204NoContent);
                }

                var response = _mapper.Map<CategoryResponse[]>(categoryList);
                return new CommonResponse<CategoryResponse[]>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(GetListCategories)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Category)));
            }
        }

        public async Task<CommonResponse<UpdateCategoryResponse>> UpdateCategory(Guid categoryId, UpdateCategoryRequest request)
        {
            if (categoryId == Guid.Empty || request == null
                || request.Category == null)
            {
                return new CommonResponse<UpdateCategoryResponse>(StatusCodes.Status400BadRequest, ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                var inValids = ValidateUpdateCategoryRequest(request);
                if (inValids.Count > 0)
                {
                    return new CommonResponse<UpdateCategoryResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Category)), null);
                }

                IRepository<Category> repo = _unitOfWork.GetRepository<Category>();
                Category category = await repo.GetByIdAsync(categoryId);

                if (category == null)
                {
                    return new CommonResponse<UpdateCategoryResponse>(StatusCodes.Status404NotFound,
                   ErrorMsgUtil.GetCannotFindMsg(nameof(Category)), null);
                }

                _mapper.Map(request, category);
                repo.Update(category);
                await _unitOfWork.CompleteAsync();
                var response = _mapper.Map<UpdateCategoryResponse>(category);
                return new CommonResponse<UpdateCategoryResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(UpdateCategory)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Category)));
            }
        }

        public async Task<CommonResponse<UpdatePostResponse>> UpdatePost(Guid postId, UpdatePostRequest request)
        {
            if (postId == Guid.Empty || request == null)
            {
                return new CommonResponse<UpdatePostResponse>(StatusCodes.Status400BadRequest, ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                var inValids = ValidateUpdatePostRequest(request);
                if (inValids.Count > 0)
                {
                    return new CommonResponse<UpdatePostResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Post)), null);
                }

                IRepository<Post> repo = _unitOfWork.GetRepository<Post>();
                Post post = await repo.GetByIdAsync(postId);

                if ( post == null)
                {
                    return new CommonResponse<UpdatePostResponse>(StatusCodes.Status404NotFound,
                    ErrorMsgUtil.GetCannotFindMsg(nameof(Post)), null);
                }

                _mapper.Map(request, post);
                repo.Update(post);
                await _unitOfWork.CompleteAsync();
                var response = _mapper.Map<UpdatePostResponse>(post);
                return new CommonResponse<UpdatePostResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(UpdatePost)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Post)));
            }
        }

        public async Task<CommonResponse<string>> DeletePost(Guid postId)
        {
            if (postId == Guid.Empty)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Post)), null);
            }
            try
            {
                IRepository<Post> repo = _unitOfWork.GetRepository<Post>();
                Post post = await repo.GetByIdAsync(postId);

                if (post == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(Post)), null);
                }

                repo.HardDelete(post);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<string>(StatusCodes.Status204NoContent, "Delete a post Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(DeletePost)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Post)));
            }
        }

        public async Task<CommonResponse<ListPostResponse>> GetListPostsWithPagination(PaginationRequest request)
        {
            if (request == null)
            {
                return new CommonResponse<ListPostResponse>(StatusCodes.Status400BadRequest, ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                IRepository<Post> repo = _unitOfWork.GetRepository<Post>();
                IQueryable<Post> queryable = repo.GetQueryable()
                    .OrderByDescending(x => x.CreatedAt);

                int totalCount = await queryable.CountAsync();

                if(totalCount == 0)
                {
                    return new CommonResponse<ListPostResponse>(StatusCodes.Status204NoContent);
                }

                Post[] posts = await queryable
                    .Skip(request.GetSkip())
                    .Take(request.Size)
                    .ToArrayAsync();

                var response = _mapper.Map<ListPostResponse>(posts);
                response.Pagination = new PaginationResponse(request, totalCount);
                return new CommonResponse<ListPostResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(DeletePost)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Post)));
            }
        }

        public async Task<CommonResponse<ListPostResponse>> SearchPosts(SearchPostsRequest request)
        {
            if (request == null)
            {
                request = new SearchPostsRequest();
            }

            try
            {
                IQueryable<Post> queryable = FilterPosts(request);

                Post[] result = await queryable
                    .Skip(request.GetSkip())
                    .Take(request.Size)
                    .ToArrayAsync();

                int totalCount = await queryable.CountAsync();

                if (totalCount == 0)
                {
                    return new CommonResponse<ListPostResponse>(StatusCodes.Status204NoContent);
                }

                var response = _mapper.Map<ListPostResponse>(result);
                response.Pagination = new PaginationResponse(request, totalCount);
                return new CommonResponse<ListPostResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(SearchPosts)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Post)));
            }
        }

        private IQueryable<Post> FilterPosts(SearchPostsRequest request)
        {
            IRepository<Post> repo = _unitOfWork.GetRepository<Post>();
            IQueryable<Post> posts = repo.GetQueryable()
                .OrderByDescending(x => x.CreatedAt);

            if (string.IsNullOrEmpty(request.Title) == false)
            {
                posts = posts.Where(x => x.Title.Contains(request.Title));
            }

            if (string.IsNullOrEmpty(request.Description) == false)
            {
                posts = posts.Where(x => x.Description.Contains(request.Description));
            }

            if (string.IsNullOrEmpty(request.Tag) == false)
            {
                posts = posts.Where(x => x.Tag.Contains(request.Tag));
            }

            if (string.IsNullOrEmpty(request.ReadingDuration) == false)
            {
                posts = posts.Where(x => x.ReadingDuration.Contains(request.ReadingDuration));
            }

            return posts;
        }

        private Dictionary<string, string> ValidateCreateCategoryRequest(CreateCategoryRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Category))
            {
                inValids.Add(JsonPropertyNames.Category, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Category)));
            }

            return inValids;
        }

        private Dictionary<string, string> ValidateUpdateCategoryRequest(UpdateCategoryRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Category))
            {
                inValids.Add(JsonPropertyNames.Category, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Category)));
            }

            return inValids;
        }

        private Dictionary<string, string> ValidateAddPostRequest(AddPostRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Title))
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            else if (string.IsNullOrEmpty(request.Description))
            {
                inValids.Add(JsonPropertyNames.Description, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Description)));
            }

            else if (request.CategoryId == Guid.Empty)
            {
                inValids.Add(JsonPropertyNames.CategoryId, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.CategoryId)));
            }

            return inValids;
        }

        private Dictionary<string, string> ValidateUpdatePostRequest(UpdatePostRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Title))
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            else if (string.IsNullOrEmpty(request.Description))
            {
                inValids.Add(JsonPropertyNames.Description, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Description)));
            }

            else if (request.CategoryId == Guid.Empty)
            {
                inValids.Add(JsonPropertyNames.CategoryId, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.CategoryId)));
            }

            return inValids;
        }
    }
}

