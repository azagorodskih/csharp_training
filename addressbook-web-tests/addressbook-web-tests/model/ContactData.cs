using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
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

        [Column(Name = "firstname"), NotNull]
        public string Firstname { get; set; }

        [Column(Name = "middlename"), NotNull]
        public string Middlename { get; set; }

        [Column(Name = "lastname"), NotNull]
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

        [Column(Name = "nickname"), NotNull]
        public string Nickname { get; set; }

        [Column(Name = "company"), NotNull]
        public string Company { get; set; }

        [Column(Name = "title"), NotNull]
        public string Title { get; set; }

        [Column(Name = "address"), NotNull]
        public string Address { get; set; }

        [Column(Name = "home"), NotNull]
        public string Home { get; set; }

        [Column(Name = "mobile"), NotNull]
        public string Mobile { get; set; }

        [Column(Name = "work"), NotNull]
        public string Work { get; set; }

        [Column(Name = "fax"), NotNull]
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

        [Column(Name = "email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "email2"), NotNull]
        public string Email2 { get; set; }

        [Column(Name = "email3"), NotNull]
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

        [Column(Name = "homepage"), NotNull]
        public string Homepage { get; set; }

        [Column(Name = "bday"), NotNull]
        public string bDay { get; set; }

        [Column(Name = "bmonth"), NotNull]
        public string bMonth { get; set; }

        [Column(Name = "byear"), NotNull]
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

        [Column(Name = "aday"), NotNull]
        public string aDay { get; set; }

        [Column(Name = "amonth"), NotNull]
        public string aMonth { get; set; }

        [Column(Name = "ayear"), NotNull]
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

        [Column(Name = "address2"), NotNull]
        public string secAddress { get; set; }

        [Column(Name = "phone2"), NotNull]
        public string secHome { get; set; }

        [Column(Name = "notes"), NotNull]
        public string Notes { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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
                + "\nlastname=" + Lastname;
                //+ "\nnickname=" + Nickname
                //+ "\ntitle=" + Title
                //+ "\ncompany=" + Company
                //+ "\naddress=" + Address
                //+ "\nhome=" + Home
                //+ "\nmobile=" + Mobile
                //+ "\nwork=" + Work
                //+ "\nfax=" + Fax
                //+ "\nemail=" + Email
                //+ "\nemail2=" + Email2
                //+ "\nemail3=" + Email3
                //+ "\nhomepage=" + Homepage
                //+ "\nbirthday=" + Birthday
                //+ "\nanniversary=" + Anniversary
                //+ "\nsec_address=" + secAddress
                //+ "\nsec_home=" + secHome
                //+ "\nnotes=" + Notes;
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

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
