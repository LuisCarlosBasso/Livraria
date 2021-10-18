using System;
using Flunt.Notifications;
using Livraria.Infra.Interfaces.Commands;

namespace Livraria.Domain.Commands.Inputs
{
    public class ExcluirLivroCommand : Notifiable, ICommandPadrao
    {
        public long Id { get; set; }
        
        public bool ValidarCommad()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(Id.ToString()))
                    AddNotification("Id", "ID é um campo obrigatório");
                if(Id < 0)
                    AddNotification("Id", "Id deve ser maior que zero");

                return Valid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}