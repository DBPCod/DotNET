using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

}