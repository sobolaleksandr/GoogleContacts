namespace GoogleContacts.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    [TestCaseOrderer(nameof(PriorityOrderer), nameof(Tests))]
    public class GoogleServiceTests
    {
        public GoogleServiceTests()
        {
            //GoogleService.Initialize();
            _groupModel = new GroupModel(GROUP_NAME, string.Empty);
        }

        private readonly GroupModel _groupModel;
        private const string GROUP_NAME = "TestName";
        private const string CHANGED_GROUP_NAME = "ChangedGroupName";

        //[Fact, TestPriority(3)]
        //public async Task GetGroups_Initialized_ReturnsGroups()
        //{
        //    List<GroupModel> groups = await GoogleService.GetAll();

        //    Assert.Equal(11, groups.Count);
        //}

        //[Fact]
        //public async Task GetGroups_NotInitialized_ReturnsNull()
        //{
        //    List<GroupModel> groups = await GoogleService.GetAll();

        //    Assert.Null(groups);
        //}

        //[Fact]
        //public async Task CreateGroup_WithNull_ReturnsNull()
        //{
        //    GoogleService.Initialize();
        //    GroupModel groupModel = null;

        //    GroupModel group = await GoogleService.Create(groupModel);

        //    Assert.Null(group);
        //}

        //[Fact]
        //public async Task CreateGroup_NotInitialized_ReturnsNull()
        //{
        //    GroupModel groupModel = new("TestName");

        //    GroupModel group = await GoogleService.Create(groupModel);

        //    Assert.Null(group);
        //}

        internal static async Task TestAsync()
        {
            //var personModel = new PersonModel("John", "Doe", "JohnD@yahoo.com", "+7800553535");
            //var groupModel = new GroupModel("testGroup2");
            var query = "Ринат"; // "JohnD@yahoo.com";
            var properties = "names,emailAddresses"; // "emailAddresses";
            var personFields = "names,emailAddresses,phoneNumbers,organizations,memberships";
            var resources = new List<string> { "people/c8717037971012891222" };
            //GoogleService.Initialize();
            //var groups = await GoogleService.GetAll();
            //GroupModel group = await GoogleContacts.Create(groupModel);
            //var modGroup = await GoogleContacts.ModifyGroup("contactGroups/2f4d42e08a6f5e7f",resources);
            //groupModel.modelResourceName = "contactGroups/2f4d42e08a6f5e7f";
            //var updated = await GoogleContacts.Update(groups.FirstOrDefault());
            //GoogleContacts.Create(personModel);
            //var model = (await GoogleService.GetAll(personFields)).FirstOrDefault();
            //model.modelEmail = "JohnD@yahoo.com";
            //var model = (await GoogleContacts.SearchContact(query, properties)).FirstOrDefault();
            //await GoogleContacts.Update(model, personFields);
            //await GoogleContacts.Delete(model);
        }

        //[Fact]
        //[TestPriority(0)]
        //public async Task CreateGroup_WithGroup_ReturnsGroup()
        //{
        //    var result = await GoogleService.Create(_groupModel);

        //    Assert.Equal(GROUP_NAME, result.Name);
        //}

        //[Fact]
        //[TestPriority(2)]
        //public async Task DeleteGroup_WithGroup_ReturnsGroup()
        //{
        //    var groupModel = (await GoogleService.GetAll()).FirstOrDefault();

        //    var result = await GoogleService.Delete(groupModel);

        //    Assert.True(result);
        //}

        //[Fact]
        //[TestPriority(1)]
        //public async Task UpdateGroup_WithGroup_ReturnsGroup()
        //{
        //    var groupModels = await GoogleService.GetAll();
        //    var groupModel = groupModels.FirstOrDefault();

        //    Assert.NotNull(groupModel);
        //    groupModel.Name = CHANGED_GROUP_NAME;
        //    groupModel = await GoogleService.Update(groupModel);
        //    Assert.Equal(CHANGED_GROUP_NAME, groupModel.Name);
        //}
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }

    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
            foreach (var testCase in testCases)
            {
                var priority = testCase.TestMethod.Method
                    .GetCustomAttributes(assemblyName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var testCase in
                sortedMethods.Keys.SelectMany(
                    priority => sortedMethods[priority].OrderBy(
                        testCase => testCase.TestMethod.Method.Name)))
            {
                yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary, TKey key)
            where TKey : struct
            where TValue : new()
        {
            return dictionary.TryGetValue(key, out var result)
                ? result
                : dictionary[key] = new TValue();
        }
    }
}