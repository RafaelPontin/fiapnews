﻿using Dominio.Enum;

namespace Aplicacao.DTOs
{
    public class UsuarioDto: BaseDto
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}