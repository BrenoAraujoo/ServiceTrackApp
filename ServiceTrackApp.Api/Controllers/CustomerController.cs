using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.Customer;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Api.Controllers;


[ApiController]
public class CustomerController : ApiControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("v1/customer")]
    public IActionResult Create([FromBody] CustomerCreateModel model)
    {
        return null;
    }
}