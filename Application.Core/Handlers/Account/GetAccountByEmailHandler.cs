using Application.Core.Commands.Account;
using Application.Core.Responses.Account;
using AutoMapper;
using Data.Interfaces;
using MediatR;
using Middleware.Extensions;
using Middleware.Notifications;

namespace Application.Core.Handlers.Account;

public class GetAccountByEmailHandler : IRequestHandler<GetAccountByEmailCommand, AccountResponse>
{
    private readonly IAccount _account;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public GetAccountByEmailHandler(IAccount account, IMapper mapper, NotificationContext notificationContext)
    {
        _account = account;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }

    public async Task<AccountResponse> Handle(GetAccountByEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNull())
        {
            _notificationContext.AddNotification("EmptyEmail", "O E-mail para consulta não foi informado.");
            return null;
        }

        var account = await _account.GetAccountByEmail(request.Email);
        if (account == null)
            return null;

        return _mapper.Map<AccountResponse>(account);
    }
}