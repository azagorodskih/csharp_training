using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<ContactData> contact = new List<ContactData>();
            contact.Add(ContactData.GetAll().Except(group.GetContacts()).First());

            app.Contacts.AddSelectedContactsToGroup(contact, group);
            
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact[0]);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
