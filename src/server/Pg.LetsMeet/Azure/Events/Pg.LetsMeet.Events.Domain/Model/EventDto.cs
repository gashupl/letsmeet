namespace Pg.LetsMeet.Events.Domain.Model
{
    public class EventDto
    {
        public Guid EventId { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int? AllowedParticipants { get; set; }
    }

    public class EventCreateDto
    {
        public string? Name { get; set; }
        public string? Details { get; set; }
        public string? PartnerName { get; set; }
        public string? PartnerId { get; set; }
        public string? PartnerEmail { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int? AllowedParticipants { get; set; }
    }
}
