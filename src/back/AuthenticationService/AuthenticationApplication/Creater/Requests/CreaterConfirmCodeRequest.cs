using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApplication.Creater.Requests
{
    public class CreaterConfirmCodeRequest : IRequest<string>
    {
        public Guid Id { get; set; }
        public string CreaterName {  get; set; }
        public string Email { get; set; }
        public int Code {  get; set; }
    }
}
