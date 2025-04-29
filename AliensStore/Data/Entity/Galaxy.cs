namespace AliensStore.Data.Entity
{
	public class Galaxy : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Planet>? Planets { get; set; } = new List<Planet>();
	}
}
