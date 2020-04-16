using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Common.AssemblyFileReader;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Core.Infra.Test.FileResourceReader
{
    [TestFixture]
    public class SqlFileQueryReaderTests
    {
        [Test]
        public void Should_return_sql_query_when_use_short_file_name()
        {
            IAssemblyResourceReader assemblyResourceReader = new AssemblyResourceReader();
            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);
            string query = sut.GetQuery("SqlTestFile.sql");
            Assert.IsNotEmpty(query);
        }

        [Test]
        public void Should_return_sql_query_when_use_long_file_name()
        {
            IAssemblyResourceReader assemblyResourceReader = new AssemblyResourceReader();
            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);
            string query = sut.GetQuery("SqlQueryFiles.UnitTests.SqlTestFile.sql");
            Assert.IsNotEmpty(query);
        }

        [Test]
        public void Should_return_exception_if_file_name_is_empty([Values(null, "", " ")] string sqlFileName)
        {
            IAssemblyResourceReader assemblyResourceReader = new AssemblyResourceReader();
            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);

            Action code = () => sut.GetQuery(sqlFileName);
            Assert.Throws(Is.TypeOf<ArgumentNullException>(), code.Invoke);
        }

        [Test]
        public void Should_return_exception_if_file_name_does_not_contain_sql()
        {
            IAssemblyResourceReader assemblyResourceReader = new AssemblyResourceReader();
            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);

            string sqlFileName = "SqlTestFile";
            Action code = () => sut.GetQuery(sqlFileName);
            string expectedMessage = $"{sqlFileName} is not an sql file";
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo(expectedMessage), code.Invoke);
        }

        [Test]
        public void Should_return_sql_query_when_use_long_file_name_and_file_does_not_found()
        {
            IAssemblyResourceReader assemblyResourceReader = new AssemblyResourceReader();
            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);

            string sqlFileName = "SqlQueryFiles.UnitTests.test.sql";
            Action code = () => sut.GetQuery(sqlFileName);

            Assert.Throws(Is.TypeOf<FileNotFoundException>(), code.Invoke);
        }

        [Test]
        public void Should_return_sql_query_when_use_short_file_name_and_file_does_not_found()
        {
            Dictionary<string, string> resources = new Dictionary<string, string>();
            resources.Add("Repo1.read.sql", "query 1");
            IReadOnlyDictionary<string, string> dd = resources;

            IAssemblyResourceReader assemblyResourceReader = Substitute.For<IAssemblyResourceReader>();
            assemblyResourceReader.GetResourcesContent(Arg.Any<Assembly>(), Arg.Any<IEnumerable<string>>())
                .Returns(Task.FromResult<IReadOnlyDictionary<string, string>>(resources));

            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);

            string sqlFileName = "test.sql";
            Action code = () => sut.GetQuery(sqlFileName);

            string expectedMessage = $"Embedded file {sqlFileName} could not be found in assembly";
            Assert.Throws(Is.TypeOf<FileNotFoundException>().And.Message.StartsWith(expectedMessage), code.Invoke);
        }

        [Test]
        public void Should_return_sql_query_when_use_short_file_name_and_file_found_in_many_times()
        {
            Dictionary<string, string> resources = new Dictionary<string, string>();
            resources.Add("Repo1.test.sql", "query 1");
            resources.Add("Repo2.test.sql", "query 2");
            IReadOnlyDictionary<string, string> dd = resources;

            IAssemblyResourceReader assemblyResourceReader = Substitute.For<IAssemblyResourceReader>();
            assemblyResourceReader.GetResourcesContent(Arg.Any<Assembly>(), Arg.Any<IEnumerable<string>>())
                .Returns(Task.FromResult<IReadOnlyDictionary<string, string>>(resources));

            ISqlFileQueryReader sut = new SqlFileQueryReader(assemblyResourceReader);

            string sqlFileName = "test.sql";
            Action code = () => sut.GetQuery(sqlFileName);

            string expectedMessage = $"Embedded file {sqlFileName} was found multiple times in";
            Assert.Throws(Is.TypeOf<FileNotFoundException>().And.Message.StartsWith(expectedMessage), code.Invoke);
        }
    }
}
