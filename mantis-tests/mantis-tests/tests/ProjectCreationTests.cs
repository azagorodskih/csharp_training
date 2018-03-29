using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        //создать проект с несуществующим названием
        public void CreateProjectTest_NotExistsName()
        {
            ProjectData newProject = new ProjectData("Project1")
            {
                Description = "Project1 Description"
            };

            List<ProjectData> oldProjects = app.Project.GetProjectsFromUI();

            bool isExist = false;
            foreach (ProjectData p in oldProjects)
            {
                if (p.Name == newProject.Name)
                {
                    isExist = true;
                }
                if (isExist)
                {
                    break;
                }
            }

            if (!isExist)
            {
                bool isSuccess = app.Project.CreateFromUI(newProject);
                if (isSuccess)
                {
                    oldProjects.Add(newProject);

                    List<ProjectData> newProjects = app.Project.GetProjectsFromUI();
                    oldProjects.Sort();
                    newProjects.Sort();
                    Assert.AreEqual(oldProjects, newProjects);
                }
                else
                {
                    Console.Out.Write("При создании проекта возникла непредвиденная ошибка! Возможно, проект с именем " + newProject.Name + " уже существует.");
                }
            }
            else
            {
                Console.Out.Write("Проект с именем " + newProject.Name + " уже существует!");
            }
        }

        [Test]
        //создать проект с существующим названием
        public void CreateProjectTest_ExistsName()
        {
            ProjectData newProject = new ProjectData("Project1")
            {
                Description = "Project1 Description"
            };

            List<ProjectData> oldProjects = app.Project.GetProjectsFromUI();

            bool isExist = false;
            foreach (ProjectData p in oldProjects)
            {
                if (p.Name == newProject.Name)
                {
                    isExist = true;
                }
                if (isExist)
                {
                    break;
                }
            }

            if (isExist)
            {
                bool isSuccess = app.Project.CreateFromUI(newProject);
                if (!isSuccess)
                {
                    //проверяем, что список проектов не поменялся
                    List<ProjectData> newProjects = app.Project.GetProjectsFromUI();
                    oldProjects.Sort();
                    newProjects.Sort();
                    Assert.AreEqual(oldProjects, newProjects);
                }
                else
                {
                    Console.Out.Write("При создании проекта возникла непредвиденная ошибка! Проект с именем " + newProject.Name + " не должен быть создан.");
                }
            }
            else
            {
                Console.Out.Write("Проекта с именем " + newProject.Name + " не существует! Для тестирования неоходимо создавать проект с существующим названием.");
            }
        }

        [Test]
        //создать проект с несуществующим именем; получение списка проектов и создание проекта реализовать через веб-сервис
        public void CreateProjectTest_WithWebService()
        {
            ProjectData newProject = new ProjectData("Project3")
            {
                Description = "Project3 Description"
            };

            List<ProjectData> oldProjects = app.Project.GetProjectsFromSoap();

            bool isExist = false;
            foreach (ProjectData p in oldProjects)
            {
                if (p.Name == newProject.Name)
                {
                    isExist = true;
                }
                if (isExist)
                {
                    break;
                }
            }

            if (!isExist)
            {
                string isSuccess = app.Project.CreateFromSoap(newProject);
                if (isSuccess != null || isSuccess != "")
                {
                    oldProjects.Add(newProject);

                    List<ProjectData> newProjects = app.Project.GetProjectsFromSoap();
                    oldProjects.Sort();
                    newProjects.Sort();
                    Assert.AreEqual(oldProjects, newProjects);
                }
                else
                {
                    Console.Out.Write("При создании проекта возникла непредвиденная ошибка! Возможно, проект с именем " + newProject.Name + " уже существует.");
                }
            }
            else
            {
                Console.Out.Write("Проект с именем " + newProject.Name + " уже существует!");
            }
        }
    }
}
