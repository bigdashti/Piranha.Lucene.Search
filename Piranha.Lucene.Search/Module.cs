using Piranha.Extend;

namespace Piranha.Lucene.Search
{
    /// <summary>
    ///     The identity module.
    /// </summary>
    public class Module : IModule
    {
        /// <summary>
        ///     Gets the Author
        /// </summary>
        public string Author => "Piranha";

        /// <summary>
        ///     Gets the Name
        /// </summary>
        public string Name => "Piranha.Lucene.Search";

        /// <summary>
        ///     Gets the Version
        /// </summary>
        public string Version => Utils.GetAssemblyVersion(GetType().Assembly);

        /// <summary>
        ///     Gets the description
        /// </summary>
        public string Description => "Search module for Piranha CMS using Lucene.NET.";

        /// <summary>
        ///     Gets the package url.
        /// </summary>
        public string PackageUrl => "https://www.nuget.org/packages/Piranha.Lucene.Search";

        /// <summary>
        ///     Gets the icon url.
        /// </summary>
        public string IconUrl => "https://piranhacms.org/assets/twitter-shield.png";

        /// <summary>
        ///     Initializes the module.
        /// </summary>
        public void Init()
        {
        }
    }
}