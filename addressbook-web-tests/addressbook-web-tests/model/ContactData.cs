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
        private string fio;
        private string birthday;
        private string anniversary;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string FIO
        {
            get
            {
                if (fio != null)
                {
                    return fio;
                }
                else
                {
                    return (Firstname.Trim() + " " + Middlename.Trim() + " " + Lastname.Trim()).Trim();
                }
            }
            set
            {
                fio = value;
            }
        }

        public string Nickname { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Home { get; set; }

        public string Mobile { get; set; }

        public string Work { get; set; }

        public string Fax { get; set; }

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
                    return (CleanUpPhone(Home) + CleanUpPhone(Mobile) + CleanUpPhone(Work) + CleanUpPhone(secHome)).Trim();
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

        public string Homepage { get; set; }

        public string bDay { get; set; }

        public string bMonth { get; set; }

        public string bYear { get; set; }

        public string Birthday
        {
            get
            {
                if (birthday != null)
                {
                    return birthday;
                }
                else
                {
                    return BuildDate(bDay, bMonth, bYear);
                }
            }
            set
            {
                birthday = value;
            }
        }

        public string aDay { get; set; }

        public string aMonth { get; set; }

        public string aYear { get; set; }

        public string Anniversary
        {
            get
            {
                if (anniversary != null)
                {
                    return anniversary;
                }
                else
                {
                    return BuildDate(aDay, aMonth, aYear);
                }
            }
            set
            {
                anniversary = value;
            }
        }
        
        public string secAddress { get; set; }

        public string secHome { get; set; }

        public string Notes { get; set; }

        public string Id { get; set; }

        public string CleanUpPhone(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(phone, "[ /()-]", "") + "\r\n";
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

        public string CleanUpMultiline(string multiline)
        {
            if (multiline == null || multiline == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(multiline, " \r\n", "\r\n");
            }
        }

        public string BuildDate(string day, string month, string year)
        {
            string date = "";

            if (day != null)
            {
                date = day + ". ";
            }
            if (month != null)
            {
                date = date + month + " ";
            }
            if (year != null)
            {
                date = date + year.Trim();
            }
            date.Trim();

            return date;
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
                + "\nlastname=" + Lastname
                + "\nnickname=" + Nickname
                + "\ntitle=" + Title
                + "\ncompany=" + Company
                + "\naddress=" + Address
                + "\nhome=" + Home
                + "\nmobile=" + Mobile
                + "\nwork=" + Work
                + "\nfax=" + Fax
                + "\nemail=" + Email
                + "\nemail2=" + Email2
                + "\nemail3=" + Email3
                + "\nhomepage=" + Homepage
                + "\nbirthday=" + Birthday
                + "\nanniversary=" + Anniversary
                + "\nsec_address=" + secAddress
                + "\nsec_home=" + secHome
                + "\nnotes=" + Notes;
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
