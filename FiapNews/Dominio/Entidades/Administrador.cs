namespace Dominio.Entidades;

public class Administrador : Usuario
{
    public Administrador()
    {

    }
    public Administrador(string nome, string login, string senha, string email, string foto)
        : base(nome, login, senha, email, foto)
    {

    }
}

