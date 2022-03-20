using Application.Core.Commands.Authentication;
using Application.Core.Responses.Authentication;
using AutoMapper;
using Data.Interfaces;
using Domain.Enums;
using MediatR;
using Middleware.Extensions;
using Middleware.Notifications;

namespace Application.Core.Handlers.Authentication
{
    public class CreateAuthenticationHandler
        : IRequestHandler<CreateAuthenticationCommand, AuthenticationResponse>
    {
        private readonly IAccount _account;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public CreateAuthenticationHandler(IAccount account, NotificationContext notificationContext, IMapper mapper)
        {
            _account = account;
            _notificationContext = notificationContext;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Handle(CreateAuthenticationCommand request, CancellationToken cancellationToken)
        {
            // Check if is not null
            if (request.Email.Address.IsNull())
            {
                _notificationContext.AddNotification("EmptyEmail", "O E-mail para consulta não foi informado.");
                return null;
            }

            // Get account
            var account = await _account.GetAccountByEmail(request.Email.Address);
            if (account == null)
            {
                _notificationContext.AddNotification("PasswordOrEmail", "O e-mail informado ainda não foi utilizado.");
                return null;
            }

            // check password, if user created account in platform
            if (account.Email.LoginType == ELoginType.SelfAccount)
            {
                // If is different
                if (account.Password != request.Password.EncryptToSha512())
                {
                    _notificationContext.AddNotification("PasswordOrEmail", "O e-mail ou a senha informados, não foram aceitos.");
                    return null;
                }

                // If is true
                return _mapper.Map<AuthenticationResponse>(account);
            }

            // if user use login by Apple, Google or another platform, user will be available
            return _mapper.Map<AuthenticationResponse>(account);
        }
    }
}
