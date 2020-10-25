using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Security.Data;

namespace Security.Token
{
    public class AuthenticationRequest : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AuthenticationCommandHandler : IRequestHandler<AuthenticationRequest, bool>
        {
            private readonly SecurityDbContext _context;

            public AuthenticationCommandHandler(SecurityDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(AuthenticationRequest command, CancellationToken cancellationToken)
                => await _context.Users
                    .AnyAsync(u => u.Username == command.Username 
                                   && u.Password == command.Password, cancellationToken: cancellationToken);
        }
    }
}
