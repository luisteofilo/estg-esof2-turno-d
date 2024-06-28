namespace Common.Dtos.Profile;

public class SkillDto
{
    public Guid SkillId { get; set; }
    public string Name { get; set; }
    
    public SkillDto Clone()
    {
        return new SkillDto
        {
            SkillId = SkillId,
            Name = Name
        };
    }
}