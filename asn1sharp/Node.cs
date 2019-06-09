using System.Collections.Generic;
using System.Linq;

namespace asn1sharp
{
    public sealed class Node
    {
        #region Constructor

        private Node(Tag tag, IEnumerable<Node> children)
        {
            Tag = tag.RequireNotNull(nameof(tag));

            Children = children.RequireNotNull(nameof(children));
        }

        #endregion

        #region Properties

        public Tag Tag { get; }

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

            return new Node(tag, children);
        }

        #endregion
    }
}
