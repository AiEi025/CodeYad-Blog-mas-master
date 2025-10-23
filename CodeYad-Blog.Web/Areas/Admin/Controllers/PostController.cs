using CodeYad_Blog.CoreLayer.DTOs.Posts;
using CodeYad_Blog.CoreLayer.Services.Posts;
using CodeYad_Blog.CoreLayer.Utilities;
using CodeYad_Blog.Web.Areas.Admin.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index(int pageid = 1, string title = null, string categoryslug = null)
        {
            var model = _postService.GetPostsByFilter(new CoreLayer.DTOs.Posts.PostFilterParams()
            {
                CategorySlug = categoryslug,
                PageId = pageid,
                Title = title,
                Take = 20
            });
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createPostViewModel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var result = _postService.Createpost(new CreatePostDTO()
            {
                CategoryId = createPostViewModel.CategoryId,
                Title = createPostViewModel.Title,
                ImageFile = createPostViewModel.ImageFile,
                Description = createPostViewModel.Description,
                Slug = createPostViewModel.Slug,
                SubCategoryId = createPostViewModel.SubCategoryId,
                UserId = User.GetUserId()
            });
            if (result.Status != OperationResultStatus.Success)
                return View();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if(post == null)
                return RedirectToAction("Index");
            var model = new EditPostViewModel()
            {
                CategoryId = post.CategoryId,
                Description = post.Description,
                Slug = post.Slug,
                SubCategoryId = post.SubCategoryId
            };
            return View();
        }
        public IActionResult Edit(EditPostViewModel editPostViewModel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var result = _postService.EditPost(new EditPostDTO()
            {
                CategoryId = editPostViewModel.CategoryId,
                Title = editPostViewModel.Title,
                ImageFile = editPostViewModel.ImageFile,
                Description = editPostViewModel.Description,
                Slug = editPostViewModel.Slug,
                SubCategoryId = editPostViewModel.SubCategoryId
            });
            return View();
        }
    }
}
