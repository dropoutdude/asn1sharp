using System;
using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Asn1Sharp
    {
        public static async Task<Node> Parse(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return await stream.Parse();
            }
        }

        public static Task<Node> Parse(this FileStream stream)
        {
            var adapter = new StreamAdapter(stream);

            return adapter.Parse().ContinueWith(t =>
            {
                adapter.Dispose();

                return t.Result;
            });
        }
    }
}
