using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/suppliers")]
[ApiController]
public class SupplierController(SupplierService supplierService) : ControllerBase
{
    private readonly SupplierService _supplierService = supplierService;

}