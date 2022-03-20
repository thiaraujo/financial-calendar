using Application.Core.Commands.Account;
using Application.Core.Responses.Account;
using AutoMapper;
using Data.Interfaces;
using Data.UnitOfWork;
using MediatR;
using Middleware.Notifications;

namespace Application.Core.Handlers.Account
{
    public class UpdateAccountHandler
        : IRequestHandler<UpdateAccountCommand, AccountResponse>
    {
        private readonly IAccount _account;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notificationContext;

        public UpdateAccountHandler(IAccount account, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, NotificationContext notificationContext)
        {
            _account = account;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
            _notificationContext = notificationContext;
        }

        public async Task<AccountResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            // Check if account already exists
            var existAccount = await _account.Get(request.Id);
            if (existAccount == null)
            {
                _notificationContext.AddNotification("NotFound", "A conta solicitada para atualização não existe.");
                return null;
            }

            // Update info from account
            existAccount.ChangeName(request.Name);
            existAccount.ChangePassword(request.Password);
            existAccount.ChangeRegistrationDate(DateTime.Now);
            existAccount.ChangeEmail(request.Email);
            existAccount.ChangePicture(request.Picture);

            var created = await _account.Update(existAccount, request.Id);

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
            var result = _mapper.Map<AccountResponse>(created);
            return result;
        }
    }
}
