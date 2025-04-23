using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Domain.Data;
using Pg.LetsMeet.Events.Domain.Model;
using System.Linq;
using System.Net.Mail;

namespace Pg.LetsMeet.Events.Domain.Services
{
    public class EventService : IEventService
    { 
        private readonly IEventRepository _eventRepository;
        private readonly IAccountRepository _accountRepository;
        public EventService(IEventRepository eventRepository, IAccountRepository accountRepository)
        {
            _eventRepository = eventRepository;  
            _accountRepository = accountRepository;
        }

        public void CreateEvent(EventCreateDto @event)
        {
            Account? partner;
            if (String.IsNullOrEmpty(@event.PartnerId))
            {
                partner = CreatePartner(@event);
            }
            else
            {
                partner = _accountRepository.FindByAccountCode(@event.PartnerId);
                if (partner != null)
                { 
                    _accountRepository.Update(new Account
                    {
                        Id = partner.Id, 
                        Name = @event.PartnerName,
                        EMailAddress1 = @event.PartnerEmail
                    });
                }
                else
                {
                    throw new ArgumentException($"Invalid ID: {@event.PartnerId}"); 
                }
            }

            _eventRepository.Create(new pg_event
            {
                pg_Name = @event.Name,
                pg_partnerId = partner?.ToEntityReference(),
                pg_details = @event.Details,
                pg_allowedparticipantsquantity = @event.AllowedParticipants,
                pg_date = @event.PlannedDate
            }); 
        }

        private Account CreatePartner(EventCreateDto @event)
        {
            var partner = new Account();
            partner.Name = @event.PartnerName;
            partner.EMailAddress1 = @event.PartnerEmail;
            partner.Id = _accountRepository.Create(partner);
            return partner; 
        }

        public IList<EventDto> FindByAccountCode(string accountCode)
        {
            var partner = _eventRepository.FindByAccountCode(accountCode);
            return partner.Select(e => new EventDto()
            {
                EventId = e.Id,
                Name = e.pg_Name,
                Details = e.pg_details,
                AccountId = e.pg_partnerId.Id,
                PlannedDate = e.pg_date,
                AllowedParticipants = e.pg_allowedparticipantsquantity
            }).ToList(); 
        }
    }
}
