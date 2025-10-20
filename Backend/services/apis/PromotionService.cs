namespace Backend.Services.Apis;

public class PromotionService(PromotionRepository promotionRepository)
{
    private readonly PromotionRepository _promotionRepository = promotionRepository;

}
