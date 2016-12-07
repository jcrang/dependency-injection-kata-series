using System;
using Autofac;
using DependencyInjection.Console.CharacterWriters;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    public class PatternAppModule : Module
    {
        public PatternAppModule(bool useColors, string pattern)
        {
            UseColors = useColors;
            Pattern = pattern;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PatternWriter>();
            builder
                .RegisterType<PatternGenerator>();
            builder
                .RegisterType<PatternApp>();
            builder
                .Register((c, p) => GetCharacterWriter(UseColors))
                .As<ICharacterWriter>();
            builder.Register((c, p) => GetSquarePainter(Pattern))
                .As<ISquarePainter>();
        }

        public string Pattern { get; set; }

        public bool UseColors { get; set; }

        private static ICharacterWriter GetCharacterWriter(bool useColors)
        {
            var writer = new AsciiWriter();
            return useColors ? (ICharacterWriter)new ColorWriter(writer) : writer;
        }

        private static ISquarePainter GetSquarePainter(string pattern)
        {
            switch (pattern)
            {
                case "circle":
                    return new CircleSquarePainter();
                case "oddeven":
                    return new OddEvenSquarePainter();
                case "white":
                    return new WhiteSquarePainter();
                default:
                    throw new ArgumentException($"Pattern '{pattern}' not found!");
            }
        }
    }
}