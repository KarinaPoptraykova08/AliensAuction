namespace AliensStore.Data.Entity
{
	public class Dealer : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public decimal CurrentBalance { get; set; }

		public ICollection<Alien>? AlienList { get; set; } = new List<Alien>();

    }
}
