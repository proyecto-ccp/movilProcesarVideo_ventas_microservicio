﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videos.Aplicacion.Dto
{
    public class VideoIn
    {
        //public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Video { get; set; }
    }
}
