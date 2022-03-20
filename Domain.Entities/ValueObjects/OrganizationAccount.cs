namespace Domain.Entities.ValueObjects
{
    public class OrganizationAccount
    {
        public string OrganizationId { get; set; }
        public string AccountId { get; set; }

        public OrganizationAccount(string organizationId, string accountId)
        {
            OrganizationId = organizationId;
            AccountId = accountId;
        }
    }
}
