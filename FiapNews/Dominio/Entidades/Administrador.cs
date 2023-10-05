<<<<<<< HEAD
namespace Dominio.Entidades;

public class Administrador : Usuario
{
    public Administrador()
    {

    }
    public Administrador(string nome, string login, string senha, string email, string foto)
        : base(nome, login, senha, email, foto)
=======
using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Administrador : Usuario
>>>>>>> 805759053df6e8cd728b6ad97514d8199645f41f
    {

<<<<<<< HEAD
=======
        }
        public Administrador(string nome, string login, string senha, string email, string foto)
            : base(nome, login, senha, email, foto, TipoUsuario.ADMINISTRADOR)
        {

        }
>>>>>>> 805759053df6e8cd728b6ad97514d8199645f41f
    }
}

