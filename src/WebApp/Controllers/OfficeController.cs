using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("offices")]
[Produces("application/json")]
public class OfficeController : ControllerBase
{
    private IValidator<OfficeCreateDto> ValidatorCreate { get; }
    private IValidator<OfficeUpdateDto> ValidatorUpdate { get; }

    private IOfficeService Service { get; }
    private IMapper Mapper { get; }

    public OfficeController(
        IOfficeService service,
        IMapper mapper,
        IValidator<OfficeCreateDto> validatorCreate,
        IValidator<OfficeUpdateDto> validatorUpdate)
    {
        Service = service;
        Mapper = mapper;
        ValidatorCreate = validatorCreate;
        ValidatorUpdate = validatorUpdate;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(ICollection<OfficeDto>))]
    public async Task<IActionResult> GetAll(
        [Required] int offset = 0,
        [Required] [Range(0, 50)] int limit = 50)
    {
        var offices = await Service.GetAllAsync(offset, limit);

        return Ok(offices);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(OfficeDto))]
    public async Task<IActionResult> GetOneById([Required] int id)
    {
        try
        {
            var office = await Service.GetByIdAsync(id);
            return Ok(office);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id:int}/change-status")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(OfficeDto))]
    public async Task<IActionResult> ChangeStatus([Required] int id, [Required] bool newStatus)
    {
        try
        {
            var office = await Service.ChangeStatusAsync(id, newStatus);
            return Ok(office);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(415)]
    [ProducesResponseType(409)]
    [ProducesResponseType(201, Type = typeof(OfficeDto))]
    public async Task<IActionResult> Create([FromBody] OfficeCreateDto newOffice)
    {
        var result = await ValidatorCreate.ValidateAsync(newOffice);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        try
        {
            var createdOffice = await Service.CreateAsync(newOffice);

            return CreatedAtAction(nameof(GetOneById), new { id = createdOffice.Id }, createdOffice);
        }
        catch (AlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(415)]
    [ProducesResponseType(409)]
    [ProducesResponseType(201, Type = typeof(OfficeDto))]
    public async Task<IActionResult> Update(int id, [FromBody] OfficeUpdateDto updatedOffice)
    {
        var result = await ValidatorUpdate.ValidateAsync(updatedOffice);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        try
        {
            var office = await Service.UpdateAsync(id, updatedOffice);

            return Ok(office);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (AlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
