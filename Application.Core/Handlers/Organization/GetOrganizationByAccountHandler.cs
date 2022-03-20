using Application.Core.Commands.Organization;
using Application.Core.Responses.Organization;
using AutoMapper;
using Data.Interfaces;
using MediatR;
using Middleware.Notifications;

namespace Application.Core.Handlers.Organization;

public class GetOrganizationByAccountHandler : IRequestHandler<GetOrganizationByAccountCommand, IEnumerable<OrganizationResponse>>
{
    private readonly IOrganization _organization;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public GetOrganizationByAccountHandler(
        IOrganization organization,
        NotificationContext notificationContext,
        IMapper mapper)
    {
        _organization = organization;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrganizationResponse>> Handle(GetOrganizationByAccountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.AccountId))
        {
            _notificationContext.AddNotification("IdIsNull", "Sua busca não pode ser completada, verifique os dados.");
            return null;
        }

        // Check if exists
        var organization = await _organization.GetByAccountId(request.AccountId);
        if (!organization.Any())
        {
            _notificationContext.AddNotification("NotFound", "Nenhuma organização foi encontrada.");
            return null;
        }

        return _mapper.Map<IEnumerable<OrganizationResponse>>(organization);
    }
}