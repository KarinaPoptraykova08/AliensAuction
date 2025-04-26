namespace AliensStore.Data.Entity
{
	public class Planet : BaseEntity
	{
		public string Name { get; set; }
		public int GalaxyId { get; set; }
		public virtual Galaxy Galaxy { get; set; }

		public ICollection<Alien>? Aliens { get; set; }
	}
}
