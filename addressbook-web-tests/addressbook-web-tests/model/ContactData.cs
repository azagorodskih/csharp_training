using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string Home { get; set; }

        public string Mobile { get; set; }

        public string Work { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhone(Home) + CleanUpPhone(Mobile) + CleanUpPhone(Work)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string AllEmails
        {
            get
            {
                if(allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string Id { get; set; }

        public string CleanUpPhone(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
            }
        }

        public string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(email, "[ ]", "") + "\r\n";
            }
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (Firstname == other.Firstname)
            {
                return Lastname == other.Lastname;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + Firstname
                + "\nmiddlename=" + Middlename
                + "\nnickname=" + Nickname
                + "\ntitle=" + Title
                + "\ncompany=" + Company
                + "\naddress=" + Address
                + "\nhome=" + Home
                + "\nmobile=" + Mobile
                + "\nwork=" + Work
                + "\nemail=" + Email
                + "\nemail2=" + Email2
                + "\nemail3=" + Email3;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Firstname.CompareTo(other.Firstname) == 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }
            else
            {
                return Firstname.CompareTo(other.Firstname);
            }
        }
    }
}
