using Dapper;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Domain.Query;
using Livraria.Infra.Data.DataContexts;
using Livraria.Infra.Data.Respositories.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Livraria.Infra.Data.Respositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public long Inserir(Livro livro)
        {
            try
            {
                _parameters.Add("Nome", livro.Nome, DbType.String);
                _parameters.Add("Autor", livro.Autor, DbType.String);
                _parameters.Add("Edicao", livro.Edicao, DbType.Int32);
                _parameters.Add("Isbn", livro.Isbn, DbType.String);
                _parameters.Add("Imagem", livro.Imagem, DbType.String);

                return _dataContext.MySqlConexao.ExecuteScalar<long>(LivroQueries.Inserir, _parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(Livro livro)
        {
            try
            {
                _parameters.Add("Id", livro.Id, DbType.Int64);
                _parameters.Add("Nome", livro.Nome, DbType.String);
                _parameters.Add("Autor", livro.Autor, DbType.String);
                _parameters.Add("Edicao", livro.Edicao, DbType.Int32);
                _parameters.Add("Isbn", livro.Isbn, DbType.String);
                _parameters.Add("Imagem", livro.Imagem, DbType.String);

                _dataContext.MySqlConexao.Execute(LivroQueries.Atualizar, _parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(long id)
        {
            try
            {
                _parameters.Add("Id", id, DbType.Int64);

                _dataContext.MySqlConexao.Execute(LivroQueries.Excluir, _parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LivroQueryResult> Listar()
        {
            try
            {
                var livros = _dataContext.MySqlConexao.Query<LivroQueryResult>(LivroQueries.Listar).ToList();
                return livros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LivroQueryResult Obter(long id)
        {
            try
            {
                _parameters.Add("Id", id, DbType.Int64);

                var livro = _dataContext.MySqlConexao.Query<LivroQueryResult>(LivroQueries.Obter, _parameters).FirstOrDefault();
                return livro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckId(long id)
        {
            try
            {
                _parameters.Add("Id", id, DbType.Int64);

                return _dataContext.MySqlConexao.Query<bool>(LivroQueries.CheckId, _parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}