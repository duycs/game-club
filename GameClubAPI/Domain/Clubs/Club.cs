namespace Domain.Clubs
{
    public class Club : EntityBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Event>? Events { get; set; }


        public static Club Create(string? name, string? description)
        {
            return new Club()
            {
                Name = name,
                Description = description
            };
        }

        public static Club Create(string? name, string? description, List<Event> events)
        {
            return new Club()
            {
                Name = name,
                Description = description,
                Events = events
            };
        }

        public Club Update(string? name, string? description)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }

            if (!string.IsNullOrEmpty(description))
            {
                Description = description;
            }

            return this;
        }
    }
}
