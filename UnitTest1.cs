using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPFileSystem;

namespace Tests_Unitaires
{
    [TestClass]
    public class UnitTest1
    {
        public Directory RepertoireCourant;
        public File Test;
        
        [TestInitialize]
        public void SetUp()
        {
           RepertoireCourant = new Directory("/", null);
           RepertoireCourant.Mkdir("Repertoire1");
           RepertoireCourant.createNewFile("Test");
        }


         [TestMethod]
        public void Cd()
        {
            RepertoireCourant.Mkdir("Test");
            Test = RepertoireCourant.Cd("Test");
            File fichier = RepertoireCourant.Cd("Test");
            Assert.AreEqual(fichier, Test);
        }

         [TestMethod]
         public void NoCdInFile()
         {
             File fichier = RepertoireCourant.Cd("Test");
             Assert.AreEqual(fichier, null);
         }

         [TestMethod]
         public void NoPermissionsInCd()
         {
             RepertoireCourant.permission = 0;
             File fichier = RepertoireCourant.Cd("Test");
             Assert.AreEqual(fichier, null);
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
             Assert.AreEqual(RepertoireCourant.Ls().Count, 2);
         }

         [TestMethod]
         public void NotLs()
         {
             Assert.AreEqual(RepertoireCourant.Ls().Count, 0);
         }

         [TestMethod]
         public void NoPermissionsInLs()
         {
             RepertoireCourant.Chmod(0);
             Assert.AreEqual(RepertoireCourant.Ls().Count, 0);
         }

         [TestMethod]
         public void GetPath() //to do
         {

         }

        [TestMethod]
        public void GetRoot() //to do
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
           Assert.IsFalse(RepertoireCourant.IsFile());
        }

        [TestMethod]
       public void  GetName() //to do
        {

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
        public void GetPermissions() //to do
        {

        }

        [TestMethod]
        public void  Search()
        {
            RepertoireCourant.Chmod(7);
            RepertoireCourant.Mkdir("Test1");
            RepertoireCourant.Mkdir("Test2");
            RepertoireCourant.Mkdir("Test3");
            RepertoireCourant.Mkdir("Test3");
            Assert.AreEqual(RepertoireCourant.Search("Test3").Count, 2);
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
        public void CreateNewFile() //to do
        {

        }
        [TestMethod]
        public void NoPermissionsInCreateNewFile() //to do
        {

        }
    }
}
