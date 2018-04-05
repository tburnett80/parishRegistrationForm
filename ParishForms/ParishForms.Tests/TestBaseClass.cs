using System.IO;
using System.Reflection;

namespace ParishForms.Tests
{
    public abstract class TestBaseClass
    {
        protected Stream GetEmbededResourceStreamByName(string name)
        {
            using (var st = Assembly.GetExecutingAssembly().GetManifestResourceStream($"ParishForms.Tests.TestResources.{name}"))
            {
                var ms = new MemoryStream();

                st.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);

                return ms;
            }
        }

        protected byte[] GetEmbededFileByName(string name)
        {
            var ms = (MemoryStream)GetEmbededResourceStreamByName(name);
            return ms.ToArray();
        }
    }
}
