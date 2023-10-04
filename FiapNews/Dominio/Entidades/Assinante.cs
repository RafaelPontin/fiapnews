namespace Dominio.Entidades
{
    public class Assinante : Usuario
    {
        public Assinante()
        {

        }
        public Assinante(string nome, string login, string senha, string email, string foto)
            : base(nome, login, senha, email, foto)
        {

        }
        public Guid IdAssinatura { get; private set; }
        //public virtual Assinatura Assinatura { get; set; }

        public virtual ICollection<Comentario>? Comentarios { get; private set; }

        public void AdicionarAssinatura (Guid idAssinatura)
        {            
            if (Guid.Empty == idAssinatura)
                throw new ArgumentException($"Informe o Id da Assinatura.", nameof(IdAssinatura));

            IdAssinatura = idAssinatura;
        }

        public bool PodeComentar()
        {
            // validar assinatura
            return true;
        }
    }
}
