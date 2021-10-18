using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivroController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }
        
        [HttpPost]
        [Route("v1/livros/atualizar")]
        public ICommandResult AtualizarLivro([FromBody] AtualizarLivroCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }  
        
        [HttpDelete]
        [Route("v1/livros/excluir")]
        public ICommandResult ExcluirLivro([FromQuery] ExcluirLivroCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }
    }
}
