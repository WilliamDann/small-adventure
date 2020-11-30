using System;
using System.Reflection;

namespace Adventure
{
    public class Effect
    {
        public string  Function { get; set; }
        public string  Target   { get; set; }
        public int     Amount   { get; set; }

        Actor DetermineTarget(World context)
        {
            if (Target == "Player")
                return context.Player;
            else if (context.CurrentLevel.Actors.ContainsKey(Target))
                return context.CurrentLevel.Actors[Target];
            else
                return null;
        }

        public void Apply(World context, int? forceAmount=null)
        {
            Actor target = DetermineTarget(context);
            if (target == null)
                throw new EffectFailedException($"Invalid target: {Target}");

            MethodInfo method = typeof(Actor).GetMethod(Function);
            if (method == null)
                throw new EffectFailedException($"{Function} is not a valid effect");
            
            try
            {
                int arg = (forceAmount != null) ? (int)forceAmount : Amount;

                method.Invoke(target, new object[]{ arg });
            }
            catch (ArgumentException)
            {
                throw new EffectFailedException($"{Function} is not a supported effect");
            }
        }

        public void Revoke(World context)
        {
            Apply(context, forceAmount: -Amount);
        }
    }

    public class EffectFailedException : System.Exception
    {
        public EffectFailedException() { }
        public EffectFailedException(string message) : base(message) { }
    }
}