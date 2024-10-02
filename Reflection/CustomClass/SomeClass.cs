using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.CustomClass
{
    [Serializable]
    public class SomeClass : ISerializable
    {


        public Guid? Id { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Position { get; set; }
        public string? MainEmail { get; set; }
        public string? MainTelephoneNumber { get; set; }
        public string? About { get; set; }

        public SomeClass()
        { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private SomeClass(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            Id = new Guid(info.GetString("Id"));
            Firstname = info.GetString("Firstname");
            Surname = info.GetString("Surname");
            Position = info.GetString("Position");
            MainEmail = info.GetString("MainEmail");
            MainTelephoneNumber = info.GetString("MainTelephoneNumber");
            About = info.GetString("About");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("Id", Id);
            info.AddValue("Firstname", Firstname);
            info.AddValue("Surname", Surname);
            info.AddValue("Position", Position);
            info.AddValue("MainEmail", MainEmail);
            info.AddValue("MainTelephoneNumber", MainTelephoneNumber);
            info.AddValue("About", About);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            var  _fields = this.GetType().GetFields(bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
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
