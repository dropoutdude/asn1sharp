using System.Linq;
using System.Text;

namespace asn1sharp.visual
{
    public static class StringFormatter
    {
        private const string Indent = "  ";

        public static string Print(this Node node)
        {
            var builder = new StringBuilder();

            var level = 0;

            node.Print(level, builder);

            node.PrintChildren(level, builder);

            return builder.ToString();
        }

        private static void PrintChildren(this Node parent, int parentLevel, StringBuilder builder)
        {
            var level = parentLevel + 1;

            foreach (var child in parent.Children)
            {
                child.Print(level, builder);

                child.PrintChildren(level, builder);
            }
        }

        private static void Print(this Node node, int level, StringBuilder builder)
        {
            var indent = string.Concat(Enumerable.Repeat(Indent, level));

            builder.AppendLine($"{indent}{node.Tag.Class} - {node.Tag.Type} - Header: {node.Sizes.Header} / Data: {node.Sizes.Data} ({node.Sizes.Total} bytes)");
        }
    }
}
