using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Reflection.CustomClass
{
    [Serializable]
    public class F : ISerializable
    {
        public int? i1;
        public int? i2;
        public int? i3;
        public int? i4;
        public int? i5;

        public F() { }

        public F(int i1, int i2, int i3, int i4, int i5)
        {
            this.i1 = i1;
            this.i2 = i2;
            this.i3 = i3;
            this.i4 = i4;
            this.i5 = i5;
        }

        private F(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            i1 = info.GetInt32("i1");
            i2 = info.GetInt32("i2");
            i3 = info.GetInt32("i3");
            i4 = info.GetInt32("i4");
            i5 = info.GetInt32("i5");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            var f = Get();
            info.AddValue("i1", f.i1);
            info.AddValue("i2", f.i2);
            info.AddValue("i3", f.i3);
            info.AddValue("i4", f.i4);
            info.AddValue("i5", f.i5);
        }

        F Get()
        {
            if (this.i1 == null)
            {
                return new F()
                {
                    i1 = 1,
                    i2 = 2,
                    i3 = 3,
                    i4 = 4,
                    i5 = 5
                };
            }
            else
            {
                return this;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            var _fields = this.GetType().GetFields(bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
            foreach (var field in _fields)
            {
                stringBuilder.Append(field.Name + "=" +
                    this.GetType().GetField(field.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.GetValue(this) + ", ");
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 1);

            return stringBuilder.ToString();
        }
    }
}
