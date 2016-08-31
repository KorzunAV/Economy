﻿using Economy.Parsers;
using Ninject.Modules;

namespace Economy.IoC
{
    public class ConvertersModule : NinjectModule
    {
        public const string HistoryConverter = "History";
        public const string BelinvestConverter = "Belinvest";
        public const string PriorConverter = "Prior";

        public override void Load()
        {
            Bind<IConverter>()
                .To<CourseArhiveConverter>()
                .InSingletonScope()
                .Named(HistoryConverter);

            Bind<IConverter>()
              .To<BelinvestConverter>()
              .InSingletonScope()
              .Named(BelinvestConverter);

            Bind<IConverter>()
              .To<PriorConverter>()
              .InSingletonScope()
              .Named(PriorConverter);
        }
    }
}