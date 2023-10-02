﻿using Dominio.ObjetosDeValor;

namespace Dominio.Entidades
{
    public class Noticia : Base
    {
        private const int LIMITE_TITULO = 250;
        private const int LIMITE_SUBTITULO = 120;
        private const int LIMITE_CONTEUDO = 10000;
        private const int LIMITE_LEAD = 1000;
        private const int LIMITE_IMAGENS = 5;
        private const int LIMITE_AUTORES = 5;
        private const int LIMITE_CATEGORIAS = 5;
        private const string LINK_COMPARTILHAMENTO = "https://www.teste.com.br/api/ler-noticia/{0}";

        private List<Imagem>? _imagens;
        private List<Comentario>? _comentarios;
        private List<Autor> _autores;
        private List<Categoria> _categorias;
        private List<Noticia>? _noticiasRelacionadas;
        private List<Tag>? _tags;

        public string Titulo { get; private set; }
        public string SubTitulo { get; private set; }        
        public string Conteudo { get; private set; }        
        public string Lead { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public IReadOnlyCollection<Autor> Autores { get => _autores; } //créditos (cada um com seu papel) ???
        public IReadOnlyCollection<Categoria> Categorias { get => _categorias; }
        public Regiao Regiao { get; private set; }
        public bool ExclusivoParaAssinantes { get; private set; }
        public bool Ativa { get; private set; }
        public string LinkDeCompartilhamento { get => string.Format(LINK_COMPARTILHAMENTO, Id.ToString()); }
        public IReadOnlyCollection<Imagem>? Imagens { get => _imagens; }        
        public IReadOnlyCollection<Comentario>? ComentariosModerados { get => _comentarios.FindAll(x => x.ValidadoPelaModeracao); }
        public IReadOnlyCollection<Comentario>? Comentarios { get => _comentarios; }        
        public IReadOnlyCollection<Noticia>? NoticiasRelacionadas { get => _noticiasRelacionadas; }
        public IReadOnlyCollection<Tag>? Tags { get => _tags; }        

        public Noticia(string titulo, string subTitulo, string conteudo, string lead,
            ICollection<Categoria> categorias,
            ICollection<Autor> autores,
            Regiao regiao,
            bool exclusivoParaAssinantes,
            ICollection<Imagem>? imagens = null,
            ICollection<Comentario>? comentarios = null,
            ICollection<Noticia>? noticiasRelacionadas = null,
            ICollection<Tag>? tags = null)
        {
            DataCriacao = DateTime.UtcNow;
            ExclusivoParaAssinantes = exclusivoParaAssinantes;
            Ativa = true;

            DefinirTitulo(titulo);
            DefinirSubtitulo(subTitulo);
            DefinirConteudo(conteudo);
            DefinirLead(lead);
            AdicionarCategorias(categorias);
            AdicionarAutores(autores);
            DefinirRegiao(regiao);
            AdicionarImagens(imagens);
            AdicionarComentarios(comentarios);
            AdicionarNoticiasRelacionadas(noticiasRelacionadas);
            AdicionarTags(tags);
        }

        public void DesativarNoticia()
        {
            Ativa = false;
        }

        public void AtivarNoticia()
        {
            Ativa = true;
        }

        public void AdicionarTag(Tag tag)
        {
            if (_tags == null)
                _tags = new List<Tag>();

            if (_tags.Contains(tag))
                throw new ArgumentException("Essa tag já existe!");

            _tags.Add(tag);
        }

        public void RemoverTag(Tag tag)
        {
            if (_tags == null || !_tags.Contains(tag))
                throw new ArgumentException("Essa tag não foi encontrada!");

            _tags.Remove(tag);
        }

        public void AdicionarNoticiaRelacionada(Noticia noticia)
        {
            if (_noticiasRelacionadas == null)
                _noticiasRelacionadas = new List<Noticia>();

            if (_noticiasRelacionadas.Contains(noticia))
                throw new ArgumentException("Essa notícia já existe na lista de notícias relacionadas!");

            _noticiasRelacionadas.Add(noticia);
        }

        public void RemoverNoticiaRelacionada(Noticia noticia)
        {
            if (_noticiasRelacionadas == null || !_noticiasRelacionadas.Contains(noticia))
                throw new ArgumentException("Essa notícia relacionada não foi encontrada!");

            _noticiasRelacionadas.Remove(noticia);
        }

        public void AdicionarComentario(Comentario comentario)
        {
            if (_comentarios == null)
                _comentarios = new List<Comentario>();

            if (_comentarios.Contains(comentario))
                throw new ArgumentException("Esse comentário já existe!");

            _comentarios.Add(comentario);
        }

        public void RemoverComentario(Comentario comentario)
        {
            if (_comentarios == null || !_comentarios.Contains(comentario))
                throw new ArgumentException("Esse comentário não foi encontrado!");

            _comentarios.Remove(comentario);
        }

        public void AdicionarImagem(Imagem imagem)
        {
            if (_imagens == null)
                _imagens = new List<Imagem>();

            if (_imagens.Contains(imagem))
                throw new ArgumentException("Essa imagem já existe!");

            if (_imagens.Count == LIMITE_IMAGENS)
                throw new ArgumentException($"Limite de {LIMITE_IMAGENS} imagens atingido");

            _imagens.Add(imagem);
        }

        public void RemoverImagem(Imagem imagem)
        {
            if (_imagens == null || !_imagens.Contains(imagem))
                throw new ArgumentException("Essa imagem não foi encontrada!");

            _imagens.Remove(imagem);
        }

        public void DefinirRegiao(Regiao regiao)
        {
            if (regiao == null)
                throw new ArgumentNullException("É obrigatório que a notícia tenha uma região!");

            Regiao = regiao;
        }

        public void AdicionarAutor(Autor autor)
        {
            if (_autores == null)
                _autores = new List<Autor>();

            if (_autores.Contains(autor))
                throw new ArgumentException("Autor já existente!");

            if (_autores.Count == LIMITE_AUTORES)
                throw new ArgumentException($"Limite de {LIMITE_AUTORES} autores atingido");

            _autores.Add(autor);
        }

        public void RemoverAutor(Autor autor)
        {
            if (_autores == null || !_autores.Contains(autor))
                throw new ArgumentException("Esse autor não foi encontrado!");

            if (_autores.Count == 1)
                throw new ArgumentException("A matéria não pode ficar sem nenhum autor!");

            _autores.Remove(autor);
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            if (_categorias == null)
                _categorias = new List<Categoria>();

            if (_categorias.Contains(categoria))
                throw new ArgumentException("Categoria já existente!");

            if (_categorias.Count == LIMITE_CATEGORIAS)
                throw new ArgumentException($"Limite de {LIMITE_CATEGORIAS} categorias atingido");

            _categorias.Add(categoria);
        }

        public void RemoverCategoria(Categoria categoria)
        {
            if (_categorias == null || !_categorias.Contains(categoria))
                throw new ArgumentException("Essa categoria não foi encontrada!");

            if (_categorias.Count == 1)
                throw new ArgumentException("A matéria não pode ficar sem nenhuma categoria!");

            _categorias.Remove(categoria);
        }

        public void DefinirLead(string lead)
        {
            if (lead.Length > LIMITE_LEAD)
                throw new ArgumentException($"Título ultrapassa o máximo permitido de {LIMITE_LEAD} caracteres!");

            Lead = lead.Trim();
        }

        public void DefinirConteudo(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                throw new ArgumentException("O conteúdo é obrigatório!");

            if (conteudo.Length > LIMITE_CONTEUDO)
                throw new ArgumentException($"Título ultrapassa o máximo permitido de {LIMITE_CONTEUDO} caracteres!");

            Conteudo = conteudo.Trim();
        }

        public void DefinirSubtitulo(string subTitulo)
        {
            if (subTitulo.Length > LIMITE_SUBTITULO)
                throw new ArgumentException($"Subtítulo ultrapassa o máximo permitido de {LIMITE_SUBTITULO} caracteres!");

            SubTitulo = subTitulo.Trim();
        }

        public void DefinirTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título é obrigatório!");

            if (titulo.Length > LIMITE_TITULO)
                throw new ArgumentException($"Título ultrapassa o máximo permitido de {LIMITE_TITULO} caracteres!");

            Titulo = titulo.Trim();
        }

        private void AdicionarTags(ICollection<Tag>? tags)
        {
            if (tags == null)
                return;

            tags.ToList().ForEach(AdicionarTag);
        }

        private void AdicionarNoticiasRelacionadas(ICollection<Noticia>? noticiasRelacionadas)
        {
            if (noticiasRelacionadas == null)
                return;

            noticiasRelacionadas.ToList().ForEach(AdicionarNoticiaRelacionada);
        }

        private void AdicionarComentarios(ICollection<Comentario>? comentarios)
        {
            if (comentarios == null)
                return;

            comentarios.ToList().ForEach(AdicionarComentario);
        }

        private void AdicionarImagens(ICollection<Imagem>? imagens)
        {
            if (imagens == null)
                return;

            if (imagens.Count > LIMITE_IMAGENS)
                throw new ArgumentException($"Ultrapassa o máximo permitido de {LIMITE_IMAGENS} imagens!");

            imagens.ToList().ForEach(AdicionarImagem);
        }

        private void AdicionarAutores(ICollection<Autor> autores)
        {
            if (autores == null)
                throw new ArgumentNullException("É obrigatório que a notícia tenha ao menos um autor!");

            if (autores.Count > LIMITE_AUTORES)
                throw new ArgumentException($"Ultrapassa o máximo permitido de {LIMITE_AUTORES} autores!");

            autores.ToList().ForEach(AdicionarAutor);
        }

        private void AdicionarCategorias(ICollection<Categoria> categorias)
        {
            if (categorias == null)
                throw new ArgumentNullException("É obrigatório que a notícia tenha ao menos uma categoria!");

            if (categorias.Count > LIMITE_CATEGORIAS)
                throw new ArgumentException($"Ultrapassa o máximo permitido de {LIMITE_CATEGORIAS} categorias!");

            categorias.ToList().ForEach(AdicionarCategoria);
        }
    }
}