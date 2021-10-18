using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Commands.Outputs;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Interfaces.Commands;
using System;

namespace Livraria.Domain.Handlers
{
    public class LivroHandler : ICommandHandler<AdicionarLivroCommand>, ICommandHandler<AtualizarLivroCommand>,
        ICommandHandler<ExcluirLivroCommand>
    {
        private readonly ILivroRepository _repository;

        public LivroHandler(ILivroRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommad())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                        command.Notifications);

                long id = 0;
                string nome = command.Nome;
                string autor = command.Autor;
                int edicao = command.Edicao;
                string isbn = command.Isbn;
                string imagem = command.Imagem;

                Livro livro = new Livro(id, nome, autor, edicao, isbn, imagem);

                id = _repository.Inserir(livro);

                var retorno = new LivroCommandResult(true, "Livro adicionado com sucesso!", new
                {
                    Id = id,
                    Nome = livro.Nome,
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Isbn = livro.Isbn,
                    Imagem = livro.Imagem
                });

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommandResult Handle(AtualizarLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommad())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                        command.Notifications);
                long id = command.Id;

                if (_repository.CheckId(id))
                {
                    Livro livro =
                        new Livro(id, command.Nome, command.Autor, command.Edicao, command.Isbn, command.Imagem);
                    _repository.Atualizar(livro);
                    return new LivroCommandResult(true, "Livro atualizado com sucesso", livro);
                }
                else
                {
                    return new LivroCommandResult(false, "Livro não existente", new { });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult Handle(ExcluirLivroCommand command)
        {
            try
            {
                if (!command.ValidarCommad())
                    return new LivroCommandResult(false, "Por favor corrija as inconsistências abaixo",
                        command.Notifications);
                long id = command.Id;

                if (_repository.CheckId(id))
                {
                    _repository.Excluir(id);
                    return new LivroCommandResult(true, "Livro excluido com sucesso", new {});
                }
                return new LivroCommandResult(false, "Livro não existente", new {});
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}