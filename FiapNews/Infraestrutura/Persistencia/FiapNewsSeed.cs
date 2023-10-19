using Dominio.Entidades;
using Dominio.ObjetosDeValor;

namespace Infraestrutura.Persistencia
{
    public static class FiapNewsSeed
    {
        public static void Seed(FiapNewsContext context)
        {
            if (!context.Administradores.Any())
            {
                context.Administradores.AddRange(Administradores());
                context.Autores.AddRange(Autores());
                context.Assinantes.AddRange(Assinantes());
                context.RedesSociais.AddRange(RedesSociais());
                context.Tags.AddRange(Tags());
                context.Categorias.AddRange(Categorias());
                context.SaveChanges();
            }            
        }

        private static List<Administrador> Administradores()
        {
            return new List<Administrador>()
            {
                new Administrador("Administrador", "admin", "123456", "admin@email.com", "foto.jpg"),
            };
        }

        private static List<Autor> Autores()
        {
            return new List<Autor>()
            {
                new Autor("Autor", "autor", "123456", "autor@email.com", "foto.jpg", "Descrição do autor"),
            };
        }

        private static List<Assinante> Assinantes()
        {
            return new List<Assinante>()
            {
                new Assinante("Assinante", "assinante", "123456", "autor@email.com", "foto.jpg"),
            };
        }

        private static List<RedeSocial> RedesSociais()
        {
            return new List<RedeSocial>()
            {
                new RedeSocial("Facebook", "Facebook"),
                new RedeSocial("Instagram", "Instagram"),
                new RedeSocial("Twitter", "Twitter"),
                new RedeSocial("WhatsApp", "WhatsApp"),
            };
        }
        private static List<Tag> Tags()
        {
            return new List<Tag>()
            {
                new Tag("Flamengo"),
                new Tag("Vasco"),
                new Tag("EUA"),
                new Tag("Israel"),                
            };
        }
        private static List<Categoria> Categorias()
        {
            return new List<Categoria>()
            {
                new Categoria("Futebol"),
                new Categoria("Politica"),
                new Categoria("Economia"),
                new Categoria("Mundo"),
            };
        }
    }
}
