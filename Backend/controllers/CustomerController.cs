using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/customers")]
[ApiController]
public class CustomerController(CustomerService customerService) : ControllerBase
{
    private readonly CustomerService _customerService = customerService;

}