using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Domain.Dtos
{
    public class RecivedCoinDto
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Value { get; set; }
    }
}
