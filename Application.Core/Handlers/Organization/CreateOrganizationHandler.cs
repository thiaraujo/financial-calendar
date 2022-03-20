using Application.Core.Commands.Account;
using Application.Core.Commands.Organization;
using Application.Core.Responses.Organization;
using AutoMapper;
using Data.Interfaces;
using Data.UnitOfWork;
using MediatR;
using Middleware.Notifications;

namespace Application.Core.Handlers.Organization;

public class CreateOrganizationHandler
    : IRequestHandler<CreateOrganizationCommand, OrganizationResponse>
{

    private readonly IOrganization _organization;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly NotificationContext _notificationContext;

    public CreateOrganizationHandler(IOrganization organization,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        NotificationContext notificationContext,
        IMediator mediator)
    {
        _organization = organization;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationContext = notificationContext;
        _mediator = mediator;
    }

    public async Task<OrganizationResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        // Check if account exists
        var existAccount = await _mediator.Send(new GetAccountByIdCommand { Id = request.AccountId });
        if (existAccount == null)
        {
            return null;
        }

        var organization = _mapper.Map<Domain.Entities.Entities.Organization>(request);
        var created = await _organization.Create(organization);

        // Validate values
        if (!created.Valid)
        {
            _notificationContext.AddNotifications(created.ValidationResult);
            return null;
        }

        // Commit object
        var saved = await _unitOfWork.Commit();
        if (!saved)
        {
            return null;
        }

        // Return values
        var result = _mapper.Map<OrganizationResponse>(created);
        return result;
    }
}