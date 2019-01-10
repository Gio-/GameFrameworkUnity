public interface IGameInteraction
{
    bool  IsLocked  { get; set; }
    bool  IsOneShot { get; set; }
    float Delay     { get; set; }
    float CoolDown  { get; set; }
    
    void Interaction();
}