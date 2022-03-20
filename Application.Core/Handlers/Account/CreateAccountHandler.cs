using Application.Core.Commands.Account;
using Application.Core.Responses.Account;
using AutoMapper;
using Data.Interfaces;
using Data.UnitOfWork;
using MediatR;
using Middleware.Notifications;

namespace Application.Core.Handlers.Account
{
    public class CreateAccountHandler
        : IRequestHandler<CreateAccountCommand, AccountResponse>
    {
        private readonly IAccount _account;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notificationContext;

        public CreateAccountHandler(IAccount account,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext,
            IMediator mediator)
        {
            _account = account;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _mediator = mediator;
        }

        public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            // Check if account already exists
            var existAccount = await _mediator.Send(new GetAccountByEmailCommand { Email = request.Email.Address });
            if (existAccount != null)
            {
                _notificationContext.AddNotification("DuplicateEmail", "O e-mail informado já foi registrado anteriormente.");
                return null;
            }

            var account = _mapper.Map<Domain.Entities.Entities.Account>(request);

            // Transform password into a SHA512
            account.EncryptPassword();

            var created = await _account.Create(account);

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
