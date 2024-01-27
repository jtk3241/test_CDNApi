using Moq;
using NUnit.Framework;
using Serilog;

namespace CompleteDevNet.Tests.DataAccessTests;

public class DataAccessTests01 : DataAccessTestsBase
{
    private readonly Mock<ILogger> _mockLogger = new Mock<ILogger>();
    private readonly Mock<CDNContext> _mockContext = new Mock<CDNContext>();

    //private DataAccess subjectBeingTested;
    private Mock<DataAccess> subjectBeingTested;

    public DataAccessTests01()
    {
        _mockLogger.Setup(x => x.ForContext<object>()).Returns(_mockLogger.Object);
        //this.subjectBeingTested = new DataAccess(_mockLogger.Object, _mockContext.Object);
        this.subjectBeingTested = new Moq.Mock<DataAccess>(_mockLogger.Object, _mockContext.Object);
        this.subjectBeingTested.CallBase = true;
    }

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public async Task Test_DataAccess_GetDeveloperList()
    {
        var dataDevelopers = new List<TDeveloper>
        {
            new TDeveloper
            {
                Id = 10001,
                Identguid = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "test_user1",
                Email = "test01@cdn.com",
            },
            new TDeveloper
            {
                Id = 10002,
                Identguid = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "test_user2",
                Email = "test02@cdn.com",
            },
            new TDeveloper
            {
                Id = 10003,
                Identguid = new Guid("00000000-0000-0000-0000-000000000003"),
                Name = "test_user3",
                Email = "test03@cdn.com",
            },
            new TDeveloper
            {
                Id = 10004,
                Identguid = new Guid("00000000-0000-0000-0000-000000000004"),
                Name = "test_user4",
                Email = "test04@cdn.com",
            },
        };

        //*** mock
        var mockDevelopers = CreateDbSetMock(dataDevelopers.AsQueryable());
        _mockContext.Setup(m => m.TDevelopers).Returns(mockDevelopers.Object);

        //*** run
        var actual = await subjectBeingTested.Object.GetDeveloperList();

        //*** asserts
        Assert.That(actual, Is.Not.Null);
        Assert.That(dataDevelopers.Count, Is.EqualTo(actual.Count));
    }

    [Test]
    [TestCase("00000000-0000-0000-0000-000000000002", true, Description = "positive test case.")]
    [TestCase("00000000-0000-0000-0000-000000000009", false, Description = "negative test case.")]
    public async Task Test_DataAccess_CheckDeveloperIdentGuid(string sIdentGuid, bool bExpectedResult)
    {
        var dataDevelopers = new List<TDeveloper>
        {
            new TDeveloper
            {
                Id = 10001,
                Identguid = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "test_user1",
                Email = "test01@cdn.com",
            },
            new TDeveloper
            {
                Id = 10002,
                Identguid = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "test_user2",
                Email = "test02@cdn.com",
            },
            new TDeveloper
            {
                Id = 10003,
                Identguid = new Guid("00000000-0000-0000-0000-000000000003"),
                Name = "test_user3",
                Email = "test03@cdn.com",
            },
            new TDeveloper
            {
                Id = 10004,
                Identguid = new Guid("00000000-0000-0000-0000-000000000004"),
                Name = "test_user4",
                Email = "test04@cdn.com",
            },
        };

        //*** mock
        var mockDevelopers = CreateDbSetMock(dataDevelopers.AsQueryable());
        _mockContext.Setup(m => m.TDevelopers).Returns(mockDevelopers.Object);

        //*** run
        Guid identGuid = new Guid(sIdentGuid);
        var actual = await subjectBeingTested.Object.CheckDeveloperIdentGuid(identGuid);

        //*** asserts
        Assert.That(actual, Is.EqualTo(bExpectedResult));
    }

    [Test]
    public async Task Test_DataAccess_AddDeveloper()
    {
        var dataDevelopers = new List<TDeveloper>
        {
            new TDeveloper
            {
                Id = 10001,
                Identguid = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "test_user1",
                Email = "test01@cdn.com",
            },
            new TDeveloper
            {
                Id = 10002,
                Identguid = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "test_user2",
                Email = "test02@cdn.com",
            },
            new TDeveloper
            {
                Id = 10003,
                Identguid = new Guid("00000000-0000-0000-0000-000000000003"),
                Name = "test_user3",
                Email = "test03@cdn.com",
            },
            new TDeveloper
            {
                Id = 10004,
                Identguid = new Guid("00000000-0000-0000-0000-000000000004"),
                Name = "test_user4",
                Email = "test04@cdn.com",
            },
        };

        var objNewDeveloper = new Core.Entities.DeveloperCore
        {
            Name = "new user",
            Email = "new@cdn.com",
            PhoneNumber = "1234567890",
            SkillSet = "some skillset",
            Hobby = "some hobby"
        };

        decimal dExpectedID = 999;

        //*** mock
        var mockDevelopers = CreateDbSetMock(dataDevelopers.AsQueryable());
        _mockContext.Setup(m => m.TDevelopers).Returns(mockDevelopers.Object);

        this.subjectBeingTested.Setup(x => x.GetNextIdAsync(It.IsAny<string>())).Returns(Task<decimal>.Factory.StartNew(() => dExpectedID));

        var actual = await subjectBeingTested.Object.AddDeveloper(objNewDeveloper);

        //*** asserts
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Id, Is.EqualTo(dExpectedID));
        Assert.That(actual.UpdatedOn, Is.Not.Null);

    }

}
