using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Common.AssemblyFileReader;
using NUnit.Framework;

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
            string query = sut.GetQuery("SqlQueryFiles.Tests.SqlTestFile.sql");
            Assert.IsNotEmpty(query);
        }
    }
}
