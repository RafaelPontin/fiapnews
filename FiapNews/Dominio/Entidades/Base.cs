﻿namespace Dominio.Entidades
{
    public class Base
    {
        public Guid Id { get; private set; }

        public Base()
        {
            Id = Guid.NewGuid();
        }
    }
}
