using Application.Core.Commands.Organization;
using Application.Core.Responses.Organization;
using AutoMapper;
using Data.Interfaces;
using MediatR;
using Middleware.Notifications;

namespace Application.Core.Handlers.Organization;

public class GetOrganizationByIdHandler
            : IRequestHandler<GetOrganizationByIdComand, OrganizationResponse>
{
    private readonly IOrganization _organization;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public GetOrganizationByIdHandler(
        IOrganization organization,
        NotificationContext notificationContext,
        IMapper mapper)
    {
        _organization = organization;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task<OrganizationResponse> Handle(GetOrganizationByIdComand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.AccountId) && string.IsNullOrEmpty(request.OrganizationId))
        {
            _notificationContext.AddNotification("IdIsNull", "Sua busca não pode ser completada, verifique os dados.");
            return null;
        }

        // Check if exists
        var organization = await _organization.Get(request.OrganizationId);
        if (organization == null)
        {
            _notificationContext.AddNotification("NotFound", "Nenhuma organização foi encontrada.");
            return null;
        }

        // Check if organization is from account
        if (organization.AccountId != request.AccountId)
        {
            _notificationContext.AddNotification("NotFound", "Nenhuma organização foi encontrada em seu diretório.");
            return null;
        }

        return _mapper.Map<OrganizationResponse>(organization);
    }
}