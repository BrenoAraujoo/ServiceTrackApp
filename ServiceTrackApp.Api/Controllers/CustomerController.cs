using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.Customer;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

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
    public async Task<IActionResult> Create([FromBody] CustomerCreateModel model)
    {
        var result = await _customerService.Create(model);
        return result.IsSuccess ? Ok(result) : ApiControllerHandleResult(result);
    }

    [HttpGet("v1/customer")]
    public async Task<IActionResult> Get([FromQuery] CustomerFilter filter, [FromQuery] PaginationRequest pagination)
    {
        var result = await _customerService.GetAll(filter, pagination);
        return Ok(result);
    }

    [HttpPut("v1/customer/{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CustomerUpdateModel model)
    {
        var result = await _customerService.Update(id, model);
        return result.IsSuccess ? Ok(result) : ApiControllerHandleResult(result);
    }}