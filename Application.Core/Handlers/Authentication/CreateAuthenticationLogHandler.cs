using Application.Core.Commands.Account;
using Application.Core.Commands.Authentication;
using AutoMapper;
using Data.Interfaces;
using Data.UnitOfWork;
using Domain.Entities.Entities;
using MediatR;
using Middleware.Extensions;

namespace Application.Core.Handlers.Authentication;

// This class will not have Notification, this is a class for control access only

public class CreateAuthenticationLogHandler : IRequestHandler<CreateAuthenticationLogCommand, bool>
{
    private readonly IAccountLoginLog _loginLog;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthenticationLogHandler(
        IAccountLoginLog loginLog, 
        IMediator mediator, 
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _loginLog = loginLog;
        _mediator = mediator;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateAuthenticationLogCommand request, CancellationToken cancellationToken)
    {
        // Check values
        if (request.AccountId.IsNull())
            return false;

        // Check if account exists
        var account = await _mediator.Send(new GetAccountByIdCommand { Id = request.AccountId });
        if (account == null)
            return false;

        
        var log = _mapper.Map<AccountLoginLog>(request);
        var created = await _loginLog.Create(log);

        // Validate values
        if (!created.Valid)
        {
            return false;
        }

        // Commit object
        var saved = await _unitOfWork.Commit();
        if (!saved)
        {
            return false;
        }

        // Return values
        return true;
    }
}