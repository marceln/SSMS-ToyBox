using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;
using ToyBox.Models.Enums;
using ToyBox.Models.Models;

namespace ToyBox.Services.Services
{
    internal class ObjectFilteringServiceImpl : IObjectFilteringService
    {
        #region IObjectFilteringService implementation

        public async Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results)
        {
            return await FilterObjectsInternal(lookupTerm, results, StringSearchType.Any, null);
        }

        public async Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType)
        {
            return await FilterObjectsInternal(lookupTerm, results, searchType, null);
        }

        public async Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType, DbObjectType objectType)
        {
            return await FilterObjectsInternal(lookupTerm, results, searchType, objectType);
        }

        #endregion

        #region Private implementation

        private Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsInternal(string lookupTerm, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType, DbObjectType? objectType)
        {
            return Task.Factory.StartNew(() => PerformLookup(lookupTerm, results, searchType, objectType));
        }

        private static IEnumerable<ObjectLookupResultModel> PerformLookup(string objectName, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType, DbObjectType? objectType)
        {
            if (searchType == StringSearchType.Any)
            {
                var databaseObjectModels = results as IList<DatabaseObjectModel> ?? results.ToList();
                var objectsThatStartWithTerm = PerformBasicFiltering(objectName, databaseObjectModels, StringSearchType.StartsWith, objectType).ToList();
                var objectsThatContainTerm = PerformBasicFiltering(objectName, databaseObjectModels, StringSearchType.Contains, objectType).ToList();
                var objectsThatEndWithTerm = PerformBasicFiltering(objectName, databaseObjectModels, StringSearchType.EndsWith, objectType).ToList();

                objectsThatStartWithTerm.ForEach(i => i.MatchWeight = Int32.MinValue);
                objectsThatContainTerm.ForEach(i => i.MatchWeight = i.DatabaseObject.ObjectName.IndexOf(objectName, StringComparison.InvariantCultureIgnoreCase));
                objectsThatEndWithTerm.ForEach(i => i.MatchWeight = Int32.MaxValue);

                return objectsThatStartWithTerm.Union(objectsThatContainTerm).Union(objectsThatEndWithTerm);
            }

            return PerformBasicFiltering(objectName, results, searchType, objectType);
        }

        private static IEnumerable<ObjectLookupResultModel> PerformBasicFiltering(string objectName, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType, DbObjectType? objectType)
        {
            var filteredDbObjects = results.ToList().
                Where(o => IsMatch(o.ObjectName, objectName, searchType)).
                Select(o =>
                {
                    var lookupResult = DatabaseObjectModelToLookupResultModel(o);
                    var matchingOutput = GetMatchElements(o.ObjectName, objectName, searchType);
                    lookupResult.MatchElements.AddRange(matchingOutput.Item1);
                    lookupResult.MatchIndex = matchingOutput.Item2;
                    lookupResult.MatchWeight = 1;

                    return lookupResult;
                });

            if (objectType.HasValue)
            {
                filteredDbObjects = filteredDbObjects.ToList().Where(o => o.DatabaseObject.MappedObjectType == objectType.Value);
            }

            return filteredDbObjects;
        }

        private static ObjectLookupResultModel DatabaseObjectModelToLookupResultModel(DatabaseObjectModel model)
        {
            return new ObjectLookupResultModel
            {
                DatabaseObject = model
            };
        }

        private static Tuple<IEnumerable<string>, int> GetMatchElements(string fullText, string lookupTerm, StringSearchType searchType)
        {
            switch (searchType)
            {
                case StringSearchType.Any:
                case StringSearchType.Contains:
                    var indexOfLookupTerm = fullText.IndexOf(lookupTerm, StringComparison.InvariantCultureIgnoreCase);
                    var anyOrContainsElements = new List<string>()
                    {
                        fullText.Substring(0, indexOfLookupTerm),
                        fullText.Substring(indexOfLookupTerm, lookupTerm.Length),
                        fullText.Substring(indexOfLookupTerm + lookupTerm.Length, fullText.Length - (indexOfLookupTerm + lookupTerm.Length)),
                    };

                    return new Tuple<IEnumerable<string>, int>(anyOrContainsElements, 1);
                case StringSearchType.StartsWith:
                    var startWithElements = new List<string>()
                    {
                        fullText.Substring(0, lookupTerm.Length), 
                        fullText.Substring(lookupTerm.Length, fullText.Length - lookupTerm.Length)
                    };

                    return new Tuple<IEnumerable<string>, int>(startWithElements, 0);
                case StringSearchType.EndsWith:
                    var endsWithElements = new List<string>()
                    {
                        fullText.Substring(0, fullText.Length - lookupTerm.Length), 
                        fullText.Substring(fullText.Length - lookupTerm.Length, lookupTerm.Length)
                    };

                    return new Tuple<IEnumerable<string>, int>(endsWithElements, 1);
                case StringSearchType.MatchTerm:
                    var exactMatchElements = new List<string>()
                    {
                        fullText
                    };

                    return new Tuple<IEnumerable<string>, int>(exactMatchElements, 0);
            }

            return null;
        }

        private static bool IsMatch(string s1, string s2, StringSearchType searchType)
        {
            switch (searchType)
            {
                case StringSearchType.Any:
                    return s1.ToLowerInvariant().Contains(s2.ToLowerInvariant());
                case StringSearchType.Contains:
                    return s1.ToLowerInvariant().Contains(s2.ToLowerInvariant()) &&
                        !(s1.EndsWith(s2, StringComparison.InvariantCultureIgnoreCase) || s1.StartsWith(s2, StringComparison.InvariantCultureIgnoreCase));
                case StringSearchType.EndsWith:
                    return s1.EndsWith(s2, StringComparison.InvariantCultureIgnoreCase);
                case StringSearchType.MatchTerm:
                    return String.Compare(s1, s2, StringComparison.InvariantCultureIgnoreCase) == 0;
                case StringSearchType.StartsWith:
                    return s1.StartsWith(s2, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        #endregion

    }
}
