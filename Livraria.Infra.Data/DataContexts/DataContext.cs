using Livraria.Infra.Settings;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Livraria.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public MySqlConnection MySqlConexao { get; set; }

        public DataContext(AppSettings appSettings)
        {
            try
            {
                MySqlConexao = new MySqlConnection(appSettings.ConnectionString);
                MySqlConexao.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                if (MySqlConexao.State != ConnectionState.Closed)
                    MySqlConexao.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}