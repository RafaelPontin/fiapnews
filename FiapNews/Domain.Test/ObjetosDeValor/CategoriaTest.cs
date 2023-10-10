using Dominio.ObjetosDeValor;

namespace Domain.Test.ObjetosDeValor;

public class CategoriaTest
{
    [Fact]
    public void TestaCategoriaValida()
    {
        Categoria categoria = new Categoria("Descricao Categoria");
        Assert.Equal(categoria.Descricao.Length < 100, true);
    }

    [Fact]
    public void TestaDescricaoCategoriaNull()
    {
        Categoria categoria = null;
        try
        {
            categoria = new Categoria(null);
        }
        catch (ArgumentNullException ex)
        {
            Assert.Equal(categoria, null);
        }
    }

    [Fact]
    public void TesteTamnahoMaiorQue100Caraxteres()
    {
        Categoria categoria = null;
        try
        {
            categoria = new Categoria("teste tamanho 111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal(categoria, null);
        }
    }

    [Fact]
    public void TestaAlterarDescricaoCategoria()
    {
        Categoria categoria = new Categoria("Descricao Categoria");
        string novaCategoria = "Descricao Categoria Update";
        categoria.DefinirDescricao(novaCategoria);
        Assert.True(categoria.Descricao.Equals(novaCategoria));
    }

}