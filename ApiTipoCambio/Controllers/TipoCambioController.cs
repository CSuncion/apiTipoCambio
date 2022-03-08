using ApiTipoCambio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiTipoCambio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Activar para el uso de POSTMAN
    //[Authorize]
    public class TipoCambioController : ControllerBase
    {
        private readonly TipoCambioDbContext _context;

        public TipoCambioController(TipoCambioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoCambio>>> GetTipoCambio()
        {
            return await _context.TipoCambio.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCambio>> GetTipoCambioById(int id)
        {
            return await _context.TipoCambio.SingleOrDefaultAsync(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tip = await _context.TipoCambio.SingleOrDefaultAsync(x => x.Id == id);
            if (tip == null) return NotFound("Tipo de cambio con el Id " + id + " no existe");

            _context.TipoCambio.RemoveRange(tip);
            _context.SaveChangesAsync();

            return Ok("Tipo de cambio con el Id " + id + " fue eliminado");
        }

        [HttpPost]
        public async Task<ActionResult> AddTipoCambio(TipoCambio tipoCambio)
        {
            decimal monto = tipoCambio.Monto;
            string monedaOrigen = tipoCambio.MonedaOrigen.ToString();
            string monedaDestino = tipoCambio.MonedaDestino.ToString();


            if (monedaOrigen == "USD" && monedaDestino == "SOL")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(3.75);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            if (monedaOrigen == "SOL" && monedaDestino == "USD")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(0.27);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            if (monedaOrigen == "SOL" && monedaDestino == "EUR")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(0.24);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            if (monedaOrigen == "EUR" && monedaDestino == "SOL")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(4.10);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            if (monedaOrigen == "USD" && monedaDestino == "EUR")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(0.91);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            if (monedaOrigen == "EUR" && monedaDestino == "USD")
            {
                tipoCambio.MontoTipoCambio = Convert.ToDecimal(1.09);
                tipoCambio.MontoConversion = monto * tipoCambio.MontoTipoCambio;
            }

            tipoCambio.Fecha = DateTime.Now;

            _context.TipoCambio.AddAsync(tipoCambio);
            _context.SaveChangesAsync();
            return Created("/api/tipocambio/" + tipoCambio.Id, tipoCambio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTipoCambio(int id, TipoCambio tipoCambio)
        {
            var tip = await _context.TipoCambio.SingleOrDefaultAsync(x => x.Id == id);
            if (tip == null) return NotFound("Tipo de cambio con el Id " + id + " no existe");

            if (tipoCambio.MontoTipoCambio != 0) tip.MontoTipoCambio = tipoCambio.MontoTipoCambio;
            if (tipoCambio.Monto != 0) tip.Monto = tipoCambio.Monto;
            if (tipoCambio.MonedaOrigen != null) tip.MonedaOrigen = tipoCambio.MonedaOrigen;
            if (tipoCambio.MonedaDestino != null) tip.MonedaDestino = tipoCambio.MonedaDestino;
            if (tipoCambio.MontoConversion != 0) tip.MontoConversion = tipoCambio.MontoConversion;
            if (!tipoCambio.Fecha.HasValue) tip.Fecha = tipoCambio.Fecha;

            _context.TipoCambio.UpdateRange(tip);
            _context.SaveChangesAsync();
            return Ok("Tipo de cambio con el Id " + id + " fue actualizado");
        }
    }
}
