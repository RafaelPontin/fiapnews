using Aplicacao.DTOs;

namespace Aplicacao.Mensagem;

public interface IRabbit
{
    void Send(Object entity, string fila);
}
