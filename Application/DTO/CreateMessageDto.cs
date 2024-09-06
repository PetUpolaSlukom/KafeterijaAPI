using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateMessageDto
    {
        public string Text { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
    public class MessageDto : CreateMessageDto
    {
        public int Id { get; set; }
    }
}
