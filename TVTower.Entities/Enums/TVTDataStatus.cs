
namespace TVTower.Entities
{
	public enum TVTDataStatus
	{
        Incorrect = 1,
        Incomplete = 2,        
        NoFakes = 4,
        OnlyDE = 8,
        OnlyEN = 16,
        Complete = 32,
        Approved = 64
	}
}
