using System.Numerics;

namespace AliensStore.Data.Entity
{
	public class Alien : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
        public decimal CoinsPerDay { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
		public int Legs { get; set; }
		public int Arms { get; set; }
		public int Eyes { get; set; }
        public bool IsForSale { get; set; }
        public int PlanetId { get; set; }
		public virtual Planet Planet { get; set; }
		public int? DealerId { get; set; }
		public virtual Dealer? Dealer { get; set; }
		public string ImageUrl { get; set; }
	}
}
