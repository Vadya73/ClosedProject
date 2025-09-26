using Meta;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class DataCenterLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<SceneDataCenter>();
        }
    }
}
