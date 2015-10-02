using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPFileSystem;

namespace Tests_Unitaires
{
    [TestClass]
    public class UnitTest1
    {
        public Directory RepertoireCourant;
        public File Fichier;
        public Directory Chemin;
        public Directory CheminPlus;

        [TestInitialize]
        public void SetUp()
        {
           RepertoireCourant = new Directory("/", null);
            Chemin = new Directory("chemin", RepertoireCourant);
        }


         [TestMethod]
        public void Cd()
        {
           Directory Java = new Directory("Java", RepertoireCourant);
           RepertoireCourant.ListeFiles.Add(Java);
           Assert.AreEqual(Java, RepertoireCourant.Cd(Java.Nom));
        }

         [TestMethod]
         public void NoPermissionsInCd()
         {
             RepertoireCourant.Chmod(0);
             Directory Chemin = new Directory("chemin", RepertoireCourant);
             File destination = RepertoireCourant.Cd("chemin");
             Assert.AreEqual(destination, null);
         }

         [TestMethod]
         public void Mkdir()
         {
             RepertoireCourant.Chmod(2); // Nécessité d'avoir les droits d'écriture
             Assert.IsTrue(RepertoireCourant.Mkdir("Test"));
         }

         [TestMethod]
         public void NoPermissionsInMkdir()
         {
             RepertoireCourant.permission = 0;
             Assert.IsFalse(RepertoireCourant.Mkdir("Test"));
         }

         [TestMethod]
         public void Ls()
         {
             RepertoireCourant.Chmod(7);
             RepertoireCourant.Mkdir("Test");
             RepertoireCourant.Mkdir("Test2");
             Assert.AreEqual(2, RepertoireCourant.Ls().Count);
         }

         [TestMethod]
         public void NotLs()
         {
             Assert.AreEqual(0, RepertoireCourant.Ls().Count);
         }

         [TestMethod]
         public void NoPermissionsInLs()
         {
             RepertoireCourant.Chmod(0);
             Assert.AreEqual(0, RepertoireCourant.Ls().Count);
         }

         [TestMethod]
         public void GetPath()
         {
             Directory Test = new Directory("Test", RepertoireCourant);
             Directory Test1 = new Directory("Test1", Test);
             Assert.AreEqual("/" + Test.Nom + "/" + Test1.Nom + "/", Test1.GetPath());
         }

        [TestMethod]
        public void GetRoot()
         {
            Directory Test = new Directory("Test", RepertoireCourant);
            Directory Test2 = new Directory("Test2", Test);
            Directory Test3 = new Directory("Test3", Test2);
            Assert.AreEqual(Test2, Test3.GetRoot());
         }

        [TestMethod]
        public void RenameTo()
        {
            Assert.IsTrue(RepertoireCourant.RenameTo("Ok"));
        }

        [TestMethod]
         public void GetParent() //to do
        {
        }

        [TestMethod]
        public void IsDirectory()
        {
            Assert.IsTrue(RepertoireCourant.IsDirectory());
        }

        [TestMethod]
        public void NotIsDirectory()
        {
            File testFile = new File("test", RepertoireCourant);
            Assert.IsFalse(testFile.IsDirectory());
        }

        [TestMethod]
        public void IsFile()
        {
            File testFile = new File("test", RepertoireCourant);
            Assert.IsTrue(testFile.IsFile());
        }

        [TestMethod]
        public void NotIsFile()
        {
            Assert.IsFalse(Chemin.IsFile());
        }

        [TestMethod]
       public void  GetName()
        {
            Directory Test = new Directory("Test", RepertoireCourant);
            File TestFile = new File("TestFile", Test);
            Assert.AreEqual("TestFile", TestFile.GetName());
        }

        [TestMethod]
        public void Chmod()
        {
            int NewPermissions = 7;
            RepertoireCourant.Chmod(NewPermissions);
            Assert.AreEqual(NewPermissions, RepertoireCourant.permission);
        }

        [TestMethod]
        public void CanWrite()
        {
            Assert.IsTrue(RepertoireCourant.permission >= 2);
        }

        [TestMethod]
        public void CanExecute()
        {
            Assert.IsTrue(RepertoireCourant.permission >= 1);
        }

        [TestMethod]
        public void CanRead()
        {
            Assert.IsTrue(RepertoireCourant.permission >= 4);
        }

        [TestMethod]
        public void CanWriteAndRead()
        {
            RepertoireCourant.permission = 6;
            Assert.IsTrue(RepertoireCourant.permission >= 6);
        }

        [TestMethod]
        public void CanWriteAndExecute()
        {
            Assert.IsTrue(RepertoireCourant.permission >= 3);
        }

        [TestMethod]
        public void CanReadAndExecute()
        {
            RepertoireCourant.permission = 5;
            Assert.IsTrue(RepertoireCourant.permission >= 5);
        }

        [TestMethod]
        public void  Search()
        {
            RepertoireCourant.Chmod(7);
            RepertoireCourant.Mkdir("Test1");
            RepertoireCourant.Mkdir("Test2");
            RepertoireCourant.Mkdir("Test3");
            RepertoireCourant.Mkdir("Test3");
            Assert.AreEqual(2, RepertoireCourant.Search("Test3").Count);
        }



        [TestMethod]
        public void Delete()
        {
            RepertoireCourant.Chmod(7);
            RepertoireCourant.Mkdir("Test");
            Assert.IsTrue(RepertoireCourant.Delete("Test"));
        }

        public void NotDelete()
        {
            RepertoireCourant.Chmod(7);
            RepertoireCourant.Mkdir("Test");
            Assert.IsFalse(RepertoireCourant.Delete("Test"));
        }

        [TestMethod]
        public void CreateNewFile()
        {
            RepertoireCourant.Chmod(7);
            Assert.IsTrue(RepertoireCourant.createNewFile("Test"));
            Assert.IsTrue(RepertoireCourant.ListeFiles.Count == 1);
        }
        [TestMethod]
        public void NoPermissionsInCreateNewFile()
        {
            Assert.IsFalse(RepertoireCourant.createNewFile("Test"));
            Assert.IsTrue(RepertoireCourant.ListeFiles.Count == 0);
        }
    }
}
