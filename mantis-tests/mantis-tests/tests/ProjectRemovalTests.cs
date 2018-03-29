using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void RemoveProjectTest()
        {
            int index = 0; //для упрощения будем удалять самый первый проект

            List<ProjectData> oldProjects = app.Project.GetProjectsFromUI();
            if (oldProjects.Count == 0)
            {
                ProjectData p = new ProjectData("ttt");
                bool isSuccess = app.Project.Create(p);
                if (isSuccess)
                {
                    oldProjects.Add(p);
                }
                else
                {
                    Console.Out.Write("При создании проекта возникла непредвиденная ошибка! Возможно, проект с именем " + p.Name + " уже существует.");
                    goto Finish;
                }
            }

            app.Project.Remove(index);
            oldProjects.RemoveAt(index);

            Finish:
                List<ProjectData> newProjects = app.Project.GetProjectsFromUI();
                oldProjects.Sort();
                newProjects.Sort();
                Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
