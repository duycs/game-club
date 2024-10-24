namespace Domain.Clubs
{
    public class Event : EntityBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Scheduled { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }



        public static Event Create(int clubId, string title, string description, DateTime scheduled)
        {
            return new Event()
            {
                ClubId = clubId,
                Title = title,
                Description = description,
                Scheduled = scheduled
            };
        }

        public Event Update(string? title, string? description, DateTime scheduled)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
            }

            if (!string.IsNullOrEmpty(description))
            {
                Description = description;
            }

            if (scheduled != null)
            {
                Scheduled = scheduled;
            }

            return this;
        }
    }
}
