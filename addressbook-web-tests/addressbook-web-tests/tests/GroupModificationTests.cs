﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        //замена старых значений новыми
        public void GroupModificationTest_NewData()
        {
            GroupData newData = new GroupData("mmm");
            newData.Header = "nnn";
            newData.Footer = "ooo";

            app.Groups.Modify(1, newData);
        }

        [Test]
        //замена старых значений пустыми
        public void GroupModificationTest_EmptyData()
        {
            GroupData emptyData = new GroupData("");
            emptyData.Header = "";
            emptyData.Footer = "";

            app.Groups.Modify(3, emptyData);
        }
    }
}