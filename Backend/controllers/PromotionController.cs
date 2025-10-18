using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/promotions")]
[ApiController]
public class PromotionController(PromotionService promotionService) : ControllerBase
{
    private readonly PromotionService _promotionService = promotionService;

}