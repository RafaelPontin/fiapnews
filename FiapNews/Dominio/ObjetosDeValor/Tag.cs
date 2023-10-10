using Dominio.Entidades;

namespace Dominio.ObjetosDeValor
{
    public class Tag : Base, IEquatable<Tag>
    {
        public string Texto { get; set; }
        public IReadOnlyCollection<Noticia>? Noticias { get; }

        public bool Equals(Tag? other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            return other.Texto == Texto;
        }
    }
}
