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
        //удалить проект; получение списка проектов и при необходимости создание проекта реализовать через интерфейс
        public void RemoveProjectTest_WithUI()
        {
            int index = 0; //для упрощения будем удалять самый первый проект

            List<ProjectData> oldProjects = app.Project.GetProjectsFromUI();
            if (oldProjects.Count == 0)
            {
                ProjectData p = new ProjectData("ttt")
                {
                    Description = "ddd"
                };
                bool isSuccess = app.Project.CreateFromUI(p);
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

        [Test]
        //удалить проект; получение списка проектов и при необходимости создание проекта реализовать через веб-сервис
        public void RemoveProjectTest_WithSoap()
        {
            int index = 0; //для упрощения будем удалять самый первый проект

            List<ProjectData> oldProjects = app.Project.GetProjectsFromSoap();
            if (oldProjects.Count == 0)
            {
                ProjectData p = new ProjectData("ttt")
                {
                    Description = "ddd"
                };
                string isSuccess = app.Project.CreateFromSoap(p);
                //проверить, что содержит isSuccess, возможно, это значение нужно обрабатывать
                oldProjects.Add(p);
            }

            app.Project.Remove(index);
            oldProjects.RemoveAt(index);

            List<ProjectData> newProjects = app.Project.GetProjectsFromSoap();
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
