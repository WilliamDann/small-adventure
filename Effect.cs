using System;
using System.Reflection;

namespace Adventure
{
    public class Effect
    {
        public string  Function { get; set; }
        public string  Target   { get; set; }
        public dynamic Argument   { get; set; }

        Actor DetermineTarget(World context)
        {
            if (Target == "player")
                return context.player;
            else if (context.CurrentLevel.actors.ContainsKey(Target))
                return context.CurrentLevel.actors[Target];
            else
                return null;
        }

        public void Apply(World context)
        {
            Actor target = DetermineTarget(context);
            if (target == null)
                throw new EffectFailedException($"Invalid target: {Target}");

            MethodInfo method = typeof(Actor).GetMethod(Function);
            if (method == null)
                throw new EffectFailedException($"{Function} is not a valid effect");
            
            try
            {
                method.Invoke(target, new object[]{ Argument });
            }
            catch (ArgumentException)
            {
                throw new EffectFailedException($"{Function} is not a supported effect");
            }
        }
    }

    public class EffectFailedException : System.Exception
    {
        public EffectFailedException() { }
        public EffectFailedException(string message) : base(message) { }
    }
}