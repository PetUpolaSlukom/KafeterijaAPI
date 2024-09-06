using Application.DTO;
using Application.UseCases.Commands.Messages;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Messages
{
    public class EfCreateMessageCommand : ICreateMessageCommand
    {
        public int Id => 15;

        public string Name => "Create message";

        private Context _context;
        private CreateMessageValidation _validator;

        public EfCreateMessageCommand(Context context, CreateMessageValidation validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreateMessageDto data)
        {
            _validator.ValidateAndThrow(data);

            Message message = new Message
            {
                Text = data.Text,
                Email = data.Email,
                FullName = data.FullName,
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

        }
    }
}
