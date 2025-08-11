using VContainer;
using VContainer.Unity;

namespace _Project.Runtime
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            BindSaveLoadManager(builder);
        }

        private void BindSaveLoadManager(IContainerBuilder builder)
        {
            builder.Register<SaveLoadManager>(Lifetime.Singleton);
        }
    }
}