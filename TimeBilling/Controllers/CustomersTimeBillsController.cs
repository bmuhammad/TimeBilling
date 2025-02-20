﻿using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using TimeBilling.Data;

namespace TimeBilling.Controllers;

[Route("/api/customers/{id:int}/timebills")]
public class CustomersTimeBillsController : ControllerBase
{
    private readonly ILogger<CustomersTimeBillsController> _logger;
    private readonly IBillingRepository _repository;

    public CustomersTimeBillsController(ILogger<CustomersTimeBillsController> logger,
        IBillingRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<TimeBill>>> GetCustomerTimeBills(int id)
    {
        var result = await _repository.GetTimeBillsForCustomer(id);

        if (result is not null) return Ok(result);
        return BadRequest();
        
    }

    [HttpGet("{billId:int}")]
    public async Task<ActionResult<IEnumerable<TimeBill>>> GetCustomerTimeBills(int id, int billId)
    {
        var result = await _repository.GetTimeBillForCustomer(id, billId);

        if (result is not null) return Ok(result);
        return BadRequest();
    }

}
