﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.DTOs.Requests
{
    public class AssuntoRequest
    {
        public int? Cod { get; set; }
        public string Descricao { get; set; }
    }
}
