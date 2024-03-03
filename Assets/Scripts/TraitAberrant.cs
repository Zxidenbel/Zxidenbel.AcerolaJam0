namespace Card
{
    public abstract partial class Trait
    {
        public class Aberrant : Trait
        {
            public Aberrant()
            {
                base.name = "Aberrant";
                base.type = "Aberrance";
                base.turns = -1;
                base.description = "-20% HUMANITY";
            }
            public override void updateModifications(Character character)
            {
                character.ModHumanity -= 1;
            }
        }
    }
}
