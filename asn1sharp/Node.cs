using System.Collections.Generic;
using System.Linq;

namespace asn1sharp
{
    public sealed class Node
    {
        #region Constructor

        private Node(Tag tag, Sizes sizes, IEnumerable<Node> children)
        {
            Tag = tag.RequireNotNull(nameof(tag));

            Sizes = sizes.RequireNotNull(nameof(sizes));

            Children = children.RequireNotNull(nameof(children));
        }

        #endregion

        #region Properties

        public Tag Tag { get; }

        public Sizes Sizes { get; }

        public IEnumerable<Node> Children { get; }

        #endregion

        #region Methods

        internal static Node From(NodeDescription description)
        {
            var tag = new Tag(description.Header.Tag);

            var children = new List<Node>();

            if (tag.CanHaveChildren())
            {
                children.AddRange(description.Children()
                                             .Select(c => From(c)));
            }

            return new Node(tag, Sizes.From(description), children);
        }

        #endregion
    }
}
