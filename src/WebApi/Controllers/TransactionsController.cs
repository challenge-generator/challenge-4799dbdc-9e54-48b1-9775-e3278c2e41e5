using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities;
using Application.DTOs;
using Application.UseCases;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;
        
        public TransactionsController(
            IMediator mediator,
            ILogger<TransactionsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] TransactionType? type = null,
            [FromQuery] TransactionStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            if (page < 1)
            {
                return BadRequest("El número de página debe ser mayor o igual a 1");
            }
            
            if (pageSize < 1 || pageSize > 1000)
            {
                return BadRequest("El tamaño de página debe estar entre 1 y 1000");
            }
            
            _logger.LogInformation(
                "Consultando transacciones - Página: {Page}, Tamaño: {PageSize}, Tipo: {Type}, Estado: {Status}",
                page, pageSize, type, status);
            
            try
            {
                var query = new GetAllTransactionsQuery(page, pageSize, type, status);
                var result = await _mediator.Send(query, cancellationToken);
                
                _logger.LogInformation("Se recuperaron {Count} transacciones", result.Count());
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar transacciones");
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Consultando transacción con ID: {TransactionId}", id);
            
            try
            {
                var query = new GetTransactionByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(
            [FromBody] CreateTransactionDto createDto,
            CancellationToken cancellationToken = default)
        {
            if (createDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío");
            }
            
            if (string.IsNullOrWhiteSpace(createDto.Description))
            {
                return BadRequest("La descripción es obligatoria");
            }
            
            if (createDto.Amount <= 0)
            {
                return BadRequest("El monto debe ser mayor que cero");
            }
            
            _logger.LogInformation(
                "Creando transacción: {Description}, Monto: {Amount}, Tipo: {Type}",
                createDto.Description, createDto.Amount, createDto.Type);
            
            try
            {
                var command = new CreateTransactionCommand(
                    createDto.Description,
                    createDto.Amount,
                    createDto.Type,
                    createDto.Category);
                
                var result = await _mediator.Send(command, cancellationToken);
                
                _logger.LogInformation(
                    "Transacción creada exitosamente con ID: {TransactionId}",
                    result.Id);
                
                return CreatedAtAction(
                    nameof(GetTransactionById),
                    new { id = result.Id },
                    result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear transacción");
                return UnprocessableEntity(ex.Errors.Select(e => new 
                {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear transacción");
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(
            [FromRoute] Guid id,
            [FromBody] UpdateTransactionDto updateDto,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            if (updateDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío");
            }
            
            _logger.LogInformation("Actualizando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new UpdateTransactionCommand(
                    id,
                    updateDto.Description,
                    updateDto.Amount,
                    updateDto.Status);
                
                var result = await _mediator.Send(command, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada para actualizar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción actualizada exitosamente: {TransactionId}", id);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> DeleteTransaction(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Eliminando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new DeleteTransactionCommand(id);
                var result = await _mediator.Send(command, cancellationToken);
                
                if (!result)
                {
                    _logger.LogWarning("Transacción no encontrada para eliminar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción eliminada exitosamente: {TransactionId}", id);
                
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflicto al eliminar transacción {TransactionId}", id);
                return Conflict($"No se puede eliminar la transacción {id}: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
        
        [HttpPost("{id:guid}/process")]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<TransactionDto>> ProcessTransaction(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID de transacción no puede estar vacío");
            }
            
            _logger.LogInformation("Procesando transacción con ID: {TransactionId}", id);
            
            try
            {
                var command = new ProcessTransactionCommand(id);
                var result = await _mediator.Send(command, cancellationToken);
                
                if (result == null)
                {
                    _logger.LogWarning("Transacción no encontrada para procesar: {TransactionId}", id);
                    return NotFound($"Transacción con ID {id} no encontrada");
                }
                
                _logger.LogInformation("Transacción procesada exitosamente: {TransactionId}", id);
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Conflicto al procesar transacción {TransactionId}", id);
                return Conflict($"Error al procesar la transacción {id}: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar transacción {TransactionId}", id);
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }
    }
    
    public record GetAllTransactionsQuery(
        int Page,
        int PageSize,
        TransactionType? Type,
        TransactionStatus? Status) : IRequest<IEnumerable<TransactionDto>>;
    
    public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto?>;
    
    public record CreateTransactionCommand(
        string Description,
        decimal Amount,
        TransactionType Type,
        string? Category) : IRequest<TransactionDto>;
    
    public record UpdateTransactionCommand(
        Guid Id,
        string? Description,
        decimal? Amount,
        TransactionStatus? Status) : IRequest<TransactionDto?>;
    
    public record DeleteTransactionCommand(Guid Id) : IRequest<bool>;
    
    public record ProcessTransactionCommand(Guid Id) : IRequest<TransactionDto?>;
}