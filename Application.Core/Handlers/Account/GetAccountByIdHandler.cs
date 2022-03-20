using Application.Core.Commands.Account;
using Application.Core.Responses.Account;
using AutoMapper;
using Data.Interfaces;
using MediatR;
using Middleware.Extensions;
using Middleware.Notifications;

namespace Application.Core.Handlers.Account;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdCommand, AccountResponse>
{
    private readonly IAccount _account;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public GetAccountByIdHandler(IAccount account, IMapper mapper, NotificationContext notificationContext)
    {
        _account = account;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }

    public async Task<AccountResponse> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
    {
        if (request.Id.IsNull())
        {
            _notificationContext.AddNotification("EmptyId", "O Id para consulta não foi informado.");
            return null;
        }

        var account = await _account.Get(request.Id);
        if (account == null)
        {
            _notificationContext.AddNotification("EmptyAccount", "Não foi encontrado nenhum registro com o Id informado.");
            return null;
        }

        return _mapper.Map<AccountResponse>(account);
    }
}