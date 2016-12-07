using Autofac;
using Autofac.Core;
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
                .RegisterType<PatternGenerator>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISquarePainter),
                        (pi, ctx) => ctx.ResolveNamed<ISquarePainter>(Pattern)));
            builder
                .RegisterType<PatternApp>();
            builder
                .Register((c, p) => GetCharacterWriter(UseColors))
                .As<ICharacterWriter>();
            
            builder.RegisterType<CircleSquarePainter>().Named<ISquarePainter>("circle");
            builder.RegisterType<OddEvenSquarePainter>().Named<ISquarePainter>("oddeven");
            builder.RegisterType<WhiteSquarePainter>().Named<ISquarePainter>("white");
            builder.RegisterType<BlackSquarePainter>().Named<ISquarePainter>("black");
        }

        public string Pattern { get; set; }

        public bool UseColors { get; set; }

        private static ICharacterWriter GetCharacterWriter(bool useColors)
        {
            var writer = new AsciiWriter();
            return useColors ? (ICharacterWriter)new ColorWriter(writer) : writer;
        }
    }
}