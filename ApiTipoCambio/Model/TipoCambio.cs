
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTipoCambio.Model
{
    public class TipoCambio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int Id { get; set; }
        public decimal MontoTipoCambio { get; set; }
        public decimal Monto { get; set; }
        public string? MonedaOrigen { get; set; }
        public string? MonedaDestino { get; set; }
        public decimal MontoConversion { get; set; }
        public DateTime? Fecha { get; set; }

    }
}
